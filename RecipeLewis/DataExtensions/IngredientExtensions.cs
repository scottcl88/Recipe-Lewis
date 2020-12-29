using RecipeLewis.Data;
using RecipeLewis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeLewis.DataExtensions
{
    public static class IngredientExtensions
    {
        public static IngredientModel ToModel(this Ingredient data)
        {
            if (data == null) return null;
            return new IngredientModel()
            {
                IngredientID = data.IngredientID,
                Title = data.Title,
                Amount = data.Amount,
                Category = data.Category.ToModel(),
                IsLiquid = data.IsLiquid,
                CreatedDateTime = data.CreatedDateTime,
                ModifiedDateTime = data.ModifiedDateTime,
                DeletedDateTime = data.DeletedDateTime
            };
        }
        public static Ingredient ToData(this IngredientModel model)
        {
            if (model == null) return null;
            return new Ingredient()
            {
                Title = model.Title,
                Amount = model.Amount,
                Category = model.Category.ToData(),
                IsLiquid = model.IsLiquid,
                CreatedDateTime = model.CreatedDateTime,
                ModifiedDateTime = model.ModifiedDateTime,
                DeletedDateTime = model.DeletedDateTime
            };
        }
    }
}
