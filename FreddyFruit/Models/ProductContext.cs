using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace FreddyFruit.Models
{
    public class ProductContext : DbContext
    {
        public ProductContext() : base("FreddyFruit")
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<CartItem> ShoppingCartItems { get; set; }
    }
}