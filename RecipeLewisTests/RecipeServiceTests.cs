using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RecipeLewis.Data;
using RecipeLewis.Models;
using RecipeLewis.Services;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeLewisTests
{
    [TestClass]
    public class RecipeServiceTests
    {
        private RecipeService _recipeService;
        private Recipe _testRecipe;
        private RecipeModel _testRecipeModel;
        [TestInitialize]
        public void Setup()
        {
            _recipeService = new RecipeService(new TestDbAppContext(), new NullLoggerFactory().CreateLogger<Recipe>());
            _testRecipeModel = new RecipeModel();
        }
        [TestMethod]
        public async Task GetRecipe()
        {
            var result = await _recipeService.GetRecipe(_testRecipeModel.RecipeID);
            Assert.IsTrue(result.Success);
        }
        [TestMethod]
        public async Task GetRecipes()
        {
            var result = await _recipeService.GetAllRecipes();
            Assert.IsTrue(result.Success);
        }
        [TestMethod]
        public async Task AddRecipe()
        {
            var result = await _recipeService.AddRecipe(_testRecipeModel);
            Assert.IsTrue(result.Success);
        }
        [TestMethod]
        public async Task UpdateRecipe()
        {
            var result = await _recipeService.UpdateRecipe(_testRecipeModel);
            Assert.IsTrue(result.Success);
        }
        [TestMethod]
        public async Task DeleteRecipe()
        {
            var result = await _recipeService.DeleteRecipe(_testRecipeModel);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public async Task GetAllBlogsAsync_orders_by_name()
        {

            var data = new List<Recipe>
            {
                new Recipe { Title = "BBB" },
                new Recipe { Title = "ZZZ" },
                new Recipe { Title = "AAA" },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Recipe>>();
            mockSet.As<IDbAsyncEnumerable<Recipe>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Recipe>(data.GetEnumerator()));

            mockSet.As<IQueryable<Recipe>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Recipe>(data.Provider));

            mockSet.As<IQueryable<Recipe>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Recipe>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Recipe>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TestDbAppContext>();
            //mockContext.Setup(c => c.Recipes).Returns(mockSet.Object);

            var service = new RecipeService(mockContext.Object, new NullLoggerFactory().CreateLogger<Recipe>());
            var blogs = await service.GetAllRecipes();

            Assert.AreEqual(3, blogs.DataList.Count);
            Assert.AreEqual("AAA", blogs.DataList[0].Title);
            Assert.AreEqual("BBB", blogs.DataList[1].Title);
            Assert.AreEqual("ZZZ", blogs.DataList[2].Title);
        }
    }
}
