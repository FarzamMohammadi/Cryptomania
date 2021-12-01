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
using Newtonsoft.Json;

namespace cryptomania.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptosController : ControllerBase
    {
        private readonly CryptoContext _context;

        public CryptosController(CryptoContext context)
        {
            // Set context and array of currency to retrieve
            string currencyList = "BTC,ETH,BNB,LUNA,SOL,ADA,CRP,DOT,DOGE,AVAX";
            _context = context;

            //If table records exist delete them so it can be updated
            if (GetTableCount() >= 0) { DeleteAllCryptos().Wait(); }
            
            
            List<Crypto> currenciesToAdd = GetCrpytoInfo(currencyList);
            // If list is null then there was an error retireving the data, thus it does nothing, instead of breaking the code
            if (currenciesToAdd != null)
            {
                foreach (Crypto currencyToAdd in currenciesToAdd)
                {
                    PostCrypto(currencyToAdd).Wait();
                }
            }
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
                    return NoContent();
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

        public List<Crypto> GetCrpytoInfo(string currencytId)
        {
            List<Crypto> currenciesToReturn = new List<Crypto>();
            string url = "https://api.nomics.com/v1/currencies/ticker";
            string urlParameter = "?key=f93bfb1a0c1f0bc759f2a4f51566b66fe594929b&ids=" + currencytId + "&interval=1d&convert=USD&per-page=100&page=1";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.          
            HttpResponseMessage response = client.GetAsync(urlParameter).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                char[] splitterChars = { ',', '{', '"', 'i', 'd', '"', ':', };
                string splitter = new string(splitterChars);
                // Parse the response body.
                JArray apiCallResults = (JArray)JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                // Goes throguh all the crtytos from the call based on count
                for (int i = 0; i < apiCallResults.Count; i++)
                {
                    Crypto currencyToReturn = new Crypto();
                    dynamic data = JObject.Parse(apiCallResults[i].ToString());
                    List<JProperty> dataProperties = new List<JProperty>();
                    // Sepeartes items to add to list (in a way parses results so it can be accessed/mutated later)
                    foreach (var property in data)
                    {
                        dataProperties.Add(property);
                    }
                    // Sets all crypto properties to be added/update the database
                    foreach(JProperty property in dataProperties)
                    {
                        switch (property.Name)
                        {
                            case "id":
                                currencyToReturn.Id = property.Value.ToString();
                                break;
                            case "logo_url":
                                currencyToReturn.LogoUrl = property.Value.ToString();
                                break;
                            case "name":
                                currencyToReturn.Name = property.Value.ToString();
                                break;
                            case "price":
                                currencyToReturn.Price = property.Value.ToString();
                                break;
                            case "market_cap":
                                currencyToReturn.MarketCap = property.Value.ToString();
                                break;
                            case "1d":
                                // Sepeartes items to add to list(in a way parses results so it can be accessed / mutated later)
                                foreach (var item in property)
                                {
                                    foreach(JProperty oneDayProp in item)
                                    {
                                        if (oneDayProp.Name == "price_change_pct")
                                        {
                                            currencyToReturn.PriceChangePct = oneDayProp.Value.ToString();
                                        }
                                    }    
                                }
                                break;
                        }
                    }
                    currenciesToReturn.Add(currencyToReturn);
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                client.Dispose();
                return null;
            }
            // Dispose once all HttpClient calls are complete.
            client.Dispose();
            return currenciesToReturn;

        }

        private int GetTableCount()
        {
            int count = _context.Cryptos.Select(x => x.Id).Count();

            return count;
        }
        private string[] GetTableIds()
        {
            // Great query for future project reference
            string[] ids = _context.Cryptos.AsEnumerable().Select(row => row.Id.ToString()).ToArray();

            return ids;
        }


        // DELETE: api/Cryptos/deleteall
        [HttpDelete("deleteall")]
        private async Task<bool> DeleteAllCryptos()
        {
            string[] ids = GetTableIds();
            foreach(string id in ids)
            {
                 await DeleteCrypto(id.ToString());
            }
            return true;
        }
    }
}
