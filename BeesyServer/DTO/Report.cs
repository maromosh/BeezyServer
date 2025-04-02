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
        public List<ReportPicture>? Pictures { get; set; }

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
            Pictures = new List<ReportPicture>();
            if (r.ReportPictures != null)
            {
                foreach(var p in r.ReportPictures)
                    Pictures.Add(new ReportPicture(p));
            }

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
            if (this.Pictures != null)
            {
                r.ReportPictures = new List<Models.ReportPicture>();
                foreach (var picture in this.Pictures)
                    r.ReportPictures.Add(picture.GetModel());
            }

            return r;
        }
    }

    public class ReportPicture
    {
        public int PicId { get; set; }

        public int? ReportId { get; set; }
        public string? PicPath { get; set; }

        public ReportPicture() { }
        public ReportPicture(Models.ReportPicture picture)
        {
            this.PicId = picture.PicId;
            this.ReportId = picture.ReportId;
        }
        public Models.ReportPicture GetModel()
        {
            return new Models.ReportPicture()
            {
                PicId = this.PicId,
                ReportId = this.ReportId
            };
        }
    }
}
