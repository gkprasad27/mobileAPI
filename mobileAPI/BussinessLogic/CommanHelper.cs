using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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


        public static bool IsDateBetweenDates(this DateTime? value, DateTime? startDate, DateTime? endDate)
        {
            value = Convert.ToDateTime(((DateTime)value).ToString("yyyy-MM-dd"));
            startDate = Convert.ToDateTime(((DateTime)startDate).ToString("yyyy-MM-dd"));
            endDate = Convert.ToDateTime(((DateTime)endDate).ToString("yyyy-MM-dd"));

            return (value >= startDate && value <= endDate);
        }


        #region Email Code
        public static void SendMail(IConfiguration iConfig)
        {
            //string empcode = iConfig.GetValue<string>("MailConfiguration:HostName");
            string vbody = string.Empty, HostName=string.Empty, FromEmailid=string.Empty,password, toEmailId=string.Empty;
             FromEmailid = iConfig.GetValue<string>("MailConfiguration:FromMailId");
             password = iConfig.GetValue<string>("MailConfiguration:EmailPassword");
             HostName = iConfig.GetValue<string>("MailConfiguration:HostName");
             toEmailId = iConfig.GetValue<string>("MailConfiguration:ToMailId");
            
            vbody = "<html><body>Dear <span font-variant: small-caps;>Test ,</span><br><br> has applied  strLeaveCode from  leaveapply.Leave_From  To  leaveapply.Leave_To  for a duration of .Kindly review and Approve / Reject.<br><br> <br><br><p style=" + "text-decoration:none;" + ">This is system generated message and Do Not reply.</p></body></html>";
            vbody = "Testing for satstek";

            MailMessage mailMessage = new MailMessage(new MailAddress(FromEmailid), new MailAddress(toEmailId));
            SmtpClient objSmtpClient = new SmtpClient();
            objSmtpClient.EnableSsl = true;

            mailMessage.Subject = "Mail for Visit test";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = vbody;

            NetworkCredential NetworkCred = new NetworkCredential();
            NetworkCred.UserName = FromEmailid;
            NetworkCred.Password = password;

            
            objSmtpClient.Host = HostName;
            objSmtpClient.UseDefaultCredentials = true;
            objSmtpClient.Credentials = NetworkCred;
            objSmtpClient.Port = iConfig.GetValue<int>("MailConfiguration:PortNo");
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

        static bool mailSent = false;
        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("Message sent.");
            }
            mailSent = true;
        }
        public static void SENDMAILLLL()
        {
            // Command-line argument must be the SMTP host.
            SmtpClient client = new SmtpClient("Smtp.office365.com");
            // Specify the email sender.
            // Create a mailing address that includes a UTF8 character
            // in the display name.
            MailAddress from = new MailAddress("jane@contoso.com","Jane " + (char)0xD8 + " Clayton",System.Text.Encoding.UTF8);
            // Set destinations for the email message.
            MailAddress to = new MailAddress("deorepankaj2015@gmail.com");
            // Specify the message content.
            MailMessage message = new MailMessage(from, to);
            message.Body = "This is a test email message sent by an application. ";
            // Include some non-ASCII characters in body and subject.
            string someArrows = new string(new char[] { '\u2190', '\u2191', '\u2192', '\u2193' });
            message.Body += Environment.NewLine + someArrows;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = "test message 1" + someArrows;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            // Set the method that is called back when the send operation ends.
            client.SendCompleted += new
            SendCompletedEventHandler(SendCompletedCallback);
            // The userState can be any object that allows your callback
            // method to identify this send operation.
            // For this example, the userToken is a string constant.
            string userState = "test message1";
            client.SendAsync(message, userState);
           // Console.WriteLine("Sending message... press c to cancel mail. Press any other key to exit.");
            //string answer = Console.ReadLine();
            //// If the user canceled the send, and mail hasn't been sent yet,
            //// then cancel the pending operation.
            //if (answer.StartsWith("c") && mailSent == false)
            //{
            //    client.SendAsyncCancel();
            //}
            // Clean up.
            message.Dispose();
            //Console.WriteLine("Goodbye.");
        }
    }
}

    


