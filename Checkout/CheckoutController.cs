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
        
        // POST
        //[HttpPost]
        // [Route("finishorderid={ID}name={Name}&saddress={ShippingAddress}&cnum={CreditCardNumber}&cexpmon={CreditCardExperationMonth}&cexpyear={CreditCardExperationYear}&ccvv={CreditCardCVV}&cart={Cart}")]
        // public async Task<IResult> PostOrder(int id,string Name,string ShippingAddress,int CreditCardNumber,int CreditCardExperationMonth,int CreditCardExperationYear,int CreditCardCVV,List<Items> Cart)
        // {
        //     double CartPrice = 0;
        //     Checkout tempOrder = new Checkout();
        //     tempOrder.Id = id;
        //     tempOrder.Name = Name;
        //     tempOrder.ShippingAddress = ShippingAddress;
        //     tempOrder.CreditCardNumber = CreditCardNumber;
        //     tempOrder.CreditCardExperationMonth = CreditCardExperationMonth;
        //     tempOrder.CreditCardExperationYear = CreditCardExperationYear;
        //     tempOrder.CreditCardCVV = CreditCardCVV;
        //     tempOrder.Cart = Cart;

        //     for (int i = 0; i < Cart.Count; i++)
        //     {
        //         CartPrice += Cart[i].unit_price * Cart[i].quantity;
        //     }
        //     tempOrder.TotalPrice = CartPrice;
            
        //     _orders.Checkout.Add(tempOrder);
        //     await _orders.SaveChangesAsync();
        //     return Results.Created($"/{tempOrder.Id}", tempOrder);
        // }
        
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
    }
}