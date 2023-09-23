using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class UserHistory : Entity
    {
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int HistoryId { get; set; }
        [ForeignKey("HistoryId")]
        public History History { get; set; }
    }
}
