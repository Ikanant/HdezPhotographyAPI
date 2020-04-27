using System.ComponentModel.DataAnnotations;

namespace HdezPhotography.Api.Entities {
    public class Tag {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
    }
}
