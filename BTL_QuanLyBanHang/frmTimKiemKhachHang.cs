using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BTL_QuanLyBanHang.Class;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace BTL_QuanLyBanHang
{
    public partial class frmTimKiemKhach : Form
    {
        DataTable tblTimKiemKH;
        public frmTimKiemKhach()
        {
            InitializeComponent();
        }

        private void frmTimKiemKhach_Load(object sender, EventArgs e)
        {
            LoadDataGridViewTimKiemKH();
        }

        private void LoadDataGridViewTimKiemKH()
        {
            string sql;
            sql = "select * from tblKhach";
            tblTimKiemKH = Functions.GetDataTable(sql);

            dgrTimKiemKhach.DataSource = tblTimKiemKH;

            dgrTimKiemKhach.Columns[1].HeaderText = "Tên khách";
            dgrTimKiemKhach.Columns[2].HeaderText = "Địa chỉ";
            dgrTimKiemKhach.Columns[3].HeaderText = "Điện thoại";
            dgrTimKiemKhach.Columns[0].Width = 100;
            dgrTimKiemKhach.Columns[1].Width = 150;
            dgrTimKiemKhach.Columns[2].Width = 150;
            dgrTimKiemKhach.Columns[3].Width = 150;
            dgrTimKiemKhach.AllowUserToAddRows = false;
            dgrTimKiemKhach.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (txtNoiDungTimKiem.Text == "")
            {
                MessageBox.Show("Yêu cầu nhập nội dung cần tìm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNoiDungTimKiem.Focus();
                return;
            }
            if(cboKieuTimKiem.SelectedIndex == 0)
            {
                string constr = ConfigurationManager.ConnectionStrings["btl_qlbh"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("select * from tblKhach where sMaKhach Like '%"+txtNoiDungTimKiem.Text+"%'", cnn))
                    {
                        cmd.CommandType = CommandType.Text;
                        using(SqlDataAdapter ad = new SqlDataAdapter(cmd))
                        {
                            DataTable tb = new DataTable();
                            ad.Fill(tb);

                            dgrTimKiemKhach.DataSource = tb;
                        }
                    }
                }
                return;
            }
            else if(cboKieuTimKiem.SelectedIndex == 1)
            {
                string constr = ConfigurationManager.ConnectionStrings["btl_qlbh"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("select * from tblKhach where sTenKhach Like N'%" + txtNoiDungTimKiem.Text.Trim() + "%'", cnn))
                    {
                        cmd.CommandType = CommandType.Text;
                        using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                        {
                            DataTable tb = new DataTable();
                            ad.Fill(tb);

                            dgrTimKiemKhach.DataSource = tb;
                        }
                    }
                }
                return;
            }
            else if(cboKieuTimKiem.SelectedIndex == 2)
            {
                string constr = ConfigurationManager.ConnectionStrings["btl_qlbh"].ConnectionString;
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("select * from tblKhach where sDiaChi Like N'%" + txtNoiDungTimKiem.Text.Trim() + "%'", cnn))
                    {
                        cmd.CommandType = CommandType.Text;
                        using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                        {
                            DataTable tb = new DataTable();
                            ad.Fill(tb);

                            dgrTimKiemKhach.DataSource = tb;
                        }
                    }
                }
                return;
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            LoadDataGridViewTimKiemKH();
            cboKieuTimKiem.Text = "";
            txtNoiDungTimKiem.Text = "";
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult ret = MessageBox.Show("Bạn có muốn thoát tìm kiếm khách hàng", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(ret == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
