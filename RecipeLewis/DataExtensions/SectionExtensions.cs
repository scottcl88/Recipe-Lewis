using RecipeLewis.Data;
using RecipeLewis.Models;
using System.Linq;

namespace RecipeLewis.DataExtensions
{
    public static class SectionExtensions
    {
        public static SectionModel<IngredientModel> ToModel(this Section<Ingredient> data)
        {
            if (data == null) return null;
            return new SectionModel<IngredientModel>()
            {
                SectionID = data.SectionID,
                Title = data.Title,
                List = data.List.Select(x => x.ToModel()).ToList(),
                CreatedDateTime = data.CreatedDateTime,
                ModifiedDateTime = data.ModifiedDateTime,
                DeletedDateTime = data.DeletedDateTime
            };
        }
        public static SectionModel<StepModel> ToModel(this Section<Step> data)
        {
            if (data == null) return null;
            return new SectionModel<StepModel>()
            {
                SectionID = data.SectionID,
                Title = data.Title,
                List = data.List.Select(x => x.ToModel()).ToList(),
                CreatedDateTime = data.CreatedDateTime,
                ModifiedDateTime = data.ModifiedDateTime,
                DeletedDateTime = data.DeletedDateTime
            };
        }

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
        public static Section<Ingredient> ToData(this SectionModel<IngredientModel> model)
        {
            if (model == null) return null;
            return new Section<Ingredient>()
            {
                Title = model.Title,
                List = model.List.Select(x => x.ToData()).ToList(),
                CreatedDateTime = model.CreatedDateTime,
                ModifiedDateTime = model.ModifiedDateTime,
                DeletedDateTime = model.DeletedDateTime
            };
        }
        public static Section<Step> ToData(this SectionModel<StepModel> model)
        {
            if (model == null) return null;
            return new Section<Step>()
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