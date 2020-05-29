namespace Afw.WinForm
{
    partial class RegisterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnUserEnrollCancel = new System.Windows.Forms.Button();
            this.btnUserEnroll = new System.Windows.Forms.Button();
            this.picboxUserAvatar = new System.Windows.Forms.PictureBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbEmployeeNo = new System.Windows.Forms.TextBox();
            this.tbDeptId = new System.Windows.Forms.TextBox();
            this.tbUserId = new System.Windows.Forms.TextBox();
            this.lblAvatar = new System.Windows.Forms.Label();
            this.lblDeptId = new System.Windows.Forms.Label();
            this.lblEmployeeNo = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picboxUserAvatar)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnUserEnrollCancel);
            this.groupBox1.Controls.Add(this.btnUserEnroll);
            this.groupBox1.Controls.Add(this.picboxUserAvatar);
            this.groupBox1.Controls.Add(this.tbName);
            this.groupBox1.Controls.Add(this.tbEmployeeNo);
            this.groupBox1.Controls.Add(this.tbDeptId);
            this.groupBox1.Controls.Add(this.tbUserId);
            this.groupBox1.Controls.Add(this.lblAvatar);
            this.groupBox1.Controls.Add(this.lblDeptId);
            this.groupBox1.Controls.Add(this.lblEmployeeNo);
            this.groupBox1.Controls.Add(this.lblName);
            this.groupBox1.Controls.Add(this.lblId);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(985, 528);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "录入人员信息";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label6.Location = new System.Drawing.Point(558, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(377, 18);
            this.label6.TabIndex = 12;
            this.label6.Text = "单击以下区域选择照片，大小建议不超过200KB";
            // 
            // btnUserEnrollCancel
            // 
            this.btnUserEnrollCancel.Location = new System.Drawing.Point(248, 423);
            this.btnUserEnrollCancel.Name = "btnUserEnrollCancel";
            this.btnUserEnrollCancel.Size = new System.Drawing.Size(115, 41);
            this.btnUserEnrollCancel.TabIndex = 11;
            this.btnUserEnrollCancel.Text = "取消";
            this.btnUserEnrollCancel.UseVisualStyleBackColor = true;
            this.btnUserEnrollCancel.Click += new System.EventHandler(this.btnUserEnrollCancel_Click);
            // 
            // btnUserEnroll
            // 
            this.btnUserEnroll.Location = new System.Drawing.Point(66, 423);
            this.btnUserEnroll.Name = "btnUserEnroll";
            this.btnUserEnroll.Size = new System.Drawing.Size(115, 41);
            this.btnUserEnroll.TabIndex = 10;
            this.btnUserEnroll.Text = "确定";
            this.btnUserEnroll.UseVisualStyleBackColor = true;
            this.btnUserEnroll.Click += new System.EventHandler(this.btnUserEnroll_Click);
            // 
            // picboxUserAvatar
            // 
            this.picboxUserAvatar.BackColor = System.Drawing.SystemColors.Window;
            this.picboxUserAvatar.Location = new System.Drawing.Point(561, 97);
            this.picboxUserAvatar.Name = "picboxUserAvatar";
            this.picboxUserAvatar.Size = new System.Drawing.Size(400, 400);
            this.picboxUserAvatar.TabIndex = 9;
            this.picboxUserAvatar.TabStop = false;
            this.picboxUserAvatar.Click += new System.EventHandler(this.picboxUserAvatar_Click);
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(198, 101);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(206, 28);
            this.tbName.TabIndex = 8;
            // 
            // tbEmployeeNo
            // 
            this.tbEmployeeNo.Location = new System.Drawing.Point(198, 142);
            this.tbEmployeeNo.Name = "tbEmployeeNo";
            this.tbEmployeeNo.ShortcutsEnabled = false;
            this.tbEmployeeNo.Size = new System.Drawing.Size(206, 28);
            this.tbEmployeeNo.TabIndex = 7;
            this.tbEmployeeNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbEmployeeNo_KeyPress);
            // 
            // tbDeptId
            // 
            this.tbDeptId.Location = new System.Drawing.Point(198, 183);
            this.tbDeptId.Name = "tbDeptId";
            this.tbDeptId.ShortcutsEnabled = false;
            this.tbDeptId.Size = new System.Drawing.Size(206, 28);
            this.tbDeptId.TabIndex = 6;
            this.tbDeptId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbDeptId_KeyPress);
            // 
            // tbUserId
            // 
            this.tbUserId.Location = new System.Drawing.Point(198, 60);
            this.tbUserId.Name = "tbUserId";
            this.tbUserId.ShortcutsEnabled = false;
            this.tbUserId.Size = new System.Drawing.Size(206, 28);
            this.tbUserId.TabIndex = 5;
            this.tbUserId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbUserId_KeyPress);
            // 
            // lblAvatar
            // 
            this.lblAvatar.AutoSize = true;
            this.lblAvatar.Location = new System.Drawing.Point(486, 102);
            this.lblAvatar.Name = "lblAvatar";
            this.lblAvatar.Size = new System.Drawing.Size(44, 18);
            this.lblAvatar.TabIndex = 4;
            this.lblAvatar.Text = "照片";
            // 
            // lblDeptId
            // 
            this.lblDeptId.AutoSize = true;
            this.lblDeptId.Location = new System.Drawing.Point(53, 188);
            this.lblDeptId.Name = "lblDeptId";
            this.lblDeptId.Size = new System.Drawing.Size(62, 18);
            this.lblDeptId.TabIndex = 3;
            this.lblDeptId.Text = "部门Id";
            // 
            // lblEmployeeNo
            // 
            this.lblEmployeeNo.AutoSize = true;
            this.lblEmployeeNo.Location = new System.Drawing.Point(53, 144);
            this.lblEmployeeNo.Name = "lblEmployeeNo";
            this.lblEmployeeNo.Size = new System.Drawing.Size(62, 18);
            this.lblEmployeeNo.TabIndex = 2;
            this.lblEmployeeNo.Text = "雇员号";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(53, 102);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(44, 18);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "姓名";
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(53, 63);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(26, 18);
            this.lblId.TabIndex = 0;
            this.lblId.Text = "Id";
            // 
            // RegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 552);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RegisterForm";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "注册人脸数据";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picboxUserAvatar)).EndInit();
            this.ResumeLayout(false);

        }



        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblDeptId;
        private System.Windows.Forms.Label lblEmployeeNo;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.PictureBox picboxUserAvatar;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbEmployeeNo;
        private System.Windows.Forms.TextBox tbDeptId;
        private System.Windows.Forms.TextBox tbUserId;
        private System.Windows.Forms.Label lblAvatar;
        private System.Windows.Forms.Button btnUserEnrollCancel;
        private System.Windows.Forms.Button btnUserEnroll;
        private System.Windows.Forms.Label label6;
    }
}