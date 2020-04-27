using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HdezPhotography.Api.Entities {
    public class Album {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public int View { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; }
    }
}
