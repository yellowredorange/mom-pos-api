using Microsoft.AspNetCore.Mvc;
using MomPosApi.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MomPosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }


        [HttpPost]
        public async Task<ActionResult<OrderResponseDto>> CreateOrder([FromBody] JsonElement rawRequest)
        {
            _logger.LogInformation("Received raw order request: {RawRequest}", rawRequest.ToString());

            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    NumberHandling = JsonNumberHandling.AllowReadingFromString
                };

                var request = JsonSerializer.Deserialize<CreateOrderRequestDto>(rawRequest.ToString(), options);

                if (request == null || request.Items == null || !request.Items.Any())
                {
                    _logger.LogWarning("Invalid order request: Empty or null");
                    return BadRequest("Order must contain at least one item");
                }

                var order = await _orderService.CreateOrderAsync(request);
                return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, order);
            } catch (JsonException jsonEx)
            {
                _logger.LogError(jsonEx, "Error deserializing order request");
                return BadRequest("Invalid request format");
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating order");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [HttpGet("history")]
        public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetOrderHistory()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var success = await _orderService.DeleteOrderAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
