//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SevenReport.PredefinedReports {
    
    public partial class SCnBlome : DevExpress.XtraReports.UI.XtraReport {
        private void InitializeComponent() {
            DevExpress.XtraReports.ReportInitializer reportInitializer = new DevExpress.XtraReports.ReportInitializer(this, "SevenReport.PredefinedReports.SCnBlome.repx");

            // Controls
            this.DetailArea1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.DetailBand>("DetailArea1");
            this.GroupHeaderArea1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.GroupHeaderBand>("GroupHeaderArea1");
            this.GroupFooterArea1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.GroupFooterBand>("GroupFooterArea1");
            this.ReportHeaderArea1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.ReportHeaderBand>("ReportHeaderArea1");
            this.PageHeaderArea1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.PageHeaderBand>("PageHeaderArea1");
            this.ReportFooterArea1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.ReportFooterBand>("ReportFooterArea1");
            this.PageFooterArea1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.PageFooterBand>("PageFooterArea1");
            this.TopMargin = reportInitializer.GetControl<DevExpress.XtraReports.UI.TopMarginBand>("TopMargin");
            this.BottomMargin = reportInitializer.GetControl<DevExpress.XtraReports.UI.BottomMarginBand>("BottomMargin");
            this.BLOANOP1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("BLOANOP1");
            this.BLOMESP1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("BLOMESP1");
            this.Estado_1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("Estado_1");
            this.PageHeaderSection1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.SubBand>("PageHeaderSection1");
            this.PageHeaderSection2 = reportInitializer.GetControl<DevExpress.XtraReports.UI.SubBand>("PageHeaderSection2");
            this.Text1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("Text1");
            this.Text2 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("Text2");
            this.PEMPNOMB1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("PEMPNOMB1");
            this.PEMPCODI1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("PEMPCODI1");
            this.Line1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLine>("Line1");
            this.Line2 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLine>("Line2");
            this.Text3 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("Text3");
            this.Text4 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("Text4");
            this.Text5 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("Text5");
            this.Text6 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("Text6");
            this.Line3 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLine>("Line3");
            this.Text7 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("Text7");
            this.PFORMATOFECHA1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("PFORMATOFECHA1");
            this.Text8 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("Text8");

            // Parameters
            this.P_EMP_CODI = reportInitializer.GetParameter("P_EMP_CODI");
            this.P_EMP_NOMB = reportInitializer.GetParameter("P_EMP_NOMB");
            this.P_USER = reportInitializer.GetParameter("P_USER");
            this.P_FORMATO_FECHA = reportInitializer.GetParameter("P_FORMATO_FECHA");
            this.FORMATO = reportInitializer.GetParameter("FORMATO");

            // Data Sources
            this.SevenDesarrollo = reportInitializer.GetDataSource<DevExpress.DataAccess.Sql.SqlDataSource>("SevenDesarrollo");

            // Calculated Fields
            this.Pagina = reportInitializer.GetCalculatedField("Pagina");
            this.Fecha = reportInitializer.GetCalculatedField("Fecha");
            this.Hora = reportInitializer.GetCalculatedField("Hora");
            this.Estado = reportInitializer.GetCalculatedField("Estado");
            this.codebar39 = reportInitializer.GetCalculatedField("codebar39");
        }
        private DevExpress.XtraReports.UI.DetailBand DetailArea1;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeaderArea1;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooterArea1;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeaderArea1;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeaderArea1;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooterArea1;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooterArea1;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRLabel BLOANOP1;
        private DevExpress.XtraReports.UI.XRLabel BLOMESP1;
        private DevExpress.XtraReports.UI.XRLabel Estado_1;
        private DevExpress.XtraReports.UI.SubBand PageHeaderSection1;
        private DevExpress.XtraReports.UI.SubBand PageHeaderSection2;
        private DevExpress.XtraReports.UI.XRLabel Text1;
        private DevExpress.XtraReports.UI.XRLabel Text2;
        private DevExpress.XtraReports.UI.XRLabel PEMPNOMB1;
        private DevExpress.XtraReports.UI.XRLabel PEMPCODI1;
        private DevExpress.XtraReports.UI.XRLine Line1;
        private DevExpress.XtraReports.UI.XRLine Line2;
        private DevExpress.XtraReports.UI.XRLabel Text3;
        private DevExpress.XtraReports.UI.XRLabel Text4;
        private DevExpress.XtraReports.UI.XRLabel Text5;
        private DevExpress.XtraReports.UI.XRLabel Text6;
        private DevExpress.XtraReports.UI.XRLine Line3;
        private DevExpress.XtraReports.UI.XRLabel Text7;
        private DevExpress.XtraReports.UI.XRLabel PFORMATOFECHA1;
        private DevExpress.XtraReports.UI.XRLabel Text8;
        private DevExpress.DataAccess.Sql.SqlDataSource SevenDesarrollo;
        private DevExpress.XtraReports.UI.CalculatedField Pagina;
        private DevExpress.XtraReports.UI.CalculatedField Fecha;
        private DevExpress.XtraReports.UI.CalculatedField Hora;
        private DevExpress.XtraReports.UI.CalculatedField Estado;
        private DevExpress.XtraReports.UI.CalculatedField codebar39;
        private DevExpress.XtraReports.Parameters.Parameter P_EMP_CODI;
        private DevExpress.XtraReports.Parameters.Parameter P_EMP_NOMB;
        private DevExpress.XtraReports.Parameters.Parameter P_USER;
        private DevExpress.XtraReports.Parameters.Parameter P_FORMATO_FECHA;
        private DevExpress.XtraReports.Parameters.Parameter FORMATO;
    }
}
