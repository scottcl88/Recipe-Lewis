using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeLewis.Data
{
    public class Step
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StepID { get; set; }
        public virtual Category Category { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        [Display(Name = "Created Date")]
        public DateTime CreatedDateTime { get; set; }
        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDateTime { get; set; }
        [Display(Name = "Deleted Date")]
        public DateTime? DeletedDateTime { get; set; }
    }
}
