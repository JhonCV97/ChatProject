using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Cache;
using System.Text;

namespace Domain.Models
{
    public class History : Entity
    {
        public DateTime QueryDate { get; set; }
        public string Question  { get; set; }
        public string Answer { get; set; }
        public int? ParentHistoryId { get; set; }

    }
}
