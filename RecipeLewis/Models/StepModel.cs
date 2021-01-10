using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeLewis.Models
{
    public class StepModel
    {
        public StepModel()
        {
            CreatedDateTime = DateTime.UtcNow;
            TempID = (int)DateTime.Now.Ticks;
        }
        public int StepID { get; set; }
        public int TempID { get; set; }//Used to identify tags that have not yet been saved to db
        public virtual CategoryModel Category { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
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