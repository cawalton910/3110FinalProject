using _3110FinalProject.Models.ViewModels;
using _3110FinalProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace _3110FinalProject.Controllers
{
    /// <summary>
    /// This controller is for creating, editing, deleting, and reading products.
    /// </summary>
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepo;

        public ProductController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }
        /// <summary>
        /// This method reads all products and displays them as a list
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return View(await _productRepo.ReadAllAsync());
        }
        /// <summary>
        /// This is the GET method for editing a product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// This is the POST method for editing a product
        /// </summary>
        /// <param name="productVM"></param>
        /// <returns></returns>
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
        /// <summary>
        /// This is the GET method for creating a product
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// This is the POST method for creating a product
        /// </summary>
        /// <param name="productVM"></param>
        /// <returns></returns>
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
        /// <summary>
        /// This is the GET method for deleting a product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepo.ReadAsync(id);
            return View(product);
        }
        /// <summary>
        /// This is the POST method for deleting a product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productRepo.RemoveAsync(id);
            return RedirectToAction("Index");
        }
    }
}
