using RecipeLewis.Data;
using RecipeLewis.Models;

namespace RecipeLewis.DataExtensions
{
    public static class TagExtensions
    {
        public static TagModel ToModel(this Tag data)
        {
            if (data == null) return null;
            return new TagModel()
            {
                TagID = data.TagID,
                Title = data.Title,
                CreatedDateTime = data.CreatedDateTime,
                ModifiedDateTime = data.ModifiedDateTime,
                DeletedDateTime = data.DeletedDateTime
            };
        }

        public static Tag ToData(this TagModel model)
        {
            if (model == null) return null;
            return new Tag()
            {
                Title = model.Title,
                CreatedDateTime = model.CreatedDateTime,
                ModifiedDateTime = model.ModifiedDateTime,
                DeletedDateTime = model.DeletedDateTime
            };
        }
    }
}