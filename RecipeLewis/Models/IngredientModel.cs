using RecipeLewis.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeLewis.Models
{
    public class IngredientModel: EntityModel
    {
        public IngredientModel()
        {
            CreatedDateTime = DateTime.UtcNow;
            TempID = (int)DateTime.Now.Ticks;
            Section = new SectionModel();
        }
        public int IngredientID { get; set; }
        public int TempID { get; set; }//Used to identify tags that have not yet been saved to db
        public virtual SectionModel Section { get; set; }
        public virtual CategoryModel Category { get; set; }
        public string Title { get; set; }
        public string Amount { get; set; }
        public bool IsLiquid { get; set; }
        public int DisplayOrder { get; set; }
    }
}