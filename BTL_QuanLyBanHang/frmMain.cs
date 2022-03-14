using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_QuanLyBanHang
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Class.Functions.Connect();  //Mở kết nối
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            Class.Functions.Disconnect(); //Đóng kết nối
            Application.Exit();           //Thoát
        }

        private void mnuChatLieu_Click(object sender, EventArgs e)
        {
            frmDanhMucChatLieu frmDMCL = new frmDanhMucChatLieu();
            frmDMCL.MdiParent = this;
            frmDMCL.Show();
        }

        private void mnuNhanVien_Click(object sender, EventArgs e)
        {
            frmDanhMucNhanVien frmDMNV = new frmDanhMucNhanVien();
            frmDMNV.MdiParent = this;
            frmDMNV.Show();
        }

        private void mnuHangHoa_Click(object sender, EventArgs e)
        {
            frmDanhMucHangHoa frmDMHH = new frmDanhMucHangHoa();
            frmDMHH.MdiParent = this;     //frmHangHoa nhận frmMain là cha
            frmDMHH.Show();
        }

        private void mnuKhachHang_Click(object sender, EventArgs e)
        {
            frmDanhMucKhachHang frmDMKH = new frmDanhMucKhachHang();
            frmDMKH.MdiParent = this;
            frmDMKH.Show();
        }

        private void mnuTimKiemKhachHang_Click(object sender, EventArgs e)
        {
            frmTimKiemKhach frmTKKH = new frmTimKiemKhach();
            frmTKKH.MdiParent = this;
            frmTKKH.Show();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult ret = MessageBox.Show("Bạn có muốn thoát!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ret == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void mnuHoaDonBan_Click(object sender, EventArgs e)
        {
            frmHoaDonBan frmHDBan = new frmHoaDonBan();
            frmHDBan.MdiParent = this;
            frmHDBan.Show();
        }

        private void mnuBCHangTon_Click(object sender, EventArgs e)
        {
            frmBaoCaoHangTon frmBCHT = new frmBaoCaoHangTon();
            frmBCHT.MdiParent = this;
            frmBCHT.Show();
        }

        private void mnuTimKiemHoaDon_Click(object sender, EventArgs e)
        {
            frmTimKiemHoaDon frmTKHD = new frmTimKiemHoaDon();
            frmTKHD.MdiParent = this;
            frmTKHD.Show();
        }
    }
}
