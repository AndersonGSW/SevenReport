using DevExpress.Compatibility.System.Web;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraReports.Web.ReportDesigner;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace SevenReport.Controllers
{
    [Route("api/[controller]")]
    public class ReportDesignerController : Controller
    {
      
        [HttpPost("[action]")]
        public object GetReportDesignerModel([FromForm] string reportUrl)
        {
            Dictionary<string, object> dataSources = new Dictionary<string, object>();
            SqlDataSource ds = new SqlDataSource("SevenR");

            // Create a SQL query to access the Products data table.
            SelectQuery query = SelectQueryFluentBuilder.AddTable("GN_EMPRE").SelectAllColumnsFromTable().Build("GN_EMPRE");
            ds.Queries.Add(query);
            ds.RebuildResultSchema();
            dataSources.Add("Northwind", ds);

            string modelJsonScript = new ReportDesignerClientSideModelGenerator(HttpContext.RequestServices).GetJsonModelScript(reportUrl, dataSources, "/DXXRD", "/DXXRDV", "/DXXQB");
            return new JavaScriptSerializer().Deserialize<object>(modelJsonScript);
        }
    }
}
