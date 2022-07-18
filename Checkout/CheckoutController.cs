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
        [Route("finishorderinformation")]
        public async Task<IResult> PostOrderInformation(PersonalOrderInformation orderinformation)
        {
            _orders.Checkout.Add(orderinformation);
            await _orders.SaveChangesAsync();

            return Results.Created($"/{orderinformation.Id}", orderinformation);
        }
        [HttpPost]
        [Route("finishordercart/{id}")]
        public async Task<IResult> PostCartInformation(List<Item> cart, long id)
        {
            for (int i = 0; i < cart.Count; i++)
            {
                cart[i].CheckoutId = id;
                _orders.Cart.Add(cart[i]);
            }
            await _orders.SaveChangesAsync();
            return Results.Created($"/{id}", cart);
        }
        //Get
        [HttpGet("orderinformation/{id}")]
        public async Task<ActionResult<Checkout>> GetOrder(long id)
        {
            var order = await _orders.Checkout.FindAsync(id);
            if(order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
        [HttpGet("cartinformation/{id}")]
        public async Task<ActionResult<Checkout>> GetCart(long id)
        {
            var CartInformation = _orders.Cart.Where(i => i.CheckoutId == id).ToList();
            if(CartInformation == null)
            {
                return NotFound();
            }
            return Ok(CartInformation);
        }

        //Put
        [HttpPut]
        [Route("changeorderinformation")]
        public async Task<IResult> updateOrderInformation(PersonalOrderInformation order)
        {
            var order2 = await _orders.Checkout.FindAsync(order.Id);
            if (order2 == null)
            {
                return Results.NotFound();
            }
            order2.Name = order.Name;
            order2.ShippingAddress = order.ShippingAddress;
            order2.CreditCardNumber = order.CreditCardNumber;
            order2.CreditCardExperationMonth = order.CreditCardExperationMonth;
            order2.CreditCardExperationYear = order.CreditCardExperationYear;
            order2.CreditCardCVV = order.CreditCardCVV;

            await _orders.SaveChangesAsync();
            return Results.NoContent();
        }
        [HttpPut]
        [Route("changecartinformation/{id}")]
        public async Task<IResult> updateCartInformation(List<Item> cart, long id)
        {
            var orderinformation = await _orders.Checkout.FindAsync(id);
            var CartInformation = _orders.Cart.Where(i => i.CheckoutId == id).ToList();
            for (int i = 0; i < CartInformation.Count; i++)
            {
                _orders.Cart.Remove(CartInformation[i]);
            }
            for (int i = 0; i < cart.Count; i++)
            {
                cart[i].CheckoutId = orderinformation.Id;
                _orders.Cart.Add(cart[i]);
            }
            await _orders.SaveChangesAsync();
            return Results.NoContent();
        }

        //Delete
        [HttpDelete]
        [Route("deleteorderinformation/{id}")]
        public async Task<IResult> DeleteOrderInformation(long id)
        {
            if (await _orders.Checkout.FindAsync(id) is PersonalOrderInformation orderinformation)
            {
                _orders.Remove(orderinformation);
                await _orders.SaveChangesAsync();
                return Results.Ok(orderinformation);
            }
            return Results.NotFound();
        }
        [HttpDelete]
        [Route("deletecartinformation/{id}")]
        public async Task<IResult> DeleteCartInformation(long id)
        {
            var CartInformation = _orders.Cart.Where(i => i.CheckoutId == id).ToList();
            if (CartInformation == null)
            {
                return Results.NotFound();
            }
            for (int i = 0; i < CartInformation.Count; i++)
            {
                _orders.Cart.Remove(CartInformation[i]);
            }
            await _orders.SaveChangesAsync();
            return Results.Ok(CartInformation);
        }
    }
}