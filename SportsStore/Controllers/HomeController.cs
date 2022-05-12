using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStoreRepository _repository;
        public int PageSize = 4;

        public HomeController(IStoreRepository repository) => _repository = repository;

        public ViewResult Index(string? category, int productPage = 1)
        {
            var products = _repository.Products
                        .Where(p => category == null || p.Category == category)
                        .OrderBy(p => p.ProductId)
                        .Skip((productPage - 1) * PageSize)
                        .Take(PageSize);

            Console.Out.WriteLine($"Products size: {products.Count()}");

            var pagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = _repository.Products.Where(p => category == null || p.Category == category).Count()
            };

            var model = new ProductsListViewModel
            {
                Products = products,
                PagingInfo = pagingInfo,
                CurrentCategory = category,
            };

            return View(model);
        }
    }
}
