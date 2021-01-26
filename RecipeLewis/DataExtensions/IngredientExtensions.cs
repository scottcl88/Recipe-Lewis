using RecipeLewis.Data;
using RecipeLewis.Models;

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
                DisplayOrder = data.DisplayOrder,
                Section = data.Section.ToModel(),
                CreatedDateTime = data.CreatedDateTime,
                ModifiedDateTime = data.ModifiedDateTime,
                DeletedDateTime = data.DeletedDateTime
            };
        }

        public static Ingredient ToData(this IngredientModel model, Recipe recipe)
        {
            if (model == null) return null;
            return new Ingredient()
            {
                Title = model.Title,
                Amount = model.Amount,
                Category = model.Category.ToData(),
                IsLiquid = model.IsLiquid,
                DisplayOrder = model.DisplayOrder,
                Section = model.Section.ToData(recipe),
                CreatedDateTime = model.CreatedDateTime,
                ModifiedDateTime = model.ModifiedDateTime,
                DeletedDateTime = model.DeletedDateTime
            };
        }
    }
}