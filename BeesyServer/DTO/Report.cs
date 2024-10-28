using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeezyServer.DTO
{
    public class Report
    {
        public int ReportId { get; set; }

        public int UserId { get; set; }

        public int? BeeKeeperId { get; set; }

        public decimal? ReportLocation { get; set; }

        public string ReportDirectionsExplanation { get; set; } = null!;

       public string ReportUserNumber { get; set; } = null!;

        public string ReportExplanation { get; set; } = null!;

        public Report() { }
        public Report(Models.Report r)
        {
            ReportId = r.ReportId;
            UserId = r.UserId;
            BeeKeeperId = r.BeeKeeperId;
            ReportLocation = r.ReportLocation;
            ReportDirectionsExplanation = r.ReportDirectionsExplanation;
            ReportUserNumber = r.ReportUserNumber;
            ReportExplanation = r.ReportExplanation;
        }

        public Models.Report GetModel()
        {
            Models.Report r = new Models.Report();
            r.ReportId = ReportId;
            r.UserId = UserId;
            r.BeeKeeperId = BeeKeeperId;
            r.ReportLocation = ReportLocation;
            r.ReportDirectionsExplanation = ReportDirectionsExplanation;
            r.ReportUserNumber = ReportUserNumber;
            r.ReportExplanation = ReportExplanation;

            return r;
        }
    }
}
