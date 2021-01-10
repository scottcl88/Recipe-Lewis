using Microsoft.AspNetCore.Mvc;
using RecipeLewis.Services;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeLewis.Areas.Recipes.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RecipesController : Controller
    {
        private readonly RecipeService _recipeService;
        public RecipesController(RecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet("[action]")]
        //3 day cache by paramaters
        [ResponseCache(Duration = 259200, VaryByQueryKeys = new []{ "recipeId", "documentId" })]
        public async Task<ActionResult> GetRecipeImage(int recipeId, int documentId)
        {
            var recipe = await _recipeService.GetRecipe(recipeId);
            var document = recipe.Data.Documents.FirstOrDefault(x => x.DocumentID == documentId);
            return File(document.Bytes, document.ContentType);
        }
    }
}
