using _3110FinalProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace _3110FinalProject.Controllers
{
    /// <summary>
    /// This controller handles adding and removing products from purchase orders and viewing the related data. 
    /// </summary>
    public class ProductPurchaseController : Controller
    {
        private readonly IProductRepository _productRepo;
        private readonly IPurchaseRepository _purchaseRepo;
        private readonly IPurchaseProductRepository _productPurchaseRepo;
        private readonly ICustomerRepository _customerRepo;

        public ProductPurchaseController(ICustomerRepository customerRepo, IProductRepository productRepo, IPurchaseRepository purchaseRepo, IPurchaseProductRepository productPurchaseRepo)
        {
            _productRepo = productRepo;
            _purchaseRepo = purchaseRepo;
            _productPurchaseRepo = productPurchaseRepo;
            _customerRepo = customerRepo;
        }
        /// <summary>
        /// This is the GET method for removing a product from a purchase
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <param name="purchaseId">Purchase ID</param>
        /// <param name="productPurchaseId">ProductPurchase ID</param>
        /// <returns></returns>
        public async Task<IActionResult> RemoveItem(int id, int purchaseId, [Bind(Prefix = "productId")] int productPurchaseId)
        {
            ViewData["customerid"] = id;
            ViewData["purchaseId"] = purchaseId;
            var productPurchase = await _productPurchaseRepo.ReadAsync(productPurchaseId);
            return View(productPurchase);
        }
        /// <summary>
        /// This is the POST method for removing a product from a purchase
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <param name="purchaseId">Purchase ID</param>
        /// <param name="productPurchaseId">ProductPurchase ID</param>
        /// <returns></returns>
        [HttpPost, ActionName("RemoveItem")]
        public async Task<IActionResult> RemoveItemConfirmed(int id, int purchaseId, [Bind(Prefix = "productId")] int productPurchaseId)
        {
            await _productPurchaseRepo.RemoveAsync(purchaseId, productPurchaseId);
            return RedirectToAction("Details", new { id = id, purchaseId = purchaseId });
        }
        /// <summary>
        /// This method displays a customers purchase with each product and their quantities
        /// </summary>
        /// <param name="Id">Customer ID</param>
        /// <param name="purchaseId">Purchase ID</param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int Id, int purchaseId)
        {
            var customer = await _customerRepo.ReadAsync(Id);
            if (customer == null)
            {
                return RedirectToAction("ViewPurchases", new { id = Id });
            }
            var purchase = await _purchaseRepo.ReadAsync(purchaseId);
            if (purchase == null)
            {
                return RedirectToAction("ViewPurchases", new { id = Id });
            }
            ViewData["customerid"] = Id;
            ViewData["purchaseId"] = purchaseId;
            return View(purchase.Products);
        }
        /// <summary>
        /// This is the GET method for adding a product to a purchase
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <param name="purchaseId">Purchase ID</param>
        /// <returns></returns>
        public async Task<IActionResult> AddItem(int id, int purchaseId)
        {
            ViewData["id"] = id;
            ViewData["purchaseId"] = purchaseId;
            var products = await _productRepo.ReadAllAsync();
            return View(products);
        }
        /// <summary>
        /// This is the POST method for adding a product to a purchase
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <param name="purchaseId">Purchase ID</param>
        /// <param name="productId">Product ID</param>
        /// <param name="quantity">Product Quantity</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddItem(int id, int purchaseId, int productId, int quantity)
        {
            var product = await _productRepo.ReadAsync(productId);
            var purchase = await _purchaseRepo.ReadAsync(purchaseId);
            if (purchase != null && product != null)
            {
                await _productPurchaseRepo.CreateAsync(purchaseId, productId, quantity);
            }
            return RedirectToAction("AddItem", new { id = id, purchaseId = purchase!.Id });

        }
    }
}
