using RecipeLewis.Data;
using System.Linq;

namespace RecipeLewisTests
{
    public class TestRecipeDbSet : TestDbSet<Recipe>
    {
        public override Recipe Find(params object[] keyValues)
        {
            return this.SingleOrDefault(product => product.RecipeID == (int)keyValues.Single());
        }
    }
}
