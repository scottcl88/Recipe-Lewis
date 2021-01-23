using RecipeLewis.Data;
using RecipeLewis.Models;
using System.Linq;

namespace RecipeLewis.DataExtensions
{
    public static class SectionExtensions
    {
        public static SectionModel<TModel> ToModel<TData, TModel>(this Section<TData> data)
        {
            if (data == null) return null;
            return new SectionModel<TModel>()
            {
                SectionID = data.SectionID,
                Title = data.Title,
                List = data.List.Select(x => x.ToModel()).ToList(),
                CreatedDateTime = data.CreatedDateTime,
                ModifiedDateTime = data.ModifiedDateTime,
                DeletedDateTime = data.DeletedDateTime
            };
        }

        public static Section<TData> ToData<TModel, TData>(this SectionModel<TModel> model)
        {
            if (model == null) return null;
            return new Section<TData>()
            {
                Title = model.Title,
                CreatedDateTime = model.CreatedDateTime,
                ModifiedDateTime = model.ModifiedDateTime,
                DeletedDateTime = model.DeletedDateTime
            };
        }
    }
}