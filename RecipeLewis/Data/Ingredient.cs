using RecipeLewis.DataExtensions;
using RecipeLewis.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace RecipeLewis.Data
{
    public class Ingredient : EntityData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IngredientID { get; set; }

        public virtual Category Category { get; set; }
        public string Title { get; set; }
        public string Amount { get; set; }
        public bool IsLiquid { get; set; }
        public int DisplayOrder { get; set; }

        public override IngredientModel ToModel()
        {
            return IngredientExtensions.ToModel(this);
        }
    }
}