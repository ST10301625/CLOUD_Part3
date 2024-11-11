using CloudPart3.Models;
using CloudPart3.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CloudPart3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRepository _homeRepository;

        public HomeController(ILogger<HomeController> logger, IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string sterm="",int styleId=0)
        {
            IEnumerable<Product> products = await _homeRepository.GetProducts(sterm, styleId);
            IEnumerable<Style> styles = await _homeRepository.Styles();
            ProductDisplayModel productModel = new ProductDisplayModel
            {
              Products=products,
              Styles=styles,
              STerm=sterm,
              StyleId=styleId
            };
            return View(productModel);
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