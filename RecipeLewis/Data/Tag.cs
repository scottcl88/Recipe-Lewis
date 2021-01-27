using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeLewis.Data
{
    public class Tag : EntityData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TagID { get; set; }

        public string Title { get; set; }
    }
}