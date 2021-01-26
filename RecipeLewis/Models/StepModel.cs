using RecipeLewis.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeLewis.Models
{
    public class StepModel: EntityModel
    {
        public StepModel()
        {
            CreatedDateTime = DateTime.UtcNow;
            TempID = (int)DateTime.Now.Ticks;
            Section = new SectionModel();
        }
        public int StepID { get; set; }
        public int TempID { get; set; }//Used to identify tags that have not yet been saved to db
        public virtual CategoryModel Category { get; set; }
        public virtual SectionModel Section { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
    }
}