using DocumentConverter.Common.Models;
using DocumentConverter.Common.Services;
using DocumentConverter.Producer.Models;
using DocumentConverter.Producer.Services.Abstract;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace DocumentConverter.Producer.Services.Concrete
{
    public class DocumentManager : IDocumentService
    {
        private readonly RabbitMqClientService _rabbitMqClientService;

        public DocumentManager(RabbitMqClientService rabbitMqClientService)
        {
            _rabbitMqClientService = rabbitMqClientService;
        }

        public string ConvertWordToPdf(WordToPdf wordToPdf)
        {
            try
            {
                _rabbitMqClientService.Connect(true);

                MessageWordToPdf messageWordToPdf = new MessageWordToPdf
                {
                    Email = wordToPdf.Email,
                    FileName = Path.GetFileNameWithoutExtension(wordToPdf.WordFile.FileName)
                };

                using (MemoryStream ms = new MemoryStream())
                {
                    wordToPdf.WordFile.CopyTo(ms);
                    messageWordToPdf.WordByte = ms.ToArray();
                }

                string serializeMessage = JsonConvert.SerializeObject(messageWordToPdf);

                var byteMessage = Encoding.UTF8.GetBytes(serializeMessage);

                _rabbitMqClientService.SendMessage(byteMessage, true);

                return "When your Word file converted to Pdf file, will be send a email to your specified address";
            }
            catch (Exception e)
            {
                return "There was an error occured, please contact us";
            }
        }
    }
}
