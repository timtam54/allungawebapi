using Microsoft.EntityFrameworkCore;
using AllungaWebAPI.Models;
//using System.Collections.Generic;


namespace AllungaWebAPI.Data
{
    public class dbcontext : DbContext
    {
        public dbcontext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<IP> IP { get; set; }
        public virtual DbSet<SeriesEvent> SeriesEvent { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Staff> Staff{ get; set; }
        public virtual DbSet<EvTyp> EvTyp { get; set; }
        public virtual DbSet<Series> Series { get; set; }
        public virtual DbSet<Sample> Sample { get; set; }
        public virtual DbSet<Master> Master { get; set; }
        public virtual DbSet<Report> Report { get; set; }

        public virtual DbSet<Reading> Reading { get; set; }
        public virtual DbSet<Dispatch> Dispatch { get; set; }
        public virtual DbSet<AllungaWebAPI.Models.DispatchSample> DispatchSample { get; set; }
        public virtual DbSet<ExposureType> ExposureType { get; set; }
        public virtual DbSet<Param> Param { get; set; }
        public virtual DbSet<ReportParam> ReportParam { get; set; }
        public virtual DbSet<SeriesProjectedReturns> SeriesProjectedReturns { get; set; }
        public virtual DbSet<RptSampleOnSite> RptSampleOnSite { get; set; }
        public virtual DbSet<DispatchStatus> DispatchStatus { get; set; }
        public virtual DbSet<SeriesSearch> SeriesSearch { get; set; }
        public virtual DbSet<Site> Site { get; set; }
        public virtual DbSet<RptRack> RptRack { get; set; }
        public DbSet<SampleHistory>? SampleHistory { get; set; }
        public DbSet<AllungaWebAPI.Models.RptExposureOnSiteMonthBilling>? RptExposureOnSiteMonthBilling { get; set; }
        public DbSet<AllungaWebAPI.Models.RptParamsAggSampleBilling>? RptParamsAggSampleBilling { get; set; }
        public DbSet<AllungaWebAPI.Models.RptExposureMovement>? RptExposureMovement { get; set; }

        public DbSet<Audit> Audit { get; set; }
    }
}
