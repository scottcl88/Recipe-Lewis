using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeLewis.Models
{
    public class TagModel
    {
        public TagModel()
        {
            TempID = (int)DateTime.Now.Ticks;
            CreatedDateTime = DateTime.UtcNow;
        }
        public int TagID { get; set; }
        public int TempID { get; set; }//Used to identify tags that have not yet been saved to db
        [Required]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Created Date")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDateTime { get; set; }

        [Display(Name = "Deleted Date")]
        public DateTime? DeletedDateTime { get; set; }
    }
}