using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Threading;

namespace ConsoleApp56
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SendMail.SendEmail();
        }
    }
    class SendMail
    {
        static string smtpAddress = "smtp.gmail.com";
        static int portNumber = 587;
        static bool enableSSL = true;
        static string emailFromAddress = "";
        static string password = "";
        static string[] emailToAddress = { "", "" };  

        static string subject = "Hello";
        static string body = "Hello, This is Email sending test using gmail.";
        public static void SendEmail()
        {

            Thread[] thread = new Thread[emailToAddress.Length];
            for(int i = 0;i < emailToAddress.Length; i++)
            {
                int arrayIndex = i;
                thread[i]= new Thread(()=>{
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(emailFromAddress);
                        mail.To.Add(emailToAddress[arrayIndex]);
                        mail.Subject = subject;
                        mail.Body = body;
                        mail.IsBodyHtml = true;

                        using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                        {
                            smtp.Credentials = new NetworkCredential(emailFromAddress, password);
                            smtp.EnableSsl = enableSSL;
                            smtp.Send(mail);
                        }
                    }
                });
            }
           
            foreach(var t in thread)
            {
                t.Start();
            }
            foreach (var t in thread)
            {
                t.Join();
            }
        }
        public static void fun1()
        {
            Console.WriteLine("asdfgb");
        }
    }
}