using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeLewis.Data
{
    public class Category : EntityData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; }

        public string Title { get; set; }
    }
}