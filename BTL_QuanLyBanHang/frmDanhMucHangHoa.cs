using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using BTL_QuanLyBanHang.Class;
using System.Configuration;

namespace BTL_QuanLyBanHang
{
    public partial class frmDanhMucHangHoa : Form
    {
        DataTable tblHang;
        public frmDanhMucHangHoa()
        {
            InitializeComponent();
        }

        private void frmDanhMucHangHoa_Load(object sender, EventArgs e)
        {
            txtMaHang.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            LoadDataGridView();
            ResetValues();

            //Đổ dữ liệu Combobox
            //(hiển thị theo tên chất liệu và khi chọn thì sẽ lấy được mã tương ứng để đổ vào tblHang)
            string sql = "Select * from tblChatLieu";
            Functions.FillComBoBox(sql, cboMaChatLieu, "sMaChatLieu", "sTenChatLieu");
            cboMaChatLieu.SelectedIndex = -1;
        }

        private void LoadDataGridView()
        {
            string sql = "select * from tblHang";
            tblHang = Functions.GetDataTable(sql);
            dgrHangHoa.DataSource = tblHang;

            dgrHangHoa.Columns[0].HeaderText = "Mã hàng";
            dgrHangHoa.Columns[1].HeaderText = "Tên hàng";
            dgrHangHoa.Columns[2].HeaderText = "Chất liệu";
            dgrHangHoa.Columns[3].HeaderText = "Số lượng";
            dgrHangHoa.Columns[4].HeaderText = "Đơn giá nhập";
            dgrHangHoa.Columns[5].HeaderText = "Đơn giá bán";
            dgrHangHoa.Columns[6].HeaderText = "Ảnh";
            dgrHangHoa.Columns[7].HeaderText = "Ghi chú";
            dgrHangHoa.Columns[0].Width = 80;
            dgrHangHoa.Columns[1].Width = 140;
            dgrHangHoa.Columns[2].Width = 80;
            dgrHangHoa.Columns[3].Width = 80;
            dgrHangHoa.Columns[4].Width = 100;
            dgrHangHoa.Columns[5].Width = 100;
            dgrHangHoa.Columns[6].Width = 200;
            dgrHangHoa.Columns[7].Width = 300;

            dgrHangHoa.AllowUserToAddRows = false;
            dgrHangHoa.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void ResetValues()
        {
            txtMaHang.Text = "";
            txtTenHang.Text = "";
            cboMaChatLieu.Text = "";
            txtSoLuong.Text = "0";
            txtDonGiaNhap.Text = "0";
            txtDonGiaBan.Text = "0";
            txtSoLuong.Enabled = true;
            txtDonGiaNhap.Enabled = false;
            txtDonGiaBan.Enabled = false;
            txtAnh.Text = "";
            picAnh.Image = null;
            txtGhiChu.Text = "";
        }

        private void dgrHangHoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string sql;
            string MaChatLieu;
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaHang.Focus();
                return;
            }

            if (tblHang.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu bản ghi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            txtMaHang.Text = dgrHangHoa.CurrentRow.Cells["sMaHang"].Value.ToString();
            txtTenHang.Text = dgrHangHoa.CurrentRow.Cells["sTenHang"].Value.ToString();

            //Lấy ra mã
            MaChatLieu = dgrHangHoa.CurrentRow.Cells["sMaChatLieu"].Value.ToString();
            //Lấy đúng tên
            sql = "SELECT sTenChatLieu FROM tblChatLieu WHERE sMaChatLieu=N'" + MaChatLieu + "'";
            //nhưng nạp lên cbo là tên
            cboMaChatLieu.Text = Functions.GetFieldValues(sql);    //trả về sMaChatLieu và  hiện sTenChatLieu
            txtSoLuong.Text = dgrHangHoa.CurrentRow.Cells["iSoLuong"].Value.ToString();
            txtDonGiaNhap.Text = dgrHangHoa.CurrentRow.Cells["fDonGiaNhap"].Value.ToString();
            txtDonGiaBan.Text = dgrHangHoa.CurrentRow.Cells["fDonGiaBan"].Value.ToString();

            sql = "SELECT sAnh FROM tblHang WHERE sMaHang=N'" + txtMaHang.Text + "'";
            txtAnh.Text = Functions.GetFieldValues(sql);          //trả sMaHang và hiện sAnh
            picAnh.Image = Image.FromFile(txtAnh.Text);           //Lấy ảnh theo đường dẫn
            sql = "SELECT sGhichu FROM tblHang WHERE sMaHang = N'" + txtMaHang.Text + "'";
            txtGhiChu.Text = Functions.GetFieldValues(sql);
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMaHang.Enabled = true;
            txtMaHang.Focus();
            txtSoLuong.Enabled = true;
            txtDonGiaNhap.Enabled = true;
            txtDonGiaBan.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaHang.Focus();
                return;
            }
            if (txtTenHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenHang.Focus();
                return;
            }
            if (cboMaChatLieu.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập chất liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaChatLieu.Focus();
                return;
            }
            if (txtAnh.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn ảnh minh hoạ cho hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnMo.Focus();
                return;
            }
            sql = "SELECT sMaHang FROM tblHang WHERE sMaHang=N'" + txtMaHang.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã hàng này đã tồn tại, bạn phải chọn mã hàng khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaHang.Focus();
                return;
            }

            string constr = ConfigurationManager.ConnectionStrings["btl_qlbh"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_tblHang_Insert";
                    cmd.Parameters.AddWithValue("@sMaHang", txtMaHang.Text);
                    cmd.Parameters.AddWithValue("@sTenHang", txtTenHang.Text);
                    cmd.Parameters.AddWithValue("@sMaChatLieu", cboMaChatLieu.SelectedValue.ToString()); //trường giá trị là sMaChatLieu
                    cmd.Parameters.AddWithValue("@iSoLuong", int.Parse(txtSoLuong.Text));
                    cmd.Parameters.AddWithValue("@fDonGiaNhap", float.Parse(txtDonGiaNhap.Text));
                    cmd.Parameters.AddWithValue("@fDonGiaBan", float.Parse(txtDonGiaBan.Text));
                    cmd.Parameters.AddWithValue("@sAnh", txtAnh.Text);
                    cmd.Parameters.AddWithValue("@sGhiChu", txtGhiChu.Text);

                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    cnn.Close();

                    LoadDataGridView();
                    ResetValues();

                    btnBoQua.Enabled = false;
                    btnThem.Enabled = true;
                    btnSua.Enabled = true;
                    btnXoa.Enabled = true;
                }
            }
        }

        private void btnMo_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "Bitmap(*.bmp)|*.bmp|JPEG(*.jpg)|*.jpg|GIF(*.gif)|*.gif|All files(*.*)|*.*";
            dlgOpen.FilterIndex = 2;
            dlgOpen.Title = "Chọn ảnh minh hoạ cho sản phẩm";
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                picAnh.Image = Image.FromFile(dlgOpen.FileName);
                txtAnh.Text = dlgOpen.FileName;
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnThem.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaHang.Enabled = false;
        }

        private void btnHienThids_Click(object sender, EventArgs e)
        {
            string sql;
            sql = "SELECT sMaHang,sTenHang,sMaChatLieu,iSoLuong,fDonGiaNhap,fDonGiaBan,sAnh,sGhichu FROM tblHang";
            tblHang = Functions.GetDataTable(sql);
            dgrHangHoa.DataSource = tblHang;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            DialogResult ret = MessageBox.Show("Bạn có muốn thoát danh mục hàng hóa!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ret == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void frmDanhMucHangHoa_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult ret = MessageBox.Show("Bạn có muốn thoát danh mục hàng hóa!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ret == DialogResult.Yes)
            {
                e.Cancel=false;
            }
            else
            {
                e.Cancel=true;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (tblHang.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaHang.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaHang.Focus();
                return;
            }
            if (txtTenHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenHang.Focus();
                return;
            }
            if (cboMaChatLieu.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập chất liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaChatLieu.Focus();
                return;
            }
            if (txtAnh.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải ảnh minh hoạ cho hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAnh.Focus();
                return;
            }

            string constr = ConfigurationManager.ConnectionStrings["btl_qlbh"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_tblHang_Update";
                    cmd.Parameters.AddWithValue("@sMaHang", txtMaHang.Text);
                    cmd.Parameters.AddWithValue("@sTenHang", txtTenHang.Text);
                    cmd.Parameters.AddWithValue("@sMaChatLieu", cboMaChatLieu.SelectedValue.ToString()); //trường giá trị là sMaChatLieu
                    cmd.Parameters.AddWithValue("@iSoLuong", int.Parse(txtSoLuong.Text));
                    cmd.Parameters.AddWithValue("@fDonGiaNhap", float.Parse(txtDonGiaNhap.Text));
                    cmd.Parameters.AddWithValue("@fDonGiaBan", float.Parse(txtDonGiaBan.Text));
                    cmd.Parameters.AddWithValue("@sAnh", txtAnh.Text);
                    cmd.Parameters.AddWithValue("@sGhiChu", txtGhiChu.Text);

                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    cnn.Close();

                    LoadDataGridView();
                    ResetValues();

                    btnBoQua.Enabled = false;
                    btnThem.Enabled = true;
                    btnSua.Enabled = true;
                    btnXoa.Enabled = true;
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (tblHang.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txtMaHang.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaHang.Focus();
                return;
            }

            DialogResult ret = MessageBox.Show("Bạn có muốn xóa " + txtTenHang.Text + "", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ret == DialogResult.Yes)
            {
                string constr = ConfigurationManager.ConnectionStrings["btl_qlbh"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.Connection = cnn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "sp_tblHang_Delete";
                        cmd.Parameters.AddWithValue("@sMaHang", txtMaHang.Text);
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                        LoadDataGridView();
                        ResetValues();
                    }
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql;
            if ((txtMaHang.Text == "") && (txtTenHang.Text == "") && (cboMaChatLieu.Text == ""))
            {
                MessageBox.Show("Bạn hãy nhập điều kiện tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * from tblHang WHERE 1=1";
            if (txtMaHang.Text != "")
                sql += " AND sMaHang LIKE N'%" + txtMaHang.Text + "%'";
            if (txtTenHang.Text != "")
                sql += " AND sTenHang LIKE N'%" + txtTenHang.Text + "%'";
            if (cboMaChatLieu.Text != "")
                sql += " AND sMaChatLieu LIKE N'%" + cboMaChatLieu.SelectedValue + "%'";
            tblHang = Functions.GetDataTable(sql);
            if (tblHang.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Có " + tblHang.Rows.Count + "  bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgrHangHoa.DataSource = tblHang;
            }
            ResetValues();
        }
    }
}
