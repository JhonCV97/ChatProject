using Application.DTOs.Role;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Application.DTOs.DataInfo
{
    public class DataInfoDto
    {
        public int Id { get; set; }
        public string QueryType { get; set; }
        public string Response { get; set; }
        public int RoleId { get; set; }
        public RoleDto Role { get; set; }
    }
}
