using ShopApi.Models;
using ShopLib.Model;
using System;
using System.Linq;

namespace ShopApi.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ShopContext context)
        {
            context.Database.EnsureCreated(); 

            if (context.Shops.Any())
            {
                return;   
            }

            var shops = new Shop[] 
            {
                new Shop{Name="Espresso House Vaasa", Owner="Alex Holmes", NumberOfWorkers=10,Director="Jens Skog"},
                new Shop{Name="Espresso House Turku", Owner="Tom Hanks", NumberOfWorkers=15,Director="Jens Skog"},
                new Shop{Name="Citymarket Vaasa Keskusta", Owner="Markku Tapani Jussila", NumberOfWorkers=30,Director="Jorma Rauhala"},
                new Shop{Name="K-Market Kapteeninkatu", Owner="Mikko Keski-Hirvi", NumberOfWorkers=4,Director="Jorma Rauhala"},
                new Shop{Name="S-Market Palosaari", Owner="Jarmo Palola", NumberOfWorkers=5,Director="Hannu Krook"}

            };
            foreach (Shop s in shops) 
            {
                context.Shops.Add(s); 
            }
            context.SaveChanges(); 

        }
    }
}