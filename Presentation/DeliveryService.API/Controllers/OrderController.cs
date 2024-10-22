using DeliveryService.Application.Enums;
using DeliveryService.Application.Requests.Orders.Read.GetAll;
using DeliveryService.Application.Requests.Orders.Read.GetByDeliveryAreaAndTime;
using DeliveryService.Application.Requests.Orders.Read.GetById;
using DeliveryService.Application.Requests.Orders.Write.Create;
using DeliveryService.Application.Requests.Orders.Write.Delete;
using DeliveryService.Application.Requests.Orders.Write.Patch;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrderController : BaseController
    {
        /// <summary>
        /// Creates the order
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /order/Create
        /// {
        ///
        ///     weight: "order weight in kilograms",
        ///     deliveryDateTime: "order delivery time (maximum 60 days)"
        /// }
        /// </remarks>
        /// <param name="dto">CreateOrderDTO object</param>
        /// <param name="district">Order district (enum)</param>
        /// <returns>Returns CreateOrderResponse</returns>
        /// <response code="201">Success</response>
        /// <response code="400">The server could not understand the request due to incorrect syntax</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateOrderDTO dto, [FromQuery] District district)
        {
            CreateOrderRequest request = new CreateOrderRequest
            {
                DeliveryDateTime = dto.DeliveryDateTime,
                Weight = dto.Weight,
                DistrictName = district.ToString()
            };

            CreateOrderResponse response = await Mediator.Send(request);
            return Created("", response);
        }

        /// <summary>
        /// Change order status to completed
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PATCH /order/Patch/bd36fda0-37f4-4dd4-8cb0-c223847ef691
        /// </remarks>
        /// <param name="id">Order id (guid)</param>
        /// <returns>Returns PatchOrderResponse</returns>
        /// <response code="200">Success</response>
        /// <response code="400">The server could not understand the request due to incorrect syntax</response>
        /// <response code="404">The server can not find the requested resource.</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Patch(Guid id)
        {
            PatchOrderRequest request = new PatchOrderRequest
            {
                Id = id
            };

            PatchOrderResponse response = await Mediator.Send(request);
            return Ok(response);
        }

        /// <summary>
        /// Delete the order
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /order/Delete/bd36fda0-37f4-4dd4-8cb0-c223847ef691
        /// </remarks>
        /// <param name="id">Order id (guid)</param>
        /// <returns>Returns DeleteOrderResponse</returns>
        /// <response code="200">Success</response>
        /// <response code="400">The server could not understand the request due to incorrect syntax</response>
        /// <response code="404">The server can not find the requested resource.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            DeleteOrderRequest request = new DeleteOrderRequest
            {
                Id = id,
            };
            DeleteOrderResponse response = await Mediator.Send(request);

            return Ok(response);
        }

        /// <summary>
        /// Gets a list of all orders
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /order/GetAll
        /// </remarks>
        /// <returns>Returns GetAllOrderResponse</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            GetAllOrderRequest request = new GetAllOrderRequest();

            List<GetAllOrderResponse> responce = await Mediator.Send(request);
            return Ok(responce);
        }

        /// <summary>
        /// Gets the order by ID
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /order/GetById/bd36fda0-37f4-4dd4-8cb0-c223847ef691
        /// </remarks>
        /// <param name="id">Order id (guid)</param>
        /// <returns>Returns GetByIdOrderResponse</returns>
        /// <response code="200">Success</response>
        /// <response code="400">The server could not understand the request due to incorrect syntax</response>
        /// <response code="404">The server can not find the requested resource.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            GetByIdOrderRequest request = new GetByIdOrderRequest
            {
                Id = id,
            };
            GetByIdOrderResponse responce = await Mediator.Send(request);
            return Ok(responce);
        }

        /// <summary>
        /// Get a list of orders by district and the time of the first order. 
        /// Displays a list of orders to be delivered to a specific area of the city within half an hour after the time of the first order.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /order/GetByDeliveryAreaAndTime?district=Partisans?firstDeliveryDateTime=yyyy-MM-ddTHH:mm:ss
        /// </remarks>
        /// <param name="district">Order district (enum)</param>
        /// <param name="firstDeliveryDateTime">Order firstDeliveryDateTime (datetime)</param>
        /// <returns>Returns GetByDeliveryAreaAndTimeOrderResponse</returns>
        /// <response code="200">Success</response>
        /// <response code="400">The server could not understand the request due to incorrect syntax</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByDeliveryAreaAndTime([FromQuery] District district, [FromQuery] DateTime firstDeliveryDateTime)
        {
            GetByDeliveryAreaAndTimeOrderRequest request = new GetByDeliveryAreaAndTimeOrderRequest
            {
                DistrictName = district.ToString(),
                FirstDeliveryDateTime = firstDeliveryDateTime
            };

            List<GetByDeliveryAreaAndTimeOrderResponse> response = await Mediator.Send(request);
            return Ok(response);
        }
    }
}
