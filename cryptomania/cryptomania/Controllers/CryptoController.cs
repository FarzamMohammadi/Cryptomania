using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cryptomania.DataAccess;
using cryptomania.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace cryptomania.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        private readonly CryptoContext _context;
        public class DataObject
        {
            public string name { get; set; }
        }


        public void GetCrpytoInfo(string currencytId)
        {
            string url = "https://api.nomics.com/v1/currencies/ticker";
            string urlParameter = "?key=<InsertKeyHere>=" + currencytId + "&interval=1d&per-page=100&page=1&";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParameter).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                string dataObjects = response.Content.ReadAsStringAsync().Result;
                JArray jsonArray = JArray.Parse(dataObjects);
                dynamic data = JObject.Parse(jsonArray[0].ToString());
                foreach(var item in data)
                {
                    Console.WriteLine(item);
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            // Make any other calls using HttpClient here.

            // Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            client.Dispose();

        }
        public CryptoController(CryptoContext context)
        {
            GetCrpytoInfo("BTC");
            _context = context;
        }

        // GET: api/Crypto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Crypto>>> GetCryptos()
        {
            return await _context.Cryptos.ToListAsync();
        }

        // GET: api/Crypto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Crypto>> GetCrypto(string id)
        {
            var crypto = await _context.Cryptos.FindAsync(id);

            if (crypto == null)
            {
                return NotFound();
            }

            return crypto;
        }

        // PUT: api/Crypto/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCrypto(string id, Crypto crypto)
        {
            if (id != crypto.Id)
            {
                return BadRequest();
            }

            _context.Entry(crypto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CryptoExists(id))
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

        // POST: api/Crypto
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Crypto>> PostCrypto(Crypto crypto)
        {
            _context.Cryptos.Add(crypto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CryptoExists(crypto.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCrypto", new { id = crypto.Id }, crypto);
        }

        // DELETE: api/Crypto/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Crypto>> DeleteCrypto(string id)
        {
            var crypto = await _context.Cryptos.FindAsync(id);
            if (crypto == null)
            {
                return NotFound();
            }

            _context.Cryptos.Remove(crypto);
            await _context.SaveChangesAsync();

            return crypto;
        }

        private bool CryptoExists(string id)
        {
            return _context.Cryptos.Any(e => e.Id == id);
        }
    }
}
