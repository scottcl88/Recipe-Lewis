using RecipeLewis.Data;
using RecipeLewis.DataExtensions;
using System;

namespace RecipeLewis.Models
{
    public class IngredientModel: EntityModel
    {
        public IngredientModel()
        {
            CreatedDateTime = DateTime.UtcNow;
            TempID = (int)DateTime.Now.Ticks;
        }
        public int IngredientID { get; set; }
        public int TempID { get; set; }//Used to identify tags that have not yet been saved to db
        public virtual CategoryModel Category { get; set; }
        public string Title { get; set; }
        public string Amount { get; set; }
        public bool IsLiquid { get; set; }
        public int DisplayOrder { get; set; }

        public override EntityData ToData()
        {
            return IngredientExtensions.ToData(this);
        }
    }
}