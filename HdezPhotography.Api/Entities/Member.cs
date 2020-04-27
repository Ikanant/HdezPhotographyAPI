using System.ComponentModel.DataAnnotations;

namespace HdezPhotography.Api.Entities {
    public class Member {
        [Key]
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
