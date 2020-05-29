using System;
using System.Runtime.InteropServices;

namespace Afw.WinForm
{
    partial class FaceForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            if (disposing && registerForm != null)
            {
                registerForm.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FaceForm));
            this.txbDisplay = new System.Windows.Forms.TextBox();
            this.btnReloadFaceFeature = new System.Windows.Forms.Button();
            this.btnRegisterFaceFeature = new System.Windows.Forms.Button();
            this.dgvFace = new System.Windows.Forms.DataGridView();
            this.lblImageGrid = new System.Windows.Forms.Label();
            this.lblThreshold = new System.Windows.Forms.Label();
            this.rdbThresholdHigh = new System.Windows.Forms.RadioButton();
            this.rdbThresholdMid = new System.Windows.Forms.RadioButton();
            this.rdbThresholdLow = new System.Windows.Forms.RadioButton();
            this.videoSource = new AForge.Controls.VideoSourcePlayer();
            this.btnStartVideo = new System.Windows.Forms.Button();
            this.chbAutoCloseVideo = new System.Windows.Forms.CheckBox();
            this.cmsDgvMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.chbLiveness = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFace)).BeginInit();
            this.cmsDgvMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // txbDisplay
            // 
            this.txbDisplay.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txbDisplay.Location = new System.Drawing.Point(15, 636);
            this.txbDisplay.Multiline = true;
            this.txbDisplay.Name = "txbDisplay";
            this.txbDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txbDisplay.Size = new System.Drawing.Size(1474, 111);
            this.txbDisplay.TabIndex = 2;
            // 
            // btnReloadFaceFeature
            // 
            this.btnReloadFaceFeature.Enabled = false;
            this.btnReloadFaceFeature.Location = new System.Drawing.Point(15, 579);
            this.btnReloadFaceFeature.Name = "btnReloadFaceFeature";
            this.btnReloadFaceFeature.Size = new System.Drawing.Size(148, 45);
            this.btnReloadFaceFeature.TabIndex = 3;
            this.btnReloadFaceFeature.Text = "重新载入数据";
            this.btnReloadFaceFeature.UseVisualStyleBackColor = true;
            this.btnReloadFaceFeature.Click += new System.EventHandler(this.btnReloadFaceFeature_Click);
            // 
            // btnRegisterFaceFeature
            // 
            this.btnRegisterFaceFeature.Enabled = false;
            this.btnRegisterFaceFeature.Location = new System.Drawing.Point(182, 579);
            this.btnRegisterFaceFeature.Name = "btnRegisterFaceFeature";
            this.btnRegisterFaceFeature.Size = new System.Drawing.Size(148, 45);
            this.btnRegisterFaceFeature.TabIndex = 4;
            this.btnRegisterFaceFeature.Text = "注册人脸数据";
            this.btnRegisterFaceFeature.UseVisualStyleBackColor = true;
            this.btnRegisterFaceFeature.Click += new System.EventHandler(this.btnRegisterFaceFeature_Click);
            // 
            // dgvFace
            // 
            this.dgvFace.AllowUserToAddRows = false;
            this.dgvFace.AllowUserToDeleteRows = false;
            this.dgvFace.AllowUserToResizeColumns = false;
            this.dgvFace.AllowUserToResizeRows = false;
            this.dgvFace.ColumnHeadersHeight = 25;
            this.dgvFace.Location = new System.Drawing.Point(15, 47);
            this.dgvFace.MultiSelect = false;
            this.dgvFace.Name = "dgvFace";
            this.dgvFace.ReadOnly = true;
            this.dgvFace.RowHeadersWidth = 40;
            this.dgvFace.RowTemplate.Height = 50;
            this.dgvFace.Size = new System.Drawing.Size(830, 520);
            this.dgvFace.TabIndex = 1;
            this.dgvFace.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvFace_CellMouseDown);
            this.dgvFace.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvFace_RowHeaderMouseDoubleClick);
            // 
            // lblImageGrid
            // 
            this.lblImageGrid.AutoSize = true;
            this.lblImageGrid.Location = new System.Drawing.Point(12, 9);
            this.lblImageGrid.Name = "lblImageGrid";
            this.lblImageGrid.Size = new System.Drawing.Size(62, 18);
            this.lblImageGrid.TabIndex = 0;
            this.lblImageGrid.Text = "人脸库";
            // 
            // lblThreshold
            // 
            this.lblThreshold.AutoSize = true;
            this.lblThreshold.Location = new System.Drawing.Point(1011, 592);
            this.lblThreshold.Name = "lblThreshold";
            this.lblThreshold.Size = new System.Drawing.Size(80, 18);
            this.lblThreshold.TabIndex = 5;
            this.lblThreshold.Text = "安全等级";
            // 
            // rdbThresholdHigh
            // 
            this.rdbThresholdHigh.AutoSize = true;
            this.rdbThresholdHigh.Enabled = false;
            this.rdbThresholdHigh.Location = new System.Drawing.Point(1110, 592);
            this.rdbThresholdHigh.Name = "rdbThresholdHigh";
            this.rdbThresholdHigh.Size = new System.Drawing.Size(51, 22);
            this.rdbThresholdHigh.TabIndex = 6;
            this.rdbThresholdHigh.Text = "高";
            this.rdbThresholdHigh.UseVisualStyleBackColor = true;
            this.rdbThresholdHigh.CheckedChanged += new System.EventHandler(this.rdbThresholdHigh_CheckedChanged);
            // 
            // rdbThresholdMid
            // 
            this.rdbThresholdMid.AutoSize = true;
            this.rdbThresholdMid.Enabled = false;
            this.rdbThresholdMid.Location = new System.Drawing.Point(1173, 592);
            this.rdbThresholdMid.Name = "rdbThresholdMid";
            this.rdbThresholdMid.Size = new System.Drawing.Size(51, 22);
            this.rdbThresholdMid.TabIndex = 7;
            this.rdbThresholdMid.Text = "中";
            this.rdbThresholdMid.UseVisualStyleBackColor = true;
            this.rdbThresholdMid.CheckedChanged += new System.EventHandler(this.rdbThresholdMid_CheckedChanged);
            // 
            // rdbThresholdLow
            // 
            this.rdbThresholdLow.AutoSize = true;
            this.rdbThresholdLow.Checked = true;
            this.rdbThresholdLow.Enabled = false;
            this.rdbThresholdLow.Location = new System.Drawing.Point(1236, 592);
            this.rdbThresholdLow.Name = "rdbThresholdLow";
            this.rdbThresholdLow.Size = new System.Drawing.Size(69, 22);
            this.rdbThresholdLow.TabIndex = 8;
            this.rdbThresholdLow.TabStop = true;
            this.rdbThresholdLow.Text = "普通";
            this.rdbThresholdLow.UseVisualStyleBackColor = true;
            this.rdbThresholdLow.CheckedChanged += new System.EventHandler(this.rdbThresholdLow_CheckedChanged);
            // 
            // videoSource
            // 
            this.videoSource.Location = new System.Drawing.Point(882, 47);
            this.videoSource.Name = "videoSource";
            this.videoSource.Size = new System.Drawing.Size(607, 520);
            this.videoSource.TabIndex = 9;
            this.videoSource.Text = "videoSource";
            this.videoSource.VideoSource = null;
            this.videoSource.Paint += new System.Windows.Forms.PaintEventHandler(this.videoSource_Paint);
            // 
            // btnStartVideo
            // 
            this.btnStartVideo.Enabled = false;
            this.btnStartVideo.Location = new System.Drawing.Point(1341, 579);
            this.btnStartVideo.Name = "btnStartVideo";
            this.btnStartVideo.Size = new System.Drawing.Size(148, 45);
            this.btnStartVideo.TabIndex = 10;
            this.btnStartVideo.Text = "开启摄像头";
            this.btnStartVideo.UseVisualStyleBackColor = true;
            this.btnStartVideo.Click += new System.EventHandler(this.btnStartVideo_Click);
            // 
            // chbAutoCloseVideo
            // 
            this.chbAutoCloseVideo.AutoSize = true;
            this.chbAutoCloseVideo.Location = new System.Drawing.Point(760, 590);
            this.chbAutoCloseVideo.Name = "chbAutoCloseVideo";
            this.chbAutoCloseVideo.Size = new System.Drawing.Size(232, 22);
            this.chbAutoCloseVideo.TabIndex = 11;
            this.chbAutoCloseVideo.Text = "比对成功自动关闭摄像头";
            this.chbAutoCloseVideo.UseVisualStyleBackColor = true;
            // 
            // cmsDgvMenu
            // 
            this.cmsDgvMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cmsDgvMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEdit,
            this.tsmiDelete});
            this.cmsDgvMenu.Name = "cmsDgvMenu";
            this.cmsDgvMenu.Size = new System.Drawing.Size(117, 64);
            // 
            // tsmiEdit
            // 
            this.tsmiEdit.Name = "tsmiEdit";
            this.tsmiEdit.Size = new System.Drawing.Size(116, 30);
            this.tsmiEdit.Text = "编辑";
            this.tsmiEdit.Click += new System.EventHandler(this.tsmiEdit_Click);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(116, 30);
            this.tsmiDelete.Text = "删除";
            this.tsmiDelete.Click += new System.EventHandler(this.tsmiDelete_Click);
            // 
            // chbLiveness
            // 
            this.chbLiveness.AutoSize = true;
            this.chbLiveness.Location = new System.Drawing.Point(580, 590);
            this.chbLiveness.Name = "chbLiveness";
            this.chbLiveness.Size = new System.Drawing.Size(169, 22);
            this.chbLiveness.TabIndex = 12;
            this.chbLiveness.Text = "启用RGB活体检测";
            this.chbLiveness.UseVisualStyleBackColor = true;
            // 
            // FaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1508, 759);
            this.Controls.Add(this.chbLiveness);
            this.Controls.Add(this.chbAutoCloseVideo);
            this.Controls.Add(this.btnStartVideo);
            this.Controls.Add(this.videoSource);
            this.Controls.Add(this.rdbThresholdLow);
            this.Controls.Add(this.rdbThresholdMid);
            this.Controls.Add(this.rdbThresholdHigh);
            this.Controls.Add(this.lblThreshold);
            this.Controls.Add(this.btnRegisterFaceFeature);
            this.Controls.Add(this.btnReloadFaceFeature);
            this.Controls.Add(this.txbDisplay);
            this.Controls.Add(this.dgvFace);
            this.Controls.Add(this.lblImageGrid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FaceForm";
            this.Text = "人脸识别测试版";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.faceForm_FormClosing);
            this.Load += new System.EventHandler(this.FaceForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFace)).EndInit();
            this.cmsDgvMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion
        private System.Windows.Forms.DataGridView dgvFace;
        private System.Windows.Forms.TextBox txbDisplay;
        private System.Windows.Forms.Button btnReloadFaceFeature;
        private System.Windows.Forms.Button btnRegisterFaceFeature;
        private System.Windows.Forms.Label lblImageGrid;
        private System.Windows.Forms.Label lblThreshold;
        private System.Windows.Forms.RadioButton rdbThresholdHigh;
        private System.Windows.Forms.RadioButton rdbThresholdMid;
        private System.Windows.Forms.RadioButton rdbThresholdLow;
        private AForge.Controls.VideoSourcePlayer videoSource;
        private System.Windows.Forms.Button btnStartVideo;
        private System.Windows.Forms.CheckBox chbAutoCloseVideo;
        private System.Windows.Forms.ContextMenuStrip cmsDgvMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.CheckBox chbLiveness;
    }
}

