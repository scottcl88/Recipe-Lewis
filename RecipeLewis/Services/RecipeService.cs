using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ServiceResult<RecipeModel>> GetAllRecipes()
        {
            try
            {
                var list = await _context.Recipes.Where(x => x.DeletedDateTime == null).Select(x => x.ToModel()).ToListAsync();
                return new ServiceResult<RecipeModel>(true) { Data = list };
            }
            catch (Exception ex)
            {
                return new ServiceResult<RecipeModel>(false, ex.Message);
            }

        }

        public async Task<ServiceResult> AddRecipe(RecipeModel model)
        {
            try
            {
                var data = model.ToData();
                _context.Add(data);
                var result = await _context.SaveChangesAsync();
                return result > 0 ? ServiceResult.SuccessResult : ServiceResult.FailureResult;
            }
            catch(Exception ex)
            {
                return new ServiceResult(false, ex.Message);
            }
        }

        public async Task<ServiceResult> UpdateRecipe(RecipeModel model)
        {
            try
            {
                var data = model.ToData();
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
                var data = model.ToData();
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
