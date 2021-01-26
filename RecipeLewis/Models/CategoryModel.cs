using RecipeLewis.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeLewis.Models
{
    public class CategoryModel: EntityModel
    {
        public int CategoryID { get; set; }
        public string Title { get; set; }
    }
}