using RecipeLewis.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeLewis.Models
{
    public class TagModel : EntityModel
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
    }
}