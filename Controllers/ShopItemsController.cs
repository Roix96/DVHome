using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DVHome.Models;

namespace DVHome.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopItemsController : ControllerBase
    {
        private readonly UserContext _context;

        public ShopItemsController(UserContext Context)
        {
            _context = Context;
        }


        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShopItem>>> GetShopItems()
        {
            if (_context.Items == null)
            {
                return NotFound();
            }
            return await _context.Items.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<ShopItem>> InsertShopItem([FromHeader] string Token, [FromBody] ShopItem Item)
        {
            if (_context.Items == null)
            {
                return Problem("Entity set 'UserContext.Item'  is null.");
            }
            if (_context.Users == null)
            {
                return Problem("Entity set 'UserContext.Users'  is null.");
            }

            if (!IsAuthenticated(Token))
            {
                return Unauthorized();
            }

            Item.AddedBy = GetUserByToken(Token).Username;
            DateTimeOffset now = DateTimeOffset.UtcNow;
            Item.AddedAt = now.ToUnixTimeMilliseconds();
            Item.LastModifiedBy = null;
            Item.ModifiedAt = null;
            _context.Items.Add(Item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShopItems", new { item = Item.Item }, Item);
        }

        [HttpPut]
        public async Task<ActionResult<ShopItem>> EditShopItem([FromHeader] string Token, [FromBody] ShopItem Item)
        {
            if (_context.Items == null)
            {
                return Problem("Entity set 'UserContext.Item'  is null.");
            }
            if (_context.Users == null)
            {
                return Problem("Entity set 'UserContext.Users'  is null.");
            }

            if (!IsAuthenticated(Token))
            {
                return Unauthorized();
            }

            ShopItem DbItem = await _context.Items.FirstAsync(e => e.Item.ToLower() == Item.Item.ToLower());
            if (DbItem == null)
            {
                return NotFound();
            }

            DbItem.Count = Item.Count;
            DbItem.LastModifiedBy = GetUserByToken(Token).Username;
            DateTimeOffset now = DateTimeOffset.UtcNow;
            DbItem.ModifiedAt = now.ToUnixTimeMilliseconds();

            _context.Entry(DbItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();

        }


        [HttpPut("add")]
        public async Task<ActionResult<ShopItem>> AddShopItem([FromHeader] string Token, [FromBody] ShopItem Item)
        {
            if (_context.Items == null)
            {
                return Problem("Entity set 'UserContext.Item'  is null.");
            }
            if (_context.Users == null)
            {
                return Problem("Entity set 'UserContext.Users'  is null.");
            }

            if (!IsAuthenticated(Token))
            {
                return Unauthorized();
            }

            ShopItem DbItem = await _context.Items.FirstAsync(e => e.Item.ToLower() == Item.Item.ToLower());
            if (DbItem == null)
            {
                return NotFound();
            }

            DbItem.Count = DbItem.Count + Item.Count;
            DbItem.LastModifiedBy = GetUserByToken(Token).Username;
            DateTimeOffset now = DateTimeOffset.UtcNow;
            DbItem.ModifiedAt = now.ToUnixTimeMilliseconds();

            _context.Entry(DbItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();

        }

        [HttpPut("remove")]
        public async Task<ActionResult<ShopItem>> RemoveShopItem([FromHeader] string Token, [FromBody] ShopItem Item)
        {
            if (_context.Items == null)
            {
                return Problem("Entity set 'UserContext.Item'  is null.");
            }
            if (_context.Users == null)
            {
                return Problem("Entity set 'UserContext.Users'  is null.");
            }

            if (!IsAuthenticated(Token))
            {
                return Unauthorized();
            }

            ShopItem DbItem = await _context.Items.FirstAsync(e => e.Item.ToLower() == Item.Item.ToLower());
            if (DbItem == null)
            {
                return NotFound();
            }

            DbItem.Count = Math.Max(0, DbItem.Count - Item.Count);
            DbItem.LastModifiedBy = GetUserByToken(Token).Username;
            DateTimeOffset now = DateTimeOffset.UtcNow;
            DbItem.ModifiedAt = now.ToUnixTimeMilliseconds();

            _context.Entry(DbItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();

        }

        [HttpDelete("{item}")]
        public async Task<ActionResult<ShopItem>> DeleteShopItem([FromHeader] string Token, [FromRoute] string Item)
        {
            if (_context.Items == null)
            {
                return Problem("Entity set 'UserContext.Item'  is null.");
            }
            if (_context.Users == null)
            {
                return Problem("Entity set 'UserContext.Users'  is null.");
            }

            if (!IsAuthenticated(Token))
            {
                return Unauthorized();
            }

            ShopItem DbItem = await _context.Items.FirstOrDefaultAsync(e => e.Item.ToLower() == Item.ToLower());
            if (DbItem == null)
            {
                return NotFound();
            }


            _context.Items.Remove(DbItem);
            await _context.SaveChangesAsync();


            return Ok();

        }


        public User GetUserByToken(string Token)
        {
            return (_context.Users.First(e => e.Token == Token));
        }

        public bool IsAuthenticated(string Token)
        {
            return (_context.Users.Any(e => e.Token == Token)) ;
        }



    }
}