using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeLewis.Data
{
    public class RecipeService
    {
        public RecipeService(ApplicationDbContext context)
        {
            _context = context;
        }
        public ApplicationDbContext _context { get; set; }
        public async Task<List<Recipe>> GetForecastsAsync()
        {
            //try
            //{
                Console.WriteLine("Forecasts started");
                _context.Add(new Recipe() { Title = "Test", Date = DateTime.UtcNow });
                var result = await _context.SaveChangesAsync();
                Console.WriteLine("Forecast added");
                return await Task.FromResult(_context.Recipes.ToList());
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Forecast failed: "+ex.Message);
            //    var rng = new Random();
            //    return await Task.FromResult(Enumerable.Range(1, 5).Select(index => new Recipe
            //    {
            //        Date = DateTime.UtcNow.AddDays(index)
            //    }).ToList());
            //}

        }
    }
}
