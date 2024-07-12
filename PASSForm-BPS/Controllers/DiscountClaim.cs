using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PASSForm_BPS.Models;

namespace PASSForm_BPS.Controllers
{
    public class DiscountClaim : Controller
    {
        private readonly PassDbContext _passDbContext;
        private readonly string _connectionString;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DiscountClaim(PassDbContext passDbContext,  IConfiguration configuration1, IWebHostEnvironment webHostEnvironment)
        {
            this._passDbContext = passDbContext;
            
            _webHostEnvironment = webHostEnvironment;
           
            _connectionString = configuration1.GetConnectionString("MySqlServerConnection");
           }
        public IActionResult SubmittedList()
        {
            try
            {
                var Orderlist = _passDbContext.TransactionDetails.FromSqlRaw("call OrderList").ToList();
                return View(Orderlist);

            }

            catch (Exception ex) { 
                ViewBag.ErrorMessage = ex.Message;
            }
            return View();
        }

        public IActionResult OrderListView()
        {
            return View();
        }
        public IActionResult OrderListEdit(int Id)
        {
            try
            {
                var Orderlist = _passDbContext.TransactionDetails.FromSqlRaw($"call OrderListById('{Id}')").ToList();
                return View(Orderlist);

            }

            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }
            return RedirectToAction("SubmittedList");   
        }

        public IActionResult OrderListEditById(int Id,string InvoiceNumber,string ProductCode,int Quantity)
        {
            try
            {
                var Orderlist = _passDbContext.Database.ExecuteSqlRaw($"call sp_UpdateOrderdetails({Id}, '{InvoiceNumber}', '{ProductCode}', {Quantity})");
                ViewBag.Message = "Successfully Updated";
                
               
            }

            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }
            return RedirectToAction("SubmittedList");
        }
    }
}
