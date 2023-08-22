using System;
using Microsoft.AspNetCore.Http;
using PlanAndTrack.Application.Repositories;
using PlanAndTrack.Domain.Entities;

namespace PlanAndTrack.Infrastructure.EntityFrameworkCore.TestRequest
{
    public class PlanPeriodResourceRepository : BaseRepository<PlanPeriodResource, TestRequestDbContext>, IPlanPeriodResourceRepository
    {
        private readonly TestRequestDbContext _context;
        public PlanPeriodResourceRepository(TestRequestDbContext context) : base(context)
        {
            _context = context;
        }

        public IList<PlanPeriodResource>? GetPlanResources(int planId)
        {
            return _context.PlanPeriodResource?.Where(x => x.PlannedPeriodId == planId).OrderBy(x => x.Id).ToList();
        }
    }
}

