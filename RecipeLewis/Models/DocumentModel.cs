using RecipeLewis.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeLewis.Models
{
    public class DocumentModel: EntityModel
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
    }
}