using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentConverter.PdfConsumer.Services.Models
{
    public class EmailModel
    {
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; }

        public IEnumerable<AttachmentFileModel> AttachmentFileModels { get; set; }
        public IEnumerable<string> EmailReceiverList { get; set; }

    }
}
