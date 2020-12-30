using RecipeLewis.Data;
using RecipeLewis.Models;

namespace RecipeLewis.DataExtensions
{
    public static class CategoryExtensions
    {
        public static CategoryModel ToModel(this Category data)
        {
            if (data == null) return null;
            return new CategoryModel()
            {
                CategoryID = data.CategoryID,
                Title = data.Title,
                CreatedDateTime = data.CreatedDateTime,
                ModifiedDateTime = data.ModifiedDateTime,
                DeletedDateTime = data.DeletedDateTime
            };
        }

        public static Category ToData(this CategoryModel model)
        {
            if (model == null) return null;
            return new Category()
            {
                Title = model.Title,
                CreatedDateTime = model.CreatedDateTime,
                ModifiedDateTime = model.ModifiedDateTime,
                DeletedDateTime = model.DeletedDateTime
            };
        }
    }
}