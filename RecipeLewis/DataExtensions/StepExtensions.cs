using RecipeLewis.Data;
using RecipeLewis.Models;

namespace RecipeLewis.DataExtensions
{
    public static class StepExtensions
    {
        public static StepModel ToModel(this Step data)
        {
            if (data == null) return null;
            return new StepModel()
            {
                StepID = data.StepID,
                Title = data.Title,
                Category = data.Category.ToModel(),
                Description = data.Description,
                DisplayOrder = data.DisplayOrder,
                Section = data.Section.ToModel(),
                CreatedDateTime = data.CreatedDateTime,
                ModifiedDateTime = data.ModifiedDateTime,
                DeletedDateTime = data.DeletedDateTime
            };
        }

        public static Step ToData(this StepModel model, Recipe recipe)
        {
            if (model == null) return null;
            return new Step()
            {
                Title = model.Title,
                Category = model.Category.ToData(),
                Description = model.Description,
                DisplayOrder = model.DisplayOrder,
                Section = model.Section.ToData(recipe),
                CreatedDateTime = model.CreatedDateTime,
                ModifiedDateTime = model.ModifiedDateTime,
                DeletedDateTime = model.DeletedDateTime
            };
        }
    }
}