using System;
using Microsoft.EntityFrameworkCore;
using PlanAndTrack.Domain.Entities;

namespace PlanAndTrack.Infrastructure.EntityFrameworkCore.TestRequest
{
    public class TestRequestDbContext:DbContext
    {
        public DbSet<Domain.Entities.TestRequest>? TestRequest { get; set; }
        public DbSet<Domain.Entities.PlanPeriod>? PlanPeriod { get; set; }
        public DbSet<Domain.Entities.PlanPeriodResource>? PlanPeriodResource { get; set; }
        public DbSet<Domain.Entities.PlanPerformance>? PlanPerformance { get; set; }
        public DbSet<Domain.Entities.PlanTestRequest>? PlanTestRequest { get; set; }

        public TestRequestDbContext(DbContextOptions<TestRequestDbContext> options) : base(options)
        {
        }

       
    }
}
