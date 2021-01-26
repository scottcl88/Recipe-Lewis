using RecipeLewis.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeLewis.Models
{
    public class RecipeModel: EntityModel
    {
        public RecipeModel()
        {
            Ingredients = new List<IngredientModel>();
            Steps = new List<StepModel>();
            Documents = new List<DocumentModel>();
            Tags = new List<TagModel>();
            Sections = new List<SectionModel>();
            CreatedDateTime = DateTime.UtcNow;
            TotalTimeCalculated = true;
        }

        public int RecipeID { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string ServingSize { get; set; }
        public string NumberOfServings { get; set; }
        public string Author { get; set; }
        public string PrepTime { get; set; }
        public string CookTime { get; set; }
        public string InactiveTime { get; set; }
        public string TotalTime { get; set; }
        public bool TotalTimeCalculated { get; set; }
        public List<IngredientModel> Ingredients { get; set; }
        public List<SectionModel> Sections { get; set; }
        public List<StepModel> Steps { get; set; }
        public List<DocumentModel> Documents { get; set; }
        public List<TagModel> Tags { get; set; }
    }
}