using _3110FinalProject.Models.ViewModels;
using _3110FinalProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace _3110FinalProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepo;

        public ProductController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _productRepo.ReadAllAsync());
        }
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productRepo.ReadAsync(id);
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            var productVM = new ProductVM
            {
                Id = product.Id,
                Name = product.Name,
                Weight = product.Weight,
                Price = product.Price,
                ProductPurchases = product.ProductPurchases,
            };
            return View(productVM);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                var product = productVM.GetPurchaseInstance();
                await _productRepo.UpdateAsync(product.Id, product);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                var product = productVM.GetPurchaseInstance();
                await _productRepo.CreateAsync(product);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _productRepo.RemoveAsync(id);
            return RedirectToAction("Index");
        }
    }
}
