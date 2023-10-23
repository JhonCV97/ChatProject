using Application.DTOs.History;
using Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Application.DTOs.UserHistory
{
    public class UserHistoryDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserDto User { get; set; }
        public int HistoryId { get; set; }
        public HistoryDto History { get; set; }
    }
}
