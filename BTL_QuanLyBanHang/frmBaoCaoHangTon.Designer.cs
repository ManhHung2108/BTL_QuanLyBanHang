
namespace BTL_QuanLyBanHang
{
    partial class frmBaoCaoHangTon
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
            this.crvHangTon = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnLoc = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtMaHang = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTenHang = new System.Windows.Forms.TextBox();
            this.btnLocTen = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // crvHangTon
            // 
            this.crvHangTon.ActiveViewIndex = -1;
            this.crvHangTon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvHangTon.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvHangTon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvHangTon.Location = new System.Drawing.Point(0, 0);
            this.crvHangTon.Name = "crvHangTon";
            this.crvHangTon.Size = new System.Drawing.Size(800, 433);
            this.crvHangTon.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.crvHangTon);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 433);
            this.panel1.TabIndex = 1;
            // 
            // btnLoc
            // 
            this.btnLoc.Location = new System.Drawing.Point(147, 45);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(75, 32);
            this.btnLoc.TabIndex = 2;
            this.btnLoc.Text = "Lọc dữ liệu";
            this.btnLoc.UseVisualStyleBackColor = true;
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnLocTen);
            this.panel2.Controls.Add(this.txtTenHang);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtMaHang);
            this.panel2.Controls.Add(this.btnLoc);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 353);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 80);
            this.panel2.TabIndex = 2;
            // 
            // txtMaHang
            // 
            this.txtMaHang.Location = new System.Drawing.Point(97, 14);
            this.txtMaHang.Name = "txtMaHang";
            this.txtMaHang.Size = new System.Drawing.Size(208, 20);
            this.txtMaHang.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Mã cần tìm";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(487, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Tên cần lọc";
            // 
            // txtTenHang
            // 
            this.txtTenHang.Location = new System.Drawing.Point(575, 9);
            this.txtTenHang.Name = "txtTenHang";
            this.txtTenHang.Size = new System.Drawing.Size(167, 20);
            this.txtTenHang.TabIndex = 6;
            // 
            // btnLocTen
            // 
            this.btnLocTen.Location = new System.Drawing.Point(634, 45);
            this.btnLocTen.Name = "btnLocTen";
            this.btnLocTen.Size = new System.Drawing.Size(75, 23);
            this.btnLocTen.TabIndex = 7;
            this.btnLocTen.Text = "Lọc theo tên";
            this.btnLocTen.UseVisualStyleBackColor = true;
            this.btnLocTen.Click += new System.EventHandler(this.btnLocTen_Click);
            // 
            // frmBaoCaoHangTon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 433);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "frmBaoCaoHangTon";
            this.Text = "frmBaoCaoHangTon";
            this.Load += new System.EventHandler(this.frmBaoCaoHangTon_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvHangTon;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtMaHang;
        private System.Windows.Forms.TextBox txtTenHang;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLocTen;
    }
}