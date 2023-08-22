using System;
using Microsoft.AspNetCore.Http;
using PlanAndTrack.Application.Repositories;
using PlanAndTrack.Domain.Entities;

namespace PlanAndTrack.Infrastructure.EntityFrameworkCore.TestRequest
{
    public class PlanTestRequestRepository : BaseRepository<PlanTestRequest, TestRequestDbContext>, IPlanTestRequestRepository
    {
        private readonly TestRequestDbContext _context;
        public PlanTestRequestRepository(TestRequestDbContext context) : base(context)
        {
            _context = context;
        }

        public IList<PlanTestRequest>? GetPlanTestRequests(int planId)
        {
            return _context.PlanTestRequest?.Where(x => x.PlannedPeriodId == planId).OrderBy(x=>x.Id).ToList();
        }
    }
}

