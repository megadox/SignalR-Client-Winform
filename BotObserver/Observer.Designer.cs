namespace BotObserver
{
    partial class Observer
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.label22 = new DevExpress.XtraEditors.LabelControl();
            this.txtServer = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtRDAID = new DevExpress.XtraEditors.TextEdit();
            this.btnConnect = new DevExpress.XtraEditors.SimpleButton();
            this.btnDisconnect = new DevExpress.XtraEditors.SimpleButton();
            this.txtSubpath = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtNickname = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lblStatus = new DevExpress.XtraEditors.LabelControl();
            this.btnTest = new DevExpress.XtraEditors.SimpleButton();
            this.lblVersion = new DevExpress.XtraEditors.LabelControl();
            this.ckbLog = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtServer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRDAID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubpath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNickname.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label22
            // 
            this.label22.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Appearance.Options.UseFont = true;
            this.label22.Location = new System.Drawing.Point(45, 41);
            this.label22.Margin = new System.Windows.Forms.Padding(2);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(71, 17);
            this.label22.TabIndex = 0;
            this.label22.Text = "Server url :  ";
            // 
            // txtServer
            // 
            this.txtServer.Enabled = false;
            this.txtServer.Location = new System.Drawing.Point(190, 37);
            this.txtServer.Margin = new System.Windows.Forms.Padding(2);
            this.txtServer.Name = "txtServer";
            this.txtServer.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServer.Properties.Appearance.Options.UseFont = true;
            this.txtServer.Size = new System.Drawing.Size(319, 24);
            this.txtServer.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(45, 123);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(2);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(52, 17);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "RDA ID : ";
            // 
            // txtRDAID
            // 
            this.txtRDAID.Enabled = false;
            this.txtRDAID.Location = new System.Drawing.Point(190, 120);
            this.txtRDAID.Margin = new System.Windows.Forms.Padding(2);
            this.txtRDAID.Name = "txtRDAID";
            this.txtRDAID.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRDAID.Properties.Appearance.Options.UseFont = true;
            this.txtRDAID.Size = new System.Drawing.Size(319, 24);
            this.txtRDAID.TabIndex = 3;
            // 
            // btnConnect
            // 
            this.btnConnect.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Question;
            this.btnConnect.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.Appearance.Options.UseBackColor = true;
            this.btnConnect.Appearance.Options.UseFont = true;
            this.btnConnect.Location = new System.Drawing.Point(190, 204);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(2);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(140, 34);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Start";
            this.btnConnect.ToolTip = "Connect server & Watching Bot";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Warning;
            this.btnDisconnect.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisconnect.Appearance.Options.UseBackColor = true;
            this.btnDisconnect.Appearance.Options.UseFont = true;
            this.btnDisconnect.Location = new System.Drawing.Point(369, 204);
            this.btnDisconnect.Margin = new System.Windows.Forms.Padding(2);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(140, 34);
            this.btnDisconnect.TabIndex = 5;
            this.btnDisconnect.Text = "Stop";
            this.btnDisconnect.ToolTip = "Disconnect server";
            // 
            // txtSubpath
            // 
            this.txtSubpath.Enabled = false;
            this.txtSubpath.Location = new System.Drawing.Point(190, 77);
            this.txtSubpath.Margin = new System.Windows.Forms.Padding(2);
            this.txtSubpath.Name = "txtSubpath";
            this.txtSubpath.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubpath.Properties.Appearance.Options.UseFont = true;
            this.txtSubpath.Size = new System.Drawing.Size(319, 24);
            this.txtSubpath.TabIndex = 6;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(45, 84);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(63, 17);
            this.labelControl1.TabIndex = 7;
            this.labelControl1.Text = "Subpath :  ";
            // 
            // txtNickname
            // 
            this.txtNickname.Enabled = false;
            this.txtNickname.Location = new System.Drawing.Point(190, 161);
            this.txtNickname.Margin = new System.Windows.Forms.Padding(2);
            this.txtNickname.Name = "txtNickname";
            this.txtNickname.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNickname.Properties.Appearance.Options.UseFont = true;
            this.txtNickname.Size = new System.Drawing.Size(319, 24);
            this.txtNickname.TabIndex = 8;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(45, 164);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(2);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(72, 17);
            this.labelControl3.TabIndex = 9;
            this.labelControl3.Text = "Nick name : ";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 17;
            this.listBox1.Location = new System.Drawing.Point(190, 264);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(350, 140);
            this.listBox1.TabIndex = 10;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(45, 264);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(2);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(46, 17);
            this.labelControl4.TabIndex = 11;
            this.labelControl4.Text = "Status : ";
            // 
            // lblStatus
            // 
            this.lblStatus.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblStatus.Appearance.Options.UseFont = true;
            this.lblStatus.Appearance.Options.UseForeColor = true;
            this.lblStatus.Location = new System.Drawing.Point(45, 299);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(2);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(85, 21);
            this.lblStatus.TabIndex = 12;
            this.lblStatus.Text = "Disconnect";
            // 
            // btnTest
            // 
            this.btnTest.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Warning;
            this.btnTest.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTest.Appearance.Options.UseBackColor = true;
            this.btnTest.Appearance.Options.UseFont = true;
            this.btnTest.Location = new System.Drawing.Point(213, 485);
            this.btnTest.Margin = new System.Windows.Forms.Padding(2);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(140, 34);
            this.btnTest.TabIndex = 13;
            this.btnTest.Text = "Test";
            this.btnTest.ToolTip = "Disconnect server";
            this.btnTest.Visible = false;
            // 
            // lblVersion
            // 
            this.lblVersion.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Appearance.Options.UseFont = true;
            this.lblVersion.Location = new System.Drawing.Point(45, 357);
            this.lblVersion.Margin = new System.Windows.Forms.Padding(2);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(18, 17);
            this.lblVersion.TabIndex = 14;
            this.lblVersion.Text = "ver";
            // 
            // ckbLog
            // 
            this.ckbLog.AutoSize = true;
            this.ckbLog.Location = new System.Drawing.Point(45, 405);
            this.ckbLog.Name = "ckbLog";
            this.ckbLog.Size = new System.Drawing.Size(84, 21);
            this.ckbLog.TabIndex = 15;
            this.ckbLog.Text = "Log Write";
            this.ckbLog.UseVisualStyleBackColor = true;
            // 
            // Observer
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 514);
            this.Controls.Add(this.ckbLog);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtNickname);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtSubpath);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtRDAID);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtServer);
            this.Controls.Add(this.label22);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Observer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bot Observer";
            ((System.ComponentModel.ISupportInitialize)(this.txtServer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRDAID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubpath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNickname.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl label22;
        private DevExpress.XtraEditors.TextEdit txtServer;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtRDAID;
        private DevExpress.XtraEditors.SimpleButton btnConnect;
        private DevExpress.XtraEditors.SimpleButton btnDisconnect;
        private DevExpress.XtraEditors.TextEdit txtSubpath;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtNickname;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.ListBox listBox1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl lblStatus;
        private DevExpress.XtraEditors.SimpleButton btnTest;
        private DevExpress.XtraEditors.LabelControl lblVersion;
        private System.Windows.Forms.CheckBox ckbLog;
    }
}

