using System.Net;
using System.Net.Mail;
using WebShop.PL.ViewModels;

namespace WebShop.PL.Util
{
    public class EmailServiceSmtp
    {
        //Server Name SMTP Address      Port SSL
        //Yahoo!	smtp.mail.yahoo.com	587	Yes
        //GMail     smtp.gmail.com	    587	Yes
        //Hotmail   smtp.live.com	    587	Yes
        public void SendEmail(ContactViewModel contactviewmodel)
        {
            string filepath = "D:\\Gewerbeanmeldung - allgemeiner Ueberblick.pdf";
            //try
            //{
            //    //SmtpClient mailServer = new SmtpClient("smtp.gmail.com", 587);
            //    SmtpClient mailServer = new SmtpClient("smtp.live.com", 587);
            //    SmtpServer
            //    mailServer.EnableSsl = true;
            //    mailServer.UseDefaultCredentials = false;
            //    mailServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            //    mailServer.Credentials = new System.Net.NetworkCredential("fariborzsaidi@hotmail.com", "Fs5922290764");

            //    string from = "afshinlinz@gmail.com";
            //    string to = "fariborzsaidi@hotmail.com";
            //    MailMessage msg = new MailMessage(from, to);
            //    string subject = "support";
            //    string body = "The message goes here.";
            //    MailMessage message = new MailMessage(from, to, subject, body);
            //    /***********************************************/
            //    // Create  the file attachment for this email message.
            //    Attachment data = new Attachment(filepath, MediaTypeNames.Application.Octet);
            //    // Add time stamp information for the file.
            //    ContentDisposition disposition = data.ContentDisposition;
            //    disposition.CreationDate = System.IO.File.GetCreationTime(filepath);
            //    disposition.ModificationDate = System.IO.File.GetLastWriteTime(filepath);
            //    disposition.ReadDate = System.IO.File.GetLastAccessTime(filepath);
            //    message.Attachments.Add(data);
            //    /***********************************************/


            //    mailServer.SendAsync(message,null);
            //}
            //catch (SmtpException ex)
            //{
            //    Console.WriteLine("Unable to send email. Error : " + ex);
            //}
            string smtpAddress = "smtp.live.com";
            int portNumber = 587;
            bool enableSSL = true;

            string emailFrom = "fariborzsaidi@hotmail.com";
            string password = "Fs5922290764";
            string emailTo = "fariborzsaidi@hotmail.com";
            string subject = "Hello";
            string body = "Hello, I'm just writing this to say Hi!";

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                // Can set to false, if you are sending pure text.

                mail.Attachments.Add(new Attachment(filepath));
                //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFrom, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }
            }
        }

    }
}
