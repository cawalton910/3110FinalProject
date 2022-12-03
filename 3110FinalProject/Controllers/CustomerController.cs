using _3110FinalProject.Models.Entities;
using _3110FinalProject.Models.ViewModels;
using _3110FinalProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace _3110FinalProject.Controllers
{
    /// <summary>
    /// This is the customer controller for creating, editing, deleting, and reading customers.
    /// </summary>
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepo;

        public CustomerController(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }
        /// <summary>
        /// This method reads all customers and displays them as a list
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return View(await _customerRepo.ReadAllAsync());
        }
        /// <summary>
        /// This is the GET method for editing a customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// This is the POST method for editing a customer
        /// </summary>
        /// <param name="customerVM"></param>
        /// <returns></returns>
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
        /// <summary>
        /// This is the GET method for creating a customer
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// This is the POST method for creating a customer
        /// </summary>
        /// <param name="customerVM"></param>
        /// <returns></returns>
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
        /// <summary>
        /// This is the GET method for deleting a customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customerRepo.ReadAsync(id);
            return View(customer);
        }
        /// <summary>
        /// This is the POST method for deleting a customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _customerRepo.RemoveAsync(id);
            return RedirectToAction("Index");
        }


    }
}
