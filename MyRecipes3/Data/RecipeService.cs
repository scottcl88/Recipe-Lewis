using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRecipes3.Data
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
            try
            {
                _context.Add(new Recipe() { Title = "Test", Date = DateTime.UtcNow });
                var result = await _context.SaveChangesAsync();
                return await Task.FromResult(_context.Recipes.ToList());
            }
            catch (Exception ex)
            {
                var rng = new Random();
                return await Task.FromResult(Enumerable.Range(1, 5).Select(index => new Recipe
                {
                    Date = DateTime.UtcNow.AddDays(index)
                }).ToList());
            }

        }
    }
}
