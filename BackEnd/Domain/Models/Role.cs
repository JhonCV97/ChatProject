using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class Role : Entity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(450)]
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}
