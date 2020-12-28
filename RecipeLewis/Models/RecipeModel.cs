using RecipeLewis.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        }
        public int RecipeID { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ServingSize { get; set; }
        public string Author { get; set; }
        public TimeSpan PrepTime { get; set; }
        public TimeSpan CookTime { get; set; }
        public TimeSpan InactiveTime { get; set; }
        public TimeSpan TotalTime { get; set; }
        public virtual List<IngredientModel> Ingredients { get; set; }
        public virtual List<StepModel> Steps { get; set; }
        public virtual List<DocumentModel> Documents { get; set; }
        [Required]
        [Display(Name = "Created Date")]
        public DateTime CreatedDateTime { get; set; }
        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDateTime { get; set; }
        [Display(Name = "Deleted Date")]
        public DateTime? DeletedDateTime { get; set; }
    }
}