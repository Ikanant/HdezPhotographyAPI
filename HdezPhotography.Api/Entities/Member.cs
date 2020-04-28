using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HdezPhotography.Api.Entities {
    public class Member {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string PhoneNum { get; set; }

        [Required]
        [MaxLength(200)]
        public string Email { get; set; }
    }
}
