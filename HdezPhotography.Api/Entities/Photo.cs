﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HdezPhotography.Api.Entities {
    public class Photo {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public int AlbumID { get; set; }
        
        [ForeignKey("MemberID")]
        public Member Member { get; set; }

        [Required]
        public int MemberID { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        
        [MaxLength(1000)]
        public string Description { get; set; }
        
        public DateTime UploadDate { get; set; }
        
        public int Views { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string ImagePath { get; set; }

        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}
