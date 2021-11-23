using cryptomania.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cryptomania.DataAccess
{
    public class CryptoContext : DbContext
    {
        public CryptoContext(DbContextOptions options) : base(options) { }
        public DbSet<Crypto> Cryptos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Cart> Carts { get; set; }
    }
}
