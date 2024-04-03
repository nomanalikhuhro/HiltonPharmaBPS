using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using PASSForm_BPS.Models;
using PASSForm_BPS.ViewModel;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PASSForm_BPS.Controllers
{
    public class PAPViewController : Controller
    {
        private readonly PassDbContext _passDbContext;
        private readonly TestSalesDbContext _testSalesDbContext;
        private readonly string _connectionString;

        public string _sqlconnection;
        private readonly IWebHostEnvironment _webHostEnvironment;

        DataTable dt_HCPDETAILS = new DataTable();
        DataTable dt_HCPDOCS = new DataTable();

        public PAPViewController(PassDbContext passDbContext, TestSalesDbContext testSalesDbContext, IConfiguration configuration1, IWebHostEnvironment webHostEnvironment)
        {
            this._passDbContext = passDbContext;
            this._testSalesDbContext = testSalesDbContext;
            _webHostEnvironment = webHostEnvironment;
            _sqlconnection = configuration1.GetConnectionString("SqlConnection");
            _connectionString = configuration1.GetConnectionString("MySqlServerConnection");
            /* _sqlconnection = "Server=192.168.10.6;Database=DSRvsMREP;User ID=test2;Password=abc123+;Integrated Security=False;Trusted_Connection=False;Encrypt=False;";*/// configuration1.GetConnectionString("SqlConnection"); // configuration1.GetConnectionString["SqlConnection"];
        }
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Submitted()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create(string RequestId, string PAPType)
        {
            if (RequestId == null)
            {
                return PartialView("Create_PartialView");
            }
            else
            {
                var Empid_SessionValue = HttpContext.Session.GetString("EmpIdbps");
                ViewBag.PapType = PAPType;
                var param1 = "44-EQU-2024";

                var distributor = _passDbContext.DisterMappings.FromSqlRaw("call sp_GetDistributerDetails(@Tracking_ID)"
                                                , new MySqlParameter("@Tracking_ID", param1)).ToList();

                var combinedViewModel = new BPSRequestListViewModel
                {

                    disterMappings = distributor,

                };
                return View(combinedViewModel);
             }

    }

        [HttpGet]
        public object GetMacrobrick(string disValue)
        {
            try
            {


                var macrobrickrecords = _passDbContext.DisMacMappings.FromSqlRaw("call sp_GetMacroBrickDetails(@DisCode)"
    , new MySqlParameter("@DisCode", disValue)).ToList();

                return Json(macrobrickrecords);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        [HttpGet]
        public object GetChemist(string brickValue)
        {
            try
            {
                var chemrecords = _passDbContext.MacChemMappings.FromSqlRaw("call sp_ChemistRecords(@MacCode)"
, new MySqlParameter("@MacCode", brickValue)).ToList();

                string html = "<option value=\"Select\">Select</option>";
                foreach (var items in chemrecords)
                {


                    var optionValue = $"{items.ChemistCode} - {items.ChemistName}";


                    html += $"<label><input id=\"{items.ChemistCode}\" type=\"checkbox\" value=\"{optionValue}\">{optionValue}</label>";


                }
                var ChmeistBrichDEtails = new
                {
                    macChemMappings = html,
                    countOfChemist = chemrecords.Count

                };

                return ChmeistBrichDEtails;
            }
            catch (Exception ex)
            {

                DateTime timestampValue = DateTime.Now;

                GlobalClass.LogException(_passDbContext, ex, nameof(GetChemist), "Error message");
                return View("ErrorView");
            }

        }
    }
}
