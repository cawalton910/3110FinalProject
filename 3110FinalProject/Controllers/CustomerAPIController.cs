using _3110FinalProject.Models.ViewModels;
using _3110FinalProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace _3110FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAPIController : Controller
    {
        private readonly ICustomerRepository _customerRepo;

        public CustomerAPIController(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }






        
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var customer = await _customerRepo.ReadAsync(id);
        //    if (customer == null)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    var customerVM = new CustomerVM
        //    {
        //        Id = customer.Id,
        //        FirstName = customer.FirstName,
        //        LastName = customer.LastName,
        //        Purchases = customer.Purchases,
        //    };
        //    return View(customerVM);
        //}


        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromForm]CustomerVM customerVM)
        {
            var customer = customerVM.GetCustomerInstance();
            await _customerRepo.UpdateAsync(customer.Id, customer);
            return NoContent();
        }








        //public IActionResult Create()
        //{
        //    return View();
        //}


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] CustomerVM customerVM)
        {
            var customer = customerVM.GetCustomerInstance();
            await _customerRepo.CreateAsync(customer);
            return NoContent();
        }







        //public async Task<IActionResult> Delete(int id)
        //{
        //    var customer = await _customerRepo.ReadAsync(id);
        //    return View(customer);
        //}


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _customerRepo.RemoveAsync(id);
            return NoContent();
        }
    }
}
