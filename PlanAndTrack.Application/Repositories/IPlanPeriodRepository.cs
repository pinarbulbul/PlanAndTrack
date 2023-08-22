using System;
using PlanAndTrack.Domain.Entities;

namespace PlanAndTrack.Application.Repositories
{
    public interface IPlanPeriodRepository :IRepository<PlanPeriod>
    {
        bool IsThereAnyContinuePlan();
        PlanPeriod GetCurrentPlan();
    }
}

