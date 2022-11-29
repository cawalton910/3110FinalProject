using _3110FinalProject.Models.Entities;
using _3110FinalProject.Models.ViewModels;
using _3110FinalProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace _3110FinalProject.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly IProductRepository _productRepo;
        private readonly IPurchaseRepository _purchaseRepo;
        private readonly IPurchaseProductRepository _productPurchaseRepo;
        private readonly ICustomerRepository _customerRepo;

        public PurchaseController(ICustomerRepository customerRepo, IProductRepository productRepo, IPurchaseRepository purchaseRepo, IPurchaseProductRepository productPurchaseRepo)
        {
            _productRepo = productRepo;
            _purchaseRepo = purchaseRepo;
            _productPurchaseRepo = productPurchaseRepo;
            _customerRepo = customerRepo;
        }
        public IActionResult Create(int id)
        {
            return View();
        }
        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> CreateConfirmed(int id, PurchaseVM purchaseVM)
        {
            var purchase = purchaseVM.GetPurchaseInstance();
            await _purchaseRepo.CreateAsync(purchase, id);
            ViewData["id"] = id;
            ViewData["purchaseId"] = purchase.Id;
            return RedirectToAction("AddItem", "ProductPurchase", new { id=id, purchaseId=purchase.Id });
            //return View(allProducts);
        }
        //public async Task<IActionResult> AddItem(int id, int purchaseId)
        //{
        //    ViewData["id"] = id;
        //    ViewData["purchaseId"] = purchaseId;
        //    var products = await _productRepo.ReadAllAsync();
        //    return View(products);
        //}
        //[HttpPost]
        //public async Task<IActionResult> AddItem(int id, int purchaseId, int productId, int quantity)
        //{
        //    var product = await _productRepo.ReadAsync(productId);
        //    var purchase = await _purchaseRepo.ReadAsync(purchaseId);
        //    if (purchase != null && product != null)
        //    {
        //        await _productPurchaseRepo.CreateAsync(purchaseId, productId, quantity);
        //    }
        //    return RedirectToAction("AddItem", new { id=id, purchaseId=purchase!.Id});

        //}
        public async Task<IActionResult> ViewPurchases(int Id)
        {
            var customer = await _customerRepo.ReadAsync(Id);
            ViewData["Customer"] = customer!.Id;
            return View(customer.Purchases);
        }
        public async Task<IActionResult> Delete(int id, int purchaseid)
        {
            var purchase = await _purchaseRepo.ReadAsync(purchaseid);
            ViewData["customerid"] = id;
            return View(purchase);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id, int purchaseid)
        {
            await _purchaseRepo.RemoveAsync(purchaseid);
            return RedirectToAction("ViewPurchases", new { id = id});
        }

        //public async Task<IActionResult> Details(int Id, int purchaseId)
        //{
        //    var customer = await _customerRepo.ReadAsync(Id);
        //    if (customer == null)
        //    {
        //        return RedirectToAction("ViewPurchases", new { id = Id });
        //    }
        //    var purchase = await _purchaseRepo.ReadAsync(purchaseId);
        //    if (purchase == null)
        //    {
        //        return RedirectToAction("ViewPurchases", new { id = Id });
        //    }
        //    ViewData["customerid"] = Id;
        //    ViewData["purchaseId"] = purchaseId;
        //    return View(purchase.Products);
        //}
        //public async Task<IActionResult> RemoveItem(int id, int purchaseId,[Bind(Prefix = "productId")] int productPurchaseId)
        //{
        //    var productPurchase = await _productPurchaseRepo.ReadAsync(productPurchaseId);
        //    return View(productPurchase);
        //}
        //[HttpPost, ActionName("RemoveItem")]
        //public async Task<IActionResult> RemoveItemConfirmed(int id, int purchaseId,[Bind(Prefix = "productId")]int productPurchaseId)
        //{
        //    await _productPurchaseRepo.RemoveAsync(purchaseId, productPurchaseId);
        //    return RedirectToAction("Details", new { id = id, purchaseId = purchaseId });
        //}
    }
}
