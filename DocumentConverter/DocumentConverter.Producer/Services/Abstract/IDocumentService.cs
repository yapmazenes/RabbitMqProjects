using DocumentConverter.Producer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentConverter.Producer.Services.Abstract
{
    public interface IDocumentService
    {
        string ConvertWordToPdf(WordToPdf wordToPdf);
    }
}
