using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeLewis.Data
{
    public class Document
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DocumentID { get; set; }
        public string Filename { get; set; }
        public string Description { get; set; }
        public byte[] Bytes { get; set; }
        public string Extension { get; set; }
        public string ContentType { get; set; }
        [Required]
        [Display(Name = "Created Date")]
        public DateTime CreatedDateTime { get; set; }
        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDateTime { get; set; }
        [Display(Name = "Deleted Date")]
        public DateTime? DeletedDateTime { get; set; }
    }
}
