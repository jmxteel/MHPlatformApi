using MHPlatform.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MHPlatform.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class SampleController: ControllerBase
    {
        private readonly IOrderFormService _orderFormService;

        public SampleController(IOrderFormService orderFormService)
        {
            _orderFormService = orderFormService;
        }

        [HttpGet("test/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _orderFormService.GetCustomerById(id);

            return Ok(result);
        }
    }
}
