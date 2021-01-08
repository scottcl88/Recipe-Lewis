using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeLewis.Models
{
    public class DocumentModel
    {
        public DocumentModel()
        {
            CreatedDateTime = DateTime.UtcNow;
            TempID = (int)DateTime.Now.Ticks;
        }

        public int DocumentID { get; set; }
        public int TempID { get; set; }//Used to identify tags that have not yet been saved to db
        public string Filename { get; set; }
        public string Description { get; set; }
        public byte[] Bytes { get; set; }
        public string ImageSource { get; set; }
        public string Extension { get; set; }
        public string ContentType { get; set; }
        public static readonly long MaxSize = 26214400;//20 MB

        [Required]
        [Display(Name = "Created Date")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDateTime { get; set; }

        [Display(Name = "Deleted Date")]
        public DateTime? DeletedDateTime { get; set; }
    }
}