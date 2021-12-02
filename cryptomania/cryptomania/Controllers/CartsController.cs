using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cryptomania.DataAccess;
using cryptomania.Models;

namespace cryptomania.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly CryptoContext _context;

        public CartsController(CryptoContext context)
        {
            _context = context;
        }

        // GET: api/Carts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
        {
            return await _context.Carts.ToListAsync();
        }

        // GET: api/Carts/5
        [HttpGet("{username}")]
        public ActionResult<Cart> GetCart(string username)
        {
            var cart = _context.Carts
                     .Where(b => b.Username == username)
                     .FirstOrDefault();

            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }

        // PUT: api/Carts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<IActionResult> PutCart(Cart cart)
        {
            if (cart.Id == null || cart.Id == "")
            {
                return BadRequest();
            }

            _context.Entry(cart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(cart.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Carts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Cart>> PostCart(Cart passedCart)
        {
            Cart cart = GetWalletByUsername(passedCart.Username);

            if (cart == null)
            {
                int tableRecords = GetTableCount();
                passedCart.Id = (tableRecords + 1).ToString();
                _context.Carts.Add(passedCart);
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CartExists(passedCart.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCart", new { id = passedCart.Id }, passedCart);
        }

        // DELETE: api/Carts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cart>> DeleteCart(string id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();

            return cart;
        }

        private bool CartExists(string id)
        {
            return _context.Carts.Any(e => e.Id == id);
        }

        private int GetTableCount()
        {
            int count = _context.Carts.Select(x => x.Id).Count();

            return count;
        }

        private Cart GetWalletByUsername(string Username)
        {
            Cart cart = _context.Carts
                   .Where(b => b.Username == Username)
                   .FirstOrDefault();
            return cart;
        }
    }
}
