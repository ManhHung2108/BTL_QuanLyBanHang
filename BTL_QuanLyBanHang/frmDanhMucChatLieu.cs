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
    public partial class frmDanhMucChatLieu : Form
    {
        public DataTable tblCL;
        public frmDanhMucChatLieu()
        {
            InitializeComponent();
        }

        private void frmDanhMucChatLieu_Load(object sender, EventArgs e)
        {
            txtMaChatLieu.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;

            LoadDataGridView();

        }

        private void LoadDataGridView()
        {
            string sql;
            sql = "select * from tblChatLieu";
            tblCL = Functions.GetDataTable(sql); //Đọc dữ liệu từ bảng
            dgrChatLieu.DataSource = tblCL;      //Gán lên DataGridView

            dgrChatLieu.Columns[0].HeaderText = "Mã chất liệu";
            dgrChatLieu.Columns[1].HeaderText = "Tên chất liệu";
            dgrChatLieu.Columns[0].Width = 100;
            dgrChatLieu.Columns[1].Width = 300;

            dgrChatLieu.AllowUserToAddRows = false; //Không cho thêm dữ liệu trực tiếp
            dgrChatLieu.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa trực tiếp
        }

        private void dgrChatLieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaChatLieu.Focus();
                return;
            }
            if (tblCL.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return; 
            }
            else
            {
                txtMaChatLieu.Text = dgrChatLieu.CurrentRow.Cells["sMaChatLieu"].Value.ToString();
                txtTenChatLieu.Text = dgrChatLieu.CurrentRow.Cells["sTenChatLieu"].Value.ToString();
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnBoQua.Enabled = true;
            }
        }

        private void ResetValue()
        {
            txtMaChatLieu.Text = "";
            txtTenChatLieu.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;

            ResetValue();                 //Xóa trắng
            txtMaChatLieu.Enabled = true; //Cho phép nhập mới
            txtMaChatLieu.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaChatLieu.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã chất liệu", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                txtMaChatLieu.Focus();
                return;
            }
            if (txtTenChatLieu.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên chất liệu", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                txtTenChatLieu.Focus();
                return;
            }

            sql = "select * from tblChatLieu where sMaChatLieu= N'" + txtMaChatLieu.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã chất liệu đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string constr = ConfigurationManager.ConnectionStrings["btl_qlbh"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_tblChatLieu_Insert";
                    cmd.Parameters.AddWithValue("@sMaChatLieu", txtMaChatLieu.Text);
                    cmd.Parameters.AddWithValue("@sTenChatLieu", txtTenChatLieu.Text);
                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    cnn.Close();
                    LoadDataGridView();
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (tblCL.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(txtMaChatLieu.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtTenChatLieu.Text.Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập tên chất liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string constr = ConfigurationManager.ConnectionStrings["btl_qlbh"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_tblChatLieu_Update";
                    cmd.Parameters.AddWithValue("@sMaChatLieu", txtMaChatLieu.Text);
                    cmd.Parameters.AddWithValue("@sTenChatLieu", txtTenChatLieu.Text);
                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    cnn.Close();
                    LoadDataGridView();
                    ResetValue();

                    btnBoQua.Enabled = false;
                }
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValue();
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            txtMaChatLieu.Enabled = false;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            DialogResult ret = MessageBox.Show("Bạn có muốn thoát danh mục chất liệu", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ret == DialogResult.Yes)
            {
                this.Close();
            }
        }

        //Dùng enter thay phím tab
        private void txtMaChatLieu_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult ret = MessageBox.Show("Bạn có thật sự muốn xóa"+txtTenChatLieu.Text+"", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ret == DialogResult.Yes)
            {
                string constr = ConfigurationManager.ConnectionStrings["btl_qlbh"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    using(SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.Connection = cnn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "sp_tblChatLieu_Delete";
                        cmd.Parameters.AddWithValue("@sMaChatLieu", txtMaChatLieu.Text);
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                        LoadDataGridView();
                    }
                }
            }
            ResetValue();
        }
    }
}
