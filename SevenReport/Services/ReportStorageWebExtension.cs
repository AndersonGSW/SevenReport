using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DevExpress.XtraReports.Web.Extensions;
using DevExpress.XtraReports.UI;
using Microsoft.AspNetCore.Hosting;
using SevenReport.PredefinedReports;
using System.Data;
using System.Web;
using DevExpress.DataAccess.Sql;

namespace SevenReport.Services
{
    public class NewSevenReport : DevExpress.XtraReports.Web.Extensions.ReportStorageWebExtension
    {
        readonly string ReportDirectory;
        const string FileExtension = ".repx";
        public NewSevenReport(IWebHostEnvironment env)
        {
            ReportDirectory = Path.Combine(env.ContentRootPath, "D:/DW/converted");
            if (!Directory.Exists(ReportDirectory))
            {
                Directory.CreateDirectory(ReportDirectory);
            }
        }

        private bool IsWithinReportsFolder(string url, string folder)
        {
            var rootDirectory = new DirectoryInfo(folder);
            var fileInfo = new FileInfo(Path.Combine(folder, url));
            return fileInfo.Directory.FullName.ToLower().StartsWith(rootDirectory.FullName.ToLower());
        }

        public override bool CanSetData(string url)
        {
            // Determines whether or not it is possible to store a report by a given URL. 
            // For instance, make the CanSetData method return false for reports that should be read-only in your storage. 
            // This method is called only for valid URLs (i.e., if the IsValidUrl method returned true) before the SetData method is called.

            return true;
        }

        public override bool IsValidUrl(string url)
        {
            // Determines whether or not the URL passed to the current Report Storage is valid. 
            // For instance, implement your own logic to prohibit URLs that contain white spaces or some other special characters. 
            // This method is called before the CanSetData and GetData methods.

            return Path.GetFileName(url) == url;
        }

        public override byte[] GetData(string url)
        {
            string filtros = "";
            url = url.Replace("and", "and");
            url = url.Replace("AND", "and");
            url = url.Replace("And", "and");
            // Returns report layout data stored in a Report Storage using the specified URL. 
            // This method is called only for valid URLs after the IsValidUrl method is called.
            // Uri Request = new Uri(HttpContext.Current.Request.Url, url).Query;
            Uri baseUri = new Uri("http://localhost/");
            Uri Request = new Uri(baseUri, url);

            // Nombre del Reporte
            string reportName = HttpUtility.ParseQueryString(Request.Query).Get("reportName");

            // Lista de Parametros
            string[] parametros = Request.Query.Split("&");

            var listaParam = new List<string>();
            foreach (var para in parametros)
            {
                if (para.Contains("promptex"))
                {
                    listaParam.Add(para.Replace("%20", " "));
                }
            }

            // Filtros

            string filtros1 = HttpUtility.ParseQueryString(Request.Query).Get("sf");
            string[] filtrosA = filtros1.Split("and");

            foreach (var filtro in filtrosA)
            {
                int position = filtro.IndexOf(".");
                if (filtros.Length == 0)
                {
                    filtros = filtro; //filtro.Substring(position + 1);
                }
                else
                {
                    filtros = filtros + " and " + filtro; //filtro.Substring(position + 1);
                }
            }


            try
            {
                if (Directory.EnumerateFiles(ReportDirectory).Select(Path.GetFileNameWithoutExtension).Contains(reportName))
                {
                    string path = Path.Combine(ReportDirectory, reportName + FileExtension);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        XtraReport report = new XtraReport();
                        report.LoadLayout(path);

                        var ds = (((DevExpress.DataAccess.Sql.SqlDataSource)report.DataSource) as SqlDataSource);
                        string sql = (ds.Queries["selectQuery1"] as CustomSqlQuery).Sql;

                        sql = sql.Replace("where", "WHERE");
                        sql = sql.Replace("WHERE", "WHERE");
                        sql = sql.Replace("Where", "WHERE");

                        int posi = sql.IndexOf("WHERE");

                        if (posi != 0)
                            sql = sql.Remove(posi);

                        filtros = " WHERE " + filtros;
                        sql = sql + filtros;

                        (ds.Queries["selectQuery1"] as CustomSqlQuery).Sql = sql;

                        //report.FilterString = filtros;

                        if (report.Parameters.Count != listaParam.Count)
                            throw new Exception("El número de parametros del reporte es diferente al número de parametros enviados, revise la versión del programa y el reporte");

                        for (int i = 0; i < listaParam.Count; i++)
                        {
                            report.Parameters[i].Value = listaParam[i].ToString().Substring(listaParam[i].ToString().IndexOf("=") + 1);
                        }

                        report.SaveLayoutToXml(ms);
                        return ms.ToArray();
                    }

                }
            }
            catch (Exception ex)
            {
                throw new DevExpress.XtraReports.Web.ClientControls.FaultException("Could not get report data.", ex);
            }
            throw new DevExpress.XtraReports.Web.ClientControls.FaultException(string.Format("Could not find report '{0}'.", url));
        }

        public override Dictionary<string, string> GetUrls()
        {
            // Returns a dictionary of the existing report URLs and display names. 
            // This method is called when running the Report Designer, 
            // before the Open Report and Save Report dialogs are shown and after a new report is saved to a storage.

            return Directory.GetFiles(ReportDirectory, "*" + FileExtension)
                                     .Select(Path.GetFileNameWithoutExtension)
                                     .Union(ReportsFactory.Reports.Select(x => x.Key))
                                     .ToDictionary<string, string>(x => x);
        }

        public override void SetData(XtraReport report, string url)
        {
            // Stores the specified report to a Report Storage using the specified URL. 
            // This method is called only after the IsValidUrl and CanSetData methods are called.
            if (!IsWithinReportsFolder(url, ReportDirectory))
                throw new DevExpress.XtraReports.Web.ClientControls.FaultException("Invalid report name.");
            report.SaveLayoutToXml(Path.Combine(ReportDirectory, url + FileExtension));
        }

        public override string SetNewData(XtraReport report, string defaultUrl)
        {
            // Stores the specified report using a new URL. 
            // The IsValidUrl and CanSetData methods are never called before this method. 
            // You can validate and correct the specified URL directly in the SetNewData method implementation 
            // and return the resulting URL used to save a report in your storage.
            SetData(report, defaultUrl);
            return defaultUrl;
        }
    }
}