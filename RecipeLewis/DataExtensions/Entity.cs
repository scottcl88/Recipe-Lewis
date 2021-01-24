using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace RecipeLewis.DataExtensions
{
    public interface IDataConvertable
    {
        public EntityModel ToModel();
    }
    public abstract class EntityData: IDataConvertable
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
    public interface IModelConvertable
    {
        public EntityData ToData();
    }
    public abstract class EntityModel: IModelConvertable
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