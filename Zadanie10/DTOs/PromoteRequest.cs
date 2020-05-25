using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Zadanie10.DTOs
{
    public class PromoteRequest
    {
        [Required]
        public string studies { get; set; }
        [Required]
        public int semester { get; set; }
    }
}
