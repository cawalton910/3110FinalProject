using _3110FinalProject.Models.ViewModels;
using _3110FinalProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace _3110FinalProject.Controllers
{
    /// <summary>
    /// This is the API controller for creating, editing, and deleting customers
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAPIController : Controller
    {
        private readonly ICustomerRepository _customerRepo;

        public CustomerAPIController(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }   
        /// <summary>
        /// This method updates the customers properties
        /// </summary>
        /// <param name="customerVM">Customer View Model coming from a form</param>
        /// <returns></returns>
        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromForm]CustomerVM customerVM)
        {
            var customer = customerVM.GetCustomerInstance();
            await _customerRepo.UpdateAsync(customer.Id, customer);
            return NoContent();
        }
        /// <summary>
        /// This method creates a customer
        /// </summary>
        /// <param name="customerVM">Customer View Model coming from a form</param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] CustomerVM customerVM)
        {
            var customer = customerVM.GetCustomerInstance();
            await _customerRepo.CreateAsync(customer);
            return NoContent();
        }
        /// <summary>
        /// This method deletes a customer
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _customerRepo.RemoveAsync(id);
            return NoContent();
        }
    }
}
