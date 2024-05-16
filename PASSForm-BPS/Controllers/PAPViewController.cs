using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using MySql.Data.MySqlClient;

using Org.BouncyCastle.Ocsp;
using Org.BouncyCastle.Utilities.IO;
using PASSForm_BPS.Models;
using PASSForm_BPS.ViewModel;
using System;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Globalization;
using System.Text.Json;
using System.Xml.Linq;
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
            return PartialView("Create_PartialView");
        }

        public ActionResult PharmaciesSubmitted()
        {
            try
            {
                var Empid_SessionValue = HttpContext.Session.GetString("EmpIdbps");
                var BPSlist = _passDbContext.BPSPAPPharmaciesListViewModels.FromSqlRaw("call sp_BPSPAPPharmaciesList(@Empid_SessionValue)"
                                                        , new MySqlParameter("@Empid_SessionValue", Empid_SessionValue)).ToList();

                return View(BPSlist);
            }catch(Exception ex)
            {

                DateTime timestampValue = DateTime.Now; // Replace with the desired DateTime value

                GlobalClass.LogException(_passDbContext, ex, nameof(PharmaciesSubmitted), "Error message");
                var feature = new Microsoft.AspNetCore.Diagnostics.ExceptionHandlerFeature
                {
                    Error = ex,

                };
                HttpContext.Features.Set(feature);
                ViewBag.Error = ex;
                return View("ErrorView");
            }
        }

        public ActionResult IVInjectionSubmitted()
        {
            try
            {
                var Empid_SessionValue = HttpContext.Session.GetString("EmpIdbps");
                var BPSlist = _passDbContext.BPSPAPPharmaciesListViewModels.FromSqlRaw("call sp_BPSPAPIVInjectionList(@Empid_SessionValue)"
                                                        , new MySqlParameter("@Empid_SessionValue", Empid_SessionValue)).ToList();
                return View(BPSlist);
            }
            catch (Exception ex)
            {
                DateTime timestampValue = DateTime.Now; // Replace with the desired DateTime value

                GlobalClass.LogException(_passDbContext, ex, nameof(IVInjectionSubmitted), "Error message");
                var feature = new Microsoft.AspNetCore.Diagnostics.ExceptionHandlerFeature
                {
                    Error = ex,

                };
                HttpContext.Features.Set(feature);
                ViewBag.Error = ex;
                return View("ErrorView");
            }
           
        }

        public ActionResult PendingApprovalsPharmacies()
        {
            try
            {
                var Empid_SessionValue = HttpContext.Session.GetString("EmpIdbps");
                var PharApprovallist = _passDbContext.BPSPAPPharmaciesApprovalModels.FromSqlRaw("call GetPendingApprovals_PAP(@Empid_SessionValue)"
                                                        , new MySqlParameter("@Empid_SessionValue", Empid_SessionValue)).ToList();
                return View(PharApprovallist);
            }
            catch (Exception ex)
            {
                DateTime timestampValue = DateTime.Now; // Replace with the desired DateTime value

                GlobalClass.LogException(_passDbContext, ex, nameof(PendingApprovalsPharmacies), "Error message");
                var feature = new Microsoft.AspNetCore.Diagnostics.ExceptionHandlerFeature
                {
                    Error = ex,

                };
                HttpContext.Features.Set(feature);
                ViewBag.Error = ex;
                return View("ErrorView");
            }

        }

        public ActionResult PendingApprovalsIvInjection()
        {
            try
            {
                var Empid_SessionValue = HttpContext.Session.GetString("EmpIdbps");
                var PharApprovallist = _passDbContext.BPSPAPPharmaciesApprovalModels.FromSqlRaw("call GetPendingApprovals_PAPivinjection(@Empid_SessionValue)"
                                                        , new MySqlParameter("@Empid_SessionValue", Empid_SessionValue)).ToList();
                return View(PharApprovallist);
            }
            catch (Exception ex)
            {
                DateTime timestampValue = DateTime.Now; // Replace with the desired DateTime value

                GlobalClass.LogException(_passDbContext, ex, nameof(PendingApprovalsPharmacies), "Error message");
                var feature = new Microsoft.AspNetCore.Diagnostics.ExceptionHandlerFeature
                {
                    Error = ex,

                };
                HttpContext.Features.Set(feature);
                ViewBag.Error = ex;
                return View("ErrorView");
            }

        }

        [HttpGet]
        public IActionResult Create(string RequestId, string PAPType)
        {
            try
            {
                if (PAPType == null)
                {
                    ViewBag.AlertMessage = "Select PAP Type!";
                    return PartialView("Create_PartialView");
                }
                else if (RequestId == null)
                {
                    ViewBag.AlertMessage = "Request ID Required";
                    return PartialView("Create_PartialView");
                }
                else if (PAPType == "Hospital Pharmacies")
                {
                    var TID = @"select * from bps_request_pap where TrackingID = '" + RequestId + "'";
                    var PharmaTIDs = _passDbContext.BPSrequestpaps.FromSqlRaw(TID).FirstOrDefault();
                    if (PharmaTIDs != null)
                    {
                        ViewBag.AlertMessage = "Request ID Already Exist";
                        return PartialView("Create_PartialView");
                    }
                    else
                    {
                        var Empid_SessionValue = HttpContext.Session.GetString("EmpIdbps");
                        ViewBag.PapType = PAPType;

                        ViewBag.Reqid = RequestId;



                        Hcprequest_pap hcprequest = _passDbContext.HcprequestPAPs.FirstOrDefault(h => h.TrackingID == RequestId.Trim());

                        List<Team> teams = _passDbContext.Teams
        .Where(h => h.TeamCode == hcprequest.TeamId.Trim())
        .ToList();



                        var distributor = _passDbContext.DisterMappings.FromSqlRaw("call sp_GetDistributerDetailsPAP(@Tracking_ID)"
                                                        , new MySqlParameter("@Tracking_ID", RequestId)).ToList();

                        var combinedViewModel = new BPSRequestListViewModel
                        {

                            disterMappings = distributor,
                            teams = teams,

                        };
                        return View(combinedViewModel);
                    }
                }
                else if (PAPType == "IV/INJECTION")
                {
                    var TID = @"select * from bps_request_papivinjection where TrackingID = '" + RequestId + "'";
                    var IVInjectionTIDs = _passDbContext.bps_request_papivinjection.FromSqlRaw(TID).FirstOrDefault();
                    if (IVInjectionTIDs != null)
                    {
                        ViewBag.AlertMessage = "Request ID Not Found";
                        return PartialView("Create_PartialView");
                    }
                    else
                    {
                        var Empid_SessionValue = HttpContext.Session.GetString("EmpIdbps");

                        ViewBag.ivreqid = RequestId;

                        Hcprequest_pap hcprequest = _passDbContext.HcprequestPAPs.FirstOrDefault(h => h.TrackingID == RequestId.Trim());
                        Team team = _passDbContext.Teams.FirstOrDefault(h => h.TeamCode == hcprequest.TeamId);

                        List<Team> teams = _passDbContext.Teams
    .Where(h => h.TeamCode == hcprequest.TeamId.Trim())
    .ToList();

                        var products = _passDbContext.Tblproducts.FromSqlRaw("call sp_GetTeamProducts(@p_TeamName)"
                                                        , new MySqlParameter("@p_TeamName", team.TeamName)).ToList();

                        var combinedIvnjectionViewModel = new BPSRequestListViewModel
                        {

                            tblproducts = products,
                            teams = teams,

                        };

                        return PartialView("IVINJECTION_CreateView", combinedIvnjectionViewModel);
                    }
                }


                return View();
            }
            catch (Exception ex)
            {
                DateTime timestampValue = DateTime.Now; // Replace with the desired DateTime value

                GlobalClass.LogException(_passDbContext, ex, nameof(Create), "Error message");
                var feature = new Microsoft.AspNetCore.Diagnostics.ExceptionHandlerFeature
                {
                    Error = ex,

                };
                HttpContext.Features.Set(feature);
                ViewBag.Error = ex;
                return View("ErrorView");
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
                DateTime timestampValue = DateTime.Now; // Replace with the desired DateTime value

                GlobalClass.LogException(_passDbContext, ex, nameof(GetMacrobrick), "Error message");
                var feature = new Microsoft.AspNetCore.Diagnostics.ExceptionHandlerFeature
                {
                    Error = ex,

                };
                HttpContext.Features.Set(feature);
                ViewBag.Error = ex;
                return View("ErrorView");
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
                DateTime timestampValue = DateTime.Now; // Replace with the desired DateTime value

                GlobalClass.LogException(_passDbContext, ex, nameof(GetChemist), "Error message");
                var feature = new Microsoft.AspNetCore.Diagnostics.ExceptionHandlerFeature
                {
                    Error = ex,

                };
                HttpContext.Features.Set(feature);
                ViewBag.Error = ex;
                return View("ErrorView");
            }

        }

        [HttpPost]
        public IActionResult PartialAcc([FromBody] PartialAccRequest requestData)
        {
            try
            {
                var teamName = requestData.TeamName;
                var chemistCode = requestData.ChemistCode;
                ViewBag.ChemCode = requestData.ChemistCode;

                List<DsrHiltonDailySalesTeamToChemist202223> salesData = new List<DsrHiltonDailySalesTeamToChemist202223>();

                using (SqlConnection connection = new SqlConnection(_sqlconnection))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetSalesDataForLastYear", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TeamName", teamName);
                        command.Parameters.AddWithValue("@ClientCode", chemistCode);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DsrHiltonDailySalesTeamToChemist202223 model = new DsrHiltonDailySalesTeamToChemist202223();
                                model.PackCode = reader["PackCode"].ToString();
                                model.ProductName = reader["ProductName"].ToString();
                                //model.SalesUnits = reader["Sales_Units"].ToString();
                                //model.SalesValueNp = reader["Sales_ValueNP"].ToString();



                                salesData.Add(model);
                            }
                        }
                    }
                }
                ViewBag.PAPProducts = _passDbContext.Tblproducts.FromSqlRaw("select * from tblproduct").ToList();


                return PartialView("Accordion_PartialView", salesData);
            }
            catch (Exception ex)
            {
                DateTime timestampValue = DateTime.Now; 

                GlobalClass.LogException(_passDbContext, ex, nameof(GetChemist), "Error message");
                var feature = new Microsoft.AspNetCore.Diagnostics.ExceptionHandlerFeature
                {
                    Error = ex,

                };
                HttpContext.Features.Set(feature);
                ViewBag.Error = ex;
                return View("ErrorView");
            }
           
        }





        public object CreatePAPBpsRecord(string PAPSalesarr, BPSrequestpap PAPHeaderData)
        {
            try
            {
                List<PAPCustomModel_Chemist> model = JsonSerializer.Deserialize<List<PAPCustomModel_Chemist>>(PAPSalesarr);



                bool isSuccess = false;
                var EmpidSessionValue = HttpContext.Session.GetString("EmpIdbps");
                try
                {
                    var outputParameter = new MySqlParameter
                    {
                        ParameterName = "p_BPS_Record_ID",
                        MySqlDbType = MySqlDbType.Int32,
                        Direction = ParameterDirection.Output
                    };



                    Hcprequest_pap hcprequest = _passDbContext.HcprequestPAPs.FirstOrDefault(h => h.TrackingID == PAPHeaderData.TrackingID.Trim());
                    var result = _passDbContext.OutPutParameters
                        .FromSqlRaw("CALL sp_InsertPAPBpsHeaderData(" + hcprequest.HCPREQID + ", '" + PAPHeaderData.TrackingID.Trim() + "', '" + Convert.ToDateTime(PAPHeaderData.DiscountDateFrom).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + "', '" + Convert.ToDateTime(PAPHeaderData.DiscountDateTo).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + "', '" + PAPHeaderData.BrickCode.Trim() + "', '" + PAPHeaderData.DistributerCode.Trim() + "', '" + PAPHeaderData.Remarks + "', '" + PAPHeaderData.DiscountType + "', '" + 1 + "','" + EmpidSessionValue + "', p_BPS_Record_ID)", outputParameter)
                        .ToList();

                    var bpsrecordid = outputParameter.Value;

                    foreach (var item in model)
                    {
                        string chmeistcode = item.ChemistCode;

                        foreach (var p in item.ProductArr)
                        {
                            string PackCode = p.PackCode;
                            string Discount = p.Discount;
                            string LastYearSKU = p.LastYearSKU;
                            string LastYearValue = p.LastYearValue;
                            string ExpectedBusinessUnit = p.ExpectedBusinessUnit;

                            string ExpectedBusinessValue = p.ExpectedBusinessValue;
                            string UnitPrice = p.UnitPrice;
                            string Capping = p.Capping;


                            var outputParameter1 = new MySqlParameter
                            {
                                ParameterName = "p_Record_ID",
                                MySqlDbType = MySqlDbType.Int32,
                                Direction = ParameterDirection.Output
                            };

                            var resultsales = _passDbContext.OutputParaMetersSales
                                .FromSqlRaw("CALL sp_InsertPAPBpsSalesData(@p_BPS_Record_ID," +
                                "@p_Chemist_Code, " +
                                "@p_PackCode," +
                                " @p_LastYearSKU," +
                                " @p_LastYearValue," +
                                " @p_Year," +
                                " @p_Discount," +
                                "@p_ExpectedBusinessUnit," +
                                "@p_ExpectedBusinessValue," +
                                "@p_UnitPrice," +
                                "@p_CreatedBy," +
                                "@p_Capping," +
                                " p_Record_ID)",
                                new MySqlParameter("@p_BPS_Record_ID", bpsrecordid),
                                new MySqlParameter("@p_Chemist_Code", chmeistcode),
                                new MySqlParameter("@p_PackCode", PackCode),
                                new MySqlParameter("@p_LastYearSKU", LastYearSKU),
                                new MySqlParameter("@p_LastYearValue", LastYearValue),
                                new MySqlParameter("@p_Year", null),
                                new MySqlParameter("@p_Discount", Discount),
                                new MySqlParameter("@p_ExpectedBusinessUnit", ExpectedBusinessUnit),
                                new MySqlParameter("@p_ExpectedBusinessValue", ExpectedBusinessValue),
                                new MySqlParameter("@p_UnitPrice", UnitPrice),
                                new MySqlParameter("@p_CreatedBy", EmpidSessionValue),
                                new MySqlParameter("@p_Capping", Capping),
                                outputParameter1)
                                .ToList();

                            int recordId = (int)outputParameter1.Value;
                        }


                    }


                    //startworkflowpap
                    using (MySqlConnection connection = new MySqlConnection(_connectionString))
                    {
                        connection.Open();

                        using (MySqlCommand command = new MySqlCommand("SpStartWF_PAP", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("p_TrackingId", PAPHeaderData.TrackingID);
                            command.Parameters.AddWithValue("p_HCPReqId", hcprequest.HCPREQID);
                            command.Parameters.AddWithValue("p_BpsId", bpsrecordid);
                            command.Parameters.AddWithValue("p_User", EmpidSessionValue);
                            command.Parameters.AddWithValue("p_paptype", 1);

                            // Execute the stored procedure
                            command.ExecuteNonQuery();
                            command.CommandTimeout = 3000;

                        }
                    }

                }
                catch (Exception ex)
                {
                    isSuccess = true;
                    return null;
                }

                isSuccess = true;

                return null;


       

            }
            catch (Exception ex)
            {
                DateTime timestampValue = DateTime.Now; // Replace with the desired DateTime value

                GlobalClass.LogException(_passDbContext, ex, nameof(CreatePAPBpsRecord), "Error message");
                var feature = new Microsoft.AspNetCore.Diagnostics.ExceptionHandlerFeature
                {
                    Error = ex,

                };
                HttpContext.Features.Set(feature);
                ViewBag.Error = ex;
                return View("ErrorView");
            }

               

        }


        [HttpPost]
        public object CreatePAPIvInjectionBpsRecord(string PAPIvInjectionSalesarr, BPSrequestpapIvInjection PAPIvInjectionHeaderData)
        {
            try
            {
                if (ModelState.IsValid)
                {


                    List<PAPIvInjectionCustomModel_Team> model = JsonSerializer.Deserialize<List<PAPIvInjectionCustomModel_Team>>(PAPIvInjectionSalesarr);



                    bool isSuccess = false;
                    var EmpidSessionValue = HttpContext.Session.GetString("EmpIdbps");
                    try
                    {
                        var outputParameter = new MySqlParameter
                        {
                            ParameterName = "p_BPS_Record_ID",
                            MySqlDbType = MySqlDbType.Int32,
                            Direction = ParameterDirection.Output
                        };

                        //var hcpreqid = _passDbContext.Hcprequests.FromSqlRaw("select * from hcprequest where trackingid = '" + PAPIvInjectionHeaderData.TrackingId + "'").FirstOrDefault().Hcpreqid;

                        Hcprequest_pap hcprequest = _passDbContext.HcprequestPAPs.FirstOrDefault(h => h.TrackingID == PAPIvInjectionHeaderData.TrackingId.Trim());
                        var result = _passDbContext.OutPutParameters
                            .FromSqlRaw("CALL sp_InsertPAPIvInjectionBPSHeaderData(" + hcprequest.HCPREQID + ", '" + PAPIvInjectionHeaderData.TrackingId + "', '" + Convert.ToDateTime(PAPIvInjectionHeaderData.DiscountFromDate).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + "', '" + Convert.ToDateTime(PAPIvInjectionHeaderData.DiscountToDate).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + "','" + EmpidSessionValue + "', p_BPS_Record_ID)", outputParameter)
                            .ToList();

                        var bpsrecordid = outputParameter.Value;

                        foreach (var item in model)
                        {
                            string Team = item.Team;

                            foreach (var p in item.ProductArr)
                            {
                                string PackCode = p.PackCode;
                                string Discount = p.Discount;
                                string Capping = p.Capping;


                                var outputParameter1 = new MySqlParameter
                                {
                                    ParameterName = "p_Record_ID",
                                    MySqlDbType = MySqlDbType.Int32,
                                    Direction = ParameterDirection.Output
                                };

                                var resultsales = _passDbContext.OutputParaMetersSales
                                    .FromSqlRaw("CALL sp_InsertPAPIvInjectionBPSSalesData(@p_BPS_Record_ID," +
                                    "@p_Team, " +
                                    "@p_PackCode," +
                                    " @p_Discount," +
                                    "@p_CreatedBy," +
                                    "@p_Capping," +
                                    " p_Record_ID)",
                                    new MySqlParameter("@p_BPS_Record_ID", bpsrecordid),
                                    new MySqlParameter("@p_Team", Team),
                                    new MySqlParameter("@p_PackCode", PackCode),
                                    new MySqlParameter("@p_Discount", Discount),
                                    new MySqlParameter("@p_CreatedBy", EmpidSessionValue),
                                    new MySqlParameter("@p_Capping", Capping),
                                    outputParameter1)
                                    .ToList();

                                int recordId = (int)outputParameter1.Value;
                            }


                        }


                        using (MySqlConnection connection = new MySqlConnection(_connectionString))
                        {
                            connection.Open();

                            using (MySqlCommand command = new MySqlCommand("SpStartWF_PAP", connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.AddWithValue("p_TrackingId", PAPIvInjectionHeaderData.TrackingId);
                                command.Parameters.AddWithValue("p_HCPReqId", hcprequest.HCPREQID);
                                command.Parameters.AddWithValue("p_BpsId", bpsrecordid);
                                command.Parameters.AddWithValue("p_User", EmpidSessionValue);
                                command.Parameters.AddWithValue("p_paptype", 2);

                                // Execute the stored procedure
                                command.ExecuteNonQuery();
                                command.CommandTimeout = 3000;

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        isSuccess = true;
                        return null;
                    }

                    isSuccess = true;
                }

                return null;
            }
            catch (Exception ex)
            {
                DateTime timestampValue = DateTime.Now; // Replace with the desired DateTime value

                GlobalClass.LogException(_passDbContext, ex, nameof(CreatePAPIvInjectionBpsRecord), "Error message");
                var feature = new Microsoft.AspNetCore.Diagnostics.ExceptionHandlerFeature
                {
                    Error = ex,

                };
                HttpContext.Features.Set(feature);
                ViewBag.Error = ex;
                return View("ErrorView");
            }



        }


        public ActionResult PharmacieView(int id)
        {
            try
            {
                BPSrequestpap bpspaprephar = _passDbContext.BPSrequestpaps.FirstOrDefault(h => h.BPS_Record_Id == id);
                var bpsreqpappharmacies = _passDbContext.BPSrequestpaps
     .Where(h => h.BPS_Record_Id == id)
     .ToList();

                var bpsreqpappharmaciesdis = _passDbContext.Distributers
    .Where(h => h.DistributerCode == bpspaprephar.DistributerCode)
    .ToList();
                var bpsreqpappharmaciesmac = _passDbContext.Macrobricks
    .Where(h => h.MacroBrickCode == bpspaprephar.BrickCode)
    .ToList();


                var bpsreqpappharmacieschem = _passDbContext.MacChemMappings
                    .Where(mcm => mcm.MacroBrickCode == bpspaprephar.BrickCode)
                    .Join(_passDbContext.Chemists, // Join with the Chemist table
                          mcm => mcm.ChemistCode, // On MacChemMapping's Chemist_Code
                          che => che.ChemistCode, // On Chemist's Chemist_Code
                          (mcm, che) => new { che.ChemistCode, che.ChemistName }) // Select the fields
                    .ToList();





                var bpssalespappharmacies = _passDbContext.Bpssalesrecordpaps
    .Where(h => h.Bps_RecordID == id)
    .ToList();

                Hcprequest_pap hcppharmaciespap = _passDbContext.HcprequestPAPs.FirstOrDefault(h => h.HCPREQID == bpspaprephar.HCPREQID);
                var Teampharmaciespap = _passDbContext.Teams
    .Where(h => h.TeamCode == hcppharmaciespap.TeamId)
    .ToList();


                //headerdatpostDatesaends
                var chemistname = new List<Chemist>();
                var resultsByChemistPaPPharmacies = new Dictionary<string, List<ExpandoObject>>();


                var chemistCodes = _passDbContext.Bpssalesrecordpaps
                    .Where(record => record.Bps_RecordID == id)
                    .Select(record => record.ChemistCode.ToString()) // Convert int? to string
                    .Distinct()
                    .ToList();



                foreach (var c in chemistCodes)
                {
                    var chemistName = _passDbContext.Chemists
                        .Where(record => record.ChemistCode == c.ToString())
                      .Select(record => new Chemist
                      {
                          ChemistCode = record.ChemistCode,
                          ChemistName = record.ChemistName

                      })
                        .Distinct()
                        .ToList();

                    chemistname.AddRange(chemistName);







                    var p_BPS_Record_ID_PaPPharmacies = new MySqlParameter("@p_BPS_Record_ID", id);
                    var p_Chemist_Code_PaPPharmacies = new MySqlParameter("@p_ChemistCode", c);
                    var preresults = new List<ExpandoObject>();

                    using (var command = _passDbContext.Database.GetDbConnection().CreateCommand())
                    {
                        command.CommandText = "CALL sp_GenerateTablePharmaciesPAP(@p_Bps_Record_ID,@p_ChemistCode)";
                        command.Parameters.Add(p_BPS_Record_ID_PaPPharmacies);
                        command.Parameters.Add(p_Chemist_Code_PaPPharmacies);

                        _passDbContext.Database.OpenConnection();

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dynamic result = new ExpandoObject();
                                var expandoDict = result as IDictionary<string, object>;

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    string columnName = reader.GetName(i);
                                    object columnValue = reader[i];

                                    expandoDict.Add(columnName, columnValue);
                                }


                                preresults.Add(result);
                            }
                        }
                    }

                    resultsByChemistPaPPharmacies[c.ToString()] = preresults;
                }




                var ViewModelPharmaciesView = new BPSRequestListViewModel
                {
                    BPSrequestpaps = bpsreqpappharmacies,
                    Bpssalesrecordpaps = bpssalespappharmacies,
                    Distributers = bpsreqpappharmaciesdis,
                    Macrobricks = bpsreqpappharmaciesmac,
                    teams = Teampharmaciespap,
                    chemists = chemistname,
                    SalesPAPDataPharmacies = resultsByChemistPaPPharmacies,
                    ChemistCodes = chemistCodes

                };
                return View(ViewModelPharmaciesView);

            }
            catch (Exception ex)
            {
                DateTime timestampValue = DateTime.Now; // Replace with the desired DateTime value

                GlobalClass.LogException(_passDbContext, ex, nameof(PharmacieView), "Error message");
                var feature = new Microsoft.AspNetCore.Diagnostics.ExceptionHandlerFeature
                {
                    Error = ex,

                };
                HttpContext.Features.Set(feature);
                ViewBag.Error = ex;
                return View("ErrorView");
            }
          
        }

        public ActionResult IVInjectionView(int id)
        {
            try
            {
                BPSrequestpapIvInjection bpspapreiv = _passDbContext.bps_request_papivinjection.FirstOrDefault(h => h.BPS_Record_ID == id);
                var bpsreqpapivinjection = _passDbContext.bps_request_papivinjection
     .Where(h => h.BPS_Record_ID == id)
     .ToList();


                var bpssalespapivinjection = _passDbContext.bps_salesrecord_papivinjection
    .Where(h => h.BPS_Record_ID == id)
    .ToList();

                Hcprequest_pap hcpivinjectionpap = _passDbContext.HcprequestPAPs.FirstOrDefault(h => h.HCPREQID == bpspapreiv.HCPREQID);
                var Teamivinjectionpap = _passDbContext.Teams
    .Where(h => h.TeamCode == hcpivinjectionpap.TeamId)
    .FirstOrDefault();
                var TeamivinjectionpapList = _passDbContext.Teams
    .Where(h => h.TeamCode == hcpivinjectionpap.TeamId)
    .ToList();


                var products = _passDbContext.BPSIvInjectionViewViewModels.FromSqlRaw("call sp_GenerateTableIVInjectionPAP(@p_Bps_Record_ID, @p_TeamName)",
                                                new MySqlParameter("@p_Bps_Record_ID", id),
                                                new MySqlParameter("@p_TeamName", Teamivinjectionpap.TeamName)).ToList();




                var ViewModelPharmaciesView = new BPSRequestListViewModel
                {
                    BPSIvInjectionViewViewModels = products,
                    teams = TeamivinjectionpapList,
                    BPSrequestpapIvInjections = bpsreqpapivinjection,
                    bpssalesrecordpapIvInjections = bpssalespapivinjection

                };


                return View(ViewModelPharmaciesView);

            }
            catch (Exception ex)
            {
                DateTime timestampValue = DateTime.Now; // Replace with the desired DateTime value

                GlobalClass.LogException(_passDbContext, ex, nameof(PharmacieView), "Error message");
                var feature = new Microsoft.AspNetCore.Diagnostics.ExceptionHandlerFeature
                {
                    Error = ex,

                };
                HttpContext.Features.Set(feature);
                ViewBag.Error = ex;
                return View("ErrorView");
            }
          
        }

        public ActionResult PharmacieApprovalView(int id, int wfid)
        {
            try
            {
                BPSrequestpap bpspaprephar = _passDbContext.BPSrequestpaps.FirstOrDefault(h => h.BPS_Record_Id == id);
                var bpsreqpappharmacies = _passDbContext.BPSrequestpaps
     .Where(h => h.BPS_Record_Id == id)
     .ToList();

                var bpsreqpappharmaciesdis = _passDbContext.Distributers
    .Where(h => h.DistributerCode == bpspaprephar.DistributerCode)
    .ToList();
                var bpsreqpappharmaciesmac = _passDbContext.Macrobricks
    .Where(h => h.MacroBrickCode == bpspaprephar.BrickCode)
    .ToList();


                var bpsreqpappharmacieschem = _passDbContext.MacChemMappings
                    .Where(mcm => mcm.MacroBrickCode == bpspaprephar.BrickCode)
                    .Join(_passDbContext.Chemists, // Join with the Chemist table
                          mcm => mcm.ChemistCode, // On MacChemMapping's Chemist_Code
                          che => che.ChemistCode, // On Chemist's Chemist_Code
                          (mcm, che) => new { che.ChemistCode, che.ChemistName }) // Select the fields
                    .ToList();





                var bpssalespappharmacies = _passDbContext.Bpssalesrecordpaps
    .Where(h => h.Bps_RecordID == id)
    .ToList();

                Hcprequest_pap hcppharmaciespap = _passDbContext.HcprequestPAPs.FirstOrDefault(h => h.HCPREQID == bpspaprephar.HCPREQID);
                var Teampharmaciespap = _passDbContext.Teams
    .Where(h => h.TeamCode == hcppharmaciespap.TeamId)
    .ToList();


                //headerdatpostDatesaends
                var chemistname = new List<Chemist>();
                var resultsByChemistPaPPharmacies = new Dictionary<string, List<ExpandoObject>>();


                var chemistCodes = _passDbContext.Bpssalesrecordpaps
                    .Where(record => record.Bps_RecordID == id)
                    .Select(record => record.ChemistCode.ToString()) // Convert int? to string
                    .Distinct()
                    .ToList();



                foreach (var c in chemistCodes)
                {
                    var chemistName = _passDbContext.Chemists
                        .Where(record => record.ChemistCode == c.ToString())
                      .Select(record => new Chemist
                      {
                          ChemistCode = record.ChemistCode,
                          ChemistName = record.ChemistName

                      })
                        .Distinct()
                        .ToList();

                    chemistname.AddRange(chemistName);







                    var p_BPS_Record_ID_PaPPharmacies = new MySqlParameter("@p_BPS_Record_ID", id);
                    var p_Chemist_Code_PaPPharmacies = new MySqlParameter("@p_ChemistCode", c);
                    var preresults = new List<ExpandoObject>();

                    using (var command = _passDbContext.Database.GetDbConnection().CreateCommand())
                    {
                        command.CommandText = "CALL sp_GenerateTablePharmaciesPAP(@p_Bps_Record_ID,@p_ChemistCode)";
                        command.Parameters.Add(p_BPS_Record_ID_PaPPharmacies);
                        command.Parameters.Add(p_Chemist_Code_PaPPharmacies);

                        _passDbContext.Database.OpenConnection();

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dynamic result = new ExpandoObject();
                                var expandoDict = result as IDictionary<string, object>;

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    string columnName = reader.GetName(i);
                                    object columnValue = reader[i];

                                    expandoDict.Add(columnName, columnValue);
                                }


                                preresults.Add(result);
                            }
                        }
                    }

                    resultsByChemistPaPPharmacies[c.ToString()] = preresults;
                }




                var ViewModelPharmaciesView = new BPSRequestListViewModel
                {
                    BPSrequestpaps = bpsreqpappharmacies,
                    Bpssalesrecordpaps = bpssalespappharmacies,
                    Distributers = bpsreqpappharmaciesdis,
                    Macrobricks = bpsreqpappharmaciesmac,
                    teams = Teampharmaciespap,
                    chemists = chemistname,
                    SalesPAPDataPharmacies = resultsByChemistPaPPharmacies,
                    ChemistCodes = chemistCodes,
                    WFID = wfid

                };
                return View(ViewModelPharmaciesView);

            }
            catch (Exception ex)
            {
                DateTime timestampValue = DateTime.Now; // Replace with the desired DateTime value

                GlobalClass.LogException(_passDbContext, ex, nameof(PharmacieView), "Error message");
                var feature = new Microsoft.AspNetCore.Diagnostics.ExceptionHandlerFeature
                {
                    Error = ex,

                };
                HttpContext.Features.Set(feature);
                ViewBag.Error = ex;
                return View("ErrorView");
            }

        }

        public ActionResult IVInjectionApprovedView(int id)
        {
            try
            {
                BPSrequestpapIvInjection bpspapreiv = _passDbContext.bps_request_papivinjection.FirstOrDefault(h => h.BPS_Record_ID == id);
                var bpsreqpapivinjection = _passDbContext.bps_request_papivinjection
     .Where(h => h.BPS_Record_ID == id)
     .ToList();


                var bpssalespapivinjection = _passDbContext.bps_salesrecord_papivinjection
    .Where(h => h.BPS_Record_ID == id)
    .ToList();

                Hcprequest_pap hcpivinjectionpap = _passDbContext.HcprequestPAPs.FirstOrDefault(h => h.HCPREQID == bpspapreiv.HCPREQID);
                var Teamivinjectionpap = _passDbContext.Teams
    .Where(h => h.TeamCode == hcpivinjectionpap.TeamId)
    .FirstOrDefault();
                var TeamivinjectionpapList = _passDbContext.Teams
    .Where(h => h.TeamCode == hcpivinjectionpap.TeamId)
    .ToList();


                var products = _passDbContext.BPSIvInjectionViewViewModels.FromSqlRaw("call sp_GenerateTableIVInjectionPAP(@p_Bps_Record_ID, @p_TeamName)",
                                                new MySqlParameter("@p_Bps_Record_ID", id),
                                                new MySqlParameter("@p_TeamName", Teamivinjectionpap.TeamName)).ToList();




                var ViewModelPharmaciesView = new BPSRequestListViewModel
                {
                    BPSIvInjectionViewViewModels = products,
                    teams = TeamivinjectionpapList,
                    BPSrequestpapIvInjections = bpsreqpapivinjection,
                    bpssalesrecordpapIvInjections = bpssalespapivinjection

                };


                return View(ViewModelPharmaciesView);

            }
            catch (Exception ex)
            {
                DateTime timestampValue = DateTime.Now; // Replace with the desired DateTime value

                GlobalClass.LogException(_passDbContext, ex, nameof(PharmacieView), "Error message");
                var feature = new Microsoft.AspNetCore.Diagnostics.ExceptionHandlerFeature
                {
                    Error = ex,

                };
                HttpContext.Features.Set(feature);
                ViewBag.Error = ex;
                return View("ErrorView");
            }

        }

        public ActionResult PharmaEdit(int id)
        {
            try
            {
                BPSrequestpap bpspaprephar = _passDbContext.BPSrequestpaps.FirstOrDefault(h => h.BPS_Record_Id == id);
                var bpsreqpappharmacies = _passDbContext.BPSrequestpaps
     .Where(h => h.BPS_Record_Id == id)
     .ToList();

                var bpsreqpappharmaciesdis = _passDbContext.Distributers
    .Where(h => h.DistributerCode == bpspaprephar.DistributerCode)
    .ToList();
                var bpsreqpappharmaciesmac = _passDbContext.Macrobricks
    .Where(h => h.MacroBrickCode == bpspaprephar.BrickCode)
    .ToList();


                var bpssalespappharmacies = _passDbContext.Bpssalesrecordpaps
    .Where(h => h.Bps_RecordID == id)
    .ToList();

                Hcprequest_pap hcppharmaciespap = _passDbContext.HcprequestPAPs.FirstOrDefault(h => h.HCPREQID == bpspaprephar.HCPREQID);
                var Teampharmaciespap = _passDbContext.Teams
    .Where(h => h.TeamCode == hcppharmaciespap.TeamId)
    .ToList();


                var macchemnamequery = @"SELECT mcm.*, che.ChemistName FROM mac_chem_mapping mcm
Inner Join chemist che on
mcm.Chemist_Code = che.Chemist_Code
where mcm.MacroBrickCode = '" + bpspaprephar.BrickCode + "'";
                var macchemname = _passDbContext.MacChemMappings.FromSqlRaw(macchemnamequery).ToList();

                //headerdatpostDatesaends
                var chemistname = new List<Chemist>();
                var resultsByChemistPaPPharmacies = new Dictionary<string, List<ExpandoObject>>();


                var chemistCodes = _passDbContext.Bpssalesrecordpaps
                    .Where(record => record.Bps_RecordID == id)
                    .Select(record => record.ChemistCode.ToString()) // Convert int? to string
                    .Distinct()
                    .ToList();



                foreach (var c in chemistCodes)
                {
                    var chemistName = _passDbContext.Chemists
                        .Where(record => record.ChemistCode == c.ToString())
                      .Select(record => new Chemist
                      {
                          ChemistCode = record.ChemistCode,
                          ChemistName = record.ChemistName

                      })
                        .Distinct()
                        .ToList();

                    chemistname.AddRange(chemistName);







                    var p_BPS_Record_ID_PaPPharmacies = new MySqlParameter("@p_BPS_Record_ID", id);
                    var p_Chemist_Code_PaPPharmacies = new MySqlParameter("@p_ChemistCode", c);
                    var preresults = new List<ExpandoObject>();

                    using (var command = _passDbContext.Database.GetDbConnection().CreateCommand())
                    {
                        command.CommandText = "CALL sp_GenerateTablePharmaciesPAP(@p_Bps_Record_ID,@p_ChemistCode)";
                        command.Parameters.Add(p_BPS_Record_ID_PaPPharmacies);
                        command.Parameters.Add(p_Chemist_Code_PaPPharmacies);

                        _passDbContext.Database.OpenConnection();

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dynamic result = new ExpandoObject();
                                var expandoDict = result as IDictionary<string, object>;

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    string columnName = reader.GetName(i);
                                    object columnValue = reader[i];

                                    expandoDict.Add(columnName, columnValue);
                                }


                                preresults.Add(result);
                            }
                        }
                    }

                    resultsByChemistPaPPharmacies[c.ToString()] = preresults;
                }




                var ViewModelPharmaciesView = new BPSRequestListViewModel
                {
                    BPSrequestpaps = bpsreqpappharmacies,
                    Bpssalesrecordpaps = bpssalespappharmacies,
                    Distributers = bpsreqpappharmaciesdis,
                    Macrobricks = bpsreqpappharmaciesmac,
                    teams = Teampharmaciespap,
                    chemists = chemistname,
                    SalesPAPDataPharmacies = resultsByChemistPaPPharmacies,
                    macChemMappings = macchemname,
                    ChemistCodes = chemistCodes,

                };
                return View(ViewModelPharmaciesView);
            }
            catch (Exception ex)
            {
                DateTime timestampValue = DateTime.Now; // Replace with the desired DateTime value

                GlobalClass.LogException(_passDbContext, ex, nameof(PharmaEdit), "Error message");
                var feature = new Microsoft.AspNetCore.Diagnostics.ExceptionHandlerFeature
                {
                    Error = ex,

                };
                HttpContext.Features.Set(feature);
                ViewBag.Error = ex;
                return View("ErrorView");
            }

            //return View();

        }

        [HttpPost]
        public IActionResult EditPartialAcc([FromBody] PartialAccRequest requestData)
        {
            try
            {
                var teamName = requestData.TeamName;
                var chemistCode = requestData.ChemistCode;
                ViewBag.ChemCode = requestData.ChemistCode;

                List<DsrHiltonDailySalesTeamToChemist202223> salesData = new List<DsrHiltonDailySalesTeamToChemist202223>();

                using (SqlConnection connection = new SqlConnection(_sqlconnection))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetSalesDataForLastYear", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TeamName", teamName);
                        command.Parameters.AddWithValue("@ClientCode", chemistCode);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DsrHiltonDailySalesTeamToChemist202223 model = new DsrHiltonDailySalesTeamToChemist202223();
                                model.PackCode = reader["PackCode"].ToString();
                                model.ProductName = reader["ProductName"].ToString();
                                //model.SalesUnits = reader["Sales_Units"].ToString();
                                //model.SalesValueNp = reader["Sales_ValueNP"].ToString();



                                salesData.Add(model);
                            }
                        }
                    }
                }
                ViewBag.PAPProducts = _passDbContext.Tblproducts.FromSqlRaw("select * from tblproduct").ToList();


                return PartialView("Accordion_EditPartialView", salesData);
            }
            catch (Exception ex)
            {
                DateTime timestampValue = DateTime.Now; // Replace with the desired DateTime value

                GlobalClass.LogException(_passDbContext, ex, nameof(EditPartialAcc), "Error message");
                var feature = new Microsoft.AspNetCore.Diagnostics.ExceptionHandlerFeature
                {
                    Error = ex,

                };
                HttpContext.Features.Set(feature);
                ViewBag.Error = ex;
                return View("ErrorView");
            }

        }

        [HttpPost]
        public object EditPAPBpsRecord(string EditPAPSalesarr, BPSrequestpap EditPAPHeaderData)
        {
            try
            {
                List<PAPCustomModel_Chemist> model = JsonSerializer.Deserialize<List<PAPCustomModel_Chemist>>(EditPAPSalesarr)
;
                bool isSuccess = false;
                var EmpidSessionValue = HttpContext.Session.GetString("EmpIdbps");
                BPSrequestpap bpspappharequest = _passDbContext.BPSrequestpaps.FirstOrDefault(h => h.TrackingID == EditPAPHeaderData.TrackingID.Trim());
                try
                {
                    using (var connection = new MySqlConnection(_connectionString))
                    {
                        connection.Open();
                        using (var command = new MySqlCommand("sp_UpdateBpsPAPPharmaciesHeaderData", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            // Add parameters
                            command.Parameters.AddWithValue("@p_DiscountDateFrom", EditPAPHeaderData.DiscountDateFrom);
                            command.Parameters.AddWithValue("@p_DiscountDateTo", EditPAPHeaderData.DiscountDateTo);
                            command.Parameters.AddWithValue("@p_TrackingId", EditPAPHeaderData.TrackingID);
                            command.Parameters.AddWithValue("@p_UpdatedBy", EmpidSessionValue);
                            command.Parameters.AddWithValue("@p_Remarks", EditPAPHeaderData.Remarks);
                            command.Parameters.AddWithValue("@p_DiscountType", EditPAPHeaderData.DiscountType);
                            command.ExecuteNonQuery();

                        }
                    }



                    using (var connection = new MySqlConnection(_connectionString))
                    {
                        connection.Open();
                        MySqlTransaction trans = connection.BeginTransaction();
                        try
                        {

                            using (var command = new MySqlCommand("delete from bps_requestsalesrecord_pap where  Bps_RecordID = " + bpspappharequest.BPS_Record_Id + "", connection))
                            {
                                command.Transaction = trans;
                                command.ExecuteNonQuery();
                            }
                            foreach (var item in model)
                            {
                                string chmeistcode = item.ChemistCode;


                                foreach (var p in item.ProductArr)
                                {
                                    string PackCode = p.PackCode;
                                    string Discount = p.Discount;
                                    string LastYearSKU = p.LastYearSKU;
                                    string LastYearValue = p.LastYearValue;
                                    string ExpectedBusinessUnit = p.ExpectedBusinessUnit;

                                    string ExpectedBusinessValue = p.ExpectedBusinessValue;
                                    string UnitPrice = p.UnitPrice;
                                    string Capping = p.Capping;

                                    using (var command = new MySqlCommand("sp_UpdateBpsPAPPharmaciesSalesData", connection))
                                    {
                                        command.CommandType = CommandType.StoredProcedure;
                                        command.Parameters.Add(new MySqlParameter("@p_Bps_RecordID", bpspappharequest.BPS_Record_Id));
                                        command.Parameters.Add(new MySqlParameter("@p_ChemistCode", chmeistcode));
                                        command.Parameters.Add(new MySqlParameter("@p_PackCode", PackCode));
                                        command.Parameters.Add(new MySqlParameter("@p_LastYearSKU", LastYearSKU));
                                        command.Parameters.Add(new MySqlParameter("@p_LastYearValue", LastYearValue));
                                        command.Parameters.Add(new MySqlParameter("@p_Year", null));
                                        command.Parameters.Add(new MySqlParameter("@p_Discount", Discount));
                                        command.Parameters.Add(new MySqlParameter("@p_ExpectedBusinessUnit", ExpectedBusinessUnit));
                                        command.Parameters.Add(new MySqlParameter("@p_ExpectedBusinessValue", ExpectedBusinessValue));
                                        command.Parameters.Add(new MySqlParameter("@p_UnitPrice", UnitPrice));
                                        command.Parameters.Add(new MySqlParameter("@p_UpdatedBy", EmpidSessionValue));
                                        command.Parameters.Add(new MySqlParameter("@p_Capping", Capping));

                                        command.Transaction = trans;
                                        using (MySqlDataReader reader = command.ExecuteReader())
                                        {
                                            while (reader.Read())
                                            {
                                            }
                                        }
                                    }

                                }

                            }

                            trans.Commit();

                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            throw;
                        }
                    }


                }
                catch (Exception ex)
                {

                }

                // Get the output parameter value
                //var updatedBpsRecordId = (int)outputParameter.Value;

                //return null;
                return RedirectToAction("PharmaciesSubmitted");
            }
            catch (Exception ex)
            {
                DateTime timestampValue = DateTime.Now; // Replace with the desired DateTime value

                GlobalClass.LogException(_passDbContext, ex, nameof(EditPAPBpsRecord), "Error message");
                var feature = new Microsoft.AspNetCore.Diagnostics.ExceptionHandlerFeature
                {
                    Error = ex,

                };
                HttpContext.Features.Set(feature);
                ViewBag.Error = ex;
                return View("ErrorView");
            }

        }

        public ActionResult IVInjectionEdit(int id)
        {
            try
            {
                BPSrequestpapIvInjection bpspapreiv = _passDbContext.bps_request_papivinjection.FirstOrDefault(h => h.BPS_Record_ID == id);
                var bpsreqpapivinjection = _passDbContext.bps_request_papivinjection
     .Where(h => h.BPS_Record_ID == id)
     .ToList();


                var bpssalespapivinjection = _passDbContext.bps_salesrecord_papivinjection
    .Where(h => h.BPS_Record_ID == id)
    .ToList();

                Hcprequest_pap hcpivinjectionpap = _passDbContext.HcprequestPAPs.FirstOrDefault(h => h.HCPREQID == bpspapreiv.HCPREQID);
                var Teamivinjectionpap = _passDbContext.Teams
    .Where(h => h.TeamCode == hcpivinjectionpap.TeamId)
    .FirstOrDefault();
                var TeamivinjectionpapList = _passDbContext.Teams
    .Where(h => h.TeamCode == hcpivinjectionpap.TeamId)
    .ToList();


                var products = _passDbContext.BPSIvInjectionViewViewModels.FromSqlRaw("call sp_GenerateTableIVInjectionPAP(@p_Bps_Record_ID, @p_TeamName)",
                                                new MySqlParameter("@p_Bps_Record_ID", id),
                                                new MySqlParameter("@p_TeamName", Teamivinjectionpap.TeamName)).ToList();




                var ViewModelPharmaciesView = new BPSRequestListViewModel
                {
                    BPSIvInjectionViewViewModels = products,
                    teams = TeamivinjectionpapList,
                    BPSrequestpapIvInjections = bpsreqpapivinjection,
                    bpssalesrecordpapIvInjections = bpssalespapivinjection


                };


                return View(ViewModelPharmaciesView);
            }
            catch(Exception ex)
            {
                DateTime timestampValue = DateTime.Now; // Replace with the desired DateTime value

                GlobalClass.LogException(_passDbContext, ex, nameof(IVInjectionEdit), "Error message");
                var feature = new Microsoft.AspNetCore.Diagnostics.ExceptionHandlerFeature
                {
                    Error = ex,

                };
                HttpContext.Features.Set(feature);
                ViewBag.Error = ex;
                return View("ErrorView");
            }

        }

        [HttpPost]
        public object EditPAPIvInjectionBpsRecord(string EditPAPIvInjectionSalesarr, BPSrequestpapIvInjection EditPAPIvInjectionHeaderData)
        {
            try
            {
                if (ModelState.IsValid)
                {


                    List<PAPIvInjectionCustomModel_Team> model = JsonSerializer.Deserialize<List<PAPIvInjectionCustomModel_Team>>(EditPAPIvInjectionSalesarr);



                    bool isSuccess = false;
                    var EmpidSessionValue = HttpContext.Session.GetString("EmpIdbps");
                    BPSrequestpapIvInjection bpspapIVrequest = _passDbContext.bps_request_papivinjection.FirstOrDefault(h => h.TrackingId == EditPAPIvInjectionHeaderData.TrackingId.Trim());
                    try
                    {
                        using (var connection = new MySqlConnection(_connectionString))
                        {
                            connection.Open();
                            using (var command = new MySqlCommand("sp_UpdateBpsPAPIVInjectionHeaderData", connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;

                                // Add parameters
                                command.Parameters.AddWithValue("@p_TrackingId", EditPAPIvInjectionHeaderData.TrackingId);
                                command.Parameters.AddWithValue("@p_DiscountFromDate", EditPAPIvInjectionHeaderData.DiscountFromDate);
                                command.Parameters.AddWithValue("@p_DiscountToDate", EditPAPIvInjectionHeaderData.DiscountToDate);
                                command.Parameters.AddWithValue("@p_UpdatedBy", EmpidSessionValue);
                                command.ExecuteNonQuery();

                            }
                        }

                        using (var connection = new MySqlConnection(_connectionString))
                        {
                            connection.Open();
                            MySqlTransaction trans = connection.BeginTransaction();
                            try
                            {

                                using (var command = new MySqlCommand("delete from bps_salesrecord_papivinjection where  BPS_Record_ID = " + bpspapIVrequest.BPS_Record_ID + "", connection))
                                {
                                    command.Transaction = trans;
                                    command.ExecuteNonQuery();
                                }
                                foreach (var item in model)
                                {
                                    string Team = item.Team;


                                    foreach (var p in item.ProductArr)
                                    {
                                        string PackCode = p.PackCode;
                                        string Discount = p.Discount;
                                        string Capping = p.Capping;

                                        using (var command = new MySqlCommand("sp_UpdateBpsIVInjectionSalesData", connection))
                                        {
                                            command.CommandType = CommandType.StoredProcedure;
                                            command.Parameters.Add(new MySqlParameter("@p_BPS_Record_ID", bpspapIVrequest.BPS_Record_ID));
                                            command.Parameters.Add(new MySqlParameter("@p_Team", Team));
                                            command.Parameters.Add(new MySqlParameter("@p_PackCode", PackCode));
                                            command.Parameters.Add(new MySqlParameter("@p_Discount", Discount));
                                            command.Parameters.Add(new MySqlParameter("@p_UpdatedBy", EmpidSessionValue));
                                            command.Parameters.Add(new MySqlParameter("@p_Capping", Capping));

                                            command.Transaction = trans;
                                            using (MySqlDataReader reader = command.ExecuteReader())
                                            {
                                                while (reader.Read())
                                                {
                                                }
                                            }
                                        }

                                    }

                                }

                                trans.Commit();

                            }
                            catch (Exception ex)
                            {
                                trans.Rollback();
                                throw;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }

                    isSuccess = true;
                }

                return null;
            }
            catch (Exception ex)
            {
                DateTime timestampValue = DateTime.Now; // Replace with the desired DateTime value

                GlobalClass.LogException(_passDbContext, ex, nameof(EditPAPIvInjectionBpsRecord), "Error message");
                var feature = new Microsoft.AspNetCore.Diagnostics.ExceptionHandlerFeature
                {
                    Error = ex,

                };
                HttpContext.Features.Set(feature);
                ViewBag.Error = ex;
                return View("ErrorView");
            }



        }

        [HttpPost]
        public ActionResult BPSPAPPharmaciesApproval(string WlstId, string comments, string TrackingId, string PAPType)
        {
            var EmpidSessionValue = HttpContext.Session.GetString("EmpIdbps");

            try
            {
                paptype paptype = _passDbContext.paptype.FirstOrDefault(h => h.PAPType == PAPType);

                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand("WF_PerformAction_TRK_PAP", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("p_WorklistId", WlstId);
                        command.Parameters.AddWithValue("p_Action", "Approved");
                        command.Parameters.AddWithValue("p_Comments", comments);
                        command.Parameters.AddWithValue("p_User", EmpidSessionValue);
                        command.Parameters.AddWithValue("p_Activity", null);
                        command.Parameters.AddWithValue("p_TrackingID", TrackingId);
                        command.Parameters.AddWithValue("p_PapType", paptype.TypeId);
                        // Execute the stored procedure
                        command.ExecuteNonQuery();
                        //command.CommandTimeout = 3000;

                    }
                }


                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }




            return null;
        }

        [HttpPost]
        public ActionResult BPSPAPPharmaciesObjection(string WlstId, string comments, string TrackingId, string PAPType)
        {
            var EmpidSessionValue = HttpContext.Session.GetString("EmpIdbps");

            try
            {
                paptype paptype = _passDbContext.paptype.FirstOrDefault(h => h.PAPType == PAPType);

                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand("WF_PerformAction_TRK_PAP", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("p_WorklistId", WlstId);
                        command.Parameters.AddWithValue("p_Action", "SendBackto");
                        command.Parameters.AddWithValue("p_Comments", comments);
                        command.Parameters.AddWithValue("p_User", EmpidSessionValue);
                        command.Parameters.AddWithValue("p_Activity", null);
                        command.Parameters.AddWithValue("p_TrackingID", TrackingId);
                        command.Parameters.AddWithValue("p_PapType", paptype.TypeId);
                        // Execute the stored procedure
                        command.ExecuteNonQuery();
                        //command.CommandTimeout = 3000;

                    }
                }


                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }




            return null;
        }


        
        public IActionResult PAPTrackingIDStatusDetails( string reqid, string selectedLabel)
        {
            return View();

        }





    }
}
