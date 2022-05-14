using MessageConsumer.Services.Abstract;
using MessageConsumer.Services.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace MessageConsumer.Services.Concrete
{
    public class EmailManager : IEmailService
    {
        public async Task<bool> EmailSend(EmailModel emailModel)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();

                mailMessage.From = new MailAddress(emailModel.From); //"yapmazenes@gmail.com"
                mailMessage.Subject = emailModel.Subject; //Pdf File Converter
                mailMessage.Body = emailModel.Body; // "Your Pdf file in the attachment list.";
                mailMessage.IsBodyHtml = emailModel.IsBodyHtml; //true
                if (emailModel.AttachmentFileModels != null)
                {
                    foreach (var attachmentFile in emailModel.AttachmentFileModels)
                    {
                        var fileContent = GetContentTypeByFileExtension(attachmentFile.Extension);

                        if (fileContent.contentType == null)
                            continue;

                        attachmentFile.File.Position = 0;

                        Attachment attachment = new Attachment(attachmentFile.File, fileContent.contentType);
                        attachment.ContentDisposition.FileName = $"{attachmentFile.FileName}.{fileContent.extension}";

                        mailMessage.Attachments.Add(attachment);
                    }
                }

                foreach (var email in emailModel.EmailReceiverList)
                {
                    mailMessage.To.Add(new MailAddress(email));
                }

                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential("", "");

                await smtpClient.SendMailAsync(mailMessage);

                Console.WriteLine($"Result: {string.Join(',', emailModel.EmailReceiverList)} to this address sended");

                foreach (var attachmentFile in emailModel.AttachmentFileModels)
                {
                    attachmentFile.File.Close();
                    attachmentFile.File.Dispose();
                }

                return true;
            }

            catch (Exception e)
            {
                Console.WriteLine($"An error Occured: {e.InnerException}");

                return false;
            }

        }

        private (ContentType contentType, string extension) GetContentTypeByFileExtension(string extension)
        {
            if (!string.IsNullOrEmpty(extension))
            {
                extension = extension.Replace(".", "");

                switch (extension)
                {
                    case "pdf":
                        return (new ContentType(MediaTypeNames.Application.Pdf), extension);
                }
            }

            return (null, extension);
        }
    }
}
