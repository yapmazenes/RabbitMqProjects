using DocumentConverter.Producer.Models;
using DocumentConverter.Producer.Services;
using DocumentConverter.Producer.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentConverter.Producer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDocumentService _documentService;

        public HomeController(ILogger<HomeController> logger, IDocumentService documentService)
        {
            _logger = logger;
            _documentService = documentService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult WordToPdf()
        {
            return View();
        }

        [HttpPost]
        public IActionResult WordToPdf(WordToPdf wordToPdf)
        {
            if (!ModelState.IsValid)
            {
                return View(wordToPdf);
            }

            ViewBag.Result = _documentService.ConvertWordToPdf(wordToPdf);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
