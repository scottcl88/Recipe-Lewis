using RecipeLewis.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeLewis.Models
{
    public class SectionModel: EntityModel
    {
        public SectionModel()
        {
            CreatedDateTime = DateTime.UtcNow;
            TempID = (int)DateTime.Now.Ticks;
        }
        public int SectionID { get; set; }
        public int TempID { get; set; }//Used to identify tags that have not yet been saved to db
        public string Title { get; set; }
        public int DisplayOrder { get; set; }
        public string NewIngredient { get; set; }
    }
}