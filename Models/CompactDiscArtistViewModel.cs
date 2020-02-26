using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompactDiscProject.Models
{
    public class CompactDiscArtistViewModel
    {
        public List<CompactDisc> CompactDiscs { get; set; }
        public SelectList Artists { get; set; }
        public string ArtistName { get; set; }
        public string SearchString { get; set; }
    }
}
