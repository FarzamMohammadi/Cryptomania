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
using Newtonsoft.Json.Linq;

namespace cryptomania.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptosController : ControllerBase
    {
        private readonly CryptoContext _context;

        public CryptosController(CryptoContext context)
        {
            _context = context;
            AddCryptoToDB();
         
        }

        public void AddCryptoToDB()
        {
           /* string[] currencyList = { "BTC", "ETH", "BNB", "LUNA", "SOL", "ADA", "CRP", "DOT", "DOGE", "AVAX" };
            foreach (string currency in currencyList)
            {
                Crypto currencyToAdd = GetCrpytoInfo(currency);
                if (currencyToAdd != null)
                {
                    bool operationComplete = PostCrypto(currencyToAdd).IsCompleted;
                    while (!operationComplete)
                    {
                        System.Threading.Thread.Sleep(1000);

                    }
                }
            }*/
        }
        // GET: api/Cryptos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Crypto>>> GetCryptos()
        {
            return await _context.Cryptos.ToListAsync();
        }

        // GET: api/Cryptos/5
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

        // PUT: api/Cryptos/5
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

        // POST: api/Cryptos
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
                    await PutCrypto(crypto.Id, crypto);
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCrypto", new { id = crypto.Id }, crypto);
        }

        // DELETE: api/Cryptos/5
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

        public Crypto GetCrpytoInfo(string currencytId)
        {
            Crypto currencyToReturn = new Crypto();
            string url = "https://api.nomics.com/v1/currencies/ticker";
            string urlParameter = "?key=f93bfb1a0c1f0bc759f2a4f51566b66fe594929b&ids>=" + currencytId + "&interval=1d&per-page=100&page=1&";

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
                List<JProperty> dataProperties = new List<JProperty>();
                List<JProperty> oneDayAnalysis = new List<JProperty>();
                foreach (var property in data)
                {
                    dataProperties.Add(property);
                }
                dynamic oneDay = null;
                if (dataProperties.Count == 24)
                {
                    currencyToReturn.Id = dataProperties[0].Value.ToString();
                    currencyToReturn.Name = dataProperties[3].Value.ToString();
                    currencyToReturn.LogoUrl = dataProperties[4].Value.ToString();
                    currencyToReturn.Price = dataProperties[6].Value.ToString();
                    currencyToReturn.MarketCap = dataProperties[11].Value.ToString();
                    oneDay = dataProperties[23];

                    foreach (var item in oneDay)
                    {
                        foreach (JProperty value in item)
                        {
                            oneDayAnalysis.Add(value);
                        }
                    }
                    currencyToReturn.PriceChangePct = oneDayAnalysis[2].Value.ToString();
                    return currencyToReturn;
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                return null;
            }
            // Dispose once all HttpClient calls are complete.
            client.Dispose();
            return null;

        }
    }
}
