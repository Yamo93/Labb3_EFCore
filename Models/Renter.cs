using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CompactDiscProject.Models
{
    public class Renter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DisplayName("Phone Number")]
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        [DisplayName("Compact Disc")]
        public CompactDisc CompactDisc { get; set; }
        [DisplayName("Compact Disc")]
        public int CompactDiscId { get; set; }
        [DisplayName("Rental Date")]
        public DateTime RentalDate { get; set; }
    }
}
