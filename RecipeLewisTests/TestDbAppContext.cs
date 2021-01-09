using Microsoft.EntityFrameworkCore;
using RecipeLewis.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeLewisTests
{
    public class TestDbAppContext : IAppDbContext
    {
        public TestDbAppContext()
        {
            this.Recipes = new TestRecipeDbSet();
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public void Add(Recipe data)
        {
            Recipes.Add(data);
        }

        public Task<int> SaveChangesAsync()
        {
            return Task.FromResult(0);
        }

        public void Update(Recipe data)
        {
            var oldData = Recipes.FirstOrDefault(x => x.RecipeID == data.RecipeID);
            Recipes.Remove(oldData);
            Recipes.Add(data);
        }
    }
}
