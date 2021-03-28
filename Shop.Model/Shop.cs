using System;
using System.ComponentModel.DataAnnotations;

namespace ShopLib.Model
{
    public class Shop
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
        public string Owner { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Only positive numbers allowed")]
        public int NumberOfWorkers { get; set; }
        public string Director { get; set; }
    }
}
