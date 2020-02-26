﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompactDiscProject.Models
{
    public class CompactDisc
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ReleaseDate { get; set; }
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
        public Renter Renter { get; set; }
    }
}
