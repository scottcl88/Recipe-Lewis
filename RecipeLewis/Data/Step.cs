using RecipeLewis.DataExtensions;
using RecipeLewis.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeLewis.Data
{
    public class Step : EntityData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StepID { get; set; }

        public virtual Category Category { get; set; }
        public int DisplayOrder { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public override StepModel ToModel()
        {
            return StepExtensions.ToModel(this);
        }
    }
}