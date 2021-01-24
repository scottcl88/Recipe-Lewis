using RecipeLewis.Data;
using RecipeLewis.Models;
using System.Linq;

namespace RecipeLewis.DataExtensions
{
    public static class SectionExtensions
    {
        public static SectionModel<EntityModel> ToModel(this Section<EntityData> data)
        {
            if (data == null) return null;
            return new SectionModel<EntityModel>()
            {
                SectionID = data.SectionID,
                Title = data.Title,
                List = data.List.Select(x => x.ToModel()).ToList(),
                CreatedDateTime = data.CreatedDateTime,
                ModifiedDateTime = data.ModifiedDateTime,
                DeletedDateTime = data.DeletedDateTime
            };
        }


        public static Section<EntityData> ToData(this SectionModel<EntityModel> model)
        {
            if (model == null) return null;
            return new Section<EntityData>()
            {
                Title = model.Title,
                List = model.List.Select(x => x.ToData()).ToList(),
                CreatedDateTime = model.CreatedDateTime,
                ModifiedDateTime = model.ModifiedDateTime,
                DeletedDateTime = model.DeletedDateTime
            };
        }
    }
}