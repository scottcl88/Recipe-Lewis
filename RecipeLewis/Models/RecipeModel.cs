using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeLewis.Models
{
    public class RecipeModel
    {
        public RecipeModel()
        {
            Ingredients = new List<IngredientModel>();
            Steps = new List<StepModel>();
            Documents = new List<DocumentModel>();
            CreatedDateTime = DateTime.UtcNow;
            TotalTimeCalculated = true;
        }

        public int RecipeID { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ServingSize { get; set; }
        public string Author { get; set; }
        public TimeSpan PrepTime { get; set; }
        public string PrepTimeStr { get; set; }
        public TimeSpan CookTime { get; set; }
        public string CookTimeStr { get; set; }
        public TimeSpan InactiveTime { get; set; }
        public string InactiveTimeStr { get; set; }
        public TimeSpan TotalTime { get; set; }
        public bool TotalTimeCalculated { get; set; }
        public string TotalTimeStr { get; set; }
        public List<IngredientModel> Ingredients { get; set; }
        public List<StepModel> Steps { get; set; }
        public List<DocumentModel> Documents { get; set; }

        [Required]
        [Display(Name = "Created Date")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDateTime { get; set; }

        [Display(Name = "Deleted Date")]
        public DateTime? DeletedDateTime { get; set; }
    }
}