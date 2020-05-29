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
using System.Linq;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Afw.Core.Helper;
using Afw.Data;

namespace Afw.WinForm
{
    /// <summary>
    /// 主窗口 消息处理 函数
    /// </summary>
    public partial class FaceForm
    {
        #region Message Handler

        protected override void DefWndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case UserMessage.WM_MESSAGE:
                    var wmMsg = Marshal.PtrToStringAnsi(m.LParam);
                    this.Invoke(new Action(delegate
                    {
                        this.txbDisplay.AppendText(wmMsg + Environment.NewLine);
                    }));
                    break;
                case UserMessage.WM_FACE_DATA_LOADED:
                    var cnt = (int)m.LParam;
                    var ldMsg = $"人脸数据源模块对接成功，共导入人脸数据{cnt}条！[{SimplifiedUtility.GetDatetimeNow}]";
                    this.Invoke(new Action(delegate
                    {
                        this.txbDisplay.AppendText(ldMsg + Environment.NewLine);
                        this.FaceDataGridViewRender(iFaceDataRepository.Table);
                    }));
                    this.ButtonReleaseForFaceDataViewReloaded();
                    break;
                case UserMessage.WM_PARENT_FORM_UNLOCK:
                    this.Invoke(new Action(delegate
                    {
                        FaceFormUnlock();
                    }));
                    break;
                case UserMessage.WM_QUERY_FACE_FEATURE_VIA_ID:
                    var ptrReq = m.WParam;
                    var userId = (int)m.LParam;
                    var userExists = iFaceDataRepository.TableNoTracking.Count(i => i.Id == userId);
                    Afw.WinForm.WinMessageHelper.SendMessage(ptrReq, UserMessage.WM_RESPONSE_FACE_FEATURE_VIA_ID, (IntPtr)userId, (IntPtr)userExists);
                    break;
                case UserMessage.WM_QUERY_FACE_FEATURE_VIA_EMPLOYEE_NO:
                    var ptrReqViaEmpNo = m.WParam;
                    var employeeNo = (int)m.LParam;
                    var employeeNoExists = iFaceDataRepository.TableNoTracking.Count(i => i.EmployeeNo == employeeNo);
                    Afw.WinForm.WinMessageHelper.SendMessage(ptrReqViaEmpNo, UserMessage.WM_RESPONSE_FACE_FEATURE_VIA_EMPLOYEE_NO, (IntPtr)employeeNo, (IntPtr)employeeNoExists);
                    break;
                case UserMessage.WM_FACE_DATA_REGISTER:
                    var faceFeature = MemoryHelper.PtrToStructure<MemberData>(m.LParam);
                    var ptrReqForm = m.WParam;
                    var e = new Afw.Data.Domain.FaceFeature
                    {
                        DeptId = faceFeature.memberWithFeature.DeptId,
                        EmployeeNo = faceFeature.memberWithFeature.EmployeeNo,
                        FaceImagePath = faceFeature.memberWithFeature.FaceImagePath,
                        Name = faceFeature.memberWithFeature.Name,
                        Id = faceFeature.memberWithFeature.Id,
                        Feature = new byte[faceFeature.memberWithFeature.AsfFaceFeature.featureSize],
                        FeatureSize = faceFeature.memberWithFeature.AsfFaceFeature.featureSize
                    };
                    Marshal.Copy(faceFeature.memberWithFeature.AsfFaceFeature.feature, e.Feature, 0, faceFeature.memberWithFeature.AsfFaceFeature.featureSize);
                    Insert(e);
                    registerForm.Visible = false;
                    FaceDataGridViewAppend(e);
                    FaceFormUnlock();
                    MemoryHelper.Free(m.LParam);
                    break;
                case UserMessage.WM_FACE_DATA_REFRESH:
                    this.Invoke(new Action(delegate
                    {
                        FaceFormUnlock();
                        ButtonLockForFaceDataViewReload();
                        FaceDataGridViewClear();
                        FaceDataLoad();
                    }));
                    break;
                case UserMessage.WM_FACE_DATA_UPDATE:
                    var faceFeatureToUpdate = MemoryHelper.PtrToStructure<MemberData>(m.LParam);
                    var ptrReqToUpdate = m.WParam;
                    var faceFeatureCandidate = new Afw.Data.Domain.FaceFeature
                    {
                        DeptId = faceFeatureToUpdate.memberWithFeature.DeptId,
                        EmployeeNo = faceFeatureToUpdate.memberWithFeature.EmployeeNo,
                        FaceImagePath = faceFeatureToUpdate.memberWithFeature.FaceImagePath,
                        Name = faceFeatureToUpdate.memberWithFeature.Name,
                        Id = faceFeatureToUpdate.memberWithFeature.Id,
                        Feature = new byte[faceFeatureToUpdate.memberWithFeature.AsfFaceFeature.featureSize],
                        FeatureSize = faceFeatureToUpdate.memberWithFeature.AsfFaceFeature.featureSize
                    };
                    Marshal.Copy(faceFeatureToUpdate.memberWithFeature.AsfFaceFeature.feature, faceFeatureCandidate.Feature, 0, faceFeatureToUpdate.memberWithFeature.AsfFaceFeature.featureSize);
                    Update(faceFeatureCandidate);
                    registerForm.Visible = false;
                    FaceDataGridViewUpdate(faceFeatureCandidate);
                    FaceFormUnlock();
                    MemoryHelper.Free(m.LParam);
                    break;
                case UserMessage.WM_FACE_DATA_DELETE:
                    var rowIndex = (int)m.WParam;
                    var faceFeatureToDelete = MemoryHelper.PtrToStructure<MemberData>(m.LParam);
                    var faceFeatureDelCandidate = new Afw.Data.Domain.FaceFeature
                    {
                        DeptId = faceFeatureToDelete.memberWithFeature.DeptId,
                        EmployeeNo = faceFeatureToDelete.memberWithFeature.EmployeeNo,
                        FaceImagePath = faceFeatureToDelete.memberWithFeature.FaceImagePath,
                        Name = faceFeatureToDelete.memberWithFeature.Name,
                        Id = faceFeatureToDelete.memberWithFeature.Id,
                        Feature = new byte[faceFeatureToDelete.memberWithFeature.AsfFaceFeature.featureSize],
                        FeatureSize = faceFeatureToDelete.memberWithFeature.AsfFaceFeature.featureSize
                    };
                    Marshal.Copy(faceFeatureToDelete.memberWithFeature.AsfFaceFeature.feature, faceFeatureDelCandidate.Feature, 0, faceFeatureToDelete.memberWithFeature.AsfFaceFeature.featureSize);
                    Delete(faceFeatureDelCandidate);
                    registerForm.Visible = false;
                    FaceDataGridViewRemove(rowIndex);
                    FaceFormUnlock();
                    MemoryHelper.Free(m.LParam);
                    break;
                default:
                    base.DefWndProc(ref m);//调用基类函数处理非自定义消息。
                    break;

            }

        }

        #endregion
    }
}
