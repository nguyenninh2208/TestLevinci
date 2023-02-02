using JetBrains.Annotations;
using Jwt.Infrastructure.Configuations;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;

namespace Jwt.Infrastructure
{
    public static class Utility
    {
        public static AppSettings Setting { get { return AppSettingServices.Get; } }

        public static bool IsProduction
        {
            get
            {
                return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production";
            }
        }

        public static string RandomTransactionCode(int length = 8)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray()).ToLower();
        }

        public static string ConvertToUnSign(string text)
        {
            for (int i = 33; i < 48; i++)
            {
                if (i != 45)
                    text = text.Replace(((char)i).ToString(), "");
            }

            for (int i = 58; i < 65; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            for (int i = 91; i < 97; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            for (int i = 123; i < 127; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            text = text.Replace("'", "");
            text = text.Replace("\"", "");
            text = text.Replace(" ", "-");
            text = text.Replace("--", "-");

            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");

            string strFormD = text.Normalize(System.Text.NormalizationForm.FormD);
            return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');


        }
        public static void SendEmail(string toAddress, string body)
        {
            MailAddress to = new MailAddress(toAddress);
            MailAddress from = new MailAddress("FromAddress");

            MailMessage _email = new MailMessage(from, to);
            _email.Subject = "Account Email";
            _email.Body = body;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.server.address";
            smtp.Port = 25;
            smtp.Credentials = new NetworkCredential("smtp_username", "smtp_password");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(_email);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}