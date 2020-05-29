/*----------------------------------------------------------------
 *  
 * C# 人脸识别 v1.0
 * 2019-7-19  
 * daniel.zhang
 * zamen@126.com
 * 文件名：FaceForm.cs  
 * 文件功能描述：人脸识别
 * 
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Afw.WinForm
{
    public partial class FaceForm
    {
        #region interface

        private static string faceDataRepositoryImplementType = Afw.Services.AppSettingManager.FileFaceDataRepository ?? "Afw.Data,Afw.Data.FileFaceDataRepository`1";

        #endregion

        #region data

        private static Afw.Data.IFaceDataRepository<Afw.Data.Domain.FaceFeature> iFaceDataRepository = null;

        #endregion

        #region OnFormLoad

        private void FaceDataLoad()
        {
            this.Invoke(new Action(delegate
            {
                bool ret = LoadFaceDataRepositoryAgent(out var errMsg);

                if (!ret)
                {
                    Afw.WinForm.WinMessageHelper.SendMessage(this.ptrMainForm, UserMessage.WM_MESSAGE, IntPtr.Zero, errMsg);
                }
                else
                {
                    this.FaceDataGridViewRenderHeader();
                    var cnt = iFaceDataRepository.Table.Count();
                    IntPtr pCnt = new IntPtr(cnt);
                    Afw.WinForm.WinMessageHelper.SendMessage(this.ptrMainForm, UserMessage.WM_FACE_DATA_LOADED, IntPtr.Zero, pCnt);
                }
            }));
        }

        #endregion

        #region method

        private bool LoadFaceDataRepositoryAgent(out string errMsg)
        {
            bool ret = true;
            errMsg = string.Empty;
            string[] faceDrArr = faceDataRepositoryImplementType.Split(',');
            string assembly = "Afw.Data";
            string repositoryType = "Afw.Data.FileFaceDataRepository`1";
            if (null != assembly && assembly.Length > 0)
            {
                assembly = faceDrArr[0];
                repositoryType = faceDrArr[1];
            }
            try
            {
                Assembly ass = Assembly.Load(assembly);//程序集名称
                Type t = ass.GetType(repositoryType);
                Type constructor = t.MakeGenericType(new Type[] { typeof(Afw.Data.Domain.FaceFeature) });
                iFaceDataRepository = (Afw.Data.IFaceDataRepository<Afw.Data.Domain.FaceFeature>)Activator.CreateInstance(constructor);
            }
            catch (Exception ex)
            {
                ret = false;
                Afw.Core.Helper.SimplifiedLogHelper.WriteIntoSystemLog(nameof(Afw.WinForm.FaceForm), $"实例化人脸数据源失败,{ex.ToString()}");
                errMsg = $"实例化人脸数据源失败,{ex.Message} [{SimplifiedUtility.GetDatetimeNow}]";
            }
            return ret;
        }

        #endregion

        #region Data 

        private void FaceDataGridViewClear()
        {
            this.dgvFace.Rows.Clear();
        }

        private void FaceDataGridViewRemove()
        {
            for (int i = 1; i < this.dgvFace.Rows.Count; i++)
            {
                dgvFace.Rows.Remove(dgvFace.Rows[i]);
            }
        }

        private void FaceDataGridViewRenderHeader()
        {
            DataGridViewImageColumn img = new DataGridViewImageColumn();
            dgvFace.ColumnCount = 4;

            dgvFace.Columns[0].Name = "用户Id";
            dgvFace.Columns[1].Name = "雇员号";
            dgvFace.Columns[2].Name = "姓名";
            dgvFace.Columns[3].Name = "部门Id";
            dgvFace.Columns.Add(img);
            dgvFace.Columns[4].Name = "照片";
            dgvFace.Columns[4].DefaultCellStyle.NullValue = null;//当没有数据时，不会显示红叉，cell.Value 必须是null，对于空串这句无效
        }

        private void FaceDataGridViewRender(IEnumerable<Afw.Data.Domain.FaceFeature> faceFeatureSet)
        {
            var s = iFaceDataRepository.Table;
            var rowIdx = 0;
            foreach (var m in s)
            {
                dgvFace.Rows.Add(new object[] { m.Id, m.EmployeeNo, m.Name, m.DeptId });
                using (var i = Image.FromFile(iFaceDataRepository.GetFaceImageFullPath(m.Id, m.FaceImagePath)))
                {
                    var f = Afw.Core.Helper.ImageHelper.ScaleImage(i, 50, 50);
                    dgvFace[4, rowIdx].Value = f;
                }

                rowIdx++;
            }
        }

        private void FaceDataGridViewAppend(Afw.Data.Domain.FaceFeature faceFeature)
        {
            using (var i = Image.FromFile(iFaceDataRepository.GetFaceImageFullPath(faceFeature.Id, faceFeature.FaceImagePath)))
            {
                var f = Afw.Core.Helper.ImageHelper.ScaleImage(i, 50, 50);
                dgvFace.Rows.Add(new object[] { faceFeature.Id, faceFeature.EmployeeNo, faceFeature.Name, faceFeature.DeptId, f });
            }

        }

        private void FaceDataGridViewUpdate(Afw.Data.Domain.FaceFeature faceFeature)
        {
            using (var i = Image.FromFile(iFaceDataRepository.GetFaceImageFullPath(faceFeature.Id, faceFeature.FaceImagePath)))
            {
                var f = Afw.Core.Helper.ImageHelper.ScaleImage(i, 50, 50);
                this.dgvFace.CurrentRow.Cells[0].Value = faceFeature.Id;
                this.dgvFace.CurrentRow.Cells[1].Value = faceFeature.EmployeeNo;
                this.dgvFace.CurrentRow.Cells[2].Value = faceFeature.Name;
                this.dgvFace.CurrentRow.Cells[3].Value = faceFeature.DeptId;
                this.dgvFace.CurrentRow.Cells[4].Value = f;
            }
        }

        private void FaceDataGridViewRemove(int rowIdx)
        {
            var rowSelected = this.dgvFace.Rows[rowIdx];
            this.dgvFace.Rows.Remove(rowSelected);
        }

        #endregion

        #region crud

        public void Insert(Afw.Data.Domain.FaceFeature faceFeature)
        {
            iFaceDataRepository?.Insert(faceFeature);
        }

        public void Update(Afw.Data.Domain.FaceFeature faceFeature)
        {
            iFaceDataRepository?.Update(faceFeature);
        }

        public void Delete(Afw.Data.Domain.FaceFeature faceFeature)
        {
            iFaceDataRepository?.Delete(faceFeature);
        }

        #endregion
    }
}
