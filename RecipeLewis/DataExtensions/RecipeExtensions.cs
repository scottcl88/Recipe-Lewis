using RecipeLewis.Data;
using RecipeLewis.Models;
using System.Collections.Generic;
using System.Linq;

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
                Documents = data.Documents?.Select(x => x.ToModel()).Where(x => !x.DeletedDateTime.HasValue).ToList(),
                InactiveTime = data.InactiveTime,
                Ingredients = data.Ingredients?.Select(x => x.ToModel()).Where(x => !x.DeletedDateTime.HasValue).ToList(),
                PrepTime = data.PrepTime,
                ServingSize = data.ServingSize,
                NumberOfServings = data.NumberOfServings,
                Steps = data.Steps?.Select(x => x.ToModel()).Where(x => !x.DeletedDateTime.HasValue).ToList(),
                Tags = data.Tags?.Select(x => x.ToModel()).ToList(),
                Title = data.Title,
                TotalTime = data.TotalTime,
                TotalTimeCalculated = data.TotalTimeCalculated,
                CreatedDateTime = data.CreatedDateTime,
                ModifiedDateTime = data.ModifiedDateTime,
                DeletedDateTime = data.DeletedDateTime
            };
        }

        public static RecipeModel ToGridModel(this Recipe data)
        {
            if (data == null) return null;
            return new RecipeModel()
            {
                RecipeID = data.RecipeID,
                Author = data.Author,
                CookTime = data.CookTime,
                Date = data.Date,
                Description = data.Description,
                InactiveTime = data.InactiveTime,
                PrepTime = data.PrepTime,
                ServingSize = data.ServingSize,
                NumberOfServings = data.NumberOfServings,
                Tags = data.Tags?.Select(x => x.ToModel()).ToList(),
                Title = data.Title,
                TotalTime = data.TotalTime,
                TotalTimeCalculated = data.TotalTimeCalculated,
                CreatedDateTime = data.CreatedDateTime,
                ModifiedDateTime = data.ModifiedDateTime,
                DeletedDateTime = data.DeletedDateTime
            };
        }

        public static Recipe ToData(this RecipeModel model, Recipe data)
        {
            if (model == null) return null;
            data.Author = model.Author;
            data.CookTime = model.CookTime;
            data.Date = model.Date;
            data.Description = model.Description;
            data.Documents = model.Documents?.Select(x => x.ToData()).ToList();
            data.InactiveTime = model.InactiveTime;
            data.Ingredients = model.Ingredients?.Select(x => x.ToData()).ToList();
            data.PrepTime = model.PrepTime;
            data.ServingSize = model.ServingSize;
            data.NumberOfServings = model.NumberOfServings;
            data.Steps = model.Steps?.Select(x => x.ToData()).ToList();
            data.Tags = model.Tags?.Select(x => x.ToData()).Where(x => !x.DeletedDateTime.HasValue).ToList();
            data.Title = model.Title;
            data.TotalTime = model.TotalTime;
            data.TotalTimeCalculated = model.TotalTimeCalculated;
            data.CreatedDateTime = model.CreatedDateTime;
            data.ModifiedDateTime = model.ModifiedDateTime;
            data.DeletedDateTime = model.DeletedDateTime;
            return data;
        }
    }
}