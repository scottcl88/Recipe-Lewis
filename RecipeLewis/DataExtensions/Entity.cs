using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeLewis.DataExtensions
{
    public abstract class EntityData
    {
        public int Id { get; set; }
        public abstract EntityModel ToModel();

        [Required]
        [Display(Name = "Created Date")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDateTime { get; set; }

        [Display(Name = "Deleted Date")]
        public DateTime? DeletedDateTime { get; set; }
    }
    public abstract class EntityModel
    {
        public int Id { get; set; }
        public abstract EntityData ToData();

        [Required]
        [Display(Name = "Created Date")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDateTime { get; set; }

        [Display(Name = "Deleted Date")]
        public DateTime? DeletedDateTime { get; set; }
    }
}