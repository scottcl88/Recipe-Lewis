using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeLewis.Models
{
    public class SectionModel<T>
    {
        public int SectionID { get; set; }
        public string Title { get; set; }
        public virtual List<T> List { get; set; }

        [Required]
        [Display(Name = "Created Date")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDateTime { get; set; }

        [Display(Name = "Deleted Date")]
        public DateTime? DeletedDateTime { get; set; }
    }
}