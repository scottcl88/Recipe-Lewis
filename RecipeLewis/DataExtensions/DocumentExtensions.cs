using RecipeLewis.Data;
using RecipeLewis.Models;
using System;

namespace RecipeLewis.DataExtensions
{
    public static class DocumentExtensions
    {
        public static DocumentModel ToModel(this Document data)
        {
            if (data == null) return null;
            var model = new DocumentModel()
            {
                DocumentID = data.DocumentID,
                Bytes = data.Bytes,
                ContentType = data.ContentType,
                Extension = data.Extension,
                Filename = data.Filename,
                Description = data.Description,
                CreatedDateTime = data.CreatedDateTime,
                ModifiedDateTime = data.ModifiedDateTime,
                DeletedDateTime = data.DeletedDateTime
            };
            var base64 = Convert.ToBase64String(model.Bytes);
            var imgSrc = $"data:image/{model.Extension};base64,{base64}";
            model.ImageSource = imgSrc;
            return model;
        }

        public static Document ToData(this DocumentModel model)
        {
            if (model == null) return null;
            return new Document()
            {
                Bytes = model.Bytes,
                ContentType = model.ContentType,
                Extension = model.Extension,
                Filename = model.Filename,
                Description = model.Description,
                CreatedDateTime = model.CreatedDateTime,
                ModifiedDateTime = model.ModifiedDateTime,
                DeletedDateTime = model.DeletedDateTime
            };
        }
    }
}