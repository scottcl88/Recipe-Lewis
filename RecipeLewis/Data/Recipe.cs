using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeLewis.Data
{
    public class Recipe
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public virtual List<Tag> Tags { get; set; }
        //public virtual List<Ingredient> Ingredients { get; set; }
        public virtual List<Section<Ingredient>> Ingredients { get; set; }
        public virtual List<Section<Step>> Steps { get; set; }
        //public virtual List<Step> Steps { get; set; }
        [JsonIgnore]
        public virtual List<Document> Documents { get; set; }

        [Required]
        [Display(Name = "Created Date")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDateTime { get; set; }

        [Display(Name = "Deleted Date")]
        public DateTime? DeletedDateTime { get; set; }
    }
}