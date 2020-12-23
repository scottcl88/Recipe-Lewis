using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeLewis.Data
{
    public class Document
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DocumentID { get; set; }
        [Required]
        [Display(Name = "Created Date")]
        public DateTime CreatedDateTime { get; set; }
        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDateTime { get; set; }
        [Display(Name = "Deleted Date")]
        public DateTime? DeletedDateTime { get; set; }
    }
}
