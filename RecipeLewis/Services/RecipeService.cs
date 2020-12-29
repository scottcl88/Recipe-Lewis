using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger _logger;
        public RecipeService(ApplicationDbContext context, ILogger<Recipe> logger)
        {
            _context = context;
            _logger = logger;
        }
        public ApplicationDbContext _context { get; set; }
        private readonly string[] permittedExtensions = new string[] { "gif", "jpg", "jpeg", "png", "tif", "tiff" };
        public async Task<ServiceResult<RecipeModel>> GetAllRecipes()
        {
            try
            {
                _logger.LogInformation("Getting recipes");
                var list = await _context.Recipes.Where(x => x.DeletedDateTime == null).ToListAsync();
                _logger.LogInformation("Recipes returned: " + list.Count);
                return new ServiceResult<RecipeModel>(true) { DataList = list.Select(x => x.ToModel()).ToList() };
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

        public async Task<ServiceResult> AddRecipe(RecipeModel model)
        {
            try
            {
                var data = model.ToData(new Recipe());
                if (data.Documents.Count > 3)
                {
                    return new ServiceResult(false, "Cannot upload more than 3 files at once");
                }
                foreach (var file in data.Documents)
                {
                    if (file.Bytes.Length > 10485760)
                    {
                        return new ServiceResult(false, "Each file cannot exceed 10mb");
                    }
                    if (string.IsNullOrEmpty(file.Extension) || !permittedExtensions.Contains(file.Extension))
                    {
                        return new ServiceResult(false, "Invalid file extension");
                    }
                }
                _context.Add(data);
                var result = await _context.SaveChangesAsync();
                return result > 0 ? ServiceResult.SuccessResult : ServiceResult.FailureResult;
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, ex.Message);
            }
        }

        public async Task<ServiceResult> UpdateRecipe(RecipeModel model)
        {
            try
            {
                var data = await _context.Recipes.FirstOrDefaultAsync(x => x.RecipeID == model.RecipeID && x.DeletedDateTime == null);
                data = model.ToData(data);
                data.ModifiedDateTime = DateTime.UtcNow;
                foreach (var file in data.Documents)
                {
                    if (file.Bytes.Length > 10485760)
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
