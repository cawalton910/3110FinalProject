using _3110FinalProject.Models.Entities;
using _3110FinalProject.Models.ViewModels;
using _3110FinalProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace _3110FinalProject.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepo;

        public CustomerController(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _customerRepo.ReadAllAsync());
        }
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _customerRepo.ReadAsync(id);
            if (customer == null)
            {
                return RedirectToAction("Index");
            }
            var customerVM = new CustomerVM
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Purchases = customer.Purchases,
            };
            return View(customerVM);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CustomerVM customerVM)
        {
            if (ModelState.IsValid)
            {
                var customer = customerVM.GetCustomerInstance();
                await _customerRepo.UpdateAsync(customer.Id, customer);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerVM customerVM)
        {
            if (ModelState.IsValid)
            {
                var customer = customerVM.GetCustomerInstance();
                await _customerRepo.CreateAsync(customer);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _customerRepo.RemoveAsync(id);
            return RedirectToAction("Index");
        }


    }
}
