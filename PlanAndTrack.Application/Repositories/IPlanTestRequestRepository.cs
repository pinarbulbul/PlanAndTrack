using System;
using PlanAndTrack.Domain.Entities;

namespace PlanAndTrack.Application.Repositories
{
    public interface IPlanTestRequestRepository :IRepository<PlanTestRequest>
    {
        IList<PlanTestRequest>? GetPlanTestRequests(int planId);
    }
}

