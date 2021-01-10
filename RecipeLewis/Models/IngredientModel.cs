using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeLewis.Models
{
    public class IngredientModel
    {
        public int IngredientID { get; set; }
        public virtual CategoryModel Category { get; set; }
        public string Title { get; set; }
        public string Amount { get; set; }
        public bool IsLiquid { get; set; }
        public int DisplayOrder { get; set; }

        [Required]
        [Display(Name = "Created Date")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDateTime { get; set; }

        [Display(Name = "Deleted Date")]
        public DateTime? DeletedDateTime { get; set; }
    }
}