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
    public class WalletsController : ControllerBase
    {
        private readonly CryptoContext _context;

        public WalletsController(CryptoContext context)
        {
            _context = context;
        }

        // GET: api/Wallets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Wallet>>> GetWallets()
        {
            return await _context.Wallets.ToListAsync();
        }
/*
        // GET: api/Wallets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Wallet>> GetWallet(string id)
        {
            var wallet = await _context.Wallets.FindAsync(id);

            if (wallet == null)
            {
                return NotFound();
            }

            return wallet;
        }*/

        // GET: api/Wallets/5
        [HttpGet("{username}")]
        public ActionResult<Wallet> GetWallet(string username)
        {
            Wallet wallet = _context.Wallets
                    .Where(b => b.Username == username)
                    .FirstOrDefault();

            if (wallet == null)
            {
                return NotFound();
            }

            return wallet;
        }

        // PUT: api/Wallets/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<IActionResult> PutWallet(Wallet passedWallet)
        {

            if (passedWallet.Id != null)
            {
                _context.Entry(passedWallet).State = EntityState.Modified;
            }
            else
            {
                return NotFound();
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (passedWallet.Id == null)
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

        // POST: api/Wallets
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Wallet>> PostWallet(Wallet passedWallet)
        {
            Wallet wallet = GetWalletByUsername(passedWallet.Username);

            if (wallet == null)
            {
                int tableRecords = GetTableCount();
                passedWallet.Id = (tableRecords + 1).ToString();
                _context.Wallets.Add(passedWallet);
            }
            else
            {
                return Conflict();
            }
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (WalletExists(wallet.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetWallet", new { id = passedWallet.Id }, passedWallet);
        }

        // DELETE: api/Wallets/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Wallet>> DeleteWallet(string id)
        {
            var wallet = await _context.Wallets.FindAsync(id);
            if (wallet == null)
            {
                return NotFound();
            }

            _context.Wallets.Remove(wallet);
            await _context.SaveChangesAsync();

            return wallet;
        }

        private bool WalletExists(string id)
        {
            return _context.Wallets.Any(e => e.Id == id);
        }

        private int GetTableCount()
        {
            int count = _context.Wallets.Select(x => x.Id).Count();

            return count;
        }

        private Wallet GetWalletByUsername(string Username)
        {
            Wallet wallet = _context.Wallets
                   .Where(b => b.Username == Username)
                   .FirstOrDefault();
            return wallet;
        }
    }
}
