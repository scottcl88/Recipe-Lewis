using RecipeLewis.Data;
using RecipeLewis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeLewis.DataExtensions
{
    public static class DocumentExtensions
    {
        public static DocumentModel ToModel(this Document data)
        {
            if (data == null) return null;
            return new DocumentModel()
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
        }
        public static Document ToData(this DocumentModel model)
        {
            if (model == null) return null;
            return new Document()
            {
                DocumentID = model.DocumentID,
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
