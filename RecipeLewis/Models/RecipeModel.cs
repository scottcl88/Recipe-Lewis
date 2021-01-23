using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeLewis.Models
{
    public class RecipeModel
    {
        public RecipeModel()
        {
            Ingredients = new List<SectionModel<IngredientModel>>();
            Steps = new List<SectionModel<StepModel>>();
            Documents = new List<DocumentModel>();
            Tags = new List<TagModel>();
            CreatedDateTime = DateTime.UtcNow;
            TotalTimeCalculated = true;
        }

        public int RecipeID { get; set; }
        public DateTime Date { get; set; }
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
        public List<SectionModel<IngredientModel>> Ingredients { get; set; }
        public List<SectionModel<StepModel>> Steps { get; set; }
        public List<DocumentModel> Documents { get; set; }
        public List<TagModel> Tags { get; set; }

        [Required]
        [Display(Name = "Created Date")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDateTime { get; set; }

        [Display(Name = "Deleted Date")]
        public DateTime? DeletedDateTime { get; set; }
    }
}