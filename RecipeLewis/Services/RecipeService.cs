using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeLewis.Data;
using RecipeLewis.DataExtensions;
using RecipeLewis.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeLewis.Services
{
    public class RecipeService
    {
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;

        public RecipeService(ApplicationDbContext context, ILogger<Recipe> logger)
        {
            _context = context;
            _logger = logger;
        }

        private readonly string[] permittedExtensions = new string[] { "gif", "jpg", "jpeg", "png", "tif", "tiff" };

        public async Task<ServiceResult<RecipeModel>> GetAllRecipes()
        {
            try
            {
                _logger.LogInformation("Getting recipes");
                var list = await _context.Recipes.Where(x => x.DeletedDateTime == null).ToListAsync();
                _logger.LogInformation("Recipes returned: " + list.Count);
                return new ServiceResult<RecipeModel>(true) { DataList = list.Select(x => x.ToGridModel()).ToList() };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Recipes failed to get all");
                return new ServiceResult<RecipeModel>(false, ex.Message);
            }
        }

        public async Task<ServiceResult<RecipeModel>> GetRecipe(int recipeId)
        {
            try
            {
                var data = await _context.Recipes.FirstOrDefaultAsync(x => x.RecipeID == recipeId && x.DeletedDateTime == null);
                return new ServiceResult<RecipeModel>(true) { Data = data.ToModel() };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Recipes failed to get all");
                return new ServiceResult<RecipeModel>(false, ex.Message);
            }
        }

        [Authorize(Roles = "Moderator")]
        public async Task<ServiceResult> AddRecipe(RecipeModel model)
        {
            try
            {
                var data = model.ToData(new Recipe());
                data.CreatedDateTime = DateTime.UtcNow;
                if (data.Documents.Count > 3)
                {
                    return new ServiceResult(false, "Cannot upload more than 3 files at once");
                }
                foreach (var file in data.Documents)
                {
                    if (file.Bytes.Length > DocumentModel.MaxSize)
                    {
                        return new ServiceResult(false, "Each file cannot exceed 10mb");
                    }
                    if (string.IsNullOrEmpty(file.Extension) || !permittedExtensions.Contains(file.Extension))
                    {
                        return new ServiceResult(false, "Invalid file extension");
                    }
                }
                _context.Add(data);
                foreach(var ingredient in data.Ingredients)
                {
                    var foundSection = data.Sections.FirstOrDefault(x => x.TempID == ingredient.Section.TempID);
                    if (foundSection != null)
                    {
                        ingredient.Section.SectionID = foundSection.SectionID;
                    }
                }
                foreach (var step in data.Steps)
                {
                    var foundSection = data.Sections.FirstOrDefault(x => x.TempID == step.Section.TempID);
                    if (foundSection != null)
                    {
                        step.Section.SectionID = foundSection.SectionID;
                    }
                }
                var result = await _context.SaveChangesAsync();
                return result > 0 ? ServiceResult.SuccessResult : ServiceResult.FailureResult;
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, ex.Message);
            }
        }

        [Authorize(Roles = "Moderator")]
        public async Task<ServiceResult> UpdateRecipe(RecipeModel model)
        {
            try
            {
                var data = await _context.Recipes.FirstOrDefaultAsync(x => x.RecipeID == model.RecipeID && x.DeletedDateTime == null);
                data = model.ToData(data);
                data.ModifiedDateTime = DateTime.UtcNow;
                foreach (var file in data.Documents)
                {
                    if (file.Bytes.Length > DocumentModel.MaxSize)
                    {
                        return new ServiceResult(false, "Each file cannot exceed 10mb");
                    }
                    if (string.IsNullOrEmpty(file.Extension) || !permittedExtensions.Contains(file.Extension))
                    {
                        return new ServiceResult(false, "Invalid file extension");
                    }
                }
                _context.Update(data);
                var result = await _context.SaveChangesAsync();
                return result > 0 ? ServiceResult.SuccessResult : ServiceResult.FailureResult;
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, ex.Message);
            }
        }

        [Authorize(Roles = "Moderator")]
        public async Task<ServiceResult> DeleteRecipe(RecipeModel model)
        {
            try
            {
                var data = await _context.Recipes.FirstOrDefaultAsync(x => x.RecipeID == model.RecipeID && x.DeletedDateTime == null);
                data = model.ToData(data);
                data.DeletedDateTime = DateTime.UtcNow;
                _context.Update(data);
                var result = await _context.SaveChangesAsync();
                return result > 0 ? ServiceResult.SuccessResult : ServiceResult.FailureResult;
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, ex.Message);
            }
        }
    }
}