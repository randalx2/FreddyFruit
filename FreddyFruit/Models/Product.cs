using System;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FreddyFruit.Models
{
    public class Product
    {
        /// <summary>
        /// Unique ID of each product
        /// </summary>
        [ScaffoldColumn(false)]
        public int ProductID { get; set; }

        /// <summary>
        /// Identifies products by name
        /// </summary>
        [Required, StringLength(100), Display(Name = "Name")]
        public string ProductName { get; set; }

        /// <summary>
        /// Used to give the customer product details
        /// </summary>
        [Required, StringLength(10000), Display(Name = "Product Description"), DataType(DataType.MultilineText)]
        public string Description { get; set; }

        /// <summary>
        /// Used to display product specials to the user
        /// </summary>
        [Required, StringLength(10000), Display(Name = "Product Special"), DataType(DataType.MultilineText)]
        public string Special { get; set; }

        /// <summary>
        /// Path to images stored locally on the server for each product
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// Price per single product unit
        /// </summary>
        [Display(Name = "Price")]
        public double? UnitPrice { get; set; }

        /// <summary>
        /// Foreign Key
        /// </summary>
        public int? CategoryID { get; set; }

        /// <summary>
        /// Navigation Property
        /// </summary>
        public virtual Category Category { get; set; }
    }
}