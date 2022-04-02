using DocumentConverter.Common.Models;
using DocumentConverter.Common.Services;
using DocumentConverter.PdfConsumer.Services.Abstract;
using DocumentConverter.PdfConsumer.Services.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Spire.Doc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DocumentConverter.PdfConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            bool result = false;
            var serviceCollection = new ServiceCollection();

            new Startup().ConfigureServices(serviceCollection);

            var services = serviceCollection.BuildServiceProvider();

            var rabbitMqClientService = services.GetService<RabbitMqClientService>();
            var _emailService = services.GetService<IEmailService>();

            var rabbitMqConnect = rabbitMqClientService.Connect();
            rabbitMqClientService.ConsumeQueue((model, ea) =>
            {
                try
                {
                    Console.WriteLine("Received a message from queue and processing...");
                    Document document = new Document();

                    string message = Encoding.UTF8.GetString(ea.Body.ToArray());

                    MessageWordToPdf messageWordToPdf = JsonConvert.DeserializeObject<MessageWordToPdf>(message);

                    document.LoadFromStream(new MemoryStream(messageWordToPdf.WordByte), FileFormat.Docx2013);

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        document.SaveToStream(memoryStream, FileFormat.PDF);
                        result = _emailService.EmailSend(new Services.Models.EmailModel
                        {
                            Subject = "Pdf File Converter",
                            Body = "Your Pdf file in the attachment list",
                            EmailReceiverList = new List<string> { "yapmazenes@gmail.com" },
                            From = "yapmazenes@gmail.com",
                            IsBodyHtml = true,
                            AttachmentFileModels = new List<AttachmentFileModel> { new AttachmentFileModel { Extension = "pdf", File = memoryStream, FileName = messageWordToPdf.FileName } }
                        });
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error occured on processing: {e.Message}");
                }

                if (result)
                {
                    Console.WriteLine("For Email has successfuly ");
                    rabbitMqConnect.BasicAck(ea.DeliveryTag, false);
                }
                else
                {
                    rabbitMqConnect.BasicNack(ea.DeliveryTag, false, true);
                }

            }, false, true);

            Console.WriteLine("Message succesfuly implemented");
            Console.ReadLine();
        }
    }
}
