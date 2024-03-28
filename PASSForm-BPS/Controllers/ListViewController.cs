using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities;
using PASSForm_BPS.Models;
using PASSForm_BPS.ViewModel;
using System.Data;
using System.Diagnostics;
using static NuGet.Packaging.PackagingConstants;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.Identity.Client;
using MySqlX.XDevAPI.Common;
using System.Globalization;
using System.Dynamic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Drawing;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.Utilities.Zlib;
using static Google.Protobuf.Reflection.FieldDescriptorProto.Types;
using System.Text.Json;
using MySqlX.XDevAPI.Relational;
using Org.BouncyCastle.Crypto;
using System.Security.Cryptography;
using NuGet.Common;
using NuGet.Packaging;
using Org.BouncyCastle.Crypto.Tls;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Hosting;
using Org.BouncyCastle.Ocsp;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Xml.Linq;
using Org.BouncyCastle.Math.EC.Multiplier;

namespace PASSForm_BPS.Controllers
{
    public class ListViewController : Controller
    {

        private readonly PassDbContext _passDbContext;
        private readonly TestSalesDbContext _testSalesDbContext;
        private readonly string _connectionString; 

        public string _sqlconnection;
        private readonly IWebHostEnvironment _webHostEnvironment;

        DataTable dt_HCPDETAILS = new DataTable();
        DataTable dt_HCPDOCS = new DataTable();

        public ListViewController(PassDbContext passDbContext, TestSalesDbContext testSalesDbContext,IConfiguration configuration1, IWebHostEnvironment webHostEnvironment)
        {
            this._passDbContext = passDbContext;
            this._testSalesDbContext = testSalesDbContext;
            _webHostEnvironment = webHostEnvironment;
            _sqlconnection = configuration1.GetConnectionString("SqlConnection");
            _connectionString = configuration1.GetConnectionString("MySqlServerConnection");
           /* _sqlconnection = "Server=192.168.10.6;Database=DSRvsMREP;User ID=test2;Password=abc123+;Integrated Security=False;Trusted_Connection=False;Encrypt=False;";*/// configuration1.GetConnectionString("SqlConnection"); // configuration1.GetConnectionString["SqlConnection"];
        }
        // GET: ListViewController
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult ApprovedView(int id)
        {
            try
            {
                var Empid_SessionValue = HttpContext.Session.GetString("EmpIdbps");
                var Roleid = HttpContext.Session.GetString("roleid");

                if (Empid_SessionValue == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                if (Roleid == "1")
                {
                    ViewBag.RoleId = "1";
                }
                else
                {
                    ViewBag.RoleId = "2";
                }

                if(Roleid == "14" || Roleid == "18" || Roleid == "12" || Roleid == "13" || Roleid == "22" || Roleid == "23" )
                { ViewBag.IsRoleEdit = false; }
                else { ViewBag.IsRoleEdit = true; }
                

                var requests = _passDbContext.BpsRequests.FromSqlRaw("call sp_BPSRecordsList(" + Roleid + "," + Empid_SessionValue + ")").ToList();
                return View(requests);

            }
            catch (Exception ex)
            {
                DateTime timestampValue = DateTime.Now; // Replace with the desired DateTime value

                GlobalClass.LogException(_passDbContext, ex,  nameof(ApprovedView), "Error message");
                var feature = new Microsoft.AspNetCore.Diagnostics.ExceptionHandlerFeature
                {
                    Error = ex,
                    
                };
                HttpContext.Features.Set(feature);
                ViewBag.Error = ex;
                return View("Error");

            }
            

        }




        public ActionResult PendingView(int id, string thirdId)
        {
            try
            {
                var Empid_SessionValue = HttpContext.Session.GetString("EmpIdbps");
                var EmpRoleId = HttpContext.Session.GetString("roleid");

                if (Empid_SessionValue == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                ViewBag.RoleId = "2";

    
                if (EmpRoleId == "16")
                {
                    var requests = _passDbContext.Wf_Worklists.FromSqlRaw("call spGetMAActivityValues(" + Empid_SessionValue + ")").ToList();
                    return View(requests);
                }
                else if (EmpRoleId == "2" || EmpRoleId == "3")
                {
                    var requests = _passDbContext.Wf_Worklists.FromSqlRaw("call GetPendingApprovals(" + Empid_SessionValue + ")").ToList();
                    var requestss = _passDbContext.BpsRequests.FromSqlRaw("call sp_BPSRecordsList(" + EmpRoleId + "," + Empid_SessionValue + ")").ToList();

                    if (thirdId == "pending")
                    {
                        // Return view for 'requests'
                        return View("PendingView", requests);
                    }
                    else if (thirdId == null)
                    {
                        return View("PendingView", requests);

                    }
                    else
                    {
                        // Return view for 'requestss'
                        return View("ApprovedView", requestss);
                    }
                }
                else
                {
                    var requests = _passDbContext.Wf_Worklists.FromSqlRaw("call GetPendingApprovals(" + Empid_SessionValue + ")").ToList();
                    return View(requests);
                }





            }
            catch (Exception ex)
            {
                DateTime timestampValue = DateTime.Now; // Replace with the desired DateTime value

                GlobalClass.LogException(_passDbContext, ex, nameof(PendingView), "Error message");
                var feature = new Microsoft.AspNetCore.Diagnostics.ExceptionHandlerFeature
                {
                    Error = ex,

                };
                HttpContext.Features.Set(feature);
                ViewBag.Error = ex;
                return View("Error");
            }

        }

        public ActionResult DetailView1(string id, string anotherid, int thirdId)
        {
            return View();
        }

            // GET: ListViewController/Details/5
            public ActionResult Details(string id, string anotherid, int thirdId, string statusId, string screenId)
        {
            try
            {
                
                if (id == null)
                {
                    return RedirectToAction("PendingView", "ListView", 2);
                }



                // string CookieValue = "";
                // var myCookie = Request.Cookies["roleid"].Value;
                string empIdCookie = HttpContext.Session.GetString("roleid");  //HttpContext.Request.Cookies["roleid"];
                //read cookie from Request object  
                //string empIdCookie = Request.Cookies["roleid"].ToString();
                //var k = HttpContext.Request.Cookies["key"];
                //HttpCookie empIdCookie = Request.Cookies["roleid"];
                ViewBag.roleid = empIdCookie;
                if (empIdCookie != null)
                {
                    

                    if (empIdCookie == "1")
                    {
                        ViewBag.RoleId = "1";

                        //if (statusId == "InProgress")
                        //{

                        //    return RedirectToAction("ApprovedView", "ListView");
                        //}
                        var Empid_SessionValue = HttpContext.Session.GetString("EmpIdbps");

                        var bpsrcordquery = @"SELECT bpsreq.*, bpsreq.HCPREQID as User_Name, bpsreq.Status_ID as StatusType, bpsreq.CreatedBy as TMCode FROM bps_request bpsreq where TrackingID = '" + id + "'";
                        var bpsrcord = _passDbContext.BpsRequests.FromSqlRaw(bpsrcordquery).ToList();
                        var statusid = bpsrcord.FirstOrDefault()?.StatusId;
                        var bpsid = bpsrcord.FirstOrDefault()?.BpsRecordId;
                        var bpsComments = bpsrcord.FirstOrDefault()?.Comments;
                        ViewBag.Status = statusid;
                        ViewBag.Screen = screenId;
                        var macroBrickCodes = bpsrcord.FirstOrDefault()?.MacroBrickCode;
                        var macrobridnamequery = @"SELECT * FROM macrobricks where MacroBrickCode = '" + macroBrickCodes + "'";
                        var macname = _passDbContext.Macrobricks.FromSqlRaw(macrobridnamequery).ToList();

                        var distributerCodes = bpsrcord.FirstOrDefault()?.DistributerCode;
                        var distributernamequery = @"SELECT * FROM distributer where DistributerCode = '" + distributerCodes + "'";
                        var disname = _passDbContext.Distributers.FromSqlRaw(distributernamequery).ToList();

                        var hcpreqquery = @"SELECT * FROM hcprequest where TrackingID = '" + id + "' ";
                        var hcpreq = _passDbContext.Hcprequests.FromSqlRaw(hcpreqquery).ToList();

                        var tmcode = hcpreq.FirstOrDefault()?.Tmcode;
                        var area = hcpreq.FirstOrDefault()?.BaseArea;
                        var hcpreqid = hcpreq.FirstOrDefault()?.Hcpreqid;
                       


                        var ternamequery = @"SELECT * FROM tblterritorymappings where TerritoryCode = '" + tmcode + "'";
                        var tername = _passDbContext.Tblterritorymappings.FromSqlRaw(ternamequery).ToList();
                        var terempid = tername.FirstOrDefault()?.EmpId;

                        var userquery = @"Select * from users where EMP_ID = '" + terempid + "'";
                        var usr = _passDbContext.Users.FromSqlRaw(userquery).ToList();


                        var teamquery = @"select * from hspreqteam where HCPREQID = '" + hcpreqid + "'";
                        var team = _passDbContext.Hspreqteams.FromSqlRaw(teamquery).ToList();
                        var teamid = team.FirstOrDefault()?.TeamCode;

                        string teamcode = hcpreq.FirstOrDefault().TeamId;
                        var tquery = @"Select * from teams where TeamCode = '" + teamcode + "'";
                        var tname = _passDbContext.Teams.FromSqlRaw(tquery).ToList();

                        var File = @"SELECT * FROM uploadedfile WHERE TrackingId = '" + id + "' AND BPSID = '" + bpsid + "' AND Filetype = 'File'";

                        var FilesNames = _passDbContext.Uploadedfiles.FromSqlRaw(File).ToList();
 

                        //headerdatpostDatesaends
                        var preDates = new List<CustomModel_PreDateRange>();
                        var postDates = new List<CustomModel_PostDateRange>();
                        var totalval = new List<CustomModel_Customers>();
                        var chemistname = new List<Chemist>();
                        var resultsByChemistPreUnits = new Dictionary<string, List<ExpandoObject>>();
                        var resultsByChemistPostUnits = new Dictionary<string, List<ExpandoObject>>();
                        var resultsByChemistPreValues = new Dictionary<string, List<ExpandoObject>>();
                        var resultsByChemistPostValues = new Dictionary<string, List<ExpandoObject>>();
                        var resultsByChemistSku = new Dictionary<string, List<ExpandoObject>>();



                        if (int.TryParse(anotherid, out int parsedId))
                        {
                            var chemistCodes = _passDbContext.BpsSalesrecords
                                .Where(record => record.BpsRecordId == parsedId)
                                .Select(record => record.ChemistCode)
                                .Distinct()
                                .ToList();



                            foreach (var c in chemistCodes)
                            {
                                var chemistName = _passDbContext.Chemists
                                    .Where(record => record.ChemistCode == c)
                                  .Select(record => new Chemist
                                  {
                                      ChemistCode = record.ChemistCode,
                                      ChemistName = record.ChemistName

                                  })
                                    .Distinct()
                                    .ToList();

                                chemistname.AddRange(chemistName);

                                //var total = @"select * FROM pass_db.bps_salesrecord WHERE BPS_Record_ID = '"+ bpsrcord.FirstOrDefault().BpsRecordId + "' AND Chemist_Code = '"+ c + "';";
                                //var totalval = _passDbContext.BpsSalesrecords.FromSqlRaw(total).ToList();
                                //var PreTotalSal = totalval.FirstOrDefault()?.PreTotalSal;

                                var preDateRange = _passDbContext.BpsSalesrecords
                                    .Where(record => record.ChemistCode == c && record.BpsRecordId == bpsrcord.FirstOrDefault().BpsRecordId)
                                    .Select(record => new CustomModel_PreDateRange
                                    {
                                        Chemist_Code = record.ChemistCode,
                                        PreFromDate = record.PreFromdate,
                                        PreToDate = record.PreTodate,
                                        PreTotal = record.PreTotalSal,
                                        Contribution = record.Contribution,
                                        discountpercentage = record.DiscountPercentagePre,

                                    })
                                    .Distinct()
                                    .ToList();

                                preDates.AddRange(preDateRange);

                                var postDateRange = _passDbContext.BpsSalesrecords
                                        .Where(record => record.ChemistCode == c && record.BpsRecordId == bpsrcord.FirstOrDefault().BpsRecordId)
                                        .Select(record => new CustomModel_PostDateRange
                                        {
                                            Chemist_Code = record.ChemistCode,
                                            PostFromDate = record.PostFromdate,
                                            PostToDate = record.PostTodate,
                                            PostTotal = record.PostTotalSal,
                                            discountpostcentage = record.DiscountPercentagePost,
                                            //ROI = record.Roi,

                                            //TotalRoiPercentage = record.TotalRoiPercentage
                                        })
                                        .Distinct()
                                        .ToList();
                                postDates.AddRange(postDateRange);

                                var TotalValuesChemist = _passDbContext.BpsSalesrecords
                                    .Where(record => record.ChemistCode == c && record.BpsRecordId == bpsrcord.FirstOrDefault().BpsRecordId)
                                    .Select(record => new CustomModel_Customers
                                    {
                                        //grandtotal = record.GrandTotal,
                                        totalwithdiscount = record.TotalWithDiscount,
                                        totalwithoutdiscount = record.TotalWithoutDiscount,
                                        bpspercentage = record.BPSPercentage,
                                        totalroipercentage = record.TotalRoiPercentage,
                                    })
                                    .Distinct()
                                    .ToList();
                                totalval.AddRange(TotalValuesChemist);




                                //preunits
                                var p_pre_BPS_Record_ID_Param = new MySqlParameter("@p_BPS_Record_ID", anotherid);
                                var p_pre_Chemist_Code_Param = new MySqlParameter("@p_Chemist_Code", c);
                                var p_pre_Sales_Type_Param = new MySqlParameter("@p_SalesType", "pre");
                                var preresults = new List<ExpandoObject>();

                                using (var command = _passDbContext.Database.GetDbConnection().CreateCommand())
                                {
                                    command.CommandText = "CALL GeneratePivotTableUnitsPre(@p_BPS_Record_ID,@p_Chemist_Code,@p_SalesType)";
                                    command.Parameters.Add(p_pre_BPS_Record_ID_Param);
                                    command.Parameters.Add(p_pre_Chemist_Code_Param);
                                    command.Parameters.Add(p_pre_Sales_Type_Param);

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



                                //postunits
                                var p_post_BPS_Record_ID_Param = new MySqlParameter("@p_BPS_Record_ID", anotherid);
                                var p_post_Chemist_Code_Param = new MySqlParameter("@p_Chemist_Code", c);
                                var p_post_Sales_Type_Param = new MySqlParameter("@p_SalesType", "post");
                                var postresults = new List<ExpandoObject>();

                                using (var command = _passDbContext.Database.GetDbConnection().CreateCommand())
                                {
                                    command.CommandText = "CALL GeneratePivotTableUnitsPost(@p_BPS_Record_ID,@p_Chemist_Code,@p_SalesType)";
                                    command.Parameters.Add(p_post_BPS_Record_ID_Param);
                                    command.Parameters.Add(p_post_Chemist_Code_Param);
                                    command.Parameters.Add(p_post_Sales_Type_Param);

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
                                            postresults.Add(result);
                                        }
                                    }
                                }


                                //prevalues
                                var p_preVal_BPS_Record_ID_Param = new MySqlParameter("@p_BPS_Record_ID", anotherid);
                                var p_preVal_Chemist_Code_Param = new MySqlParameter("@p_Chemist_Code", c);
                                var p_preVal_Sales_Type_Param = new MySqlParameter("@p_SalesType", "pre");
                                var preValresults = new List<ExpandoObject>();

                                using (var command = _passDbContext.Database.GetDbConnection().CreateCommand())
                                {
                                    command.CommandText = "CALL GeneratePivotTableValuesPre(@p_BPS_Record_ID,@p_Chemist_Code,@p_SalesType)";
                                    command.Parameters.Add(p_preVal_BPS_Record_ID_Param);
                                    command.Parameters.Add(p_preVal_Chemist_Code_Param);
                                    command.Parameters.Add(p_preVal_Sales_Type_Param);

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

                                            preValresults.Add(result);
                                        }
                                    }
                                }


                                //postvalues
                                var p_postVal_BPS_Record_ID_Param = new MySqlParameter("@p_BPS_Record_ID", anotherid);
                                var p_postVal_Chemist_Code_Param = new MySqlParameter("@p_Chemist_Code", c);
                                var p_postVal_Sales_Type_Param = new MySqlParameter("@p_SalesType", "post");
                                var postValresults = new List<ExpandoObject>();

                                using (var command = _passDbContext.Database.GetDbConnection().CreateCommand())
                                {
                                    command.CommandText = "CALL GeneratePivotTableValuesPost(@p_BPS_Record_ID,@p_Chemist_Code,@p_SalesType)";
                                    command.Parameters.Add(p_postVal_BPS_Record_ID_Param);
                                    command.Parameters.Add(p_postVal_Chemist_Code_Param);
                                    command.Parameters.Add(p_postVal_Sales_Type_Param);

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

                                            postValresults.Add(result);
                                        }
                                    }
                                }
                                resultsByChemistPreUnits[c] = preresults;
                                resultsByChemistPostUnits[c] = postresults;
                                resultsByChemistPreValues[c] = preValresults;
                                resultsByChemistPostValues[c] = postValresults;


                            }

                            var DetailViewModel = new BPSDetailsViewModel
                            {
                                detailbps = bpsrcord,
                                macrobricks = macname,
                                distributers = disname,
                                requesthcp = hcpreq,
                                tblterritorymappings = tername,
                                teams = tname,
                                users = usr,
                                SalesUnitsPre = resultsByChemistPreUnits,
                                SalesUnitsPost = resultsByChemistPostUnits,
                                SalesValuesPre = resultsByChemistPreValues,
                                SalesValuesPost = resultsByChemistPostValues,
                                ChemistCodes = chemistCodes,
                                preDateRanges = preDates,
                                postDateRanges = postDates,
                                chemists = chemistname,
                                uploadedfiles = FilesNames,
                                customersmodels = totalval,



                            };




                            return View(DetailViewModel);

                        }

                    }
                    else
                    {
                        ViewBag.RoleId = "2";
                        //if (statusId == "InProgress")
                        //{

                        //    return RedirectToAction("PendingView", "ListView", 2);
                        //}
                        ViewBag.WorklistId = thirdId;
                        var Empid_SessionValue = HttpContext.Session.GetString("EmpIdbps");

                        var bpsrcordquery = @"SELECT bpsreq.*, bpsreq.HCPREQID as User_Name, bpsreq.Status_ID as StatusType, bpsreq.CreatedBy as TMCode FROM bps_request bpsreq where TrackingID = '" + id + "'";
                        var bpsrcord = _passDbContext.BpsRequests.FromSqlRaw(bpsrcordquery).ToList();
                        var statusid = bpsrcord.FirstOrDefault()?.StatusId;
                        var bpsid = bpsrcord.FirstOrDefault()?.BpsRecordId;
                        ViewBag.Status = statusid;
                        ViewBag.Screen = screenId;
                        var macroBrickCodes = bpsrcord.FirstOrDefault()?.MacroBrickCode;
                        var macrobridnamequery = @"SELECT * FROM macrobricks where MacroBrickCode = '" + macroBrickCodes + "'";
                        var macname = _passDbContext.Macrobricks.FromSqlRaw(macrobridnamequery).ToList();

                        var distributerCodes = bpsrcord.FirstOrDefault()?.DistributerCode;
                        var distributernamequery = @"SELECT * FROM distributer where DistributerCode = '" + distributerCodes + "'";
                        var disname = _passDbContext.Distributers.FromSqlRaw(distributernamequery).ToList();

                        var hcpreqquery = @"SELECT * FROM hcprequest where TrackingID = '" + id + "' ";
                        var hcpreq = _passDbContext.Hcprequests.FromSqlRaw(hcpreqquery).ToList();

                        var tmcode = hcpreq.FirstOrDefault()?.Tmcode;
                        var area = hcpreq.FirstOrDefault()?.BaseArea;
                        var hcpreqid = hcpreq.FirstOrDefault()?.Hcpreqid;

                        var ternamequery = @"SELECT * FROM tblterritorymappings where TerritoryCode = '" + tmcode + "'";
                        var tername = _passDbContext.Tblterritorymappings.FromSqlRaw(ternamequery).ToList();
                        var terempid = tername.FirstOrDefault()?.EmpId;

                        var userquery = @"Select * from users where EMP_ID = '" + terempid + "'";
                        var usr = _passDbContext.Users.FromSqlRaw(userquery).ToList();


                        var teamquery = @"select * from hspreqteam where HCPREQID = '" + hcpreqid + "'";
                        var team = _passDbContext.Hspreqteams.FromSqlRaw(teamquery).ToList();
                        var teamid = team.FirstOrDefault()?.TeamCode;

                        string teamcode = hcpreq.FirstOrDefault().TeamId;
                        var tquery = @"Select * from teams where TeamCode = '" + teamcode + "'";
                        var tname = _passDbContext.Teams.FromSqlRaw(tquery).ToList();

                        var File = @"SELECT * FROM uploadedfile WHERE TrackingId = '" + id + "' AND BPSID = '" + bpsid + "' AND Filetype = 'File'";

                        var FilesNames = _passDbContext.Uploadedfiles.FromSqlRaw(File).ToList();
                        //headerdatpostDatesaends
                        var preDates = new List<CustomModel_PreDateRange>();
                        var postDates = new List<CustomModel_PostDateRange>();
                        var totalval = new List<CustomModel_Customers>();
                        var chemistname = new List<Chemist>();
                        var resultsByChemistPreUnits = new Dictionary<string, List<ExpandoObject>>();
                        var resultsByChemistPostUnits = new Dictionary<string, List<ExpandoObject>>();
                        var resultsByChemistPreValues = new Dictionary<string, List<ExpandoObject>>();
                        var resultsByChemistPostValues = new Dictionary<string, List<ExpandoObject>>();
                        var resultsByChemistSku = new Dictionary<string, List<ExpandoObject>>();



                        if (int.TryParse(anotherid, out int parsedId))
                        {
                            var chemistCodes = _passDbContext.BpsSalesrecords
                                .Where(record => record.BpsRecordId == parsedId)
                                .Select(record => record.ChemistCode)
                                .Distinct()
                                .ToList();



                            foreach (var c in chemistCodes)
                            {
                                var chemistName = _passDbContext.Chemists
                                    .Where(record => record.ChemistCode == c)
                                  .Select(record => new Chemist
                                  {
                                      ChemistCode = record.ChemistCode,
                                      ChemistName = record.ChemistName

                                  })
                                    .Distinct()
                                    .ToList();

                                chemistname.AddRange(chemistName);

                                var preDateRange = _passDbContext.BpsSalesrecords
                                    .Where(record => record.ChemistCode == c && record.BpsRecordId == bpsrcord.FirstOrDefault().BpsRecordId)
                                    .Select(record => new CustomModel_PreDateRange
                                    {
                                        Chemist_Code = record.ChemistCode,
                                        PreFromDate = record.PreFromdate,
                                        PreToDate = record.PreTodate,
                                        PreTotal = record.PreTotalSal,
                                        Contribution = record.Contribution,
                                        discountpercentage = record.DiscountPercentagePre,

                                    })
                                    .Distinct()
                                    .ToList();

                                preDates.AddRange(preDateRange);

                                var postDateRange = _passDbContext.BpsSalesrecords
                                        .Where(record => record.ChemistCode == c && record.BpsRecordId == bpsrcord.FirstOrDefault().BpsRecordId)
                                        .Select(record => new CustomModel_PostDateRange
                                        {
                                            Chemist_Code = record.ChemistCode,
                                            PostFromDate = record.PostFromdate,
                                            PostToDate = record.PostTodate,
                                            PostTotal = record.PostTotalSal,
                                            discountpostcentage = record.DiscountPercentagePost,
                                            //ROI = record.Roi,
                                            //DiscountPercentage = record.DiscountPercentage,
                                            //TotalRoiPercentage = record.TotalRoiPercentage
                                        })
                                        .Distinct()
                                        .ToList();
                                postDates.AddRange(postDateRange);


                                var TotalValuesChemist = _passDbContext.BpsSalesrecords
                                    .Where(record => record.ChemistCode == c && record.BpsRecordId == bpsrcord.FirstOrDefault().BpsRecordId)
                                    .Select(record => new CustomModel_Customers
                                    {
                                        totalwithoutdiscount = record.TotalWithoutDiscount,
                                        totalwithdiscount = record.TotalWithDiscount,
                                        bpspercentage = record.BPSPercentage,
                                        totalroipercentage = record.TotalRoiPercentage,
                                    })
                                    .Distinct()
                                    .ToList();
                                totalval.AddRange(TotalValuesChemist);

                                //preunits
                                var p_pre_BPS_Record_ID_Param = new MySqlParameter("@p_BPS_Record_ID", anotherid);
                                var p_pre_Chemist_Code_Param = new MySqlParameter("@p_Chemist_Code", c);
                                var p_pre_Sales_Type_Param = new MySqlParameter("@p_SalesType", "pre");
                                var preresults = new List<ExpandoObject>();

                                using (var command = _passDbContext.Database.GetDbConnection().CreateCommand())
                                {
                                    command.CommandText = "CALL GeneratePivotTableUnitsPre(@p_BPS_Record_ID,@p_Chemist_Code,@p_SalesType)";
                                    command.Parameters.Add(p_pre_BPS_Record_ID_Param);
                                    command.Parameters.Add(p_pre_Chemist_Code_Param);
                                    command.Parameters.Add(p_pre_Sales_Type_Param);

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



                                //postunits
                                var p_post_BPS_Record_ID_Param = new MySqlParameter("@p_BPS_Record_ID", anotherid);
                                var p_post_Chemist_Code_Param = new MySqlParameter("@p_Chemist_Code", c);
                                var p_post_Sales_Type_Param = new MySqlParameter("@p_SalesType", "post");
                                var postresults = new List<ExpandoObject>();

                                using (var command = _passDbContext.Database.GetDbConnection().CreateCommand())
                                {
                                    command.CommandText = "CALL GeneratePivotTableUnitsPost(@p_BPS_Record_ID,@p_Chemist_Code,@p_SalesType)";
                                    command.Parameters.Add(p_post_BPS_Record_ID_Param);
                                    command.Parameters.Add(p_post_Chemist_Code_Param);
                                    command.Parameters.Add(p_post_Sales_Type_Param);

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
                                            postresults.Add(result);
                                        }
                                    }
                                }


                                //prevalues
                                var p_preVal_BPS_Record_ID_Param = new MySqlParameter("@p_BPS_Record_ID", anotherid);
                                var p_preVal_Chemist_Code_Param = new MySqlParameter("@p_Chemist_Code", c);
                                var p_preVal_Sales_Type_Param = new MySqlParameter("@p_SalesType", "pre");
                                var preValresults = new List<ExpandoObject>();

                                using (var command = _passDbContext.Database.GetDbConnection().CreateCommand())
                                {
                                    command.CommandText = "CALL GeneratePivotTableValuesPre(@p_BPS_Record_ID,@p_Chemist_Code,@p_SalesType)";
                                    command.Parameters.Add(p_preVal_BPS_Record_ID_Param);
                                    command.Parameters.Add(p_preVal_Chemist_Code_Param);
                                    command.Parameters.Add(p_preVal_Sales_Type_Param);

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

                                            preValresults.Add(result);
                                        }
                                    }
                                }


                                //postvalues
                                var p_postVal_BPS_Record_ID_Param = new MySqlParameter("@p_BPS_Record_ID", anotherid);
                                var p_postVal_Chemist_Code_Param = new MySqlParameter("@p_Chemist_Code", c);
                                var p_postVal_Sales_Type_Param = new MySqlParameter("@p_SalesType", "post");
                                var postValresults = new List<ExpandoObject>();

                                using (var command = _passDbContext.Database.GetDbConnection().CreateCommand())
                                {
                                    command.CommandText = "CALL GeneratePivotTableValuesPost(@p_BPS_Record_ID,@p_Chemist_Code,@p_SalesType)";
                                    command.Parameters.Add(p_postVal_BPS_Record_ID_Param);
                                    command.Parameters.Add(p_postVal_Chemist_Code_Param);
                                    command.Parameters.Add(p_postVal_Sales_Type_Param);

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

                                            postValresults.Add(result);
                                        }
                                    }
                                }
                                resultsByChemistPreUnits[c] = preresults;
                                resultsByChemistPostUnits[c] = postresults;
                                resultsByChemistPreValues[c] = preValresults;
                                resultsByChemistPostValues[c] = postValresults;


                            }

                            var DetailViewModel = new BPSDetailsViewModel
                            {
                                detailbps = bpsrcord,
                                macrobricks = macname,
                                distributers = disname,
                                requesthcp = hcpreq,
                                tblterritorymappings = tername,
                                teams = tname,
                                users = usr,
                                SalesUnitsPre = resultsByChemistPreUnits,
                                SalesUnitsPost = resultsByChemistPostUnits,
                                SalesValuesPre = resultsByChemistPreValues,
                                SalesValuesPost = resultsByChemistPostValues,
                                ChemistCodes = chemistCodes,
                                preDateRanges = preDates,
                                postDateRanges = postDates,
                                chemists = chemistname,
                                uploadedfiles = FilesNames,
                                customersmodels = totalval,

                            };


                            return View(DetailViewModel);

                        }
                    }

                    return View(null);
                }
                return View(null);
            }
            catch (Exception ex)
            {
                DateTime timestampValue = DateTime.Now; // Replace with the desired DateTime value

                GlobalClass.LogException(_passDbContext, ex, nameof(Objection), "Error message");
                var feature = new Microsoft.AspNetCore.Diagnostics.ExceptionHandlerFeature
                {
                    Error = ex,

                };
                HttpContext.Features.Set(feature);
                ViewBag.Error = ex;
                return View("Error");
            }

        }

        // GET: ListViewController/Create
        public IActionResult Create(string inputValue, int id)
        {
            try
            {
                string Tracking_ID = inputValue?.Trim();
                var EmpRoleId = HttpContext.Session.GetString("roleid");

                if (HttpContext.Session.GetString("EmpIdbps") == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                if (Tracking_ID == null)
                {
                    if (id == 2)
                    {
                        ViewBag.RoleId = "2";
                        // ViewBag.AlertMessage = "Please fill tracking id";
                        return PartialView("Create_PartialView");
                    }
                    else
                    {
                        ViewBag.RoleId = "1";
                        // ViewBag.AlertMessage = "Please fill tracking id";
                        return PartialView("Create_PartialView");
                    }



                }
                else
                {
                    try
                    {

                        var bpsrcordquery = @"SELECT bpsreq.*, bpsreq.HCPREQID as User_Name, bpsreq.Status_ID as StatusType, bpsreq.CreatedBy as TMCode FROM bps_request bpsreq where TrackingID = '" + Tracking_ID + "'";
                        var bpsrcord = _passDbContext.BpsRequests.FromSqlRaw(bpsrcordquery).ToList();

                        if (bpsrcord.Count > 0)
                        {
                            if(EmpRoleId == "1")
                            {
                                ViewBag.RoleId = "1";
                            }
                            else
                            {
                                ViewBag.RoleId = "2";
                            }
                            
                            ViewBag.AlertMessage = "Tracking id is already exist record";
                            return PartialView("Create_PartialView");
                        }

                        var TID = @"select * from hcprequest where TrackingId = '" + inputValue + "'";
                        var TIDs = _passDbContext.Hcprequests.FromSqlRaw(TID).ToList();
                        if (TIDs.Count == 0 )
                        {
                            ViewBag.RoleId = "1";
                            ViewBag.AlertMessage = "Tracking ID Not found";
                            return PartialView("Create_PartialView");
                        }
                        else
                        {
                            if (EmpRoleId == "1")
                            {
                                ViewBag.RoleId = "1";
                            }
                            if(EmpRoleId == "2" || EmpRoleId == "3")
                            {
                                ViewBag.RoleId = "2";
                            }
                            
                            var Empid_SessionValue = HttpContext.Session.GetString("EmpIdbps");
                            string param1 = inputValue;



                            var bpsrecords = _passDbContext.Hcprequests.FromSqlRaw("call sp_GetBpsRecords(@Tracking_ID)"
                                , new MySqlParameter("@Tracking_ID", param1)).ToList();
                            var tmcoderec = bpsrecords.Select(record => record.Tmcode).ToList();

                            var empidrec = _passDbContext.Tblterritorymappings
        .Where(otherEntity => tmcoderec.Contains(otherEntity.TerritoryCode) && otherEntity.RoleId == 4)
        .ToList();

                            var empid = empidrec.Select(record1 => record1.EmpId).ToList();


                            var tmquery = _passDbContext.Users
        .Where(otherEntity => empid.Contains(otherEntity.EmpId))
        .ToList();


                            var diskrec = _passDbContext.DisterMappings.FromSqlRaw("call sp_GetDistributerDetails(@Tracking_ID)"
                                , new MySqlParameter("@Tracking_ID", param1)).ToList();

                            var macrobrickrec = _passDbContext.TerbrickMappings.FromSqlRaw("call sp_GetMacroBrickDetails(@Tracking_ID)"
                  , new MySqlParameter("@Tracking_ID", param1)).ToList();

                            var teamname = _passDbContext.Hcprequests.FromSqlRaw("call sp_GetTemDetails(@Tracking_ID)"
            , new MySqlParameter("@Tracking_ID", param1)).ToList();

                            var tn = teamname.Select(x => x.TeamId).FirstOrDefault();

                            var teamquery = @"Select * from teams where TeamCode = '" + tn + "'";
                            var tname = _passDbContext.Teams.FromSqlRaw(teamquery).ToList();


                            var combinedViewModel = new BPSRequestListViewModel
                            {
                                requesthcp = bpsrecords,
                                terbrickMappings = macrobrickrec,
                                disterMappings = diskrec,
                                hspreqteams = teamname,
                                users = tmquery,
                                teams = tname
                            };

                            return View(combinedViewModel);
                        }
                        
                    }
                    catch (Exception ex)
                    {

                        DateTime timestampValue = DateTime.Now; // Replace with the desired DateTime value

                        GlobalClass.LogException(_passDbContext, ex,  nameof(Create), "Error message");
                        var feature = new Microsoft.AspNetCore.Diagnostics.ExceptionHandlerFeature
                        {
                            Error = ex,

                        };
                        HttpContext.Features.Set(feature);
                        ViewBag.Error = ex;
                        return View("Error");
                    }


                }
            }
            catch (Exception ex)
            {
                DateTime timestampValue = DateTime.Now; // Replace with the desired DateTime value

                GlobalClass.LogException(_passDbContext, ex,  nameof(Create), "Error message");
                var feature = new Microsoft.AspNetCore.Diagnostics.ExceptionHandlerFeature
                {
                    Error = ex,

                };
                HttpContext.Features.Set(feature);
                ViewBag.Error = ex;
                return View("Error");
            }

  

        }

        [HttpGet]
        public object CreateAccordion(string brickValue)
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

                GlobalClass.LogException(_passDbContext, ex, nameof(CreateAccordion), "Error message");
                return View("ErrorView");
            }




        }

        [HttpGet]
        public object GetMacrobrick(string disValue,string territorycode)
        {
            try
            {

           
            var macrobrickrecords = _passDbContext.DisMacMappings.FromSqlRaw("call sp_GETMacroBrick(@DisCode,@TerritoryCode)"
, new MySqlParameter("@DisCode", disValue)
, new MySqlParameter("@TerritoryCode", territorycode)).ToList();

            return Json(macrobrickrecords);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        [HttpPost]
        public bool CreateBpsRecord( string Salesarr1,  BpsRequest HeaderData1)
        {

                List<CustomModel_Customers> model = JsonSerializer.Deserialize<List<CustomModel_Customers>>(Salesarr1);


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

                   var hcpreqid = _passDbContext.Hcprequests.FromSqlRaw("select * from hcprequest where trackingid = '" + HeaderData1.TrackingId + "'").FirstOrDefault().Hcpreqid;
                    var result = _passDbContext.OutPutParameters
                        .FromSqlRaw("CALL sp_InsertBpsHeaderData(" + hcpreqid + ", '" + HeaderData1.MacroBrickCode + "', '" + HeaderData1.DistributerCode + "', '" + Convert.ToDateTime(HeaderData1.FromDate).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + "', '" + Convert.ToDateTime(HeaderData1.ToDate).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + "', '" + 1 + "', '" + EmpidSessionValue + "', '" + 0 + "', '" + 1005 + "','" + HeaderData1.TrackingId + "','" + HeaderData1.Comments + "', p_BPS_Record_ID)", outputParameter)
                        .ToList();

                    var bpsrecordid = outputParameter.Value;


                foreach (var item in model)
                {
                    string chmeistcode = item.ChemistCode;
                    string prefromdate = item.prefromDate;
                    string pretodate = item.pretoDate;
                    string postfromdate = item.postfromDate;
                    string posttodate = item.posttoDate;
                    string ROI = item.totalroipercentage;
                    string TotalWithOutDiscount = item.totalwithoutdiscount;
                    string TotalWithDiscount = item.totalwithdiscount;
                    string BPSPercentage = item.bpspercentage;
                    //string Contributer = item;

                    foreach (var item1 in item.ProductArr)
                    {
                        string ProdctName = item1.ProductName;
                        string ProdctCode = item1.productCode;
                        //string Distributer = item1.DistrubutorSales;
                        string Contributer = item1.Contribution;
                        string SalesType = item1.SalesType;
                        string PreTotal = item1.pretotal;
                        string PostTotal = item1.posttotal;

                        string Discountpercentage = item1.discountpercentage;
                        string Discountpostcentage = item1.discountpostcentage;
                        string PostProductDescription = item1.postproductDescription;
                        string PreProductActualDiscount = item1.preproductActualDiscount;
                        //string DiscountPercentage = item1.DiscountPercentage == null ?"0": item1.DiscountPercentage;
                        //string TotalRoiPercentage = item1.TotalRoiPercentage == null ? Convert.ToString(Convert.ToDecimal(ROI) + Convert.ToDecimal(item1.DiscountPercentage)) : item1.TotalRoiPercentage;

                        foreach (var item2 in item1.Sale)
                        {
                            string month = item2.Month;
                            string year = item2.Year;
                            string sku = item2.skuSales == null || item2.skuSales == "" ? "0" : item2.skuSales;
                            string valussales = item2.valueSales == null || item2.valueSales == "" ? "0" : item2.valueSales;
                            Decimal value = Convert.ToDecimal(valussales); // Convert.ToDecimal(item2.valueSales) == null ? 0 : Convert.ToDecimal(item2.valueSales);

                            var outputParameter1 = new MySqlParameter
                            {
                                ParameterName = "p_Record_ID",
                                MySqlDbType = MySqlDbType.Int32,
                                Direction = ParameterDirection.Output
                            };

                            var resultsales = _passDbContext.OutputParaMetersSales
                                .FromSqlRaw("CALL sp_InsertBpsSalesData(@p_BPS_Record_ID," +
                                "@p_Month, " +
                                "@p_Chemist_Code," +
                                " @p_ProductName," +
                                " @p_ActualDiscount," +
                                " @p_Contribution," +
                                " @p_SalesType," +
                                "@p_Sales_Sku," +
                                "@p_CreatedBy," +
                                "@p_Sales_Value," +
                                "@p_Pre_Fromdate," +
                                "@p_Pre_Todate," +
                                "@p_Post_Fromdate," +
                                "@p_Post_Todate," +
                                "@p_Year," +
                                " @p_PackCode," +
                                " @p_PreTotal," +
                                " @p_PostTotal," +
                                " @p_Roi," +
                                " @p_PostProDesc," +
                                " @p_DiscountPercentagePre," +
                                " @p_DiscountPercentagePost," +
                                " @p_TotalRoiPercentage," +
                                " @p_BPSPercentage," +
                                " @p_TotalWithOutDis," +
                                " @p_TotalWithDis," +
                                " p_Record_ID)",
                                new MySqlParameter("@p_BPS_Record_ID", bpsrecordid),
                                new MySqlParameter("@p_Month", month),
                                new MySqlParameter("@p_Chemist_Code", chmeistcode),
                                new MySqlParameter("@p_ProductName", ProdctName),
                                new MySqlParameter("@p_ActualDiscount", PreProductActualDiscount),
                                new MySqlParameter("@p_Contribution", Contributer),
                                new MySqlParameter("@p_SalesType", SalesType),
                                new MySqlParameter("@p_Sales_Sku", sku),
                                new MySqlParameter("@p_CreatedBy", EmpidSessionValue),
                                new MySqlParameter("@p_Sales_Value", value),
                                new MySqlParameter("@p_Pre_Fromdate", prefromdate),
                                new MySqlParameter("@p_Pre_Todate", pretodate),
                                new MySqlParameter("@p_Post_Fromdate", postfromdate),
                                new MySqlParameter("@p_Post_Todate", posttodate),
                                new MySqlParameter("@p_Year", year),
                                new MySqlParameter("@p_PackCode", ProdctCode),
                                new MySqlParameter("@p_PreTotal", PreTotal),
                                new MySqlParameter("@p_PostTotal", PostTotal),
                                new MySqlParameter("@p_Roi", ROI),
                                new MySqlParameter("@p_PostProDesc", PostProductDescription),
                                 new MySqlParameter("@p_DiscountPercentagePre", Discountpercentage),
                                  new MySqlParameter("@p_DiscountPercentagePost", Discountpostcentage),
                                  new MySqlParameter("@p_TotalRoiPercentage", ROI),
                                  new MySqlParameter("@p_BPSPercentage", BPSPercentage),
                                  new MySqlParameter("@p_TotalWithOutDis", TotalWithOutDiscount),
                                  new MySqlParameter("@p_TotalWithDis", TotalWithDiscount)
                                ,
                                outputParameter1)
                                .ToList();

                            int recordId = (int)outputParameter1.Value;
                        }

                    }
                }





                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand("SpStartWF", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("p_TrackingId", HeaderData1.TrackingId);
                        command.Parameters.AddWithValue("p_HCPReqId", hcpreqid);
                        command.Parameters.AddWithValue("p_BpsId", bpsrecordid);
                        command.Parameters.AddWithValue("p_User", EmpidSessionValue);

                        // Execute the stored procedure
                        command.ExecuteNonQuery();
                        command.CommandTimeout = 3000;

                    }
                }

            }
                catch (Exception ex)
                {
                    isSuccess = true;
                    return isSuccess;
                }

                isSuccess = true;
            
                return isSuccess;
            

        }

        [HttpPost]
        public IActionResult _Accordion_PartialView(int inputValue, List<MonthYearModel> selectedMonthsAndYear, int chemistId, string brickValue, string trackingid,string chemistpanelId)
        {
          
            double percentageval = (double)inputValue / 100.0;
            ViewBag.percentageval = percentageval;
            ViewBag.chemistId = chemistId;
            ViewBag.RoleId = "1";
            var Empid_SessionValue = HttpContext.Session.GetString("EmpIdbps");

            var MonthSelectedCount = selectedMonthsAndYear.Count();
            ViewBag.MonthCount = MonthSelectedCount;
            ViewBag.SalesByMonth = selectedMonthsAndYear;




            BpsRecordsViewModel bpsRecordsViewModel = new BpsRecordsViewModel();

            var bpsrecords = _passDbContext.Hcprequests.FromSqlRaw("call sp_GetBpsRecords(@Tracking_ID)"
                , new MySqlParameter("@Tracking_ID", trackingid)).ToList();
            bpsRecordsViewModel.hcprequests = bpsrecords[0];

            var query2 = @"select chemusr.*, chem.ChemistName from mac_chem_mapping chemusr
Right Join Chemist chem on chemusr.Chemist_Code = chem.Chemist_Code
 where chemusr.MacroBrickCode = '" + brickValue + "' and chemusr.Chemist_Code = '" + chemistId + "'";



            List<MacChemMapping> chemrecords = _passDbContext.MacChemMappings.FromSqlRaw(query2).ToList();

            List<UserChemMapping> lst = new List<UserChemMapping>();

            var query3 = @"SELECT hspT.*, T.TeamName FROM hspreqteam hspT
left join teams T on T.TeamCode = hspT.TeamCode
where HCPREQID = '" + trackingid + "'";



            List<string> teamNamesList = _passDbContext.Hspreqteams
    .FromSqlRaw(query3)
    .Select(item => item.TeamCode)
    .ToList();


            string teamNamesCsv = string.Join("','", teamNamesList);


            string teamNamesParameter = $"'{teamNamesCsv}'";




            foreach (var chemistname in chemrecords)
            {
                UserChemMapping userChemMapping = new UserChemMapping();
                userChemMapping.ChemistName = chemistname.ChemistCode;
                userChemMapping.ChemistCode = chemistname.ChemistCode;




                var parameters = new[]
                {
                            new SqlParameter("@ColumnToPivot", "Sales-Units"),
                            new SqlParameter("@ListToPivot", "[January],[February],[March],[April],[May],[June],[July],[August],[September],[October],[November],[December]"),
                            new SqlParameter("@Team", teamNamesCsv),
                            new SqlParameter("@Brick", brickValue),
                           new SqlParameter("@MonthPeriod", MonthSelectedCount),
                            new SqlParameter("@ChemistName",  chemistname.ChemistName )



                        };

                var query = "EXEC [dbo].[DynamicSalesData] " +
                            "@ColumnToPivot, @ListToPivot, @Team, @Brick, @MonthPeriod,@ChemistName";



                List<DsrHiltonDailySalesTeamToChemist202223> products = _testSalesDbContext.DsrHiltonDailySalesTeamToChemist202223s.FromSqlRaw(query, parameters).ToList();




                userChemMapping.dsrHiltonDailySalesTeamToChemists = products;

                lst.Add(userChemMapping);

            }


            bpsRecordsViewModel.userChemMappings = lst;
            bpsRecordsViewModel.ChemistPanelId = chemistpanelId;
            return View("_Accordion_PartialView", bpsRecordsViewModel);
        }

        [HttpPost]
        public IActionResult PreAcc(List<MonthYearModel> inputParameter, string prefromDate, string pretoDate, string BrickValue, string TeamName, string ChemistCode, string checkboxindex, double precontribution)
        {
            try
            {
                //codedone forallvaluecontribution
                double contributionval = (double)precontribution / 100.0;
                ViewBag.Contribution = contributionval;

                ViewBag.PreMonthYear = inputParameter;

                ViewBag.checkboxindex = checkboxindex;
                var Macname = _passDbContext.Macrobricks.FromSqlRaw("select * from macrobricks where macrobrickcode = '" + BrickValue + "' ").FirstOrDefault();

                var skuquery = @"DECLARE @columns AS NVARCHAR(MAX);
                DECLARE @sql AS NVARCHAR(MAX);
                DECLARE @startDate AS DATE = '" + prefromDate + "'" +
        " DECLARE @endDate AS DATE = '" + pretoDate + "'" +

                         "        SET @columns = STUFF((                                                                         " +
                         "            SELECT ',' + QUOTENAME(YearMonth)                                                          " +
                         "                                                                                                       " +
                         "            FROM(                                                                                      " +
                         "                SELECT DISTINCT CONCAT(DATENAME(yyyy, Date), ' ', DATENAME(mm, Date)) AS YearMonth, Month(Date) as mon, year(Date) as yea    " +
                         "                                                                                                       " +
                         "                FROM [dbo].[DSR_HiltonDailySales_TeamToChemist2022-23]                                " +
                         "                                                                                                       " +
                         "                WHERE TeamName = '" + TeamName + "' AND ClientCode = '" + ChemistCode + "' AND MicroBrickCode in (select distinct MinorBrickCode from MacroMicroBrickMaping where MinorBrickName = '" + Macname.MacroBrickName + "')                   " +
                         "                                                                                                       " +
                         "                    AND Date >= @startDate AND Date <= @endDate                                        " +
                         "            ) OrderedMonths    order by yea, mon                                                                         " +
                         "                                                                                                       " +
                         "            FOR XML PATH(''), TYPE                                                                     " +
                         "        ).value('.', 'NVARCHAR(MAX)'), 1, 1, '');                                                      " +
                         "                                                                                                       " +
                         "        SET @sql = N'                                                                                  " +
                         "    SELECT*                                                                                            " +
                         "    FROM                                                                                               " +
                         "    (                                                                                                  " +
                         "        SELECT                                                                                         " +
                         "            sal.PackCode,                                                                           " +
                         "            sal.ProductName,     " +
                         "          sal.Description,                                                                         " +
                         "            CONCAT(DATENAME(yyyy, sal.Date), '' '', DATENAME(mm, sal.Date)) AS YearMonth,              " +
                         "            sal.[Sales-Units]                                                                        " +
                         "        FROM [dbo].[DSR_HiltonDailySales_TeamToChemist2022-23] sal                                    " +
                         "        WHERE sal.TeamName = ''" + TeamName + "'' AND sal.ClientCode = ''" + ChemistCode + "'' AND sal.MicroBrickCode in (select distinct MinorBrickCode from MacroMicroBrickMaping where MinorBrickName = ''" + Macname.MacroBrickName + "'')          " +
                         "            AND sal.Date >= @startDate AND sal.Date <= @endDate                                        " +
                         "    ) t                                                                                                " +
                         "    PIVOT                                                                                              " +
                         "    (                                                                                                  " +
                         "        SUM([Sales-Units])                                                                           " +
                         "        FOR YearMonth IN(' + @columns + ')                                                             " +
                         "    ) AS pivot_Table;                                                                                  " +
                         "        ';  " +

                         "                                                                                                       " +
                         "EXEC sp_executesql @sql, N'@startDate DATE, @endDate DATE', @startDate, @endDate;                      " +
                         "        ";




                var results = new List<ExpandoObject>();

                using (var command = _testSalesDbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = skuquery;

                    _testSalesDbContext.Database.OpenConnection();

                    using (var reader = command.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var dataItem = new ExpandoObject() as IDictionary<string, object>;


                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    var columnName = reader.GetName(i);

                                    var columnValue = reader[i];
                                    if (columnValue != DBNull.Value)
                                    {
                                        if (columnValue is string stringValue)
                                        {

                                            if (string.IsNullOrEmpty(stringValue) || stringValue == "{}")
                                            {
                                                dataItem.Add(columnName, "0");
                                            }
                                            else
                                            {
                                                dataItem.Add(columnName, stringValue);
                                            }
                                        }
                                        else
                                        {
                                            dataItem.Add(columnName, columnValue);
                                        }
                                    }
                                    else
                                    {
                                        dataItem.Add(columnName, null);
                                    }



                                }

                                results.Add((ExpandoObject)dataItem);
                            }
                        }
                        else
                        {

                            return View("NoDataView");

                        }
                    }
                }

                //var results = new List<ExpandoObject>();
                //using (SqlConnection connection = new SqlConnection(_sqlconnection))
                //{
                //    connection.Open();

                //    using (SqlCommand command = new SqlCommand("sp_GetBPSPreSku", connection))
                //    {
                //        command.CommandType = CommandType.StoredProcedure;

                //        command.Parameters.AddWithValue("StartDate", prefromDate);
                //        command.Parameters.AddWithValue("EndDate", pretoDate);
                //        command.Parameters.AddWithValue("TeamName", TeamName);
                //        command.Parameters.AddWithValue("ChemistCode", ChemistCode);
                //        command.Parameters.AddWithValue("MacroBrickName", Macname.MacroBrickName);


                //        using (var reader = command.ExecuteReader())
                //        {

                //            if (reader.HasRows)
                //            {
                //                while (reader.Read())
                //                {
                //                    var dataItem = new ExpandoObject() as IDictionary<string, object>;


                //                    for (int i = 0; i < reader.FieldCount; i++)
                //                    {
                //                        var columnName = reader.GetName(i);

                //                        var columnValue = reader[i];
                //                        if (columnValue != DBNull.Value)
                //                        {
                //                            if (columnValue is string stringValue)
                //                            {

                //                                if (string.IsNullOrEmpty(stringValue) || stringValue == "{}")
                //                                {
                //                                    dataItem.Add(columnName, "0");
                //                                }
                //                                else
                //                                {
                //                                    dataItem.Add(columnName, stringValue);
                //                                }
                //                            }
                //                            else
                //                            {
                //                                dataItem.Add(columnName, columnValue);
                //                            }
                //                        }
                //                        else
                //                        {
                //                            dataItem.Add(columnName, null);
                //                        }



                //                    }

                //                    results.Add((ExpandoObject)dataItem);
                //                }
                //            }
                //            else
                //            {

                //                return View("NoDataView");

                //            }

                //        }
                //    }

                //}


                var valuequery = @"DECLARE @columns AS NVARCHAR(MAX);
    DECLARE @sql AS NVARCHAR(MAX);
    DECLARE @startDate AS DATE = '" + prefromDate + "'" +
       " DECLARE @endDate AS DATE = '" + pretoDate + "'" +

                        "        SET @columns = STUFF((                                                                         " +
                        "            SELECT ',' + QUOTENAME(YearMonth)                                                          " +
                        "                                                                                                       " +
                        "            FROM(                                                                                      " +
                        "                SELECT DISTINCT CONCAT(DATENAME(yyyy, Date), ' ', DATENAME(mm, Date)) AS YearMonth, Month(Date) as mon, year(Date) as yea     " +
                        "                                                                                                       " +
                        "                FROM [dbo].[DSR_HiltonDailySales_TeamToChemist2022-23]                                " +
                        "                                                                                                       " +
                        "                WHERE TeamName = '" + TeamName + "' AND ClientCode = '"+ ChemistCode + "' AND MicroBrickCode in (select distinct MinorBrickCode from MacroMicroBrickMaping where MinorBrickName = '" + Macname.MacroBrickName + "')                  " +
                        "                                                                                                       " +
                        "                    AND Date >= @startDate AND Date <= @endDate                                        " +
                        "            ) OrderedMonths   order by yea, mon                                                                         " +
                        "                                                                                                       " +
                        "            FOR XML PATH(''), TYPE                                                                     " +
                        "        ).value('.', 'NVARCHAR(MAX)'), 1, 1, '');                                                      " +
                        "                                                                                                       " +
                        "        SET @sql = N'                                                                                  " +
                        "    SELECT*                                                                                            " +
                        "    FROM                                                                                               " +
                        "    (                                                                                                  " +
                        "        SELECT                                                                                         " +
                        "            sal.PackCode,                                                                           " +
                        "            sal.ProductName,                                                                              " +
                        "            sal.Description,                                                                              " +
                        "            CONCAT(DATENAME(yyyy, sal.Date), '' '', DATENAME(mm, sal.Date)) AS YearMonth,              " +
                        "            sal.[Sales-ValueNP]                                                                        " +
                        "        FROM [dbo].[DSR_HiltonDailySales_TeamToChemist2022-23] sal                                    " +
                        "        WHERE sal.TeamName = ''" + TeamName + "'' AND sal.ClientCode = ''" + ChemistCode + "'' AND sal.MicroBrickCode in (select distinct MinorBrickCode from MacroMicroBrickMaping where MinorBrickName = ''" + Macname.MacroBrickName + "'')         " +
                        "            AND sal.Date >= @startDate AND sal.Date <= @endDate                                        " +
                        "    ) t                                                                                                " +
                        "    PIVOT                                                                                              " +
                        "    (                                                                                                  " +
                        "        SUM([Sales-ValueNP])                                                                           " +
                        "        FOR YearMonth IN(' + @columns + ')                                                             " +
                        "    ) AS pivot_Table;                                                                                  " +
                        "        ';                                                                                             " +
                        "                                                                                                       " +
                        "EXEC sp_executesql @sql, N'@startDate DATE, @endDate DATE', @startDate, @endDate;                      " +
                        "        ";


                var valrestule = new List<ExpandoObject>();

                using (var command = _testSalesDbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = valuequery;

                    _testSalesDbContext.Database.OpenConnection();

                    using (var reader = command.ExecuteReader())
                    {
                        // Check if there are rows to read
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var dataItem = new ExpandoObject() as IDictionary<string, object>;
                                //dataItem.Add("Contribution %", 00);
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    var columnName = reader.GetName(i);
                                    var columnValue = reader[i];
                                    if (columnValue != DBNull.Value)
                                    {
                                        if (columnValue is string stringValue)
                                        {
                                            // Check if the string is empty or contains only {}
                                            if (string.IsNullOrEmpty(stringValue) || stringValue == "{}")
                                            {
                                                dataItem.Add(columnName, "0");
                                            }
                                            else
                                            {
                                                dataItem.Add(columnName, stringValue);
                                            }
                                        }
                                        else
                                        {
                                            dataItem.Add(columnName, columnValue);
                                        }
                                    }
                                    else
                                    {
                                        dataItem.Add(columnName, null);
                                    }

                                    //dataItem.Add(columnName, columnValue);
                                }

                                valrestule.Add((ExpandoObject)dataItem);
                            }
                            //var totalValue = CalculateTotalValue(valrestule);
                            //valrestule.Add(CreateTotalValueExpando(totalValue));
                        }
                        else
                        {

                            return View("NoDataView");

                        }
                    }
                }







                var dynamicValueModel = new PreeAccordionModel
                {
                    Sku = results,
                    Value = valrestule

                };

                return View("PreAcc", dynamicValueModel);
            }
            catch (Exception ex)
            {
                DateTime timestampValue = DateTime.Now; // Replace with the desired DateTime value

                GlobalClass.LogException(_passDbContext, ex,  nameof(PreAcc), "Error message");
                var feature = new Microsoft.AspNetCore.Diagnostics.ExceptionHandlerFeature
                {
                    Error = ex,

                };
                HttpContext.Features.Set(feature);
                ViewBag.Error = ex;
                return View("Error");
            }

        }

        [HttpPost]
        public IActionResult EditPreAcc(List<MonthYearModel> inputParameter, string prefromDate, string pretoDate, string BrickValue, string TeamName, string ChemistCode, string checkboxindex, double precontribution)
        {
            try
            {
                //codedone forallvaluecontribution
                double contributionval = (double)precontribution / 100.0;
                ViewBag.Contribution = contributionval;

                ViewBag.PreMonthYear = inputParameter;

                ViewBag.checkboxindex = checkboxindex;
                var Macname = _passDbContext.Macrobricks.FromSqlRaw("select * from macrobricks where macrobrickcode = '" + BrickValue + "' ").FirstOrDefault();

                var skuquery = @"DECLARE @columns AS NVARCHAR(MAX);
                DECLARE @sql AS NVARCHAR(MAX);
                DECLARE @startDate AS DATE = '" + prefromDate + "'" +
        " DECLARE @endDate AS DATE = '" + pretoDate + "'" +

                         "        SET @columns = STUFF((                                                                         " +
                         "            SELECT ',' + QUOTENAME(YearMonth)                                                          " +
                         "                                                                                                       " +
                         "            FROM(                                                                                      " +
                         "                SELECT DISTINCT CONCAT(DATENAME(yyyy, Date), ' ', DATENAME(mm, Date)) AS YearMonth, Month(Date) as mon, year(Date) as yea    " +
                         "                                                                                                       " +
                         "                FROM [dbo].[DSR_HiltonDailySales_TeamToChemist2022-23]                                " +
                         "                                                                                                       " +
                         "                WHERE TeamName = '" + TeamName + "' AND ClientCode = '" + ChemistCode + "' AND MicroBrickCode in (select distinct MinorBrickCode from MacroMicroBrickMaping where MinorBrickName = '" + Macname.MacroBrickName + "')                   " +
                         "                                                                                                       " +
                         "                    AND Date >= @startDate AND Date <= @endDate                                        " +
                         "            ) OrderedMonths    order by yea, mon                                                                         " +
                         "                                                                                                       " +
                         "            FOR XML PATH(''), TYPE                                                                     " +
                         "        ).value('.', 'NVARCHAR(MAX)'), 1, 1, '');                                                      " +
                         "                                                                                                       " +
                         "        SET @sql = N'                                                                                  " +
                         "    SELECT*                                                                                            " +
                         "    FROM                                                                                               " +
                         "    (                                                                                                  " +
                         "        SELECT                                                                                         " +
                         "            sal.PackCode,                                                                           " +
                         "            sal.ProductName,     " +
                         "          sal.Description,                                                                         " +
                         "            CONCAT(DATENAME(yyyy, sal.Date), '' '', DATENAME(mm, sal.Date)) AS YearMonth,              " +
                         "            sal.[Sales-Units]                                                                        " +
                         "        FROM [dbo].[DSR_HiltonDailySales_TeamToChemist2022-23] sal                                    " +
                         "        WHERE sal.TeamName = ''" + TeamName + "'' AND sal.ClientCode = ''" + ChemistCode + "'' AND sal.MicroBrickCode in (select distinct MinorBrickCode from MacroMicroBrickMaping where MinorBrickName = ''" + Macname.MacroBrickName + "'')          " +
                         "            AND sal.Date >= @startDate AND sal.Date <= @endDate                                        " +
                         "    ) t                                                                                                " +
                         "    PIVOT                                                                                              " +
                         "    (                                                                                                  " +
                         "        SUM([Sales-Units])                                                                           " +
                         "        FOR YearMonth IN(' + @columns + ')                                                             " +
                         "    ) AS pivot_Table;                                                                                  " +
                         "        ';  " +

                         "                                                                                                       " +
                         "EXEC sp_executesql @sql, N'@startDate DATE, @endDate DATE', @startDate, @endDate;                      " +
                         "        ";




                var results = new List<ExpandoObject>();

                using (var command = _testSalesDbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = skuquery;

                    _testSalesDbContext.Database.OpenConnection();

                    using (var reader = command.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var dataItem = new ExpandoObject() as IDictionary<string, object>;


                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    var columnName = reader.GetName(i);

                                    var columnValue = reader[i];
                                    if (columnValue != DBNull.Value)
                                    {
                                        if (columnValue is string stringValue)
                                        {

                                            if (string.IsNullOrEmpty(stringValue) || stringValue == "{}")
                                            {
                                                dataItem.Add(columnName, "0");
                                            }
                                            else
                                            {
                                                dataItem.Add(columnName, stringValue);
                                            }
                                        }
                                        else
                                        {
                                            dataItem.Add(columnName, columnValue);
                                        }
                                    }
                                    else
                                    {
                                        dataItem.Add(columnName, null);
                                    }



                                }

                                results.Add((ExpandoObject)dataItem);
                            }
                        }
                        else
                        {

                            return View("NoDataView");

                        }
                    }
                }

                //var results = new List<ExpandoObject>();
                //using (SqlConnection connection = new SqlConnection(_sqlconnection))
                //{
                //    connection.Open();

                //    using (SqlCommand command = new SqlCommand("sp_GetBPSPreSku", connection))
                //    {
                //        command.CommandType = CommandType.StoredProcedure;

                //        command.Parameters.AddWithValue("StartDate", prefromDate);
                //        command.Parameters.AddWithValue("EndDate", pretoDate);
                //        command.Parameters.AddWithValue("TeamName", TeamName);
                //        command.Parameters.AddWithValue("ChemistCode", ChemistCode);
                //        command.Parameters.AddWithValue("MacroBrickName", Macname.MacroBrickName);


                //        using (var reader = command.ExecuteReader())
                //        {

                //            if (reader.HasRows)
                //            {
                //                while (reader.Read())
                //                {
                //                    var dataItem = new ExpandoObject() as IDictionary<string, object>;


                //                    for (int i = 0; i < reader.FieldCount; i++)
                //                    {
                //                        var columnName = reader.GetName(i);

                //                        var columnValue = reader[i];
                //                        if (columnValue != DBNull.Value)
                //                        {
                //                            if (columnValue is string stringValue)
                //                            {

                //                                if (string.IsNullOrEmpty(stringValue) || stringValue == "{}")
                //                                {
                //                                    dataItem.Add(columnName, "0");
                //                                }
                //                                else
                //                                {
                //                                    dataItem.Add(columnName, stringValue);
                //                                }
                //                            }
                //                            else
                //                            {
                //                                dataItem.Add(columnName, columnValue);
                //                            }
                //                        }
                //                        else
                //                        {
                //                            dataItem.Add(columnName, null);
                //                        }



                //                    }

                //                    results.Add((ExpandoObject)dataItem);
                //                }
                //            }
                //            else
                //            {

                //                return View("NoDataView");

                //            }

                //        }
                //    }

                //}


                var valuequery = @"DECLARE @columns AS NVARCHAR(MAX);
    DECLARE @sql AS NVARCHAR(MAX);
    DECLARE @startDate AS DATE = '" + prefromDate + "'" +
       " DECLARE @endDate AS DATE = '" + pretoDate + "'" +

                        "        SET @columns = STUFF((                                                                         " +
                        "            SELECT ',' + QUOTENAME(YearMonth)                                                          " +
                        "                                                                                                       " +
                        "            FROM(                                                                                      " +
                        "                SELECT DISTINCT CONCAT(DATENAME(yyyy, Date), ' ', DATENAME(mm, Date)) AS YearMonth, Month(Date) as mon, year(Date) as yea     " +
                        "                                                                                                       " +
                        "                FROM [dbo].[DSR_HiltonDailySales_TeamToChemist2022-23]                                " +
                        "                                                                                                       " +
                        "                WHERE TeamName = '" + TeamName + "' AND ClientCode = '" + ChemistCode + "' AND MicroBrickCode in (select distinct MinorBrickCode from MacroMicroBrickMaping where MinorBrickName = '" + Macname.MacroBrickName + "')                  " +
                        "                                                                                                       " +
                        "                    AND Date >= @startDate AND Date <= @endDate                                        " +
                        "            ) OrderedMonths   order by yea, mon                                                                         " +
                        "                                                                                                       " +
                        "            FOR XML PATH(''), TYPE                                                                     " +
                        "        ).value('.', 'NVARCHAR(MAX)'), 1, 1, '');                                                      " +
                        "                                                                                                       " +
                        "        SET @sql = N'                                                                                  " +
                        "    SELECT*                                                                                            " +
                        "    FROM                                                                                               " +
                        "    (                                                                                                  " +
                        "        SELECT                                                                                         " +
                        "            sal.PackCode,                                                                           " +
                        "            sal.ProductName,                                                                              " +
                        "            sal.Description,                                                                              " +
                        "            CONCAT(DATENAME(yyyy, sal.Date), '' '', DATENAME(mm, sal.Date)) AS YearMonth,              " +
                        "            sal.[Sales-ValueNP]                                                                        " +
                        "        FROM [dbo].[DSR_HiltonDailySales_TeamToChemist2022-23] sal                                    " +
                        "        WHERE sal.TeamName = ''" + TeamName + "'' AND sal.ClientCode = ''" + ChemistCode + "'' AND sal.MicroBrickCode in (select distinct MinorBrickCode from MacroMicroBrickMaping where MinorBrickName = ''" + Macname.MacroBrickName + "'')         " +
                        "            AND sal.Date >= @startDate AND sal.Date <= @endDate                                        " +
                        "    ) t                                                                                                " +
                        "    PIVOT                                                                                              " +
                        "    (                                                                                                  " +
                        "        SUM([Sales-ValueNP])                                                                           " +
                        "        FOR YearMonth IN(' + @columns + ')                                                             " +
                        "    ) AS pivot_Table;                                                                                  " +
                        "        ';                                                                                             " +
                        "                                                                                                       " +
                        "EXEC sp_executesql @sql, N'@startDate DATE, @endDate DATE', @startDate, @endDate;                      " +
                        "        ";


                var valrestule = new List<ExpandoObject>();

                using (var command = _testSalesDbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = valuequery;

                    _testSalesDbContext.Database.OpenConnection();

                    using (var reader = command.ExecuteReader())
                    {
                        // Check if there are rows to read
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var dataItem = new ExpandoObject() as IDictionary<string, object>;
                                //dataItem.Add("Contribution %", 00);
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    var columnName = reader.GetName(i);
                                    var columnValue = reader[i];
                                    if (columnValue != DBNull.Value)
                                    {
                                        if (columnValue is string stringValue)
                                        {
                                            // Check if the string is empty or contains only {}
                                            if (string.IsNullOrEmpty(stringValue) || stringValue == "{}")
                                            {
                                                dataItem.Add(columnName, "0");
                                            }
                                            else
                                            {
                                                dataItem.Add(columnName, stringValue);
                                            }
                                        }
                                        else
                                        {
                                            dataItem.Add(columnName, columnValue);
                                        }
                                    }
                                    else
                                    {
                                        dataItem.Add(columnName, null);
                                    }

                                    //dataItem.Add(columnName, columnValue);
                                }

                                valrestule.Add((ExpandoObject)dataItem);
                            }
                            //var totalValue = CalculateTotalValue(valrestule);
                            //valrestule.Add(CreateTotalValueExpando(totalValue));
                        }
                        else
                        {

                            return View("NoDataView");

                        }
                    }
                }







                var dynamicValueModel = new PreeAccordionModel
                {
                    Sku = results,
                    Value = valrestule

                };

                return View("EditPreAcc", dynamicValueModel);
            }
            catch (Exception ex)
            {
                DateTime timestampValue = DateTime.Now; // Replace with the desired DateTime value

                GlobalClass.LogException(_passDbContext, ex, nameof(PreAcc), "Error message");
                var feature = new Microsoft.AspNetCore.Diagnostics.ExceptionHandlerFeature
                {
                    Error = ex,

                };
                HttpContext.Features.Set(feature);
                ViewBag.Error = ex;
                return View("Error");
            }

        }



        [HttpPost]
        public IActionResult PostAcc(List<MonthYearModel> postinputParameter, string postfromDate, string posttoDate, string BrickValue, string TeamName, string ChemistCode, string postcheckboxindex, string prefromDate, string pretoDate)
        {
            try
            {
                ViewBag.postinputParameters = postinputParameter;
                ViewBag.postcheckboxindex = postcheckboxindex;
                //var Macname = _passDbContext.Macrobricks.FromSqlRaw("select * from macrobricks where macrobrickcode = '" + BrickValue + "' ").FirstOrDefault();

                //var postqueru = @"select distinct sal.PackCode, sal.ProductName,  sal.PackCode as id, sal.Description from [dbo].[DSR_HiltonDailySales_TeamToChemist2022-23] sal where sal.Date between '" + prefromDate + "' and '" + pretoDate + "' and TeamName = '" + TeamName + "'  AND sal.ClientCode = '" + ChemistCode + "' AND sal.MicroBrickCode in (select distinct MinorBrickCode from MacroMicroBrickMaping where MajorBrickName = '" + Macname.MacroBrickName + "')";

                //var postsal = _testSalesDbContext.DsrHiltonDailySalesTeamToChemist202223s.FromSqlRaw(postqueru).ToList();

                var postqueru = @"SELECT * FROM tblproduct where TeamName = '"+TeamName+"'";
                var postsal = _passDbContext.Tblproducts.FromSqlRaw(postqueru).ToList();
                ViewBag.Products = _passDbContext.Tblproducts.FromSqlRaw("select * from tblproduct").ToList();

                return View("PostAcc", postsal);
            }
            catch (Exception ex)
            {
                DateTime timestampValue = DateTime.Now; // Replace with the desired DateTime value

                GlobalClass.LogException(_passDbContext, ex,  nameof(PostAcc), "Error message");
                var feature = new Microsoft.AspNetCore.Diagnostics.ExceptionHandlerFeature
                {
                    Error = ex,

                };
                HttpContext.Features.Set(feature);
                ViewBag.Error = ex;
                return View("Error");
            }

        }

        [HttpPost]
        public IActionResult EditPostAcc(List<MonthYearModel> postinputParameter, string postfromDate, string posttoDate, string BrickValue, string TeamName, string ChemistCode, string postcheckboxindex, string prefromDate, string pretoDate)
        {
            try
            {
                ViewBag.postinputParameters = postinputParameter;
                ViewBag.postcheckboxindex = postcheckboxindex;
                //var Macname = _passDbContext.Macrobricks.FromSqlRaw("select * from macrobricks where macrobrickcode = '" + BrickValue + "' ").FirstOrDefault();

                //var postqueru = @"select distinct sal.PackCode, sal.ProductName,  sal.PackCode as id, sal.Description from [dbo].[DSR_HiltonDailySales_TeamToChemist2022-23] sal where sal.Date between '" + prefromDate + "' and '" + pretoDate + "' and TeamName = '" + TeamName + "'  AND sal.ClientCode = '" + ChemistCode + "' AND sal.MicroBrickCode in (select distinct MinorBrickCode from MacroMicroBrickMaping where MajorBrickName = '" + Macname.MacroBrickName + "')";

                //var postsal = _testSalesDbContext.DsrHiltonDailySalesTeamToChemist202223s.FromSqlRaw(postqueru).ToList();

                var postqueru = @"SELECT * FROM tblproduct where TeamName = '" + TeamName + "'";
                var postsal = _passDbContext.Tblproducts.FromSqlRaw(postqueru).ToList();
                ViewBag.Products = _passDbContext.Tblproducts.FromSqlRaw("select * from tblproduct").ToList();

                return View("EditPostAcc", postsal);
            }
            catch (Exception ex)
            {
                DateTime timestampValue = DateTime.Now; // Replace with the desired DateTime value

                GlobalClass.LogException(_passDbContext, ex, nameof(PostAcc), "Error message");
                var feature = new Microsoft.AspNetCore.Diagnostics.ExceptionHandlerFeature
                {
                    Error = ex,

                };
                HttpContext.Features.Set(feature);
                ViewBag.Error = ex;
                return View("Error");
            }

        }

        [HttpPost]
        public object CalBPS(string TrackingId, string Total, decimal discountpostcentageSum)
        {
            try
            {
                ViewBag.GrandTotal = Total;
                var hcpestimatesupportquery = @"SELECT * FROM hcprequest where TrackingID = '" + TrackingId + "'";

                var hcpestimatesupportresult = _passDbContext.Hcprequests.FromSqlRaw(hcpestimatesupportquery).ToList();

                decimal bpspercentagewithoutpercentage = 0;
                decimal netpercentagewithoutpercentage = 0;
                decimal bpspercentage = 0;
                decimal netpercentage = 0;
                decimal DiscountpostcentageSum = discountpostcentageSum;

                if (hcpestimatesupportresult.Count > 0)
                {
                    var firstItem = hcpestimatesupportresult.FirstOrDefault();

                    if (firstItem != null)
                    {
                        // Access the property you need
                        var estimatedSupport = firstItem.EstimatedSupport;
                        if (decimal.TryParse(estimatedSupport, out decimal estimatedSupportValue) && decimal.TryParse(Total, out decimal totalValue))
                        {
                            if (totalValue != 0)
                            {
                                bpspercentagewithoutpercentage = estimatedSupportValue / totalValue;
                                bpspercentage = bpspercentagewithoutpercentage * 100;
                                netpercentagewithoutpercentage = (estimatedSupportValue + DiscountpostcentageSum) / totalValue;
                                netpercentage = netpercentagewithoutpercentage * 100;

                                //ViewBag.bpspercentgaes = bpspercentage.ToString("0.00");

                                ViewBag.bpspercentgaes = bpspercentage;
                            


                            }
                            else
                            {

                            }
                        }
                        else
                        {

                        }

                    }
                }
                return Json(new { bpspercentage, netpercentage });
            }
            catch (Exception ex)
            {
                DateTime timestampValue = DateTime.Now; // Replace with the desired DateTime value

                GlobalClass.LogException(_passDbContext, ex,  nameof(CalBPS), "Error message");
                var feature = new Microsoft.AspNetCore.Diagnostics.ExceptionHandlerFeature
                {
                    Error = ex,

                };
                HttpContext.Features.Set(feature);
                ViewBag.Error = ex;
                return View("Error");
            }



        }

        [HttpPost]
        public object EditCalBPS(string TrackingId, string Total, decimal discountpostcentageSum)
        {
            try
            {
                ViewBag.GrandTotal = Total;
                var hcpestimatesupportquery = @"SELECT * FROM hcprequest where TrackingID = '" + TrackingId + "'";

                var hcpestimatesupportresult = _passDbContext.Hcprequests.FromSqlRaw(hcpestimatesupportquery).ToList();

                decimal bpspercentagewithoutpercenatge = 0;
                decimal netpercentagewithoutpercentage = 0;
                decimal bpspercentage = 0;
                decimal netpercentage = 0;
                decimal DiscountpostcentageSum = discountpostcentageSum;

                if (hcpestimatesupportresult.Count > 0)
                {
                    var firstItem = hcpestimatesupportresult.FirstOrDefault();

                    if (firstItem != null)
                    {
                        // Access the property you need
                        var estimatedSupport = firstItem.EstimatedSupport;
                        if (decimal.TryParse(estimatedSupport, out decimal estimatedSupportValue) && decimal.TryParse(Total, out decimal totalValue))
                        {
                            if (totalValue != 0)
                            {
                                bpspercentagewithoutpercenatge = estimatedSupportValue / totalValue;
                                bpspercentage = bpspercentagewithoutpercenatge * 100;
                                netpercentagewithoutpercentage = (estimatedSupportValue + DiscountpostcentageSum) / totalValue;
                                netpercentage = netpercentagewithoutpercentage * 100;

                                //ViewBag.bpspercentgaes = bpspercentage.ToString("0.00");

                                ViewBag.bpspercentgaes = bpspercentage;


                            }
                            else
                            {

                            }
                        }
                        else
                        {

                        }

                    }
                }
                return Json(new { bpspercentage, netpercentage });
            }
            catch (Exception ex)
            {
                DateTime timestampValue = DateTime.Now; // Replace with the desired DateTime value

                GlobalClass.LogException(_passDbContext, ex, nameof(CalBPS), "Error message");
                var feature = new Microsoft.AspNetCore.Diagnostics.ExceptionHandlerFeature
                {
                    Error = ex,

                };
                HttpContext.Features.Set(feature);
                ViewBag.Error = ex;
                return View("Error");
            }



        }


        // GET: ListViewController/Edit/5
        public ActionResult Edit(string id, string anotherId, string thirdId, string statusId)
        {
            try
            {
                var EmpRoleId = HttpContext.Session.GetString("roleid");
                if (id == null)
                {
                    return RedirectToAction("PendingView", "ListView", 2);
                }

                if(EmpRoleId == "1")
                {
                    ViewBag.RoleId = "1";
                }
                else
                {
                    ViewBag.RoleId = "2";
                }


                  
                
                
                var Empid_SessionValue = HttpContext.Session.GetString("EmpIdbps");
                ViewBag.Products = _passDbContext.Tblproducts.FromSqlRaw("select * from tblproduct").ToList();
                var bpsrcordquery = @"SELECT bpsreq.*, bpsreq.HCPREQID as User_Name, bpsreq.Status_ID as StatusType, bpsreq.CreatedBy as TMCode FROM bps_request bpsreq where TrackingID = '" + id + "'";
                var bpsrcord = _passDbContext.BpsRequests.FromSqlRaw(bpsrcordquery).ToList();
                var bpsid = bpsrcord.FirstOrDefault()?.BpsRecordId;
                var bpsComments = bpsrcord.FirstOrDefault()?.Comments;

                var macroBrickCodes = bpsrcord.FirstOrDefault()?.MacroBrickCode;
                var macrobridnamequery = @"SELECT * FROM macrobricks where MacroBrickCode = '" + macroBrickCodes + "'";
                var macname = _passDbContext.Macrobricks.FromSqlRaw(macrobridnamequery).ToList();

                var macchemnamequery = @"SELECT mcm.*, che.ChemistName FROM mac_chem_mapping mcm
Inner Join chemist che on
mcm.Chemist_Code = che.Chemist_Code
where mcm.MacroBrickCode = '"+ macroBrickCodes + "'";
                var macchemname = _passDbContext.MacChemMappings.FromSqlRaw(macchemnamequery).ToList();

                var distributerCodes = bpsrcord.FirstOrDefault()?.DistributerCode;
                var distributernamequery = @"SELECT * FROM distributer where DistributerCode = '" + distributerCodes + "'";
                var disname = _passDbContext.Distributers.FromSqlRaw(distributernamequery).ToList();

                var hcpreqquery = @"SELECT * FROM hcprequest where TrackingID  = '" + id + "' ";
                var hcpreq = _passDbContext.Hcprequests.FromSqlRaw(hcpreqquery).ToList();

                var tmcode = hcpreq.FirstOrDefault()?.Tmcode;
                var area = hcpreq.FirstOrDefault()?.BaseArea;
                var hcpreqid = hcpreq.FirstOrDefault()?.Hcpreqid;

                var ternamequery = @"SELECT * FROM tblterritorymappings where TerritoryCode = '" + tmcode + "'";
                var tername = _passDbContext.Tblterritorymappings.FromSqlRaw(ternamequery).ToList();
                var terempid = tername.FirstOrDefault()?.EmpId;

                var userquery = @"Select * from users where EMP_ID = '" + terempid + "'";
                var usr = _passDbContext.Users.FromSqlRaw(userquery).ToList();


                var teamquery = @"select * from hspreqteam where HCPREQID = '" + hcpreqid + "'";
                var team = _passDbContext.Hspreqteams.FromSqlRaw(teamquery).ToList();
                var teamid = team.FirstOrDefault()?.TeamCode;

                //var teamquery = @"select * from teams where TeamCode = '" + hcpreqid + "'";
                //var team = _passDbContext.Teams.FromSqlRaw(teamquery).ToList();
                //var teamid = team.FirstOrDefault()?.TeamCode;

                string teamcode = hcpreq.FirstOrDefault().TeamId;

                var tquery = @"Select * from teams where TeamCode = '" + teamcode + "'";
                var tname = _passDbContext.Teams.FromSqlRaw(tquery).ToList();

                //headerdatpostDatesaends
                var preDates = new List<CustomModel_PreDateRange>();
                var postDates = new List<CustomModel_PostDateRange>();
                var totalval = new List<CustomModel_Customers>();
                var chemistname = new List<Chemist>();
                var resultsByChemistPreUnits = new Dictionary<string, List<ExpandoObject>>();
                var resultsByChemistPostUnits = new Dictionary<string, List<ExpandoObject>>();
                var resultsByChemistPreValues = new Dictionary<string, List<ExpandoObject>>();
                var resultsByChemistPostValues = new Dictionary<string, List<ExpandoObject>>();
                var resultsByChemistSku = new Dictionary<string, List<ExpandoObject>>();



                if (int.TryParse(anotherId, out int parsedId))
                {
                    var chemistCodes = _passDbContext.BpsSalesrecords
                        .Where(record => record.BpsRecordId == parsedId)
                        .Select(record => record.ChemistCode)
                        .Distinct()
                        .ToList();



                    foreach (var c in chemistCodes)
                    {
                        var chemistName = _passDbContext.Chemists
                            .Where(record => record.ChemistCode == c)
                          .Select(record => new Chemist
                          {
                              ChemistCode = record.ChemistCode,
                              ChemistName = record.ChemistName

                          })
                            .Distinct()
                            .ToList();

                        chemistname.AddRange(chemistName);

                        var preDateRange = _passDbContext.BpsSalesrecords
                            .Where(record => record.ChemistCode == c && record.BpsRecordId == bpsrcord.FirstOrDefault().BpsRecordId)
                            .Select(record => new CustomModel_PreDateRange
                            {
                                Chemist_Code = record.ChemistCode,
                                PreFromDate = record.PreFromdate,
                                PreToDate = record.PreTodate,
                                PreTotal = record.PreTotalSal,
                                Contribution = record.Contribution,
                                discountpercentage = record.DiscountPercentagePre
                            })
                            .Distinct()
                            .ToList();

                        preDates.AddRange(preDateRange);

                        var postDateRange = _passDbContext.BpsSalesrecords
                                .Where(record => record.ChemistCode == c && record.BpsRecordId == bpsrcord.FirstOrDefault().BpsRecordId)
                                .Select(record => new CustomModel_PostDateRange
                                {
                                    Chemist_Code = record.ChemistCode,
                                    PostFromDate = record.PostFromdate,
                                    PostToDate = record.PostTodate,
                                    PostTotal = record.PostTotalSal,
                                    discountpostcentage = record.DiscountPercentagePost
                                    //ROI = record.Roi,
                                    //DiscountPercentage = record.DiscountPercentage,
                                    //TotalRoiPercentage = record.TotalRoiPercentage
                                })
                                .Distinct()
                                .ToList();
                        postDates.AddRange(postDateRange);

                        var TotalValuesChemist = _passDbContext.BpsSalesrecords
    .Where(record => record.ChemistCode == c && record.BpsRecordId == bpsrcord.FirstOrDefault().BpsRecordId)
    .Select(record => new CustomModel_Customers
    {
       totalwithoutdiscount = record.TotalWithoutDiscount,
        totalwithdiscount = record.TotalWithDiscount,
        bpspercentage = record.BPSPercentage,
        totalroipercentage = record.TotalRoiPercentage,
    })
    .Distinct()
    .ToList();
                        totalval.AddRange(TotalValuesChemist);

                        //preunits
                        var p_pre_BPS_Record_ID_Param = new MySqlParameter("@p_BPS_Record_ID", anotherId);
                        var p_pre_Chemist_Code_Param = new MySqlParameter("@p_Chemist_Code", c);
                        var p_pre_Sales_Type_Param = new MySqlParameter("@p_SalesType", "pre");
                        var preresults = new List<ExpandoObject>();

                        using (var command = _passDbContext.Database.GetDbConnection().CreateCommand())
                        {
                            command.CommandText = "CALL GeneratePivotTableUnitsPre(@p_BPS_Record_ID,@p_Chemist_Code,@p_SalesType)";
                            command.Parameters.Add(p_pre_BPS_Record_ID_Param);
                            command.Parameters.Add(p_pre_Chemist_Code_Param);
                            command.Parameters.Add(p_pre_Sales_Type_Param);

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



                        //postunits
                        var p_post_BPS_Record_ID_Param = new MySqlParameter("@p_BPS_Record_ID", anotherId);
                        var p_post_Chemist_Code_Param = new MySqlParameter("@p_Chemist_Code", c);
                        var p_post_Sales_Type_Param = new MySqlParameter("@p_SalesType", "post");
                        var postresults = new List<ExpandoObject>();

                        using (var command = _passDbContext.Database.GetDbConnection().CreateCommand())
                        {
                            command.CommandText = "CALL GeneratePivotTableUnitsPost(@p_BPS_Record_ID,@p_Chemist_Code,@p_SalesType)";
                            command.Parameters.Add(p_post_BPS_Record_ID_Param);
                            command.Parameters.Add(p_post_Chemist_Code_Param);
                            command.Parameters.Add(p_post_Sales_Type_Param);

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
                                    postresults.Add(result);
                                }
                            }
                        }


                        //prevalues
                        var p_preVal_BPS_Record_ID_Param = new MySqlParameter("@p_BPS_Record_ID", anotherId);
                        var p_preVal_Chemist_Code_Param = new MySqlParameter("@p_Chemist_Code", c);
                        var p_preVal_Sales_Type_Param = new MySqlParameter("@p_SalesType", "pre");
                        var preValresults = new List<ExpandoObject>();

                        using (var command = _passDbContext.Database.GetDbConnection().CreateCommand())
                        {
                            command.CommandText = "CALL GeneratePivotTableValuesPre(@p_BPS_Record_ID,@p_Chemist_Code,@p_SalesType)";
                            command.Parameters.Add(p_preVal_BPS_Record_ID_Param);
                            command.Parameters.Add(p_preVal_Chemist_Code_Param);
                            command.Parameters.Add(p_preVal_Sales_Type_Param);

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

                                    preValresults.Add(result);
                                }
                            }
                        }


                        //postvalues
                        var p_postVal_BPS_Record_ID_Param = new MySqlParameter("@p_BPS_Record_ID", anotherId);
                        var p_postVal_Chemist_Code_Param = new MySqlParameter("@p_Chemist_Code", c);
                        var p_postVal_Sales_Type_Param = new MySqlParameter("@p_SalesType", "post");
                        var postValresults = new List<ExpandoObject>();

                        using (var command = _passDbContext.Database.GetDbConnection().CreateCommand())
                        {
                            command.CommandText = "CALL GeneratePivotTableValuesPost(@p_BPS_Record_ID,@p_Chemist_Code,@p_SalesType)";
                            command.Parameters.Add(p_postVal_BPS_Record_ID_Param);
                            command.Parameters.Add(p_postVal_Chemist_Code_Param);
                            command.Parameters.Add(p_postVal_Sales_Type_Param);

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

                                    postValresults.Add(result);
                                }
                            }
                        }
                        resultsByChemistPreUnits[c] = preresults;
                        resultsByChemistPostUnits[c] = postresults;
                        resultsByChemistPreValues[c] = preValresults;
                        resultsByChemistPostValues[c] = postValresults;


                    }

                    var DetailViewModel = new BPSDetailsViewModel
                    {
                        detailbps = bpsrcord,
                        macrobricks = macname,
                        distributers = disname,
                        requesthcp = hcpreq,
                        tblterritorymappings = tername,
                        teams = tname,
                        users = usr,
                        SalesUnitsPre = resultsByChemistPreUnits,
                        SalesUnitsPost = resultsByChemistPostUnits,
                        SalesValuesPre = resultsByChemistPreValues,
                        SalesValuesPost = resultsByChemistPostValues,
                        ChemistCodes = chemistCodes,
                        preDateRanges = preDates,
                        postDateRanges = postDates,
                        chemists = chemistname,
                        customersmodels = totalval,
                        macChemMappings = macchemname,


                    };

                    ViewBag.Products = _passDbContext.Tblproducts.FromSqlRaw("select * from tblproduct").ToList();
                    return View(DetailViewModel);

                }



            }
            catch(Exception ex)
            {
                DateTime timestampValue = DateTime.Now; // Replace with the desired DateTime value

                GlobalClass.LogException(_passDbContext, ex, nameof(Edit), "Error message");
                var feature = new Microsoft.AspNetCore.Diagnostics.ExceptionHandlerFeature
                {
                    Error = ex,

                };
                HttpContext.Features.Set(feature);
                ViewBag.Error = ex;
                return View("Error");
            }

            return View();

        }

        [HttpPost]
        public IActionResult UpdateBpsRecords( string Updatedatalist, string trackingid, BpsRequest HeaderData1)
        {
   

            try
            {

                List<CustomModel_Customers> model = JsonSerializer.Deserialize<List<CustomModel_Customers>>(Updatedatalist);


                bool isSuccess = false;
                var EmpidSessionValue = HttpContext.Session.GetString("EmpIdbps");
                try
                {

                    string updatedBpsRecordId;

                    var hcpreqid = _passDbContext.Hcprequests.FromSqlRaw("select * from hcprequest where trackingid = '" + HeaderData1.TrackingId + "'").FirstOrDefault().Hcpreqid;
                    using (var connection = new MySqlConnection(_connectionString))
                    {
                        connection.Open();

                        using (var command = new MySqlCommand("sp_UpdateBpsHeaderData", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            // Add parameters to the command
                            command.Parameters.Add(new MySqlParameter("@p_HCPREQID", hcpreqid));
                            command.Parameters.Add(new MySqlParameter("@p_MacroBrickCode", HeaderData1.MacroBrickCode));
                            command.Parameters.Add(new MySqlParameter("@p_DistributerCode", HeaderData1.DistributerCode));
                            command.Parameters.Add(new MySqlParameter("@p_TrackingId", HeaderData1.TrackingId));
                            command.Parameters.Add(new MySqlParameter("@p_UpdatedBy", EmpidSessionValue));
                            command.Parameters.Add(new MySqlParameter("@p_Comment", HeaderData1.Comments));

                            // Add output parameter
                            var outputParameter = new MySqlParameter("@p_BpsRecordId", MySqlDbType.Int32);
                            outputParameter.Direction = ParameterDirection.Output;
                            command.Parameters.Add(outputParameter);

                            var reader = command.ExecuteReader();

                             updatedBpsRecordId = outputParameter.Value.ToString();

                        }
                    }
                    using (var connection = new MySqlConnection(_connectionString))
                    {
                        connection.Open();
                        MySqlTransaction trans = connection.BeginTransaction();
                        try
                        {

                            using (var command = new MySqlCommand("delete from bps_salesrecord where  BPS_Record_ID = " + updatedBpsRecordId + "", connection))
                            {
                                command.Transaction = trans;
                                command.ExecuteNonQuery();
                            }
                     


                        //var bpsid = _passDbContext.BpsRequests.FromSqlRaw("select * from bps_request where TrackingId = '" + HeaderData1.TrackingId + "'").FirstOrDefault().BpsRecordId;

                        foreach (var item in model)
                    {
                        string chmeistcode = item.ChemistCode;
                        string prefromdate = item.prefromDate;
                        string pretodate = item.pretoDate;
                        string postfromdate = item.postfromDate;
                        string posttodate = item.posttoDate;
                        string ROI = item.totalroipercentage;
                        string TotalWithDiscount = item.totalwithdiscount;
                        string TotalWithoutDiscount = item.totalwithoutdiscount;
                                string BPSPercentage = item.bpspercentage;
                        //string Contributer = item;

                        foreach (var item1 in item.ProductArr)
                        {
                            string ProdctName = item1.ProductName;
                            string ProdctCode = item1.productCode;
                            //string Distributer = item1.DistrubutorSales;
                            string Contributer = item1.Contribution;
                            string SalesType = item1.SalesType;
                            string PreTotal = item1.pretotal;
                            string PostTotal = item1.posttotal;

                            string Discountpercentage = item1.discountpercentage;
                            string Discountpostcentage = item1.discountpostcentage;
                            string PostProductDescription = item1.postproductDescription;
                            string PreProductActualDiscount = item1.preproductActualDiscount;
                            //string DiscountPercentage = item1.DiscountPercentage == null ?"0": item1.DiscountPercentage;
                            //string TotalRoiPercentage = item1.TotalRoiPercentage == null ? Convert.ToString(Convert.ToDecimal(ROI) + Convert.ToDecimal(item1.DiscountPercentage)) : item1.TotalRoiPercentage;

                            foreach (var item2 in item1.Sale)
                            {
                                string month = item2.Month;
                                string year = item2.Year;
                                string sku = item2.skuSales == null || item2.skuSales == "" ? "0" : item2.skuSales;
                                string valussales = item2.valueSales == null || item2.valueSales == "" ? "0" : item2.valueSales;
                                Decimal value = Convert.ToDecimal(valussales); // Convert.ToDecimal(item2.valueSales) == null ? 0 : Convert.ToDecimal(item2.valueSales);

                               
                                //using (var connection = new MySqlConnection(_connectionString))
                                //{
                                //    connection.Open();
                                //    MySqlTransaction trans = connection.BeginTransaction();
                                   
                                                                                                       


                                        using (var command = new MySqlCommand("sp_UpdateBpsSalesData", connection))
                                    {
                                        command.CommandType = CommandType.StoredProcedure;

                                        // Add parameters to the command
                                        command.Parameters.Add(new MySqlParameter("@p_BPS_Record_ID", updatedBpsRecordId));
                                        command.Parameters.Add(new MySqlParameter("@p_Month", month));
                                        command.Parameters.Add(new MySqlParameter("@p_Chemist_Code", chmeistcode));
                                        command.Parameters.Add(new MySqlParameter("@p_ProductName", ProdctName));
                                        command.Parameters.Add(new MySqlParameter("@p_ActualDiscount", PreProductActualDiscount));
                                        command.Parameters.Add(new MySqlParameter("@p_Contribution", Contributer));
                                        command.Parameters.Add(new MySqlParameter("@p_SalesType", SalesType));
                                        command.Parameters.Add(new MySqlParameter("@p_Sales_Sku", sku));
                                        command.Parameters.Add(new MySqlParameter("@p_UpdatedBy", EmpidSessionValue));
                                        
                                        command.Parameters.Add(new MySqlParameter("@p_Sales_Value", value));
                                        command.Parameters.Add(new MySqlParameter("@p_Pre_Fromdate", prefromdate));
                                        command.Parameters.Add(new MySqlParameter("@p_Pre_Todate", pretodate));
                                        command.Parameters.Add(new MySqlParameter("@p_Post_Fromdate", postfromdate));
                                        command.Parameters.Add(new MySqlParameter("@p_Post_Todate", posttodate));
                                        command.Parameters.Add(new MySqlParameter("@p_Year", year));
                                        command.Parameters.Add(new MySqlParameter("@p_PackCode", ProdctCode));
                                        command.Parameters.Add(new MySqlParameter("@p_PreTotal", PreTotal));
                                        command.Parameters.Add(new MySqlParameter("@p_PostTotal", PostTotal));
                                        command.Parameters.Add(new MySqlParameter("@p_Roi", ROI));
                                        command.Parameters.Add(new MySqlParameter("@p_PostProDesc", PostProductDescription));
                                        command.Parameters.Add(new MySqlParameter("@p_DiscountPercentagePre", Discountpercentage));
                                        command.Parameters.Add(new MySqlParameter("@p_DiscountPercentagePost", Discountpostcentage));
                                        command.Parameters.Add(new MySqlParameter("@p_TotalRoiPercentage", ROI));
                                        command.Parameters.Add(new MySqlParameter("@p_BPSPercentage", BPSPercentage));
                                            command.Parameters.Add(new MySqlParameter("@p_TotalWithDiscount", TotalWithDiscount));
                                            command.Parameters.Add(new MySqlParameter("@p_TotalWithoutDiscount", TotalWithoutDiscount));

                                            //var outputParameter = new MySqlParameter("@p_Out_BPS_Record_ID", MySqlDbType.Int32);
                                            //outputParameter.Direction = ParameterDirection.Output;
                                            //command.Parameters.Add(outputParameter);
                                            command.Transaction = trans;
                                       // var reader = command.ExecuteReader();
                                            using (MySqlDataReader reader = command.ExecuteReader())
                                            {
                                                while (reader.Read())
                                                {
                                                    //do your stuff...
                                                }
                                            }
                                            // var id = outputParameter.Value.ToString();
                                            //}


                                        }
                                //var outputParameter1 = new MySqlParameter
                                //{
                                //    ParameterName = "p_Record_ID",
                                //    MySqlDbType = MySqlDbType.Int32,
                                //    Direction = ParameterDirection.Output
                                //};

                                //var resultsales = _passDbContext.OutputParaMetersSales
                                //    .FromSqlRaw("CALL sp_InsertBpsSalesData(@p_BPS_Record_ID," +
                                //    "@p_Month, " +
                                //    "@p_Chemist_Code," +
                                //    " @p_ProductName," +
                                //    " @p_ActualDiscount," +
                                //    " @p_Contribution," +
                                //    " @p_SalesType," +
                                //    "@p_Sales_Sku," +
                                //    "@p_CreatedBy," +
                                //    "@p_Sales_Value," +
                                //    "@p_Pre_Fromdate," +
                                //    "@p_Pre_Todate," +
                                //    "@p_Post_Fromdate," +
                                //    "@p_Post_Todate," +
                                //    "@p_Year," +
                                //    " @p_PackCode," +
                                //    " @p_PreTotal," +
                                //    " @p_PostTotal," +
                                //    " @p_Roi," +
                                //    " @p_PostProDesc," +
                                //    " @p_DiscountPercentagePre," +
                                //    " @p_DiscountPercentagePost," +
                                //    " @p_TotalRoiPercentage," +
                                //    " @p_BPSPercentage," +
                                //    " @p_GrandTotal," +
                                //    " p_Record_ID)",
                                //    new MySqlParameter("@p_BPS_Record_ID", bpsrecordid),
                                //    new MySqlParameter("@p_Month", month),
                                //    new MySqlParameter("@p_Chemist_Code", chmeistcode),
                                //    new MySqlParameter("@p_ProductName", ProdctName),
                                //    new MySqlParameter("@p_ActualDiscount", PreProductActualDiscount),
                                //    new MySqlParameter("@p_Contribution", Contributer),
                                //    new MySqlParameter("@p_SalesType", SalesType),
                                //    new MySqlParameter("@p_Sales_Sku", sku),
                                //    new MySqlParameter("@p_CreatedBy", EmpidSessionValue),
                                //    new MySqlParameter("@p_Sales_Value", value),
                                //    new MySqlParameter("@p_Pre_Fromdate", prefromdate),
                                //    new MySqlParameter("@p_Pre_Todate", pretodate),
                                //    new MySqlParameter("@p_Post_Fromdate", postfromdate),
                                //    new MySqlParameter("@p_Post_Todate", posttodate),
                                //    new MySqlParameter("@p_Year", year),
                                //    new MySqlParameter("@p_PackCode", ProdctCode),
                                //    new MySqlParameter("@p_PreTotal", PreTotal),
                                //    new MySqlParameter("@p_PostTotal", PostTotal),
                                //    new MySqlParameter("@p_Roi", ROI),
                                //    new MySqlParameter("@p_PostProDesc", PostProductDescription),
                                //     new MySqlParameter("@p_DiscountPercentagePre", Discountpercentage),
                                //      new MySqlParameter("@p_DiscountPercentagePost", Discountpostcentage),
                                //      new MySqlParameter("@p_TotalRoiPercentage", ROI),
                                //      new MySqlParameter("@p_BPSPercentage", BPSPercentage),
                                //      new MySqlParameter("@p_GrandTotal", GrandTotal)
                                //    ,
                                //    outputParameter1)
                                //    .ToList();

                                //int recordId = (int)outputParameter1.Value;
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

                    //var EmpidSessionValue = HttpContext.Session.GetString("EmpIdbps");
                    //var bpsrcordquery = @"SELECT bpsreq.*, bpsreq.HCPREQID as User_Name, bpsreq.Status_ID as StatusType, bpsreq.CreatedBy as TMCode FROM pass_db.bps_request bpsreq where TrackingID = '" + trackingid + "'";
                    //var bpsrcord = _passDbContext.BpsRequests.FromSqlRaw(bpsrcordquery).FirstOrDefault();
                    //var bpsid = bpsrcord?.BpsRecordId;
                    //var hcpid = bpsrcord?.Hcpreqid;
                    //List<CustomModel_UpdateCustomers> dataList = new List<CustomModel_UpdateCustomers>();

                    //foreach (var dt in Updatedatalist)
                    //{
                    //    string chmeistcode = dt.ChemistCode;
                    //    string postfromdate = dt.postfromDate;
                    //    string posttodate = dt.posttoDate;
                    //    string ROI = dt.totalroipercentage;
                    //    string GrandTotal = dt.grandtotal;
                    //    string BPSPercentage = dt.bpspercentage;

                    //    CustomModel_UpdateCustomers updatedData = new CustomModel_UpdateCustomers
                    //    {
                    //        ChemistCode = chmeistcode,
                    //        postfromDate = postfromdate,
                    //        posttoDate = posttodate,
                    //        bpspercentage = BPSPercentage,
                    //        grandtotal = GrandTotal,
                    //        totalroipercentage = ROI,
                    //        ProductArr = new List<CustomModel_UpdateProducts>()
                    //    };

                    //    foreach (var p in dt.ProductArr)
                    //    {
                    //        string ProdctName = p.ProductName;
                    //        string ProdctCode = p.productCode;
                    //        string SalesType = p.SalesType;
                    //        string PreTotal = p.pretotal;
                    //        string PostTotal = p.posttotal;

                    //        string Discountpercentage = p.discountpercentage;
                    //        string Discountpostcentage = p.discountpostcentage;
                    //        //string ROI = p.roi;
                    //        string PostProductDescription = p.postproductDescription;


                    //        //string DiscountPercentage = p.DiscountPercentage == null ? "0" : p.DiscountPercentage;  //p.DiscountPercentage;
                    //        //string ROIPercentage = p.TotalRoiPercentage == null ? Convert.ToString(Convert.ToDecimal(ROI) + Convert.ToDecimal(p.DiscountPercentage)) : p.TotalRoiPercentage; //p.TotalRoiPercentage;

                    //        CustomModel_UpdateProducts productData = new CustomModel_UpdateProducts
                    //        {
                    //            ProductName = ProdctName,
                    //            productCode = ProdctCode,
                    //            SalesType = SalesType,
                    //            pretotal = PreTotal,
                    //            posttotal = PostTotal,
                    //            discountpercentage = Discountpercentage,
                    //            discountpostcentage = Discountpostcentage,
                    //            //roi = ROI,
                    //            postproductDescription = PostProductDescription,
                    //            Sale = new List<CustomModel_UpdateSales>(),
                    //            //DiscountPercentage = DiscountPercentage,
                    //            //TotalRoiPercentage = ROIPercentage,
                    //        };

                    //        foreach (var s in p.Sale)
                    //        {
                    //            string month = s.Month;
                    //            string year = s.Year;
                    //            string sku = s.skuSales;
                    //            string value = s.valueSales;

                    //            CustomModel_UpdateSales saleData = new CustomModel_UpdateSales
                    //            {
                    //                Month = month,
                    //                Year = year,
                    //                skuSales = sku,
                    //                valueSales = value
                    //            };

                    //            productData.Sale.Add(saleData);
                    //        }

                    //        updatedData.ProductArr.Add(productData);
                    //    }

                    //    dataList.Add(updatedData);
                    //}


                    //    foreach (var data in dataList)
                    //    {
                    //        foreach (var product in data.ProductArr)
                    //        {
                    //            foreach (var sale in product.Sale)
                    //            {
                    //                var resultsales = _passDbContext.OutputParaMetersSales
                    //                           .FromSqlRaw("CALL sp_UpdateSalesRecord(@p_Month, " +
                    //                           "@p_Chemist_Code," +
                    //                           " @p_ProductName," +
                    //                           " @p_SalesType," +
                    //                           "@p_Sales_Sku," +
                    //                           "@p_UpdatedBy," +
                    //                           "@p_Sales_Value," +
                    //                           "@p_Post_Fromdate," +
                    //                           "@p_Post_Todate," +
                    //                           "@p_Year," +
                    //                           " @p_PackCode," +
                    //                           " @p_PostTotal," +
                    //                           " @p_Roi," +
                    //                           " @p_DiscountPercentagePre," +
                    //                           " @p_DiscountPercentagePost," +
                    //                           //" @p_TotalRoiPercentage," +
                    //                           //" @p_BPSPercentage," +
                    //                           //" @p_GrandTotal," +
                    //                           " @p_BPS_Record_ID)",

                    //                           new MySqlParameter("@p_Month", sale.Month),
                    //                           new MySqlParameter("@p_Chemist_Code", data.ChemistCode),
                    //                           new MySqlParameter("@p_ProductName", product.ProductName),
                    //                           new MySqlParameter("@p_SalesType", product.SalesType),
                    //                           new MySqlParameter("@p_Sales_Sku", sale.skuSales),
                    //                           new MySqlParameter("@p_UpdatedBy", EmpidSessionValue),
                    //                           new MySqlParameter("@p_Sales_Value", Convert.ToDecimal(sale.valueSales)),
                    //                           new MySqlParameter("@p_Post_Fromdate", data.postfromDate),
                    //                           new MySqlParameter("@p_Post_Todate", data.posttoDate),
                    //                           new MySqlParameter("@p_Year", sale.Year),
                    //                           new MySqlParameter("@p_PackCode", product.productCode),
                    //                           new MySqlParameter("@p_PostTotal", product.posttotal),
                    //                           new MySqlParameter("@p_Roi", data.totalroipercentage),
                    //                           new MySqlParameter("@p_DiscountPercentagePre", product.discountpercentage),
                    //                           new MySqlParameter("@p_DiscountPercentagePost", product.discountpostcentage),
                    //                           //new MySqlParameter("@p_TotalRoiPercentage", data.totalroipercentage),
                    //                           //new MySqlParameter("@p_BPSPercentage", data.bpspercentage),
                    //                           //new MySqlParameter("@p_GrandTotal", data.grandtotal),
                    //                             new MySqlParameter("@p_BPS_Record_ID", bpsid)
                    //                           )
                    //                           .ToList();


                    //            var updaterec = @"UPDATE pass_db.bps_salesrecord SET  GrandTotal = '" + data.grandtotal + "', BPSPercentage = '" + data.bpspercentage + "', TotalRoiPercentage = '" + data.totalroipercentage + "' WHERE PackCode = '" + product.productCode + "' AND Chemist_Code = '" + data.ChemistCode + "' AND   BPS_Record_ID = '" + bpsid + "' ";


                    //        }
                    //        }

                    //    }
                    //  }

                    //using (MySqlConnection connection = new MySqlConnection(_connectionString))
                    //{
                    //    connection.Open();

                    //    using (MySqlCommand command = new MySqlCommand("SpStartWF", connection))
                    //    {
                    //        command.CommandType = CommandType.StoredProcedure;

                    //        command.Parameters.AddWithValue("p_TrackingId", trackingid);
                    //        command.Parameters.AddWithValue("p_HCPReqId", hcpid);
                    //        command.Parameters.AddWithValue("p_BpsId", bpsid);
                    //        command.Parameters.AddWithValue("p_User", EmpidSessionValue);

                    //        // Execute the stored procedure
                    //        command.ExecuteNonQuery();
                    //        command.CommandTimeout = 3000;

                    //    }
                    //}
                    return Json(new { success = true });
            }
            catch (Exception ex)
            {


            }

            return View();
        }




        // GET: ListViewController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ListViewController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }





        public IActionResult Objection(int trackingid, string comments)
        {
            try
            {
                var rej = @"Update bps_request
set Status_ID = 4
Where HCPREQID = '" + trackingid + "'";
                var rejbps = _passDbContext.Database.ExecuteSqlRaw(rej);

                var rejed = @"Update hcprequest
set Status_ID = 4, Comments = '" + comments + "' Where HCPREQID = '" + trackingid + "'";
                var rejhcp = _passDbContext.Database.ExecuteSqlRaw(rejed);

                return RedirectToAction("PendingView", "ListView");
            }
            catch (Exception ex)
            {

                DateTime timestampValue = DateTime.Now; // Replace with the desired DateTime value

                GlobalClass.LogException(_passDbContext, ex,  nameof(Objection), "Error message");
                var feature = new Microsoft.AspNetCore.Diagnostics.ExceptionHandlerFeature
                {
                    Error = ex,

                };
                HttpContext.Features.Set(feature);
                ViewBag.Error = ex;
                return View("Error");
            
        }


        }

        public IActionResult TrackingIDStatus()
        {
            try
            {
                //var empIdCookie = Request.Cookies["roleid"];
                string empIdCookie = HttpContext.Session.GetString("roleid");
                if (empIdCookie != null)
                {
                    if (empIdCookie == "1")
                    {
                        ViewBag.RoleId = "1";
                        return View();
                    }
                    else if (empIdCookie == "2" || empIdCookie == "3")
                    {
                        ViewBag.RoleId = "2";
                        return View();
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
                return View("Error");
            }
      
        }

        [HttpPost]
        public IActionResult TrackingIDStatusDetails(string TrackingId)
        {
            try
            {
                var EmpRoleId = HttpContext.Session.GetString("roleid");
                var recid = TrackingId.Remove(0, 5);// TrackingId.Substring(5, TrackingId.Length);
                var bps = _passDbContext.BpsRequests.FromSqlRaw("select bpsreq.*, bpsreq.HCPREQID as User_Name, bpsreq.Status_ID as StatusType, bpsreq.CreatedBy as TMCode from bps_request bpsreq where bps_record_id =" + recid + " ").FirstOrDefault();
              
                var TID = @"SELECT * FROM wf_instance where TrackingId = '" + bps.TrackingId + "'";

                var TIDs = _passDbContext.Wf_Instances.FromSqlRaw(TID).ToList();
                if (TIDs.Count == 0)
                {
                    if (EmpRoleId == "2" || EmpRoleId == "33")
                    {
                        ViewBag.RoleId = "2";
                    }
                    else
                    {
                        ViewBag.RoleId = "1";
                    }

                    ViewBag.AlertMessage = "Tracking ID Not found";
                    return View("TrackingIDStatusDetails");
                }
                else
                {
                    if (EmpRoleId == "2" || EmpRoleId == "33")
                    {
                        ViewBag.RoleId = "2";
                    }
                    else
                    {
                        ViewBag.RoleId = "1";
                    }

                    using (MySqlConnection connection = new MySqlConnection(_connectionString))
                    {
                        connection.Open();

                        using (MySqlCommand command = new MySqlCommand("GetTrackingStatus", connection))
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;

                            // Add parameters
                            command.Parameters.Add(new MySqlParameter("p_TrackingId", MySqlDbType.VarChar)
                            {
                                Value = TrackingId
                            });

                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                var result = new List<wf_worklist>();

                                while (reader.Read())
                                {
                                    var item = new wf_worklist();
                                    {
                                        item.WFWorklistId = (int)reader["WFWorklistId"];
                                        item.WFActivityInstanceId = reader["WFActivityInstanceId"] as int?;
                                        item.Destination = reader["Destination"] as string;
                                        item.Designation = reader["Designation"] as string;
                                        item.Action = reader["Action"] as string;
                                        item.ActionBy = reader["ActionBy"] as int?;
                                        item.Status = reader["Status"] as int?;
                                        item.StartDate = reader["StartDate"] as DateTime?;
                                        item.FinishDate = reader["FinishDate"] as DateTime?;

                                        item.TrackingId = reader["TrackingId"] as string;
                                        //item.HcpReqId = reader["HcpReqId"] as int?;
                                        //item.BpsId = reader["BpsId"] as int?;
                                        //item.TMCode = reader["TMCode"] as string;
                                        item.Statustype = reader["Statustype"] as string;
                                        //item.CreatedBy = reader["CreatedBy"] as string;
                                        item.ActionByName = reader["ActionByName"] as string;
                                    };

                                    result.Add(item);
                                }

                                return View("TrackingIDStatusDetails", result);
                            }
                        }
                    }
                }

            
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
                return View("Error");
            }

         }

        
        public IActionResult TrackingIDStatusComments(int id)
        {
            try
            {
                var idParameter = new MySqlParameter("id", id);
                var comments = _passDbContext.Wf_Comments
                    .FromSqlRaw("SELECT * FROM wf_comments WHERE WFWorklistId = @id", idParameter)
                    .ToList();
                var commentValues = comments.Select(comment => comment.Comments).ToList();

                var filepath = _passDbContext.Wf_Uploadfilespaths
    .FromSqlRaw("SELECT * FROM wf_uploadfilespath WHERE WFWorklistId = @id", idParameter)
    .ToList();
                var filePathValues = filepath.Select(filepath => filepath.FileName).ToList();

                var result = new
                {
                    Comments = commentValues,
                    FilePath = filePathValues
                };

                return Json(result);
            }
            catch (Exception ex)
            {
                DateTime timestampValue = DateTime.Now; // Replace with the desired DateTime value

                GlobalClass.LogException(_passDbContext, ex, nameof(Objection), "Error message");
                var feature = new Microsoft.AspNetCore.Diagnostics.ExceptionHandlerFeature
                {
                    Error = ex,

                };
                HttpContext.Features.Set(feature);
                ViewBag.Error = ex;
                return View("Error");
            }


 
        }




        [HttpPost]
        public IActionResult BPSApproval(List<IFormFile> Files, string WlstId, string comments, string TrackingId)
        {
            try
            {
                var EmpidSessionValue = HttpContext.Session.GetString("EmpIdbps");
                foreach (var fileName in Files)
                {
                    var result = _passDbContext.Wf_Uploadfilespaths
    .FromSqlRaw("CALL spBPSUploadFile(" + WlstId + ", '" + fileName.FileName + "')")
    .ToList();
                }

                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand("WF_PerformAction_TRK", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("p_WorklistId", WlstId);
                        command.Parameters.AddWithValue("p_Action", "Approved");
                        command.Parameters.AddWithValue("p_Comments", comments);
                        command.Parameters.AddWithValue("p_User", EmpidSessionValue);
                        command.Parameters.AddWithValue("p_Activity", null);
                        command.Parameters.AddWithValue("p_TrackingID", TrackingId);

                        // Execute the stored procedure
                        command.ExecuteNonQuery();
                        //command.CommandTimeout = 3000;

                    }
                }


                //var wlstParameter = new MySqlParameter("wlstid", 39);
                //var destination = _passDbContext.Wf_Worklists
                //    .FromSqlRaw("SELECT w.*, w.WFActivityInstanceId as TrackingId, w.WFActivityInstanceId AS HcpReqId, w.WFWorklistId as  BpsId, w.Action as TMCode, w.status as Statustype, w.FinishDate as CreatedBy,\r\n\r\nw.ActionBy as  ActionByName, w.StartDate as User_Name, w.StartDate as CreatedOn  FROM wf_worklist w where w.WFWorklistId = @wlstid", wlstParameter)
                //    .ToList();
                //var desValues = destination.Select(destination => destination.Destination).FirstOrDefault();



                //var user = @"SELECT * FROM pass_db.users WHERE EMP_ID = '" + desValues + "'";
                //var uemail = _passDbContext.Users.FromSqlRaw(user).ToList();
                //var useremails = uemail.FirstOrDefault()?.UserEmail;


                //GlobalClass.Email(useremails, TrackingId, "Approved");


                return Ok("Files uploaded successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult BPSReject(List<IFormFile> Files, string WlstId, string comments, string TrackingId)
        {
            try
            {
                var EmpidSessionValue = HttpContext.Session.GetString("EmpIdbps");
                foreach (var fileName in Files)
                {
                    var result = _passDbContext.Wf_Uploadfilespaths
    .FromSqlRaw("CALL spBPSUploadFile(" + WlstId + ", '" + fileName.FileName + "')")
    .ToList();
                }

                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand("WF_PerformAction_TRK", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("p_WorklistId", WlstId);
                        command.Parameters.AddWithValue("p_Action", "Rejected");
                        command.Parameters.AddWithValue("p_Comments", comments);
                        command.Parameters.AddWithValue("p_User", EmpidSessionValue);
                        command.Parameters.AddWithValue("p_Activity", null);
                        command.Parameters.AddWithValue("p_TrackingID", TrackingId);

                        // Execute the stored procedure
                        command.ExecuteNonQuery();
                        command.CommandTimeout = 3000;

                    }
                }



                return Ok("Files uploaded successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult BPSObjection(List<IFormFile> Files, string WlstId, string comments, string TrackingId)
        {
            try
            {
                var EmpidSessionValue = HttpContext.Session.GetString("EmpIdbps");
                foreach (var fileName in Files)
                {
                    var result = _passDbContext.Wf_Uploadfilespaths
    .FromSqlRaw("CALL spBPSUploadFile(" + WlstId + ", '" + fileName.FileName + "')")
    .ToList();
                }

                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand("WF_PerformAction_TRK", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("p_WorklistId", WlstId);
                        command.Parameters.AddWithValue("p_Action", "SendBackto");
                        command.Parameters.AddWithValue("p_Comments", comments);
                        command.Parameters.AddWithValue("p_User", EmpidSessionValue);
                        command.Parameters.AddWithValue("p_Activity", null);
                        command.Parameters.AddWithValue("p_TrackingID", TrackingId);

                        // Execute the stored procedure
                        command.ExecuteNonQuery();
                        command.CommandTimeout = 3000;

                    }
                }



                //var trac = @"SELECT * FROM pass_db.hcprequest where TrackingID = '" + TrackingId + "'";
                //var trackid = _passDbContext.Hcprequests.FromSqlRaw(trac).ToList();
                //var trackidemp = trackid.FirstOrDefault()?.CreatedBy;


                //var user = @"SELECT * FROM pass_db.users WHERE EMP_ID = '" + trackidemp + "'";
                //var uemail = _passDbContext.Users.FromSqlRaw(user).ToList();
                //var useremails = uemail.FirstOrDefault()?.UserEmail;


                //GlobalClass.Email(useremails, TrackingId, "Objection");


                return Ok("Files uploaded successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult PastValues(string chemist, string packkcode, string startdatepre, string enddatepre)
        {

                var skuquery = @"DECLARE @columns AS NVARCHAR(MAX);
    DECLARE @sql AS NVARCHAR(MAX);
    DECLARE @startDate AS DATE = '" + startdatepre + "'" +
    " DECLARE @endDate AS DATE = '" + enddatepre + "'" +

         "        SET @columns = STUFF((                                                                         " +
         "            SELECT ',' + QUOTENAME(YearMonth)                                                          " +
         "                                                                                                       " +
         "            FROM(                                                                                      " +
         "                SELECT DISTINCT CONCAT(DATENAME(yyyy, Date), ' ', DATENAME(mm, Date)) AS YearMonth, Month(Date) as mon, year(Date) as yea    " +
         "                                                                                                       " +
         "                FROM [dbo].[DSR_HiltonDailySales_TeamToChemist2022-23]                                " +
         "                                                                                                       " +
         "                WHERE PackCode = '" + packkcode + "' AND ClientCode = '" + chemist + "'             " +
         "                                                                                                       " +
         "                    AND Date >= @startDate AND Date <= @endDate                                        " +
         "            ) OrderedMonths    order by yea, mon                                                                         " +
         "                                                                                                       " +
         "            FOR XML PATH(''), TYPE                                                                     " +
         "        ).value('.', 'NVARCHAR(MAX)'), 1, 1, '');                                                      " +
         "                                                                                                       " +
         "        SET @sql = N'                                                                                  " +
         "    SELECT*                                                                                            " +
         "    FROM                                                                                               " +
         "    (                                                                                                  " +
         "        SELECT                                                                                         " +
         "            sal.PackCode,                                                                           " +
        "            CONCAT(DATENAME(yyyy, sal.Date), '' '', DATENAME(mm, sal.Date)) AS YearMonth,              " +
         "            sal.[Sales-Units]                                                                        " +
         "        FROM [dbo].[DSR_HiltonDailySales_TeamToChemist2022-23] sal                                    " +
         "        WHERE sal.PackCode = ''" + packkcode + "'' AND sal.ClientCode = ''" + chemist + "''        " +
         "            AND sal.Date >= @startDate AND sal.Date <= @endDate                                        " +
         "    ) t                                                                                                " +
         "    PIVOT                                                                                              " +
         "    (                                                                                                  " +
         "        SUM([Sales-Units])                                                                           " +
         "        FOR YearMonth IN(' + @columns + ')                                                             " +
         "    ) AS pivot_Table;                                                                                  " +
         "        ';  " +

         "                                                                                                       " +
         "EXEC sp_executesql @sql, N'@startDate DATE, @endDate DATE', @startDate, @endDate;                      " +
         "        ";


                var results = new List<ExpandoObject>();

                using (var command = _testSalesDbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = skuquery;

                    _testSalesDbContext.Database.OpenConnection();

                    using (var reader = command.ExecuteReader())
                    {
                        // Check if there are rows to read
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var dataItem = new ExpandoObject() as IDictionary<string, object>;


                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    var columnName = reader.GetName(i);

                                    var columnValue = reader[i];
                                    if (columnValue != DBNull.Value)
                                    {
                                        if (columnValue is string stringValue)
                                        {
                                            // Check if the string is empty or contains only {}
                                            if (string.IsNullOrEmpty(stringValue) || stringValue == "{}")
                                            {
                                                dataItem.Add(columnName, "0");
                                            }
                                            else
                                            {
                                                dataItem.Add(columnName, stringValue);
                                            }
                                        }
                                        else
                                        {
                                            dataItem.Add(columnName, columnValue);
                                        }
                                    }
                                    else
                                    {
                                        dataItem.Add(columnName, null);
                                    }

                                    //dataItem.Add(columnName, columnValue);

                                }

                                results.Add((ExpandoObject)dataItem);
                            }
                        }
                        else
                        {

                            return View("NoDataView");

                        }
                    }
                }

                return Json(results);

        }

        [HttpPost]
        public IActionResult UpdateBPSNew(string packCode, string ClientCode, DateTime sdate, DateTime edate, decimal multiplier)
        {
            try
            {
                var results = new List<ExpandoObject>();
                using (SqlConnection connection = new SqlConnection(_sqlconnection))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("sp_GetBPSValuesByPackCode", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("PackCode", packCode);
                        command.Parameters.AddWithValue("ClientCode", ClientCode);
                        command.Parameters.AddWithValue("startDate", sdate);
                        command.Parameters.AddWithValue("endDate", edate);

                        command.Parameters.AddWithValue("multiplier", multiplier);

                     
                        using (var reader = command.ExecuteReader())
                        {
                            // Check if there are rows to read
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var dataItem = new ExpandoObject() as IDictionary<string, object>;


                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        var columnName = reader.GetName(i);

                                        var columnValue = reader[i];
                                        if (columnValue != DBNull.Value)
                                        {
                                            if (columnValue is string stringValue)
                                            {
                                                // Check if the string is empty or contains only {}
                                                if (string.IsNullOrEmpty(stringValue) || stringValue == "{}")
                                                {
                                                    dataItem.Add(columnName, "0");
                                                }
                                                else
                                                {
                                                    dataItem.Add(columnName, stringValue);
                                                }
                                            }
                                            else
                                            {
                                                dataItem.Add(columnName, columnValue);
                                            }
                                        }
                                        else
                                        {
                                            dataItem.Add(columnName, DBNull.Value);
                                        }

                                        //dataItem.Add(columnName, columnValue);

                                    }

                                    results.Add((ExpandoObject)dataItem);
                                }
                            }
                            else
                            {

                                return View("NoDataView");

                            }
                        }
                    }

                    return Json(results);
                }
               
               
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost]
        public IActionResult UpdateBPSValues(string packCode, string ClientCode, DateTime sdate, DateTime edate, decimal multiplier)
        {
            try
            {
                var results = new List<ExpandoObject>();
                using (SqlConnection connection = new SqlConnection(_sqlconnection))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("sp_GetBPSValuesByPackCodeforValues", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("PackCode", packCode);
                        command.Parameters.AddWithValue("ClientCode", ClientCode);
                        command.Parameters.AddWithValue("startDate", sdate);
                        command.Parameters.AddWithValue("endDate", edate);

                        command.Parameters.AddWithValue("multiplier", multiplier);


                        using (var reader = command.ExecuteReader())
                        {
                            // Check if there are rows to read
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var dataItem = new ExpandoObject() as IDictionary<string, object>;


                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        var columnName = reader.GetName(i);

                                        var columnValue = reader[i];
                                        if (columnValue != DBNull.Value)
                                        {
                                            if (columnValue is string stringValue)
                                            {
                                                // Check if the string is empty or contains only {}
                                                if (string.IsNullOrEmpty(stringValue) || stringValue == "{}")
                                                {
                                                    dataItem.Add(columnName, "0");
                                                }
                                                else
                                                {
                                                    dataItem.Add(columnName, stringValue);
                                                }
                                            }
                                            else
                                            {
                                                dataItem.Add(columnName, columnValue);
                                            }
                                        }
                                        else
                                        {
                                            dataItem.Add(columnName, DBNull.Value);
                                        }

                                        //dataItem.Add(columnName, columnValue);

                                    }

                                    results.Add((ExpandoObject)dataItem);
                                }
                            }
                            else
                            {

                                return View("NoDataView");

                            }
                        }
                    }

                    return Json(results);
                }


            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        public class PastValuesRequestModel
        {
            public string ChemistName { get; set; }
            public string PackCode { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }





        [HttpPost]
        public IActionResult ASMActivity(List<IFormFile> Files,string TrackingId)
        {
            try
            {
                var EmpidSessionValue = HttpContext.Session.GetString("EmpIdbps");

                var bpsrcordquery = @"SELECT bpsreq.*, bpsreq.HCPREQID as User_Name, bpsreq.Status_ID as StatusType, bpsreq.CreatedBy as TMCode FROM bps_request bpsreq where TrackingID = '" + TrackingId + "'";
                var bpsrcord = _passDbContext.BpsRequests.FromSqlRaw(bpsrcordquery).ToList();
                var bpsid = bpsrcord.FirstOrDefault()?.BpsRecordId;
                var hcpid = bpsrcord.FirstOrDefault()?.Hcpreqid;

                foreach (var fileName in Files)
                {
                    var guid = Guid.NewGuid();
                    var uploadDirectory = $"{Directory.GetCurrentDirectory()}\\wwwroot\\uploads";

                    if (!Directory.Exists(uploadDirectory))
                    {
                        Directory.CreateDirectory(uploadDirectory);
                    }

                    using (MySqlConnection connection = new MySqlConnection(_connectionString))
                    {
                        connection.Open();

                        using (MySqlCommand command = new MySqlCommand("spUploadpath", connection))
                        {
                            string guidfilename = guid + "_" + fileName.FileName;

                            var filePath = Path.Combine(uploadDirectory, fileName.FileName);
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("p_HCPREQID", hcpid);
                            command.Parameters.AddWithValue("p_TrackingId", TrackingId);
                            command.Parameters.AddWithValue("p_filename", guidfilename);
                            command.Parameters.AddWithValue("p_filepath", filePath);
                            command.Parameters.AddWithValue("p_CreatedBy", EmpidSessionValue);
                            command.Parameters.AddWithValue("p_BPSID", bpsid);
                            command.Parameters.AddWithValue("p_Filetype", "File");
                            command.Parameters.AddWithValue("p_ActivityBy", EmpidSessionValue);

                            // Execute the stored procedure
                            command.ExecuteNonQuery();
                            //command.CommandTimeout = 3000;

                        }
                    }



                }



                //                var hcpactstst = @"Update hcprequest
                //set Status_ID = 8
                //Where TrackingID = '" + TrackingId + "'";
                //                var activityHcp = _passDbContext.Database.ExecuteSqlRaw(hcpactstst);

                var bpsactstst = @"Update bps_request
                set ActivityStatus = 'InProgress'
                Where TrackingID = '" + TrackingId + "'";
                var activityBps = _passDbContext.Database.ExecuteSqlRaw(bpsactstst);


                return Ok("Files uploaded successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult PONumberActivity(string ponum, string trackingid)
        {
            try
            {
                var bpspo = @"Update bps_request
                set PONumber = '" + ponum + "' Where TrackingID = '" + trackingid + "'";
                var BpspoActivity = _passDbContext.Database.ExecuteSqlRaw(bpspo);

                var bpsactstst = @"Update bps_request
                set ActivityStatus = 'InProgress'
                Where TrackingID = '" + trackingid + "'";
                var activityBps = _passDbContext.Database.ExecuteSqlRaw(bpsactstst);
                return Ok("PO Added successfully");
            }catch(Exception ex)
            {
                DateTime timestampValue = DateTime.Now; // Replace with the desired DateTime value
                GlobalClass.LogException(_passDbContext, ex, nameof(PONumberActivity), "PO Number Error message");
                return View("ErrorView");

                throw;
            }

        }


        [HttpPost]
        public IActionResult BpsMAActivity(List<IFormFile> Files, string TrackingId)
        {
            try
            {
                var EmpidSessionValue = HttpContext.Session.GetString("EmpIdbps");

                var bpsrcordquery = @"SELECT bpsreq.*, bpsreq.HCPREQID as User_Name, bpsreq.Status_ID as StatusType, bpsreq.CreatedBy as TMCode FROM bps_request bpsreq where TrackingID = '" + TrackingId + "'";
                var bpsrcord = _passDbContext.BpsRequests.FromSqlRaw(bpsrcordquery).ToList();
                var bpsid = bpsrcord.FirstOrDefault()?.BpsRecordId;
                var hcpid = bpsrcord.FirstOrDefault()?.Hcpreqid;
                foreach (var fileName in Files)
                {
                    var guid = Guid.NewGuid();
                    var uploadDirectory = $"{Directory.GetCurrentDirectory()}\\wwwroot\\uploads";

                    if (!Directory.Exists(uploadDirectory))
                    {
                        Directory.CreateDirectory(uploadDirectory);
                    }

                   
                    using (MySqlConnection connection = new MySqlConnection(_connectionString))
                    {
                        connection.Open();

                        using (MySqlCommand command = new MySqlCommand("spUploadpath", connection))
                        {
                            string guidfilename = guid + "_" + fileName.FileName;
                            var filePath = Path.Combine(uploadDirectory, fileName.FileName);
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("p_HCPREQID", hcpid);
                            command.Parameters.AddWithValue("p_TrackingId", TrackingId);
                            command.Parameters.AddWithValue("p_filename", guidfilename);
                            command.Parameters.AddWithValue("p_filepath", filePath);
                            command.Parameters.AddWithValue("p_CreatedBy", EmpidSessionValue);
                            command.Parameters.AddWithValue("p_BPSID", bpsid);
                            command.Parameters.AddWithValue("p_Filetype", "File");
                            command.Parameters.AddWithValue("p_ActivityBy", EmpidSessionValue);

                            // Execute the stored procedure
                            command.ExecuteNonQuery();
                            //command.CommandTimeout = 3000;

                        }
                    }



                }

                var bpsactstst = @"Update bps_request
                set ActivityStatus = 'ActivityComplete'
                Where TrackingID = '" + TrackingId + "'";
                var activityBps = _passDbContext.Database.ExecuteSqlRaw(bpsactstst);


                return Ok("Files uploaded successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }







}

