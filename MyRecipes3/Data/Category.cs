using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeLewis.Data
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; }
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
