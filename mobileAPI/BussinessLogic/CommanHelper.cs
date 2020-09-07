using Microsoft.Extensions.Configuration;
using mobileAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace mobileAPI.BussinessLogic
{
    public static class CommanHelper
    {

        public static string FormatDatess(this DateTime value, string format = "yyyy-MM-dd")
        {
            return ((DateTime)value).ToString(format);
        }


        public static bool IsDateBetweenDates(this DateTime? value, DateTime? startDate,DateTime? endDate)
        {
            value = Convert.ToDateTime(((DateTime)value).ToString("yyyy-MM-dd"));
            startDate = Convert.ToDateTime(((DateTime)startDate).ToString("yyyy-MM-dd"));
            endDate = Convert.ToDateTime(((DateTime)endDate).ToString("yyyy-MM-dd"));

            return (value >= startDate && value <= endDate);
        }

        #region Email Code
        public static void SendMail(IConfiguration iConfig, Visit visit)
        {
            //string empcode = iConfig.GetValue<string>("MailConfiguration:HostName");
            string vbody = string.Empty, HostName = string.Empty, FromEmailid = string.Empty, password, toEmailId = string.Empty;
            FromEmailid = iConfig.GetValue<string>("MailConfiguration:FromMailId");
            password = iConfig.GetValue<string>("MailConfiguration:EmailPassword");
            HostName = iConfig.GetValue<string>("MailConfiguration:HostName");
            toEmailId = visit.EmailId;//iConfig.GetValue<string>("MailConfiguration:ToMailId");
            vbody = $"<html><body> To:</br>{visit.ClientName}<br/> {visit.Locatation} <br/> <br/> <br/><br/> Respected Sir/Madam,<br/> Thanks for the opportunity given to us to explain about our Products on <b>Date of Visit:</b>{DateTime.Now}<br/> <b>For further details , pleases visit our website  :</b> <a href=\"https://eurotekindia.com/\">https://eurotekindia.com</a></br></br></br><b> Note:-This is an automated alert mail, do not reply this mail, contact </b> <a href =\"info@eurotekindia.com\"> info@eurotekindia.com </a></body></html> ";
            //vbody = $"Visit reason is {visit.VisitType}  and visit id :{visit.VisitId}, Visited by {visit.VisitedEmployee}";

            MailMessage mailMessage = new MailMessage(new MailAddress(FromEmailid), new MailAddress(toEmailId));
            SmtpClient objSmtpClient = new SmtpClient();
            objSmtpClient.EnableSsl = true;

            mailMessage.Subject = $"{visit.VisitType} Visit";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = vbody;

            NetworkCredential NetworkCred = new NetworkCredential();
            NetworkCred.UserName = FromEmailid;
            NetworkCred.Password = password;
            

            objSmtpClient.Host = HostName;
            objSmtpClient.UseDefaultCredentials = true;
            objSmtpClient.Credentials = NetworkCred;
            objSmtpClient.Port = iConfig.GetValue<int>("MailConfiguration:PortNo");
            objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                objSmtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
        #endregion

    }
}
