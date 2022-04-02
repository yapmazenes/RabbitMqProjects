using DocumentConverter.PdfConsumer.Services.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentConverter.PdfConsumer.Services.Abstract
{
    public interface IEmailService
    {
        bool EmailSend(EmailModel emailModel);
    }
}
