using RecipeLewis.Data;
using RecipeLewis.Models;
using System.Linq;

namespace RecipeLewis.DataExtensions
{
    public static class SectionExtensions
    {
        public static SectionModel ToModel(this Section data)
        {
            if (data == null) return null;
            return new SectionModel()
            {
                SectionID = data.SectionID,
                Title = data.Title,
                EntityType = data.EntityType,
                CreatedDateTime = data.CreatedDateTime,
                ModifiedDateTime = data.ModifiedDateTime,
                DeletedDateTime = data.DeletedDateTime
            };
        }

        public static Section ToData(this SectionModel model, Recipe recipe)
        {
            if (model == null) return null;
            if(model.SectionID > 0 && recipe != null)
            {
                var foundSection = recipe.Sections.FirstOrDefault(x => x.SectionID == model.SectionID);
                if(foundSection != null)
                {
                    foundSection.EntityType = model.EntityType;
                    foundSection.Title = model.Title;
                    return foundSection;
                }
            }
            return new Section()
            {
                Title = model.Title,
                EntityType = model.EntityType,
                CreatedDateTime = model.CreatedDateTime,
                ModifiedDateTime = model.ModifiedDateTime,
                DeletedDateTime = model.DeletedDateTime
            };
        }
    }
}