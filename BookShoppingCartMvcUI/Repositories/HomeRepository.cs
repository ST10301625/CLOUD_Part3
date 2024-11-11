

using CloudPart3.Models;
using Microsoft.EntityFrameworkCore;

namespace CloudPart3.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _db;

        public HomeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Style>> Styles()
        {
            return await _db.Styles.ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetProducts(string sTerm = "", int styleId = 0)
        {
            sTerm = sTerm.ToLower();
            IEnumerable<Product> products = await (from product in _db.Products
                         join style in _db.Styles
                         on product.StyleId equals style.Id
                         join stock in _db.Stocks
                         on product.Id equals stock.ProductId
                         into products_stocks
                         from productWithStock in products_stocks.DefaultIfEmpty() 
                         where string.IsNullOrWhiteSpace(sTerm) || (product != null && product.ProductName.ToLower().StartsWith(sTerm))
                         select new Product
                         {
                             Id = product.Id,
                             Image = product.Image,
                             BrandName = product.BrandName,
                             ProductName = product.ProductName,
                             StyleId = product.StyleId,
                             Price = product.Price,
                             StyleName = style.StyleName,
                             Quantity= productWithStock == null? 0: productWithStock.Quantity
                         }
                         ).ToListAsync();
            if (styleId > 0)
            {

                products = products.Where(a => a.StyleId == styleId).ToList();
            }
            return products;

        }
    }
}
