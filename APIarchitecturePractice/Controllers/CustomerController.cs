using APIarchitecturePractice.DTOs;
using APIarchitecturePractice.Helper;
using APIarchitecturePractice.Models;
using APIarchitecturePractice.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace APIarchitecturePractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;
        private readonly Messages messages;


        public CustomerController(ICustomerService _customerService,
                                  IOptions<Messages> _messages)
        {
            customerService = _customerService;
            messages = _messages.Value;
        }

        [HttpGet]
        [Route("getAllCustomers")]
        public async Task<ActionResult> GetAllCustomers()
        {
            var allCustomers = await customerService.GetAllCustomers_Service();
            return Ok(allCustomers);
        }
        

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> AddCustomer([FromBody] AddCustomerDTO addCustomerDTO)
        {
            if(!ModelState.IsValid) // why
            {
                return BadRequest(ModelState);
            }

            try
            {
                await customerService.AddCustomer_Service(addCustomerDTO);
                return Ok("Customer has been added");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        [HttpGet]
        [Route("getCustomerAndOrders")]
        [ProducesResponseType(200, Type = typeof(CustomerOrderJoinDTO))]
        public async Task<ActionResult> GetCustomerAndOrders()
        {
            //throw new Exception("Error from customer controller");
            try
            {
                var getJoins = await customerService.GetCustomerOrder();
                if(getJoins == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, "No records found");
                }

                return Ok(getJoins);

            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("getCustomerAndOrders2")]
        //[ProducesResponseType(200, Type = typeof(CustomerService.ResponseObject))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<ActionResult> GetCustomerAndOrders2()
        {
            try
            {
                var gotReturnObject = await customerService.GetCustomerOrder2();
                if (gotReturnObject.CustomerOrdersList.Any() == false)
                {
                    return StatusCode(StatusCodes.Status404NotFound, gotReturnObject.Message);
                }

                return Ok(gotReturnObject);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        [HttpGet]
        [Route("GetCustomerSpecificOrders")]
        public async Task<ActionResult> GetCustomerSpecificOrders(int _customerId = 0)
        {
            if (_customerId <= 0)
            {
                return BadRequest("Enter a customerID greater than 0");
            }

            var gotAllOrdersOfCustomer = await customerService
                                            .GetCustomerSpecificOrders_Service(_customerId);
            return Ok(gotAllOrdersOfCustomer);
        }



        /// <summary>
        /// Used to delete customer
        /// </summary>
        /// <param name="_customerID">ANCHAL</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete")]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<ActionResult> DeleteCustomer(int _customerID)
        {
            try
            {
                string gotResponse = await customerService.DeleteCustomer_Service(_customerID);
                return Ok(gotResponse);
                //return StatusCode(200, gotResponse);
                //return StatusCode(StatusCodes.Status200OK, gotResponse);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



       [HttpPatch]
       [Route("updateName")]
       [ProducesResponseType(200, Type = typeof(Customer))]
       public async Task<ActionResult> UpdateCustomerName(int _customerID, string _customerName)
       {
           Customer gotResponse = await customerService.UpdateCustomer_Service(_customerID, _customerName);
           return Ok(gotResponse);
           //return StatusCode(200, gotResponse);
       }



        /// <summary>
        /// Used to get AppSettings information.
        /// </summary>
        [HttpGet]
        [Route("getAppsettings")]
        public async Task<ActionResult> GetAppSettings()
        {

            var gotMessage = messages.ExceptionMessage;
            return StatusCode(StatusCodes.Status500InternalServerError, gotMessage);
        }

    }
}
