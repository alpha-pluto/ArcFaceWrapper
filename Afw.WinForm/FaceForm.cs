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
using Afw.Core.Helper;
using System;
using System.Windows.Forms;
using Afw.Data;
using Afw.Core;
using Afw.WinForm.Extension;

namespace Afw.WinForm
{
    public partial class FaceForm : Form
    {
        #region parameter

        private IntPtr ptrMainForm = IntPtr.Zero;

        private IntPtr ptrRegisterForm = IntPtr.Zero;

        private RegisterForm registerForm;

        internal EngineContext engineContext;

        #endregion

        #region ctor
        public FaceForm()
        {

            InitializeComponent();
        }

        #endregion

        #region 初始化工作
        private void FaceForm_Load(object sender, EventArgs e)
        {
            ptrMainForm = this.Handle;
            engineContext = new EngineContext();

            if (engineContext.IrCameraIdx >= 0)
            {
                this.chbLiveness.Checked = false;
                this.chbLiveness.Visible = false;
            }

            if (ActiveEngine())
            {
                registerForm = new RegisterForm();
                registerForm.Visible = false;
                registerForm.ptrParentForm = this.Handle;
                registerForm.engineContext = this.engineContext;
                ptrRegisterForm = registerForm.Handle;
                FaceDataLoad();
                for (var i = 0; i < engineContext.FilterInfoCollection.Count; i++)
                    WinMessageHelper.SendMessage(this.Handle, UserMessage.WM_MESSAGE, IntPtr.Zero, engineContext.FilterInfoCollection[i].Name);
                if (engineContext.IrCameraIdx < 0)
                    WinMessageHelper.SendMessage(this.Handle, UserMessage.WM_MESSAGE, IntPtr.Zero, "未检测到红外摄像头，请启用RGB活体检测。强烈建议使用双目摄像头！！！");
            }

        }

        #endregion

        #region utility

        private bool ActiveEngine()
        {
            var ret = false;
            try
            {
                var retCode = engineContext.ActivateEngine();
                if (retCode == Core.MError.MOK || retCode == Core.MError.MERR_ASF_ALREADY_ACTIVATED)
                {
                    ret = true;
                }
                else
                {
                    WinMessageHelper.SendMessage(this.Handle, UserMessage.WM_MESSAGE, IntPtr.Zero, retCode.GetFieldDescription());
                }
            }
            catch (Exception ex)
            {
                WinMessageHelper.SendMessage(this.Handle, UserMessage.WM_MESSAGE, IntPtr.Zero, ex.Message);

            }
            return ret;
        }

        private void ButtonLockForFaceDataViewReload()
        {
            this.Invoke(new Action(delegate
            {
                ButtonDisableForImage();
                ButtonDisableForVideo();
            }));
        }

        private void ButtonReleaseForFaceDataViewReloaded()
        {
            this.Invoke(new Action(delegate
            {
                ButtonEnableForImage();
                ButtonEnableForVideo();
            }));
        }

        private void ButtonEnableForImage()
        {
            this.btnReloadFaceFeature.Enabled = true;
            this.btnRegisterFaceFeature.Enabled = true;
        }

        private void ButtonDisableForImage()
        {
            this.btnReloadFaceFeature.Enabled = false;
            this.btnRegisterFaceFeature.Enabled = false;
        }

        private void ButtonDisableForVideo()
        {
            this.rdbThresholdHigh.Enabled = false;
            this.rdbThresholdMid.Enabled = false;
            this.rdbThresholdLow.Enabled = false;
            this.btnStartVideo.Enabled = false;
        }

        private void ButtonEnableForVideo()
        {
            if (engineContext.FilterInfoCollection.Count > 0)
            {
                this.rdbThresholdHigh.Enabled = true;
                this.rdbThresholdMid.Enabled = true;
                this.rdbThresholdLow.Enabled = true;
                this.btnStartVideo.Enabled = true;
            }
            else
            {
                this.rdbThresholdHigh.Enabled = false;
                this.rdbThresholdMid.Enabled = false;
                this.rdbThresholdLow.Enabled = false;
                this.btnStartVideo.Enabled = false;
            }

        }

        private void FaceFormLock()
        {
            this.Enabled = false;
        }

        private void FaceFormUnlock()
        {
            this.Enabled = true;
        }

        private void PerformUpdate(int userId)
        {
            Data.Domain.FaceFeature faceModel = iFaceDataRepository.GetById(userId);
            FaceFormLock();
            var stuFaceModel = faceModel.CreateStruct();
            IntPtr ptrFaceModel = MemoryHelper.Malloc(MemoryHelper.SizeOf<MemberData>());
            MemoryHelper.StructureToPtr<MemberData>(stuFaceModel, ptrFaceModel);
            string avatarPath = iFaceDataRepository.GetFaceImageFullPath(faceModel.Id, faceModel.FaceImagePath);
            IntPtr ptrAvatarPath = MemoryHelper.StringToIntPtr(avatarPath);
            WinMessageHelper.SendMessage(ptrRegisterForm, UserMessage.WM_FACE_DATA_UPDATE, ptrAvatarPath, ptrFaceModel);
        }

        private void PerformDelete(int userId, int rowIndex)
        {
            Data.Domain.FaceFeature faceModel = iFaceDataRepository.GetById(userId);
            FaceFormLock();
            var stuFaceModel = faceModel.CreateStruct();
            IntPtr ptrFaceModel = MemoryHelper.Malloc(MemoryHelper.SizeOf<MemberData>());
            MemoryHelper.StructureToPtr<MemberData>(stuFaceModel, ptrFaceModel);
            WinMessageHelper.SendMessage(this.Handle, UserMessage.WM_FACE_DATA_DELETE, (IntPtr)rowIndex, ptrFaceModel);
        }

        #endregion

        #region event

        /// <summary>
        /// 重新载入人脸数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReloadFaceFeature_Click(object sender, EventArgs e)
        {
            if (this.btnReloadFaceFeature.Enabled)
            {
                ButtonLockForFaceDataViewReload();
                FaceDataGridViewClear();
                FaceDataLoad();
            }

        }

        /// <summary>
        /// 注册 人脸
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegisterFaceFeature_Click(object sender, EventArgs e)
        {
            if (this.btnRegisterFaceFeature.Enabled)
            {
                FaceFormLock();
                registerForm.RegisterFormReset();
                registerForm.Show();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvFace_RowHeaderMouseDoubleClick(object sender, System.Windows.Forms.DataGridViewCellMouseEventArgs e)
        {
            var userId = Convert.ToInt32(this.dgvFace.CurrentRow.Cells[0].Value);
            PerformUpdate(userId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvFace_CellMouseDown(object sender, System.Windows.Forms.DataGridViewCellMouseEventArgs e)
        {
            this.dgvFace.SetRightButtonDownShowContextMenuStrip(this.cmsDgvMenu, e);
        }

        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            var userId = Convert.ToInt32(this.dgvFace.SelectedRows[0].Cells[0].Value);
            PerformUpdate(userId);
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            var userId = Convert.ToInt32(this.dgvFace.SelectedRows[0].Cells[0].Value);
            PerformDelete(userId, this.dgvFace.SelectedRows[0].Index);
        }

        private void faceForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (videoSource.IsRunning)
                videoSource.SignalToStop();
            if (currentIrImage != null)
                currentIrImage.Dispose();
            engineContext.Dispose();
        }

        #endregion

    }
}
