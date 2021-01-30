using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeLewis.Data
{
    public class Section : EntityData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SectionID { get; set; }

        [NotMapped]
        public int TempID { get; set; }//Used to identify tags that have not yet been saved to db

        public string Title { get; set; }
        public bool DisplayTitle { get; set; }
        public int DisplayOrder { get; set; }
        public EntityType EntityType { get; set; }
    }
}