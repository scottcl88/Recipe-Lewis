using Microsoft.AspNetCore.Components;
using Radzen;
using RecipeLewis.Data;
using RecipeLewis.DataExtensions;
using RecipeLewis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeLewis.Services
{
    public class RecipeService
    {
        public RecipeService(ApplicationDbContext context)
        {
            _context = context;
        }
        public ApplicationDbContext _context { get; set; }
        public async Task<List<RecipeModel>> GetAllRecipesAsync()
        {
            //try
            //{
                Console.WriteLine("Forecasts started");
                _context.Add(new Recipe() { Title = "Test", Date = DateTime.UtcNow });
                var result = await _context.SaveChangesAsync();
                Console.WriteLine("Forecast added");
                return await Task.FromResult(_context.Recipes.Select(x => x.ToModel()).ToList());
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
