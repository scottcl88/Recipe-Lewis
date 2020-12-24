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
            try
            {
                var list = _context.Recipes.Select(x => x.ToModel()).ToList();
                return await Task.FromResult(list);
            }
            catch (Exception ex)
            {
                return null;
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
