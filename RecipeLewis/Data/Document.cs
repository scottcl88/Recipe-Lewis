using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeLewis.Data
{
    public class Document : EntityData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DocumentID { get; set; }

        public string Filename { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public byte[] Bytes { get; set; }

        public string Extension { get; set; }
        public string ContentType { get; set; }
    }
}