﻿using RecipeLewis.Data;
using RecipeLewis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                CategoryID = model.CategoryID,
                Title = model.Title,
                CreatedDateTime = model.CreatedDateTime,
                ModifiedDateTime = model.ModifiedDateTime,
                DeletedDateTime = model.DeletedDateTime
            };
        }
    }
}