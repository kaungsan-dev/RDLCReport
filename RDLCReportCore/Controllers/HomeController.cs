using AspNetCore.Reporting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RDLCReportCore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RDLCReportCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Report report = new Report();

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Report()
        {
            var dt = new DataTable();
            dt = report.GetReportList();

            string mimeType = "";
            int extension = 1;
            var path = $"{_webHostEnvironment.WebRootPath}\\Reports\\ReportExpense.rdlc";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("prm1", "RDCL Report");
            parameters.Add("prm2", DateTime.Now.ToString("dd-MM-yyyy"));
            parameters.Add("prm3", "Expense Report");

            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("dsExpense", dt);

            var res = localReport.Execute(RenderType.Pdf, extension, parameters, mimeType);
            return File(res.MainStream, "application/pdf");
        }
    }
}
