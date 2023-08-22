using System;
using PlanAndTrack.Domain.Entities;

namespace PlanAndTrack.Application.Repositories
{
    public interface IPlanPeriodResourceRepository : IRepository<PlanPeriodResource>
    {
        IList<PlanPeriodResource>? GetPlanResources(int planId);
    }
}

