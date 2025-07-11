using System.Windows.Forms;

namespace CISimageSaveBD
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnSelectSrc;
        private System.Windows.Forms.Button btnSelectDst;
        private System.Windows.Forms.TextBox txtSrc;
        private System.Windows.Forms.TextBox txtDst;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbFormat;
        private System.Windows.Forms.CheckBox chkSaveOdd;
        private System.Windows.Forms.CheckBox chkSaveEven;

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
            this.SuspendLayout();
            this.btnSelectSrc = new System.Windows.Forms.Button();
            this.btnSelectDst = new System.Windows.Forms.Button();
            this.txtSrc = new System.Windows.Forms.TextBox();
            this.txtDst = new System.Windows.Forms.TextBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // 홀수(ODD) 체크박스
            this.chkSaveOdd = new System.Windows.Forms.CheckBox();
            this.chkSaveOdd.Location = new System.Drawing.Point(250, 80);
            this.chkSaveOdd.Size = new System.Drawing.Size(80, 23);
            this.chkSaveOdd.Text = "어두운 이미지 저장";
            this.chkSaveOdd.Checked = true; // 기본값 ON
            this.Controls.Add(this.chkSaveOdd);

            // 짝수(EVEN) 체크박스
            this.chkSaveEven = new System.Windows.Forms.CheckBox();
            this.chkSaveEven.Location = new System.Drawing.Point(340, 80);
            this.chkSaveEven.Size = new System.Drawing.Size(80, 23);
            this.chkSaveEven.Text = "밝은 이미지 저장";
            this.chkSaveEven.Checked = true; // 기본값 ON
            this.Controls.Add(this.chkSaveEven);

            // btnSelectSrc
            this.btnSelectSrc.Location = new System.Drawing.Point(12, 12);
            this.btnSelectSrc.Size = new System.Drawing.Size(90, 23);
            this.btnSelectSrc.Text = "원본 폴더 선택";
            this.btnSelectSrc.Click += new System.EventHandler(this.btnSelectSrc_Click);

            // txtSrc
            this.txtSrc.Location = new System.Drawing.Point(110, 12);
            this.txtSrc.Size = new System.Drawing.Size(300, 23);
            this.txtSrc.ReadOnly = true;

            // btnSelectDst
            this.btnSelectDst.Location = new System.Drawing.Point(12, 45);
            this.btnSelectDst.Size = new System.Drawing.Size(90, 23);
            this.btnSelectDst.Text = "저장 폴더 선택";
            this.btnSelectDst.Click += new System.EventHandler(this.btnSelectDst_Click);

            // txtDst
            this.txtDst.Location = new System.Drawing.Point(110, 45);
            this.txtDst.Size = new System.Drawing.Size(300, 23);
            this.txtDst.ReadOnly = true;

            // btnRun
            this.btnRun.Location = new System.Drawing.Point(12, 150);
            this.btnRun.Size = new System.Drawing.Size(398, 30);
            this.btnRun.Text = "실행";
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);

            // lblStatus
            this.lblStatus.Location = new System.Drawing.Point(12, 120);
            this.lblStatus.Size = new System.Drawing.Size(398, 23);
            this.lblStatus.Text = "상태: 대기중";


            this.cmbFormat = new System.Windows.Forms.ComboBox();
            this.cmbFormat.Location = new System.Drawing.Point(110, 80);
            this.cmbFormat.Size = new System.Drawing.Size(120, 23);
            this.cmbFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbFormat.Items.AddRange(new object[] { "BMP", "PNG", "JPG" });
            this.cmbFormat.SelectedIndex = 2; // 기본값 BMP
            this.Controls.Add(this.cmbFormat);

            // MainForm
            this.ClientSize = new System.Drawing.Size(424, 200);
            this.Controls.Add(this.btnSelectSrc);
            this.Controls.Add(this.txtSrc);
            this.Controls.Add(this.btnSelectDst);
            this.Controls.Add(this.txtDst);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.lblStatus);
            this.Name = "MainForm";
            this.Text = "이미지 홀수/짝수 행 분리 저장기";
            this.ResumeLayout(false);
            this.PerformLayout();


            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
        }

        #endregion
    }
}

