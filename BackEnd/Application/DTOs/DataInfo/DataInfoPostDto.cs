using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.DataInfo
{
    public class DataInfoPostDto
    {
        public int Id { get; set; }
        public string QueryType { get; set; }
        public string Response { get; set; }
        public int RoleId { get; set; }
    }
}
