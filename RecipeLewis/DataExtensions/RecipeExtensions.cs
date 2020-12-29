using RecipeLewis.Data;
using RecipeLewis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeLewis.DataExtensions
{
    public static class RecipeExtensions
    {
        public static RecipeModel ToModel(this Recipe data)
        {
            if (data == null) return null;
            return new RecipeModel()
            {
                RecipeID = data.RecipeID,
                Author = data.Author,
                CookTime = data.CookTime,
                Date = data.Date,
                Description = data.Description,
                Documents = data.Documents?.Select(x => x.ToModel()).ToList(),
                InactiveTime = data.InactiveTime,
                Ingredients = data.Ingredients?.Select(x => x.ToModel()).ToList(),
                PrepTime = data.PrepTime,
                ServingSize = data.ServingSize,
                Steps = data.Steps?.Select(x => x.ToModel()).ToList(),
                Title = data.Title,
                TotalTime = data.TotalTime,
                TotalTimeCalculated = data.TotalTimeCalculated,
                CreatedDateTime = data.CreatedDateTime,
                ModifiedDateTime = data.ModifiedDateTime,
                DeletedDateTime = data.DeletedDateTime
            };
        }
        public static Recipe ToData(this RecipeModel model)
        {
            if (model == null) return null;
            return new Recipe()
            {
                RecipeID = model.RecipeID,
                Author = model.Author,
                CookTime = model.CookTime,
                Date = model.Date,
                Description = model.Description,
                Documents = model.Documents?.Select(x => x.ToData()).ToList(),
                InactiveTime = model.InactiveTime,
                Ingredients = model.Ingredients?.Select(x => x.ToData()).ToList(),
                PrepTime = model.PrepTime,
                ServingSize = model.ServingSize,
                Steps = model.Steps?.Select(x => x.ToData()).ToList(),
                Title = model.Title,
                TotalTime = model.TotalTime,
                TotalTimeCalculated = model.TotalTimeCalculated,
                CreatedDateTime = model.CreatedDateTime,
                ModifiedDateTime = model.ModifiedDateTime,
                DeletedDateTime = model.DeletedDateTime
            };
        }
    }
}
