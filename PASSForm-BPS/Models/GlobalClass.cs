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


          public static string StringDateFormat(string date)
        {

            date = DateTime.Parse(date).ToString("yyyy-MM-dd");
            return date;
        }
    }
}
