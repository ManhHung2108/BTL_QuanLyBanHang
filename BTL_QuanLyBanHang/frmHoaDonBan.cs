using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using BTL_QuanLyBanHang.Class;
using COMExel = Microsoft.Office.Interop.Excel;
using System.Configuration;

namespace BTL_QuanLyBanHang
{
    public partial class frmHoaDonBan : Form
    {
        DataTable tblCTHDB;
        public frmHoaDonBan()
        {
            InitializeComponent();
        }

        private void frmHoaDonBan_Load(object sender, EventArgs e)
        {
            btnThemHD.Enabled = true;
            btnLuuHD.Enabled = false;
            btnXoaHD.Enabled = false;
            btnInHD.Enabled = false;
            //txtMaHDBan.ReadOnly = true;
            txtTenNV.ReadOnly = true;
            txtDiaChi.ReadOnly = true;
            mtbDienThoaiKh.ReadOnly = true;
            txtTenHang.ReadOnly = true;
            txtDonGiaBan.ReadOnly = true;
            txtThanhTien.ReadOnly = true;
            txtTongTien.ReadOnly = true;
            txtGiamGia.Text = "0";
            txtTongTien.Text = "0";
            Functions.FillComBoBox("SELECT sMaKhach, sTenKhach FROM tblKhach", cboMaKhach, "sMaKhach", "sTenKhach");
            cboMaKhach.SelectedIndex = -1;
            Functions.FillComBoBox("SELECT sMaNhanVien, sTenNhanVien FROM tblNhanVien", cboMaNV, "sMaNhanVien", "sMaNhanVien");
            cboMaNV.SelectedIndex = -1;
            Functions.FillComBoBox("SELECT sMaHang, sTenHang FROM tblHang", cboMaHang, "sMaHang", "sTenHang");
            cboMaHang.SelectedIndex = -1;
            if (txtMaHDBan.Text != "")
            {
                LoadInfoHoaDon();
                btnXoaHD.Enabled = true;
                btnInHD.Enabled = true;
            }
            LoadDataGridView();
        }

        //Nạp dữ liệu lên DataGridView
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT a.sMaHang, b.sTenHang, a.iSoLuong, b.fDonGiaBan, a.fGiamGia,a.fThanhTien FROM tblChiTietHoaDonBan AS a, tblHang AS b WHERE a.sMaHang=b.sMaHang";
            tblCTHDB = Functions.GetDataTable(sql);
            dgrHoaDonBanHang.DataSource = tblCTHDB;
            dgrHoaDonBanHang.Columns[0].HeaderText = "Mã hàng";
            dgrHoaDonBanHang.Columns[1].HeaderText = "Tên hàng";
            dgrHoaDonBanHang.Columns[2].HeaderText = "Số lượng";
            dgrHoaDonBanHang.Columns[3].HeaderText = "Đơn giá";
            dgrHoaDonBanHang.Columns[4].HeaderText = "Giảm giá %";
            dgrHoaDonBanHang.Columns[5].HeaderText = "Thành tiền";
            dgrHoaDonBanHang.Columns[0].Width = 80;
            dgrHoaDonBanHang.Columns[1].Width = 130;
            dgrHoaDonBanHang.Columns[2].Width = 80;
            dgrHoaDonBanHang.Columns[3].Width = 90;
            dgrHoaDonBanHang.Columns[4].Width = 90;
            dgrHoaDonBanHang.Columns[5].Width = 90;
            dgrHoaDonBanHang.AllowUserToAddRows = false;
            dgrHoaDonBanHang.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        //Nạp chi tiết hóa đơn
        private void LoadInfoHoaDon()
        {
            string str;
            str = "SELECT dNgayBan FROM tblHoaDonBan WHERE sMaHDBan = N'" + txtMaHDBan.Text + "'";
            dtpNgayBan.Value =DateTime.Parse(Functions.GetFieldValues(str));
            str = "SELECT sMaNhanVien FROM tblHoaDonBan WHERE sMaHDBan = N'" + txtMaHDBan.Text + "'";
            cboMaNV.Text = Functions.GetFieldValues(str);
            str = "SELECT sMaKhach FROM tblHoaDonBan WHERE sMaHDBan = N'" + txtMaHDBan.Text + "'";
            cboMaKhach.Text = Functions.GetFieldValues(str);
            str = "SELECT fTongTien FROM tblHoaDonBan WHERE sMaHDBan = N'" + txtMaHDBan.Text + "'";
            txtTongTien.Text = Functions.GetFieldValues(str);
        }

        private void ResetValuesHang()
        {
            cboMaHang.Text = "";
            txtSoLuong.Text = "";
            txtGiamGia.Text = "0";
            txtThanhTien.Text = "0";
        }
        private void ResetValues()
        {
            txtMaHDBan.Text = "";
            dtpNgayBan.Value = DateTime.Now;
            cboMaNV.Text = "";
            cboMaKhach.Text = "";
            txtTongTien.Text = "0";
            cboMaHang.Text = "";
            txtSoLuong.Text = "";
            txtGiamGia.Text = "0";
            txtThanhTien.Text = "0";
        }
        private void btnThemHD_Click(object sender, EventArgs e)
        {
            txtMaHDBan.Enabled = true;
            btnXoaHD.Enabled = false;
            btnLuuHD.Enabled = true;
            btnInHD.Enabled = false;
            btnThemHD.Enabled = false;
            ResetValues();
            LoadDataGridView();
        }
        private void btnLuuHD_Click(object sender, EventArgs e)
        {
            string sql;
            double sl, SLcon, tong, Tongmoi;
            sql = "SELECT sMaHDBan FROM tblHoaDonBan WHERE sMaHDBan=N'" + txtMaHDBan.Text + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã hóa đơn đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaHDBan.Focus();
                return;
            }
            else 
            {
                // Mã hóa đơn chưa có, tiến hành lưu các thông tin chung
                if (cboMaNV.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboMaNV.Focus();
                    return;
                }
                if (cboMaKhach.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboMaKhach.Focus();
                    return;
                }

                string constr1 = ConfigurationManager.ConnectionStrings["btl_qlbh"].ConnectionString;
                using(SqlConnection cnn = new SqlConnection(constr1)) 
                {
                    using(SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.Connection = cnn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "sp_tblHoaDonBan_Insert";
                        cmd.Parameters.AddWithValue("@sMaHoaDon", txtMaHDBan.Text);
                        cmd.Parameters.AddWithValue("@sMaNhanVien", cboMaNV.SelectedValue);
                        cmd.Parameters.AddWithValue("@dNgayBan", dtpNgayBan.Value);
                        cmd.Parameters.AddWithValue("@sMaKhach", cboMaKhach.SelectedValue);
                        cmd.Parameters.AddWithValue("@fTongTien", txtTongTien.Text);

                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                    }
                }
            }

            // Lưu thông tin của các mặt hàng
            if (cboMaHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaHang.Focus();
                return;
            }
            if ((txtSoLuong.Text.Trim().Length == 0) || (txtSoLuong.Text == "0"))
            {
                MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Text = "";
                txtSoLuong.Focus();
                return;
            }
            if (txtGiamGia.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập giảm giá", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGiamGia.Focus();
                return;
            }
            sql = "SELECT sMaHang FROM tblChiTietHoaDonBan WHERE sMaHang=N'" + cboMaHang.SelectedValue + "' AND sMaHDBan = N'" + txtMaHDBan.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã hàng này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetValuesHang();
                cboMaHang.Focus();
                return;
            }
            // Kiểm tra xem số lượng hàng trong kho còn đủ để cung cấp không?
            sl = Convert.ToDouble(Functions.GetFieldValues("SELECT iSoLuong FROM tblHang WHERE sMaHang = N'" + cboMaHang.SelectedValue + "'"));
            if (Convert.ToDouble(txtSoLuong.Text) > sl)
            {
                MessageBox.Show("Số lượng mặt hàng này chỉ còn " + sl, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Text = "";
                txtSoLuong.Focus();
                return;
            }
            string constr = ConfigurationManager.ConnectionStrings["btl_qlbh"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_tblChiTietHoaDonBan_Insert";
                    cmd.Parameters.AddWithValue("@sMaHDBan",txtMaHDBan.Text);
                    cmd.Parameters.AddWithValue("@sMaHang", cboMaHang.SelectedValue);
                    cmd.Parameters.AddWithValue("@iSoLuong", txtSoLuong.Text);
                    cmd.Parameters.AddWithValue("@fDonGia", txtDonGiaBan.Text);
                    cmd.Parameters.AddWithValue("@fGiamGia", txtGiamGia.Text);
                    cmd.Parameters.AddWithValue("@fThanhTien", txtThanhTien.Text);
             
                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    cnn.Close();
                }
            }

            LoadDataGridView();

            // Cập nhật lại số lượng của mặt hàng vào bảng tblHang
            SLcon = sl - Convert.ToDouble(txtSoLuong.Text);
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_tblHang_Update_SoLuong";
                    cmd.Parameters.AddWithValue("@sMaHang", cboMaHang.SelectedValue);
                    cmd.Parameters.AddWithValue("@iSoLuong", SLcon);

                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    cnn.Close();
                }
            }

            // Cập nhật lại tổng tiền cho hóa đơn bán
            tong = Convert.ToDouble(Functions.GetFieldValues("SELECT fTongTien FROM tblHoaDonBan WHERE sMaHDBan = N'" + txtMaHDBan.Text + "'"));
            Tongmoi = tong + Convert.ToDouble(txtThanhTien.Text);
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_tblHoaDonBan_Update_TongTien";
                    cmd.Parameters.AddWithValue("@sMaHDBan", txtMaHDBan.Text);
                    cmd.Parameters.AddWithValue("@fTongTien", Tongmoi);

                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    cnn.Close();
                }
            }
            txtTongTien.Text = Tongmoi.ToString();
           
            ResetValuesHang();
            btnXoaHD.Enabled = true;
            btnThemHD.Enabled = true;
            btnInHD.Enabled = true;
        }
        private void cboMaNV_TextChanged(object sender, EventArgs e)
        {
            string str;
            if (cboMaNV.Text == "")
            {
                txtTenNV.Text = "";
            }
            // Khi chọn Mã nhân viên thì tên nhân viên tự động hiện ra
            str = "Select sTenNhanVien from tblNhanVien where sMaNhanVien =N'" + cboMaNV.SelectedValue + "'";
            txtTenNV.Text = Functions.GetFieldValues(str);
        }

        private void cboMaKhach_TextChanged(object sender, EventArgs e)
        {
            string str;
            if(cboMaKhach.Text == "")
            {
                txtTenKH.Text = "";
                txtDiaChi.Text = "";
                mtbDienThoaiKh.Text = "";
            }
            //Khi chọn Mã khách hàng thì các thông tin của khách hàng sẽ hiện ra
            str = "Select sTenKhach from tblKhach where sMaKhach = N'" + cboMaKhach.SelectedValue + "'";
            txtTenKH.Text = Functions.GetFieldValues(str);
            str = "Select sDiaChi from tblKhach where sMaKhach = N'" + cboMaKhach.SelectedValue + "'";
            txtDiaChi.Text = Functions.GetFieldValues(str);
            str = "Select sDienThoai from tblKhach where sMaKhach= N'" + cboMaKhach.SelectedValue + "'";
            mtbDienThoaiKh.Text = Functions.GetFieldValues(str);
        }

        private void cboMaHang_TextChanged(object sender, EventArgs e)
        {
            string str;
            if (cboMaHang.Text == "")
            {
                txtTenHang.Text = "";
                txtDonGiaBan.Text = "";
            }
            // Khi chọn mã hàng thì các thông tin về hàng hiện ra
            str = "SELECT sTenHang FROM tblHang WHERE sMaHang ='" + cboMaHang.SelectedValue + "'";
            txtTenHang.Text = Functions.GetFieldValues(str);
            str = "SELECT fDonGiaBan FROM tblHang WHERE sMaHang ='" + cboMaHang.SelectedValue + "'";
            txtDonGiaBan.Text = Functions.GetFieldValues(str);
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            //Khi thay đổi số lượng thì thực hiện tính lại thành tiền
            double tt, sl, dg, gg;
            if (txtSoLuong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoLuong.Text);
            if (txtGiamGia.Text == "")
                gg = 0;
            else
                gg = Convert.ToDouble(txtGiamGia.Text);
            if (txtDonGiaBan.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDonGiaBan.Text);
            tt = sl * dg - sl * dg * gg / 100;
            txtThanhTien.Text = tt.ToString();
        }

        private void txtGiamGia_TextChanged(object sender, EventArgs e)
        {
            // Khi thay đổi giảm giá thì tính lại thành tiền
            double tt, sl, dg, gg;
            if (txtSoLuong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoLuong.Text);
            if (txtGiamGia.Text == "")
                gg = 0;
            else
                gg = Convert.ToDouble(txtGiamGia.Text);
            if (txtDonGiaBan.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDonGiaBan.Text);
            tt = sl * dg - sl * dg * gg / 100;
            txtThanhTien.Text = tt.ToString();
        }

        private void btnTimKiemHD_Click(object sender, EventArgs e)
        {
            if (cboMaHD.Text == "")
            {
                MessageBox.Show("Bạn phải chọn một mã hóa đơn để tìm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaHD.Focus();
                return;
            }
            txtMaHDBan.Text = cboMaHD.Text;
            LoadInfoHoaDon();
            LoadDataGridView();
            btnXoaHD.Enabled = true;
            btnLuuHD.Enabled = true;
            btnInHD.Enabled = true;
            cboMaHD.SelectedIndex = -1;
        }

        //Kiểm soát chỉ cho nhập số nguyên
        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else e.Handled = true;
        }

        private void cboMaHD_DropDown(object sender, EventArgs e)
        {
            Functions.FillComBoBox("SELECT sMaHDBan FROM tblHoaDonBan", cboMaHD, "sMaHDBan", "sMaHDBan");
            cboMaHD.SelectedIndex = -1;
        }
    }
}
