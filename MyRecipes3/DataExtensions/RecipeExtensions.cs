using RecipeLewis.Data;
using RecipeLewis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeLewis.DataExtensions
{
    public static class RecipeExtensions
    {
        public static RecipeModel ToModel(this Recipe recipe)
        {
            return new RecipeModel();
        }
    }
}
