using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Controllers
{
    [ApiController]
    [Route("checkout")]
    public class CheckoutController: ControllerBase
    {
        private readonly OrdersDb _orders;

        public CheckoutController(ILogger<CheckoutController> logger, OrdersDb db)
        {
            _orders = db;
        }

        //Post
        [HttpPost]
        [Route("finishorder")]
        public async Task<IResult> PostOrder(Checkout order)
        {
            order.TotalPrice = 0;
            for (int i = 0; i < order.Cart.Count; i++)
            {
                order.TotalPrice += order.Cart[i].unit_price * order.Cart[i].quantity;
            }
            _orders.Checkout.Add(order);
            await _orders.SaveChangesAsync();
            return Results.Created($"/{order.Id}", order);
        }

        //Get
        [HttpGet("order{id}")]
        public async Task<ActionResult<Checkout>> GetOrder(long id)
        {
            var order = await _orders.Checkout.FindAsync(id);
            if(order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
        [HttpGet]
        [Route("allorders")]
        public async Task<ActionResult<List<Checkout>>> GetAllItems()
        {
            return await _orders.Checkout.ToListAsync();
        }

        //Put
        [HttpPut]
        [Route("changeorder")]
        public async Task<IResult> PutOrder(Checkout order)
        {
            var updatedOrder = await _orders.Checkout.FindAsync(order.Id);

            if(updatedOrder == null)
            {
                return Results.NotFound();
            }
            updatedOrder.Name = order.Name;
            updatedOrder.ShippingAddress = order.ShippingAddress;
            updatedOrder.CreditCardNumber = order.CreditCardNumber;
            updatedOrder.CreditCardExperationMonth = order.CreditCardExperationMonth;
            updatedOrder.CreditCardExperationYear = order.CreditCardExperationYear;
            updatedOrder.CreditCardCVV = order.CreditCardCVV;
            updatedOrder.Cart = order.Cart;
            for (int i = 0; i < order.Cart.Count; i++)
            {
                updatedOrder.TotalPrice += updatedOrder.Cart[i].unit_price * updatedOrder.Cart[i].quantity;
            }

            await _orders.SaveChangesAsync();
            return Results.NoContent();
        }

        //Delete
        [HttpDelete]
        [Route("delete{id}")]
        public async Task<IResult> DeleteOrder(long id)
        {
            if (await _orders.Checkout.FindAsync(id) is Checkout order)
            {
                _orders.Checkout.Remove(order);
                await _orders.SaveChangesAsync();
                return Results.Ok(order);
            }
            return Results.NotFound();
        }
    }
}