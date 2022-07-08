using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Controllers
{
    [ApiController]
    [Route("catalog")]
    public class CatalogRESTController : ControllerBase
    {
    private readonly ItemDb _db;

        public CatalogRESTController(ILogger<CatalogRESTController> logger, ItemDb db)
        {            
            _db = db;
        }


// GETS

        [HttpGet]
        public async Task<ActionResult<List<Item>>> GetAllItems()
        {
            return await _db.Items.ToListAsync();
        }

        /*   [HttpGet]
        [Route("allcompleteditems")]
        public async Task<ActionResult<List<Item>>> GetAllCompleteedItems()
        {
            return await _db.Itemss.Where(t => t.IsComplete).ToListAsync();
        } */

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(long id)
        {
            var item = await _db.Items.FindAsync(id);

            if(item == null)
                return NotFound();
            
            return Ok(item);
        }

// POSTS 

        [HttpPost]
        public async Task<IResult> PostItem(Item item)
        {
            _db.Items.Add(item);
            await _db.SaveChangesAsync();


            return Results.Created($"/{item.Id}", item);
        }


//PUT

        [HttpPut]
        public async Task<IResult>  UpdateItem(Item item)
        {
            var updatedItem = await _db.Items.FindAsync(item.Id);

            if(updatedItem == null)
                return Results.NotFound();
            
            updatedItem.title = item.title;
            updatedItem.description = item.description;
            updatedItem.unit_price = item.unit_price;
            updatedItem.quantity = item.quantity;

            await _db.SaveChangesAsync();

            return Results.NoContent();
        }

//DELETE

        [HttpDelete("{id}")]
        public async Task<IResult> DeleteItem(long id)
        {
            if(await _db.Items.FindAsync(id) is Item item)
            {
                _db.Items.Remove(item);
                await _db.SaveChangesAsync();
                return Results.Ok(item);
            }
            return Results.NotFound();
        }
    }

    
    [ApiController]
    [Route("Cart")]
    public class CheckoutRESTController : ControllerBase
    {

    }
}