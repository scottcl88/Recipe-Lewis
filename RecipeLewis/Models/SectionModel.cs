using RecipeLewis.Data;
using System;

namespace RecipeLewis.Models
{
    public class SectionModel : EntityModel
    {
        public SectionModel()
        {
            CreatedDateTime = DateTime.UtcNow;
            TempID = (int)DateTime.Now.Ticks;
        }

        public int SectionID { get; set; }
        public int TempID { get; set; }//Used to identify tags that have not yet been saved to db
        public string Title { get; set; }
        public bool DisplayTitle { get; set; }
        public int DisplayOrder { get; set; }
        public string NewIngredient { get; set; }
        public string NewStep { get; set; }
        public EntityType EntityType { get; set; }
    }
}