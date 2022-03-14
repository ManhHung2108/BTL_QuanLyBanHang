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
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace BTL_QuanLyBanHang
{
    public partial class frmBaoCaoHangTon : Form
    {
        public frmBaoCaoHangTon()
        {
            InitializeComponent();
        }

        private void frmBaoCaoHangTon_Load(object sender, EventArgs e)
        {
            ReportDocument report = new ReportDocument();
            report.Load(@"E:\Học C#\BTL_QuanLyBanHang\BTL_QuanLyBanHang\ReportHangTon.rpt");
            crvHangTon.ReportSource = report;
            crvHangTon.Refresh();
            //LoadReport();
        }
        private void LoadReport()
        {
            string sql = "Select tblHang.sMaHang, tblHang.sTenHang, tblHang.iSoLuong, tblHang.fDonGiaBan, tblChiTietHoaDonBan.fThanhTien from tblHang, tblChiTietHoaDonBan where tblHang.sMaHang = tblChiTietHoaDonBan.sMaHang";
            string constr = ConfigurationManager.ConnectionStrings["btl_qlbh"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                SqlDataAdapter ad = new SqlDataAdapter(sql, constr);
                DataTable tb = new DataTable("BangHangTon");
                ad.Fill(tb);
                //Gán nguồn dữ liệu
                ReportHangTon rptHangTon = new ReportHangTon();
                rptHangTon.SetDataSource(tb);
                //Hiển thị báo cáo
                crvHangTon.ReportSource = rptHangTon;
                crvHangTon.Refresh();
            }
        }
        private void btnLoc_Click(object sender, EventArgs e)
        {
            
            string value = txtMaHang.Text;
            ReportDocument report = new ReportDocument();
            report.Load(@"E:\Học C#\BTL_QuanLyBanHang\BTL_QuanLyBanHang\ReportHangTon.rpt");
            ParameterFieldDefinition rpd = report.DataDefinition.ParameterFields["sMaHang"];
            report.RecordSelectionFormula = "{tblHang.sMaHang} = {?sMaHang}";
            ParameterValues pv = new ParameterValues();
            ParameterDiscreteValue pdv = new ParameterDiscreteValue();
            pdv.Value = value;
            pv.Add(pdv);
            rpd.CurrentValues.Clear();
            rpd.ApplyCurrentValues(pv);
            crvHangTon.ReportSource = report;
            crvHangTon.Refresh();
        }

        private void btnLocTen_Click(object sender, EventArgs e)
        {
            string value = txtTenHang.Text;
            ReportDocument report = new ReportDocument();
            report.Load(@"E:\Học C#\BTL_QuanLyBanHang\BTL_QuanLyBanHang\ReportHangTon.rpt");
            ParameterFieldDefinition rpd = report.DataDefinition.ParameterFields["TenHang"];
            report.RecordSelectionFormula = "{tblHang.sTenHang} = {?TenHang}";
            ParameterValues pv = new ParameterValues();
            ParameterDiscreteValue pdv = new ParameterDiscreteValue();
            pdv.Value = value;
            pv.Add(pdv);
            rpd.CurrentValues.Clear();
            rpd.ApplyCurrentValues(pv);
            crvHangTon.ReportSource = report;
            crvHangTon.Refresh();

            //ParameterFieldDefinition rpd1 = report.DataDefinition.ParameterFields["Nguoilap"];
            //report.RecordSelectionFormula = "{?NguoiLap}";
            //ParameterDiscreteValue pdv1 = new ParameterDiscreteValue();
            //pdv1.Value = txtTenHang.Text;
            //ParameterValues pv1 = new ParameterValues();
            //pv1.Add(pdv);
            //rpd1.CurrentValues.Clear();
            //rpd1.ApplyCurrentValues(pv1);
            
        }
    }
}
