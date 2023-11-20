using APIarchitecturePractice.DTOs;
using APIarchitecturePractice.Services;
using APIarchitecturePractice.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIarchitecturePractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        public IConfiguration configuration { get; }

        public OrderController(IOrderService _orderService, IConfiguration _configuration)
        {
            orderService = _orderService;
            configuration = _configuration;
        }


        [HttpGet]
        [Route("getAllOrders")]
        public async Task<ActionResult> GetAllOrders()
        {
            var allOrders = await orderService.GetAllOrders_Service();
            return Ok(allOrders);
        }


        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> AddOrder([FromBody] AddOrderDTO addOrderDTO)
        {
            try
            {
                await orderService.AddOrder_Service(addOrderDTO);
                return Ok("Order has been added");

            }
            catch(Exception ex)
            {
                return BadRequest($"Response from order controller : { ex.Message}");
            }
        }



        [HttpGet]
        [Route("GroupOrdersByLocation")]
        public async Task<ActionResult> GroupOrdersByLocation()
        {
            var gotOrders = await orderService.GroupOrdersByLocation_Service();
            return Ok(gotOrders);
        }



        /// <summary>
        /// Used to get AppSettings information.
        /// </summary>
        [HttpGet]
        [Route("getAppsettings")]
        public async Task<ActionResult> GetAppSettings()
        {

            // Direct key value pairs
            //string gotKey = configuration["GoogleKey"];
            //return Ok(gotKey);

            // Nested key value pairs
            string gotMessage = configuration["Messages:ExceptionMessage"];
            return Ok(gotMessage);
        }
    }
}
