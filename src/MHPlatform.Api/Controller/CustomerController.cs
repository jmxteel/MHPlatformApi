using MHPlatform.Api.Common.Response;
using MHPlatform.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MHPlatform.Api.Controller
{
    [Route("api/customer")]
    [Authorize(Policy = "CanAccessProducts")]
    [ApiController]
    public class CustomerController: ControllerBase
    {
        private readonly IOrderFormService _orderFormService;

        public CustomerController(IOrderFormService orderFormService)
        {
            _orderFormService = orderFormService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var responseOptions = new ApiResponseOptions();
            var result = await _orderFormService.GetCustomerById(id);

            if (result is null)
            {
                responseOptions.IsSuccess = false;
                responseOptions.Message = "Customer not found";
                responseOptions.ErrorCode = 404;
                responseOptions.ErrorDetails = $"No customer found with ID {id}";
            }

            return Ok(ApiResponseFactory.Create(result, responseOptions));
        }
    }
}
