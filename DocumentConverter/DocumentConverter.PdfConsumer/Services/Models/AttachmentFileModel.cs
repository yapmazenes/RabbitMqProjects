using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentConverter.PdfConsumer.Services.Models
{
    public class AttachmentFileModel
    {
        public MemoryStream File { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
    }
}
