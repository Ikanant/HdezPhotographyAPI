﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HdezPhotography.Api.Models {
    public class PhotoDto {
        public int ID { get; set; }
        public int MemberID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
    }

    public class PhotoImportDto {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
    }

    public class PhotoUpdateDto {
        public string Title { get; set; }
        
        [Required]
        public string Description { get; set; }
        public int Views { get; set; }
        public string ImagePath { get; set; }
    }
}
