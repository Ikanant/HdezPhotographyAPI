using System;
using System.ComponentModel.DataAnnotations;

namespace HdezPhotography.Api.Entities {
    public class Comment {
        [Key]
        public int ID { get; set; }

        [Required]
        public int PhotoID { get; set; }

        [Required]
        public DateTime PostDate { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }
    }
}
