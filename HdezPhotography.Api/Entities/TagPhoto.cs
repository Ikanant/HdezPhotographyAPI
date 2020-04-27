using System.ComponentModel.DataAnnotations;

namespace HdezPhotography.Api.Entities {
    public class TagPhoto {
        [Required]
        public int TagID { get; set; }
        [Required]
        public int PhotoID { get; set; }
    }
}
