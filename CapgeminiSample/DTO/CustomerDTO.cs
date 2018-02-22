using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CapgeminiSample.Model
{
    public class CustomerDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        [ConcurrencyCheck]
        public long Version { get; set; }
    }
}
