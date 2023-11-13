using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.History
{
    public class HistoryDto
    {
        public int Id { get; set; }
        public DateTime QueryDate { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int? ParentHistoryId { get; set; }
    }
}
