using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace FreddyFruit.Models
{
    //Used to seed the database with test data

    //TODO Inherit from DropCreateDatabaseIfModelChanges<ProductContext> after resetting the db

    public class ProductDatabaseInitializer : DropCreateDatabaseAlways<ProductContext>
    {
        protected override void Seed(ProductContext context)
        {
            GetCategories().ForEach(c => context.Categories.Add(c));
            GetProducts().ForEach(p => context.Products.Add(p));
        }

        private static List<Category> GetCategories()
        {
            var categories = new List<Category> {
                new Category
                {
                    CategoryID = 1,
                    CategoryName = "Fruits"
                },
                new Category
                {
                    CategoryID = 2,
                    CategoryName = "Vegetables"
                }
            };

            return categories;
        }

        private static List<Product> GetProducts()
        {
            var products = new List<Product> {
                new Product
                {
                    ProductID = 1,
                    ProductName = "Apples",
                    Description = "Our apples are just what you need to keep the dentist away." 
                                  + " Sweet, succulent with a shiny red colour that will be enough to tempt Adam himself.",
                    Special = "R5 for every 3 apples purchased.",
                    ImagePath="apples.jpg",
                    UnitPrice = 2.00,
                    CategoryID = 1
               },

                new Product
                {
                    ProductID = 2,
                    ProductName = "Bananas",
                    Description = "No need to swing from trees like Tarzan. " 
                                  + "Just place an order with us and we will satisfy your cravings for this soft fruit within one business day."
                                  + " Just don't let your girlfriend be seen eating one of our bananas in public as they are yuuuuge.",
                    Special = "Due to limited stock you may only order a maximum of 10 bananas.",
                    ImagePath="Bananas.jpg",
                    UnitPrice = 3.00,
                    CategoryID = 1
               },

                new Product
                {
                    ProductID = 3,
                    ProductName = "Coconuts",
                    Description = "Is it a fruit or vegetable? Our web developer wasn't sure. " 
                                  + "It ain't easy getting guys to climb up tall trees for these without insurance." 
                                  + " Besides..we know you really need this because your mom needs something to throw on the ground til it breaks!",
                    Special = "Get 1 free for every 2 coconuts purchased.",
                    ImagePath="Coconuts.jpg",
                    UnitPrice = 4.00,
                    CategoryID = 2
                },
               
            };

            return products;
        }
    }
}