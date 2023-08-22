using System;
using Microsoft.AspNetCore.Http;
using PlanAndTrack.Application.Repositories;
using PlanAndTrack.Domain.Entities;
using PlanAndTrack.Domain.Enums;

namespace PlanAndTrack.Infrastructure.EntityFrameworkCore.TestRequest
{
    public class PlanPeriodRepository : BaseRepository<PlanPeriod, TestRequestDbContext>, IPlanPeriodRepository
    {
        private readonly TestRequestDbContext _context;
        public PlanPeriodRepository(TestRequestDbContext context) : base(context)
        {
            _context = context;
        }

        public bool IsThereAnyContinuePlan()
        {
            var count= _context.PlanPeriod?.Where(x => x.ActualPeriodEnd == null).ToList().Count;
            return count > 0;
        }

        public PlanPeriod GetCurrentPlan()
        {
            return _context.PlanPeriod?.Where(x => x.ActualPeriodEnd == null).ToList().FirstOrDefault();
        }
        
    }
}

