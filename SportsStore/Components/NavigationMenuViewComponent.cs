using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Components {

    public class NavigationMenuViewComponent : ViewComponent 
    {
        private IStoreRepository repository;

        public NavigationMenuViewComponent(IStoreRepository repository)
        {
            this.repository = repository;
        }

        public IViewComponentResult Invoke() {
            return View(
                repository.Products
                    .Select(product => product.Category)
                    .Distinct()
                    .OrderBy(product => product)
            );
        }
    }
}