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
using BTL_QuanLyBanHang.Class;        //Sử dụng class Functions.cs
using System.Configuration;

namespace BTL_QuanLyBanHang
{
    public partial class frmDanhMucNhanVien : Form
    {
        public DataTable tbNV;
        public frmDanhMucNhanVien()
        {
            InitializeComponent();
        }

        private void frmDanhMucNhanVien_Load(object sender, EventArgs e)
        {
            txtMaNV.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            LoadDataGridViewNV();
        }

        public void LoadDataGridViewNV()
        {
            string sql;
            sql = "select * from tblNhanVien";
            tbNV = Functions.GetDataTable(sql);
            dgrNhanVien.DataSource = tbNV;

            dgrNhanVien.Columns[0].HeaderText = "Mã nhân viên";
            dgrNhanVien.Columns[1].HeaderText = "Tên nhân viên";
            dgrNhanVien.Columns[2].HeaderText = "Giới tính";
            dgrNhanVien.Columns[3].HeaderText = "Địa chỉ";
            dgrNhanVien.Columns[4].HeaderText = "Điện thoại";
            dgrNhanVien.Columns[5].HeaderText = "Ngày sinh";

            dgrNhanVien.Columns[1].Width = 150;
            dgrNhanVien.Columns[3].Width = 150;

            dgrNhanVien.AllowUserToAddRows = false;
            dgrNhanVien.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dgrNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chết độ thêm mới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenNV.Focus();
                return;
            }

            if (tbNV.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            txtMaNV.Text= dgrNhanVien.CurrentRow.Cells["sMaNhanVien"].Value.ToString();
            txtTenNV.Text = dgrNhanVien.CurrentRow.Cells["sTenNhanVien"].Value.ToString();
            txtGioiTinhNV.Text = dgrNhanVien.CurrentRow.Cells["sGioiTinh"].Value.ToString();
            txtDiaChiNV.Text = dgrNhanVien.CurrentRow.Cells["sDiaChi"].Value.ToString();
            mtbDienThoaiNV.Text = dgrNhanVien.CurrentRow.Cells["sDienThoai"].Value.ToString();
            dtpNgaySinh.Value = (DateTime)dgrNhanVien.CurrentRow.Cells["dNgaysinh"].Value;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValue();
            txtMaNV.Enabled = true;
            txtMaNV.Focus();
        }

        private void ResetValue()
        {
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            txtDiaChiNV.Text = "";
            txtGioiTinhNV.Text = "";
            dtpNgaySinh.Value = DateTime.Now;
            mtbDienThoaiNV.Text = "";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaNV.Text.Trim() == "")
            {
                MessageBox.Show("Bạn phải nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNV.Focus();
                return;
            }

            if (txtMaNV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenNV.Focus();
                return;
            }

            if (txtDiaChiNV.Text.Trim() == "")
            {
                MessageBox.Show("Bạn phải nhập địa chỉ nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiaChiNV.Focus();
                return;
            }

            if (mtbDienThoaiNV.Text.Trim() == "(   )     -")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtbDienThoaiNV.Focus();
                return;
            }

            string sql1 = "select * from tblNhanVien where sDienThoai = N'" + mtbDienThoaiNV.Text + "'";
            if (Functions.CheckKey(sql1))
            {
                MessageBox.Show("Số điện thoại nhân viên đã tồn tại, vui lòng nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtbDienThoaiNV.Focus();
                mtbDienThoaiNV.Text = "";
                return;
            }

            sql = "select * from tblNhanVien where sMaNhanVien = N'" + txtMaNV.Text + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã nhân viên đã tồn tại, vui lòng nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNV.Focus();
                txtMaNV.Text = "";
                return;
            }

            string constr = ConfigurationManager.ConnectionStrings["btl_qlbh"].ConnectionString;
            using(SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_tblNhanVien_Insert";
                    cmd.Parameters.AddWithValue("@sMaNhanVien", txtMaNV.Text);
                    cmd.Parameters.AddWithValue("@sTenNhanVien", txtTenNV.Text);
                    cmd.Parameters.AddWithValue("@sGioiTinh", txtGioiTinhNV.Text);
                    cmd.Parameters.AddWithValue("@sDiaChi", txtDiaChiNV.Text);
                    cmd.Parameters.AddWithValue("@sDienThoai", mtbDienThoaiNV.Text);
                    cmd.Parameters.AddWithValue("@dNgaySinh", dtpNgaySinh.Value);

                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    cnn.Close();

                    LoadDataGridViewNV();
                    ResetValue();

                    btnBoQua.Enabled = false;
                    btnThem.Enabled = true;
                    btnSua.Enabled = true;
                    btnXoa.Enabled = true;
                }
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValue();
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMaNV.Enabled = false;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            DialogResult ret = MessageBox.Show("Bạn có muốn thoát danh mục nhân viên!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ret == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (tbNV.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if(txtMaNV.Text.Trim() == "")
            {
                MessageBox.Show("Yêu cầu chọn bản ghi cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txtTenNV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNV.Focus();
                return;
            }
            if (txtDiaChiNV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChiNV.Focus();
                return;
            }

            if (mtbDienThoaiNV.Text == "(   )     -")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbDienThoaiNV.Focus();
                return;
            }

            if(txtGioiTinhNV.Text.Trim() != "Nam" && txtGioiTinhNV.Text.Trim() != "Nữ")
            {
                MessageBox.Show("Chỉ được nhập giới tính Nam hoặc Nữ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGioiTinhNV.Focus();
                return;
            }

            string constr = ConfigurationManager.ConnectionStrings["btl_qlbh"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_tblNhanVien_Update";
                    cmd.Parameters.AddWithValue("@sMaNhanVien", txtMaNV.Text);
                    cmd.Parameters.AddWithValue("@sTenNhanVien", txtTenNV.Text);
                    cmd.Parameters.AddWithValue("@sGioiTinh", txtGioiTinhNV.Text);
                    cmd.Parameters.AddWithValue("@sDiaChi", txtDiaChiNV.Text);
                    cmd.Parameters.AddWithValue("@sDienThoai", mtbDienThoaiNV.Text);
                    cmd.Parameters.AddWithValue("@dNgaySinh", dtpNgaySinh.Value);

                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    cnn.Close();

                    LoadDataGridViewNV();
                    ResetValue();

                    btnBoQua.Enabled = false;
                    btnThem.Enabled = true;
                    btnSua.Enabled = true;
                    btnXoa.Enabled = true;
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (tbNV.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txtMaNV.Text.Trim() == "")
            {
                MessageBox.Show("Yêu cầu chọn bản ghi cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult ret = MessageBox.Show("Bạn có muốn xóa "+txtTenNV.Text+"!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(ret == DialogResult.Yes)
            {
                string constr = ConfigurationManager.ConnectionStrings["btl_qlbh"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.Connection = cnn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "sp_tblNhanVien_Delete";
                        cmd.Parameters.AddWithValue("@sMaNhanVien", txtMaNV.Text);

                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        cnn.Close();

                        LoadDataGridViewNV();
                        ResetValue();

                        btnBoQua.Enabled = false;
                        btnThem.Enabled = true;
                        btnSua.Enabled = true;
                        btnXoa.Enabled = true;
                    }
                }
            }
        }
    }
}
