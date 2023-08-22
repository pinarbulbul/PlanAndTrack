using System;
using Microsoft.AspNetCore.Http;
using PlanAndTrack.Application.Repositories;
using PlanAndTrack.Domain.Entities;

namespace PlanAndTrack.Infrastructure.EntityFrameworkCore.TestRequest
{
    public class PlanPerformanceRepository : BaseRepository<PlanPerformance, TestRequestDbContext>, IPlanPerformanceRepository
    {
        private readonly TestRequestDbContext _context;
        public PlanPerformanceRepository(TestRequestDbContext context) : base(context)
        {
            _context = context;
        }

        public PlanPerformance? GetPlanPerformance(int planId)
        {
            return _context.PlanPerformance?.Where(x => x.PlannedPeriodId == planId).OrderBy(x => x.Id).FirstOrDefault();
        }
    }
}

