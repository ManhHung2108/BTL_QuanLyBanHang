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
using System.Data;
using System.Configuration;

namespace BTL_QuanLyBanHang
{
    public partial class frmDanhMucKhachHang : Form
    {
        DataTable tblKH;
        public frmDanhMucKhachHang()
        {
            InitializeComponent();
        }

        private void frmDanhMucKhachHang_Load(object sender, EventArgs e)
        {
            txtMaKh.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            LoadDataGridViewKH();
        }

        private void LoadDataGridViewKH()
        {
            string sql;
            sql = "select * from tblKhach";
            tblKH = Functions.GetDataTable(sql); //Lấy dữ liệu
            dgvKhachHang.DataSource = tblKH;     //Đưa vào datagridView

            dgvKhachHang.Columns[0].HeaderText = "Mã khách";
            dgvKhachHang.Columns[1].HeaderText = "Tên khách";
            dgvKhachHang.Columns[2].HeaderText = "Địa chỉ";
            dgvKhachHang.Columns[3].HeaderText = "Điện thoại";
            dgvKhachHang.Columns[0].Width = 100;
            dgvKhachHang.Columns[1].Width = 150;
            dgvKhachHang.Columns[2].Width = 150;
            dgvKhachHang.Columns[3].Width = 150;
            dgvKhachHang.AllowUserToAddRows = false;
            dgvKhachHang.EditMode = DataGridViewEditMode.EditProgrammatically;

        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(btnThem.Enabled = false)
            {
                MessageBox.Show("Đang ở trạng thái thêm mới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKh.Focus();
                return;
            }

            if (tblKH.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            txtMaKh.Text = dgvKhachHang.CurrentRow.Cells["sMaKhach"].Value.ToString();
            txtTenKh.Text = dgvKhachHang.CurrentRow.Cells["sTenKhach"].Value.ToString();
            txtDiaChiKh.Text = dgvKhachHang.CurrentRow.Cells["sDiaChi"].Value.ToString();
            mtbDienThoaiKh.Text = dgvKhachHang.CurrentRow.Cells["sDienThoai"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnThem.Enabled = false;
            ResetValues();
            txtMaKh.Enabled = true;
            txtMaKh.Focus();
        }

        private void ResetValues()
        {
            txtMaKh.Text = "";
            txtTenKh.Text = "";
            txtDiaChiKh.Text = "";
            mtbDienThoaiKh.Text = "";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if(txtMaKh.Text.Trim() == "")
            {
                MessageBox.Show("Yêu cầu nhập mã khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKh.Focus();
                return;
            }

            if (txtTenKh.Text.Trim() == "")
            {
                MessageBox.Show("Yêu cầu nhập tên khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenKh.Focus();
                return;
            }

            if (txtDiaChiKh.Text.Trim() == "")
            {
                MessageBox.Show("Yêu cầu nhập địa chỉ khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiaChiKh.Focus();
                return;
            }

            if (mtbDienThoaiKh.Text == "(  )    -")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtbDienThoaiKh.Focus();
                return;
            }

            //Check trùng mã
            sql = "Select * from tblKhach where sMaKhach = N'" + txtMaKh.Text + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã khách hàng đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKh.Focus();
                return;
            }

            //Thêm dữ liệu
            string constr = ConfigurationManager.ConnectionStrings["btl_qlbh"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_tblKhach_Insert";
                    cmd.Parameters.AddWithValue("@sMaKhach", txtMaKh.Text);
                    cmd.Parameters.AddWithValue("@sTenKhach", txtTenKh.Text);
                    cmd.Parameters.AddWithValue("@sDiaChi", txtDiaChiKh.Text);
                    cmd.Parameters.AddWithValue("@sDienThoai", mtbDienThoaiKh.Text.Trim());

                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    cnn.Close();
                    LoadDataGridViewKH();
                    ResetValues();

                    btnThem.Enabled = true;
                    btnSua.Enabled = true;
                    btnXoa.Enabled = true;
                    btnBoQua.Enabled = false;
                    btnLuu.Enabled = false;
                }
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            txtMaKh.Enabled = false;

            btnSua.Enabled = true;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            DialogResult ret = MessageBox.Show("Bạn có muốn thoát danh mục khách hàng!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(ret == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(tblKH.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông  báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if(txtMaKh.Text == "")
            {
                MessageBox.Show("Yêu cầu chọn bản ghi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string constr = ConfigurationManager.ConnectionStrings["btl_qlbh"].ConnectionString;
            using(SqlConnection  cnn  = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    DialogResult ret = MessageBox.Show("Bạn có muốn xóa "+txtTenKh.Text+" không!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if(ret == DialogResult.Yes)
                    {
                        cmd.Connection = cnn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "sp_tblKhach_Delete";
                        cmd.Parameters.AddWithValue("@sMaKhach", txtMaKh.Text);
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                        ResetValues();
                        LoadDataGridViewKH();
                        btnThem.Enabled = true;
                        btnSua.Enabled = true;
                        btnXoa.Enabled = true;
                        btnBoQua.Enabled = false;
                        txtMaKh.Enabled = false;
                    }
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if(tblKH.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(txtMaKh.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi cần sửa!", "Thông báo",MessageBoxButtons.OK ,MessageBoxIcon.Information);
                return;
            }

            if (txtTenKh.Text.Trim() == "")
            {
                MessageBox.Show("Yêu cầu nhập tên khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenKh.Focus();
                return;
            }

            if (txtDiaChiKh.Text.Trim() == "")
            {
                MessageBox.Show("Yêu cầu nhập địa chỉ khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiaChiKh.Focus();
                return;
            }

            if (mtbDienThoaiKh.Text.Trim() == "(   )     -")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtbDienThoaiKh.Focus();
                return;
            }

            string constr = ConfigurationManager.ConnectionStrings["btl_qlbh"].ConnectionString;
            using(SqlConnection  cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_tblKhach_Update";
                    cmd.Parameters.AddWithValue("@sMaKhach", txtMaKh.Text);
                    cmd.Parameters.AddWithValue("@sTenKhach", txtTenKh.Text);
                    cmd.Parameters.AddWithValue("@sDiaChi", txtDiaChiKh.Text);
                    cmd.Parameters.AddWithValue("@sDienThoai", mtbDienThoaiKh.Text);

                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    cnn.Close();

                    ResetValues();
                    LoadDataGridViewKH();
                    btnThem.Enabled = true;
                    btnSua.Enabled = true;
                    btnXoa.Enabled = true;
                    btnLuu.Enabled = false;
                    btnBoQua.Enabled = false;
                }
            }
        }
    }
}
