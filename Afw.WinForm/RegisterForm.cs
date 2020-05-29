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
using Afw.Core.Domain;
using Afw.Core.Helper;
using Afw.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Afw.WinForm
{
    /// <summary>
    /// 注册 人脸 数据 窗口
    /// </summary>
    public partial class RegisterForm : Form
    {
        private object locker = new object();

        private Dictionary<string, bool> isDataOk = new Dictionary<string, bool>();

        private IEnumerable<char> digital = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        private string errMsg = "请填写内容";

        internal IntPtr ptrParentForm { get; set; }

        internal EngineContext engineContext;

        private MemberData memberData;

        private bool isModify;

        public RegisterForm()
        {
            isModify = false;
            ptrParentForm = IntPtr.Zero;
            InitializeComponent();
            ResetDataOk();
        }

        #region utility

        private void ResetDataOk()
        {
            memberData = new MemberData();
            isDataOk["tbUserId"] = false;
            isDataOk["tbName"] = false;
            isDataOk["tbEmployeeNo"] = false;
            isDataOk["tbDeptId"] = false;
            isDataOk["picboxUserAvatar"] = false;
        }

        internal void RegisterFormReset()
        {
            this.isModify = false;
            this.tbUserId.ReadOnly = false;
            this.tbUserId.BackColor = Color.White;
            this.tbEmployeeNo.ReadOnly = false;
            this.tbEmployeeNo.BackColor = Color.White;
            this.tbUserId.Text = string.Empty;
            this.tbName.Text = string.Empty;
            this.tbEmployeeNo.Text = string.Empty;
            this.tbDeptId.Text = string.Empty;
            this.picboxUserAvatar.Image = null;
            this.btnUserEnroll.Enabled = true;
            this.btnUserEnrollCancel.Enabled = true;
            ResetDataOk();
            this.tbUserId.Focus();

        }

        private void MarkFaceDataError(string key, string errMsg)
        {
            isDataOk[key] = false;
            this.errMsg = errMsg;
        }

        private void MarkFaceDataOk(string key)
        {
            isDataOk[key] = true;
        }

        private int ValidateUserId()
        {
            var userId = this.tbUserId.Text;
            if (!System.Text.RegularExpressions.Regex.IsMatch(userId, @"^\d+$"))
            {
                this.tbUserId.Text = "";
                this.tbUserId.Focus();
            }
            if (int.TryParse(userId, out var id))
            {
                return id;
            }
            return 0;
        }

        private int ValidateDeptId()
        {
            var deptId = this.tbDeptId.Text;
            if (!System.Text.RegularExpressions.Regex.IsMatch(deptId, @"^\d+$"))
            {
                this.tbDeptId.Text = "";
                this.tbDeptId.Focus();
            }
            if (int.TryParse(deptId, out var id))
            {
                return id;
            }
            return 0;
        }

        private string ValidateName()
        {
            var name = this.tbName.Text;
            if (name.Length < 2)
            {
                this.tbName.Focus();
                MarkFaceDataError("tbName", "请输入姓名");
            }
            else
            {
                MarkFaceDataOk("tbName");
            }
            return name;
        }

        private int ValidateEmployeeNo()
        {
            var strEmployeeNo = this.tbEmployeeNo.Text;
            if (!System.Text.RegularExpressions.Regex.IsMatch(strEmployeeNo, @"^\d+$"))
            {
                this.tbEmployeeNo.Text = "";
                this.tbEmployeeNo.Focus();
            }
            if (int.TryParse(strEmployeeNo, out var employeeNo))
            {
                return employeeNo;
            }
            return 0;
        }

        private void ValidateInput()
        {

            memberData.memberWithFeature.DeptId = ValidateDeptId();
            if (memberData.memberWithFeature.DeptId <= 0)
            {
                MarkFaceDataError("tbDeptId", "请填写部门Id");
            }
            else
            {
                MarkFaceDataOk("tbDeptId");
            }

            memberData.memberWithFeature.EmployeeNo = ValidateEmployeeNo();
            if (memberData.memberWithFeature.EmployeeNo > 0)
            {
                if (!this.isModify)
                    Afw.WinForm.WinMessageHelper.SendMessage(ptrParentForm, UserMessage.WM_QUERY_FACE_FEATURE_VIA_EMPLOYEE_NO, this.Handle, (IntPtr)memberData.memberWithFeature.EmployeeNo);
                else
                    MarkFaceDataOk("tbEmployeeNo");
            }
            else
            {
                MarkFaceDataError("tbEmployeeNo", "请填写雇员号");
            }

            memberData.memberWithFeature.Name = ValidateName();

            memberData.memberWithFeature.Id = ValidateUserId();
            if (memberData.memberWithFeature.Id > 0)
            {
                if (!this.isModify)
                    Afw.WinForm.WinMessageHelper.SendMessage(ptrParentForm, UserMessage.WM_QUERY_FACE_FEATURE_VIA_ID, this.Handle, (IntPtr)memberData.memberWithFeature.Id);
                else
                    MarkFaceDataOk("tbUserId");

            }
            else
            {
                MarkFaceDataError("tbUserId", "请填写用户Id");
            }

            if (string.IsNullOrEmpty(memberData.memberWithFeature.FaceImagePath))
            {
                MarkFaceDataError("picboxUserAvatar", "请上传头像照片");
            }

            if (memberData.Face3DStatus != 0)
            {
                MarkFaceDataError("picboxUserAvatar", "人脸数据不可信，请重新上传照片");
            }
            if (memberData.FaceNum < 1)
            {
                MarkFaceDataError("picboxUserAvatar", "未检测到人脸，请重新上传照片");
            }
            if (memberData.FaceNum > 1)
            {
                MarkFaceDataError("picboxUserAvatar", "检测到多张人脸，请重新上传照片");
            }
        }

        private void RegisterFormHide()
        {
            this.Visible = false;

            Afw.WinForm.WinMessageHelper.SendMessage(ptrParentForm, Afw.WinForm.UserMessage.WM_PARENT_FORM_UNLOCK, IntPtr.Zero, null);
        }

        private void ProcessImage(string filename)
        {
            this.Enabled = false;
            //this.picboxUserAvatar.Image = Image.FromFile(openFileDialog.FileName);
            var srcImage = Image.FromFile(filename);

            IntPtr ptrImgFeature = IntPtr.Zero;
            Afw.Core.Domain.ASF_SingleFaceInfo singleFaceInfo = new Core.Domain.ASF_SingleFaceInfo();
            int face3DStatus = -1;
            int faceNum = 0;
            string faceDesc = string.Empty;

            string errMsg = string.Empty;
            var retCode = engineContext.DeteceForMemberEnroll(out errMsg, out ptrImgFeature, out singleFaceInfo, out face3DStatus, out faceNum, out faceDesc, ref srcImage, this.picboxUserAvatar.Width, this.picboxUserAvatar.Height);
            this.picboxUserAvatar.Image = srcImage;
            if (retCode == Core.MError.MOK)
            {
                Afw.WinForm.WinMessageHelper.SendMessage(ptrParentForm, UserMessage.WM_MESSAGE, IntPtr.Zero, faceDesc);
                MarkFaceDataOk("picboxUserAvatar");
            }
            else
            {
                Afw.WinForm.WinMessageHelper.SendMessage(ptrParentForm, UserMessage.WM_MESSAGE, IntPtr.Zero, errMsg);
                MarkFaceDataError("picboxUserAvatar", errMsg);
            }

            if (face3DStatus != 0)
            {
                MarkFaceDataError("picboxUserAvatar", "人脸数据不可信，请更换图片");
            }
            if (faceNum <= 0)
            {
                MarkFaceDataError("picboxUserAvatar", "未检测到人脸数据，请更换图片");
            }
            if (faceNum > 1)
            {
                MarkFaceDataError("picboxUserAvatar", "检测到多张人脸数据，请更换图片");
            }
            if (singleFaceInfo.faceRect.left == 0 && singleFaceInfo.faceRect.right == 0)
            {
                MarkFaceDataError("picboxUserAvatar", "无效的脸部信息，请更换图片");
            }
            if (retCode == Core.MError.MOK && face3DStatus == 0 && faceNum == 1 && !(singleFaceInfo.faceRect.left == 0 && singleFaceInfo.faceRect.right == 0))
            {
                this.memberData.memberWithFeature.AsfFaceFeature = MemoryHelper.PtrToStructure<ASF_FaceFeature>(ptrImgFeature);

            }
            this.memberData.Face3DStatus = face3DStatus;
            this.memberData.FaceNum = faceNum;
            this.memberData.memberWithFeature.FaceImagePath = filename;

            ptrImgFeature = IntPtr.Zero;
            this.Enabled = true;
        }

        #endregion

        #region event

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            RegisterFormReset();
            RegisterFormHide();
            e.Cancel = true;
        }

        private void tbUserId_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                var userId = ValidateUserId();
                if (userId > 0)
                {
                    Afw.WinForm.WinMessageHelper.SendMessage(ptrParentForm, UserMessage.WM_QUERY_FACE_FEATURE_VIA_ID, this.Handle, (IntPtr)userId);
                }

            }
            else if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void btnUserEnrollCancel_Click(object sender, EventArgs e)
        {
            RegisterFormReset();
            this.Visible = false;
            Afw.WinForm.WinMessageHelper.SendMessage(ptrParentForm, Afw.WinForm.UserMessage.WM_PARENT_FORM_UNLOCK, IntPtr.Zero, null);
        }

        private void picboxUserAvatar_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择图片";
            openFileDialog.Filter = "图片文件|*.jpg;*.jpeg;*.png";
            openFileDialog.Multiselect = false;
            openFileDialog.FileName = string.Empty;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.Invoke(new Action(delegate
                {
                    ProcessImage(openFileDialog.FileName);
                }));
            }
        }

        private void tbEmployeeNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                var employeeNo = ValidateEmployeeNo();
                if (employeeNo > 0)
                {
                    Afw.WinForm.WinMessageHelper.SendMessage(ptrParentForm, UserMessage.WM_QUERY_FACE_FEATURE_VIA_EMPLOYEE_NO, this.Handle, (IntPtr)employeeNo);
                }

            }
            else if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void tbDeptId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void btnUserEnroll_Click(object sender, EventArgs e)
        {
            var dataOkFlag = true;

            this.Enabled = false;

            ValidateInput();

            foreach (var validate in isDataOk)
            {
                if (!validate.Value)
                {
                    dataOkFlag = false;
                    if (!errMsg.Contains("已经存在"))
                        MessageBox.Show(errMsg);
                    break;
                }
            }

            if (dataOkFlag)
            {
                IntPtr ptrFaceFeature = MemoryHelper.Malloc(MemoryHelper.SizeOf<MemberData>());
                MemoryHelper.StructureToPtr<MemberData>(this.memberData, ptrFaceFeature);
                if (!this.isModify)
                    Afw.WinForm.WinMessageHelper.SendMessage(ptrParentForm, UserMessage.WM_FACE_DATA_REGISTER, IntPtr.Zero, ptrFaceFeature);
                else
                    Afw.WinForm.WinMessageHelper.SendMessage(ptrParentForm, UserMessage.WM_FACE_DATA_UPDATE, IntPtr.Zero, ptrFaceFeature);
                RegisterFormHide();
                RegisterFormReset();
            }

            this.Enabled = true;

        }

        #endregion

        #region Message Handler
        protected override void DefWndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case UserMessage.WM_RESPONSE_FACE_FEATURE_VIA_ID:
                    var userId = (int)m.WParam;
                    var cnt = (int)m.LParam;
                    if (cnt <= 0)
                    {
                        MarkFaceDataOk("tbUserId");
                    }
                    else
                    {
                        MarkFaceDataError("tbUserId", $"用户{userId}已经存在");
                        MessageBox.Show($"用户{userId}已经存在");
                    }

                    break;
                case UserMessage.WM_RESPONSE_FACE_FEATURE_VIA_EMPLOYEE_NO:
                    var employeeNo = (int)m.WParam;
                    var cntEmpNo = (int)m.LParam;
                    if (cntEmpNo <= 0)
                    {
                        MarkFaceDataOk("tbEmployeeNo");
                    }
                    else
                    {
                        MarkFaceDataError("tbEmployeeNo", $"雇员号{employeeNo}已经存在");
                        MessageBox.Show($"雇员号{employeeNo}已经存在");
                    }
                    break;
                case UserMessage.WM_FACE_FORM_HIDE:

                    break;
                case UserMessage.WM_FACE_DATA_UPDATE:
                    var memberData = MemoryHelper.PtrToStructure<MemberData>(m.LParam);
                    var avatarPath = MemoryHelper.PtrToString(m.WParam);
                    MemoryHelper.Free(m.LParam);
                    MemoryHelper.Free(m.WParam);
                    this.Invoke(new Action(delegate
                    {
                        RegisterFormReset();
                        this.tbUserId.Text = memberData.memberWithFeature.Id.ToString();
                        this.tbName.Text = memberData.memberWithFeature.Name;
                        this.tbEmployeeNo.Text = memberData.memberWithFeature.EmployeeNo.ToString();
                        this.tbDeptId.Text = memberData.memberWithFeature.DeptId.ToString();
                        ProcessImage(avatarPath);
                        this.Visible = true;
                        this.isModify = true;
                        this.tbUserId.ReadOnly = true;
                        this.tbUserId.BackColor = Color.LightGray;
                        this.tbEmployeeNo.ReadOnly = true;
                        this.tbEmployeeNo.BackColor = Color.LightGray;
                    }));

                    break;
                default:
                    base.DefWndProc(ref m);//调用基类函数处理非自定义消息。
                    break;

            }
        }

        #endregion

    }
}
