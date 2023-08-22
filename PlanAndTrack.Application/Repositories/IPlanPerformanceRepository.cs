using System;
using PlanAndTrack.Domain.Entities;

namespace PlanAndTrack.Application.Repositories
{
    public interface IPlanPerformanceRepository : IRepository<PlanPerformance>
    {
        PlanPerformance? GetPlanPerformance(int planId);
    }
}

