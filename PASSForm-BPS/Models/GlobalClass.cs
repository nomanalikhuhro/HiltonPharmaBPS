using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Net;
using Microsoft.IdentityModel.Tokens;
using static System.Net.WebRequestMethods;
using MySqlX.XDevAPI;
using Humanizer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Google.Protobuf.WellKnownTypes;
using NuGet.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Drawing;
using System.IO;
using System.Runtime.Intrinsics.X86;

namespace PASSForm_BPS.Models
{
    public class GlobalClass
    {

        public static void LogException(DbContext context, Exception ex,  string actionName, string message)
        {
            context.Database.ExecuteSqlRaw("CALL InsertExceptionLog(@p_LogEvent, " +
                "@p_Exception, " +
                "@p_Message)",
                new MySqlParameter("@p_LogEvent", actionName),
                new MySqlParameter("@p_Exception", ex),
                new MySqlParameter("@p_Message", message));
        }


        //public static void Email(string userValues, string TrackingId, string status)
        //{
        //    // Sender's email address and password
        //    string senderEmail = "loanapp@hiltonpharma.com";
        //    string senderPassword = "Info@@890";

        //    // Recipient's email address
        //    string recipientEmail = "areeb.hussain@arcanainfo.com";

        //    // Create a new MailMessage
        //    MailMessage mail = new MailMessage();
        //    mail.From = new MailAddress(senderEmail);
        //    mail.To.Add(recipientEmail);
        //    mail.Subject = "PASS Request's";
        //    //mail.Body = "The following PASS Request '" + TrackingId + "' is pending at your end. " +
        //    //        "" +
        //    //        "HCP FORM : 'http://192.168.10.30:8080/'";

        //    mail.IsBodyHtml = true;

        //    if (status == "Objection")
        //    {
        //        mail.Body = @"
        //    <html>
        //        <body>
        //           <p style='font-size:15px;'>The following PASS Request <span style='font-weight:bold;'>'" + TrackingId + "'</span> has been Objected.</p>   <p style='font-weight:bold;'>HCP FORM: <a href='http://192.168.10.30:8080/'>http://192.168.10.30:8080/</a></p><p style='font-weight:bold;'>BPS FORM: <a href='http://192.168.10.30:8081/'>http://192.168.10.30:8081/</a></p></ body ></ html >";

        //    }
        //    else
        //    {
        //        mail.Body = @"
        //    <html>
        //        <body>
        //           <p style='font-size:15px;'>The following PASS Request <span style='font-weight:bold;'>'" + TrackingId + "'</span> is pending at your end.</p>   <p style='font-weight:bold;'>HCP FORM: <a href='http://192.168.10.30:8080/'>http://192.168.10.30:8080/</a></p><p style='font-weight:bold;'>BPS FORM: <a href='http://192.168.10.30:8081/'>http://192.168.10.30:8081/</a></p></ body ></ html >";

        //    }
        //        // Create a new SmtpClient
        //    SmtpClient smtpClient = new SmtpClient("smtp.office365.com");
        //    smtpClient.Port = 587;
        //    smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
        //    smtpClient.EnableSsl = true;
        //   // smtpClient.UseDefaultCredentials = true;

        //    try
        //    {
        //        // Send the email
        //        smtpClient.Send(mail);
        //        Console.WriteLine("Email sent successfully!");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error: {ex.Message}");
        //    }



        //}
    }
}
