using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Vml.Office;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Report
{
    public class Report
    {
        public int CountUserPremium { get; set; }
        public int CountUserFree { get; set; }
        public string MostGivenAnswer { get; set; }
        public string MostAskedQuestion { get; set; }
    }
}
