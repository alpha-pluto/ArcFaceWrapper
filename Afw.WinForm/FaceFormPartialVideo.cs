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
using AForge.Video.DirectShow;
using Afw.Core.Domain;
using Afw.Core.Helper;
using Afw.Data;
using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Afw.WinForm
{
    public partial class FaceForm
    {
        #region parameter

        private FaceTrackUnit trackUnit = new FaceTrackUnit();
        private Font font = new Font(FontFamily.GenericSerif, 8f);
        private SolidBrush brush = new SolidBrush(Color.Red);
        private Pen pen = new Pen(Color.Red);
        private bool isLock = false;

        private Bitmap currentIrImage;

        #endregion

        #region utility

        private void SetLivenessParam()
        {
            ASF_LivenessThreshold a = new ASF_LivenessThreshold();
            if (engineContext.IrCameraIdx >= 0)
                a.thresholdmodel_BGR = 0.75f;
            else
                a.thresholdmodel_BGR = 0.85f;
            a.thresholdmodel_IR = 0.7f;
            engineContext.SetLivenessParam(a);
        }

        /// <summary>
        /// 得到feature比较结果
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        private int CompareFeature(IntPtr feature, out float similarity)
        {
            int result = -1;
            similarity = 0f;
            //如果人脸库不为空，则进行人脸匹配
            if (iFaceDataRepository.TableNoTracking != null && iFaceDataRepository.TableNoTracking.Count() > 0)
            {
                for (int i = 0; i < iFaceDataRepository.TableNoTracking.Count(); i++)
                {
                    var featureToCompare = iFaceDataRepository.TableNoTracking.ToList()[i];
                    var featureMemberData = featureToCompare.CreateStruct();

                    IntPtr ptrFeatureCandidate = MemoryHelper.Malloc(MemoryHelper.SizeOf<ASF_FaceFeature>());
                    MemoryHelper.StructureToPtr(featureMemberData.memberWithFeature.AsfFaceFeature, ptrFeatureCandidate);

                    //调用人脸匹配方法，进行匹配
                    var retCode = engineContext.FaceFeatureCompare(out similarity, feature, ptrFeatureCandidate);
                    if (similarity >= engineContext.Threshold)
                    {
                        result = featureToCompare.Id;
                        break;
                    }

                }
            }
            return result;
        }

        #endregion

        #region event

        private void rdbThresholdHigh_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdbThresholdHigh.Checked)
            {
                engineContext.Threshold = 0.85f;
                WinMessageHelper.SendMessage(this.Handle, UserMessage.WM_MESSAGE, IntPtr.Zero, "安全等级置为'高'级");
            }
        }

        private void rdbThresholdMid_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdbThresholdMid.Checked)
            {
                engineContext.Threshold = 0.82f;
                WinMessageHelper.SendMessage(this.Handle, UserMessage.WM_MESSAGE, IntPtr.Zero, "安全等级置为'中'级");
            }
        }

        private void rdbThresholdLow_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdbThresholdLow.Checked)
            {
                engineContext.Threshold = 0.80f;
                WinMessageHelper.SendMessage(this.Handle, UserMessage.WM_MESSAGE, IntPtr.Zero, "安全等级置为'普通'级");
            }
        }

        private void btnStartVideo_Click(object sender, EventArgs e)
        {
            if (this.btnStartVideo.Enabled)
            {
                if (engineContext.FilterInfoCollection.Count <= 0)
                {
                    MessageBox.Show("未检测到摄像头，请确保已安装摄像头或驱动!");
                    return;
                }
                if (iFaceDataRepository.TableNoTracking.Count() <= 0)
                {
                    MessageBox.Show("人脸库为空，请先注册人脸数据!");
                    return;
                }
                SetLivenessParam();
                //视频处于开启状
                if (videoSource.IsRunning)
                {
                    ButtonEnableForImage();
                    btnStartVideo.Text = "开启摄像头";
                    engineContext.StopVideoCapture();
                    this.rdbThresholdHigh.Enabled = true;
                    this.rdbThresholdLow.Enabled = true;
                    this.rdbThresholdMid.Enabled = true;
                    this.dgvFace.Enabled = true;
                }
                else
                {
                    ButtonDisableForImage();
                    this.rdbThresholdHigh.Enabled = false;
                    this.rdbThresholdLow.Enabled = false;
                    this.rdbThresholdMid.Enabled = false;
                    this.dgvFace.Enabled = false;
                    btnStartVideo.Text = "关闭摄像头";
                    videoSource.VideoSource = engineContext.DeviceVideo;
                    if (engineContext.IrCameraIdx >= 0)
                        engineContext.DeviceVideoIR.NewFrame += videoDeviceIr_NewFrame;
                    engineContext.StartVideoCapture();

                }


            }
        }

        /// <summary>
        /// 图像显示到窗体上，得到每一帧图像，并进行处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void videoSource_Paint(object sender, PaintEventArgs e)
        {
            if (videoSource.IsRunning)
            {
                //得到当前摄像头下的图片
                Bitmap bitmap = videoSource.GetCurrentVideoFrame();

                if (bitmap == null)
                {
                    return;
                }

                Graphics g = e.Graphics;
                float offsetX = videoSource.Width * 1f / bitmap.Width;
                float offsetY = videoSource.Height * 1f / bitmap.Height;

                ASF_MultiFaceInfo multiFaceInfoIr = default(ASF_MultiFaceInfo);

                //检测人脸，得到Rect框
                ASF_MultiFaceInfo multiFaceInfo = engineContext.DetectFace(bitmap);
                if (multiFaceInfo.Equals(default(ASF_MultiFaceInfo)))
                    return;
                //得到最大人脸
                ASF_SingleFaceInfo maxFace = FaceProcessHelper.GetMaxFace(multiFaceInfo);
                //得到Rect
                MRECT rect = maxFace.faceRect;
                float x = rect.left * offsetX;
                float width = rect.right * offsetX - x;
                float y = rect.top * offsetY;
                float height = rect.bottom * offsetY - y;

                //根据Rect进行画框
                g.DrawRectangle(pen, x, y, width, height);
                if (trackUnit.message != "" && x > 0 && y > 0)
                {
                    //将上一帧检测结果显示到页面上
                    g.DrawString(trackUnit.message, font, brush, x, y + 5);
                }

                //保证只检测一帧，防止页面卡顿以及出现其他内存被占用情况
                if (isLock == false)
                {
                    //g.DrawRectangle(pen, x, y, width, height);
                    //if (trackUnit.message != "" && x > 0 && y > 0)
                    //{
                    //    //将上一帧检测结果显示到页面上
                    //    g.DrawString(trackUnit.message, font, brush, x, y + 5);
                    //}
                    isLock = true;
                    //异步处理提取特征值和比对，不然页面会比较卡
                    ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                    {
                        if (rect.left != 0 && rect.right != 0 && rect.top != 0 && rect.bottom != 0)
                        {
                            try
                            {
                                var liveness = 1;
                                var livenessIR = -1;

                                if (this.chbLiveness.Checked)
                                {
                                    liveness = -1;
                                    var livenessInfo = engineContext.GetLivenessScore(bitmap, maxFace);
                                    try
                                    {
                                        liveness = MemoryHelper.PtrToStructure<int>(livenessInfo.isLive);
                                    }
                                    catch (Exception ex)
                                    {
                                        liveness = -1;
                                        this.Invoke(new Action(delegate
                                        {
                                            //WinMessageHelper.SendMessage(this.Handle, UserMessage.WM_MESSAGE, IntPtr.Zero, $"liveness => {ex.Message}");
                                            Afw.Core.Helper.SimplifiedLogHelper.WriteIntoSystemLog(nameof(FaceForm), $"videoSource_Paint livenessInfo exception=> {ex.ToString()}");
                                        }));

                                    }
                                }
                                if (currentIrImage != null)
                                {

                                    //检测人脸，得到Rect框
                                    multiFaceInfoIr = engineContext.DetectFaceIR(currentIrImage);

                                    if (multiFaceInfoIr.faceNum > 0)
                                    {

                                        //得到最大人脸
                                        ASF_SingleFaceInfo maxFaceIR = FaceProcessHelper.GetMaxFace(multiFaceInfoIr);
                                        var livenessInfoIR = engineContext.GetLivenessScoreIR(currentIrImage, maxFace);

                                        try
                                        {
                                            livenessIR = MemoryHelper.PtrToStructure<int>(livenessInfoIR.isLive);
                                        }
                                        catch (Exception ex)
                                        {
                                            livenessIR = -1;
                                            this.Invoke(new Action(delegate
                                            {
                                                //WinMessageHelper.SendMessage(this.Handle, UserMessage.WM_MESSAGE, IntPtr.Zero, $"liveness IR => {ex.Message}");
                                                Afw.Core.Helper.SimplifiedLogHelper.WriteIntoSystemLog(nameof(FaceForm), $"videoSource_Paint livenessIRInfo exception=> {ex.ToString()}");
                                            }));

                                        }
                                    }
                                    else
                                    {
                                        livenessIR = -1;
                                    }

                                }
                                else
                                {
                                    livenessIR = engineContext.IrCameraIdx >= 0 ? -1 : 1;
                                }
                                if (liveness == 1 && livenessIR == 1)
                                {
                                    //提取人脸特征
                                    IntPtr feature = engineContext.ExtractFeature(bitmap, maxFace);
                                    float similarity = 0f;
                                    if (feature == IntPtr.Zero)
                                        return;
                                    //得到比对结果
                                    int result = CompareFeature(feature, out similarity);
                                    if (result > -1)
                                    {
                                        //将比对结果放到显示消息中，用于最新显示
                                        trackUnit.message = $" 员工Id{result} 相似度{similarity}";
                                        this.Invoke(new Action(delegate
                                        {
                                            var member = iFaceDataRepository.GetById(result);
                                            WinMessageHelper.SendMessage(this.Handle, UserMessage.WM_MESSAGE, IntPtr.Zero, $"人脸比对成功，【姓名：{member.Name},雇员号：{member.EmployeeNo}】");

                                            if (this.chbAutoCloseVideo.Checked)
                                            {
                                                ButtonEnableForImage();
                                                btnStartVideo.Text = "开启摄像头";
                                                engineContext.StopVideoCapture();
                                                this.rdbThresholdHigh.Enabled = true;
                                                this.rdbThresholdLow.Enabled = true;
                                                this.rdbThresholdMid.Enabled = true;
                                                this.dgvFace.Enabled = true;
                                                MessageBox.Show($"人脸比对成功，【姓名：{member.Name},雇员号：{member.EmployeeNo}】");
                                            }

                                        }));

                                    }
                                    else
                                    {
                                        //重置显示消息
                                        trackUnit.message = "";
                                    }
                                    MemoryHelper.Free(feature);
                                }
                                else
                                {
                                    trackUnit.message = "";
                                }


                            }
                            catch (Exception ex)
                            {
                                this.Invoke(new Action(delegate
                                {
                                    WinMessageHelper.SendMessage(this.Handle, UserMessage.WM_MESSAGE, IntPtr.Zero, ex.Message);
                                }));

                            }
                            finally
                            {
                                isLock = false;
                            }
                        }
                        isLock = false;

                        if (currentIrImage != null)
                            currentIrImage = null;
                    }));
                }

            }

            GC.Collect();
        }

        private void videoSourceIr_Paint(object sender, PaintEventArgs e)
        {

        }

        private void videoDeviceIr_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            this.Invoke(new Action(delegate
            {
                currentIrImage = AForge.Imaging.Image.Clone(eventArgs.Frame);
            }));

            GC.Collect();
        }

        #endregion
    }
}
