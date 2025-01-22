using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeezyServer.DTO
{
    public class Report
    {
        public int ReportId { get; set; }

        public int UserId { get; set; }

        public int? BeeKeeperId { get; set; }

        public string GooglePlaceId { get; set; }

        public string Address { get; set; } = null!;

        public string ReportDirectionsExplanation { get; set; } = null!;

        public string ReportUserNumber { get; set; } = null!;

        public string ReportExplanation { get; set; } = null!;

        public int Status { get; set; }

        public Report() { }
        public Report(Models.Report r)
        {
            ReportId = r.ReportId;
            UserId = r.UserId;
            BeeKeeperId = r.BeeKeeperId;
            GooglePlaceId = r.GooglePlaceId;
            ReportDirectionsExplanation = r.ReportDirectionsExplanation;
            ReportUserNumber = r.ReportUserNumber;
            ReportExplanation = r.ReportExplanation;
            Status = r.Status;
            Address = r.Address;
        }

        public Models.Report GetModel()
        {
            Models.Report r = new Models.Report();
            r.ReportId = ReportId;
            r.UserId = UserId;
            r.BeeKeeperId = BeeKeeperId;
            r.ReportDirectionsExplanation = ReportDirectionsExplanation;
            r.ReportUserNumber = ReportUserNumber;
            r.ReportExplanation = ReportExplanation;
            r.Status = Status;
            r.Address = Address;
            r.GooglePlaceId = GooglePlaceId;

            return r;
        }
    }
}
