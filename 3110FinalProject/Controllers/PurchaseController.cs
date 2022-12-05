using _3110FinalProject.Models.Entities;
using _3110FinalProject.Models.ViewModels;
using _3110FinalProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace _3110FinalProject.Controllers
{
    /// <summary>
    /// This controller is for creating, removing, and reading purchases.
    /// </summary>
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
        /// <summary>
        /// This is the GET method for creating a purchase for a customer
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns></returns>
        public IActionResult Create(int id)
        {
            return View();
        }
        /// <summary>
        /// This is the POST method for creating a purchase for a customer
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <param name="purchaseVM">Purchase View Model</param>
        /// <returns></returns>
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
        /// <summary>
        /// This is method displays a list of purchases for a customer
        /// </summary>
        /// <param name="Id">Customer ID</param>
        /// <returns></returns>
        public async Task<IActionResult> ViewPurchases(int Id)
        {
            var customer = await _customerRepo.ReadAsync(Id);
            ViewData["Customer"] = customer!.Id;
            return View(customer.Purchases);
        }
        /// <summary>
        /// This is the GET method for deleting a purchase
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <param name="purchaseid">Purchase ID</param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id, int purchaseid)
        {
            var purchase = await _purchaseRepo.ReadAsync(purchaseid);
            ViewData["customerid"] = id;
            return View(purchase);
        }
        /// <summary>
        /// This is the POST method for deleting a purchase
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <param name="purchaseid">Purchase ID</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id, int purchaseid)
        {
            await _purchaseRepo.RemoveAsync(purchaseid);
            return RedirectToAction("ViewPurchases", new { id = id});
        }

    }
}
