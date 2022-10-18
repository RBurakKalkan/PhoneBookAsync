using System;

namespace ReportApi.Models
{
    public class Reports
    {
        public int ReportsId { get; set; }
        public DateTime ReportDate { get; set; }
        public ReportStatus ReportStatus { get; set; }
    }
    public enum ReportStatus
    {
        Preparing,
        Done
    }
}
