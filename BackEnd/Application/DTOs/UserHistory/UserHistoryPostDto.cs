using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Application.DTOs.UserHistory
{
    public class UserHistoryPostDto
    {
        public int UserId { get; set; }
        public int HistoryId { get; set; }
    }
}
