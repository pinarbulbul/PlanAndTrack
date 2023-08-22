using System;
using PlanAndTrack.Domain.Entities;

namespace PlanAndTrack.Application.Repositories
{
    public interface ITestRequestRepository :IRepository<TestRequest>
    {
        IList<TestRequest>? GetUserTestRequests(string? userName);
        IList<TestRequest>? GetApprovedTestRequests();
        IList<TestRequest>? GetUnfinishedTestRequests();
    }
}

