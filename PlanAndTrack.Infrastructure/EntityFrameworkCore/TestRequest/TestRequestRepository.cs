using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using PlanAndTrack.Application.Repositories;
using PlanAndTrack.Domain.Entities;
using PlanAndTrack.Domain.Enums;

namespace PlanAndTrack.Infrastructure.EntityFrameworkCore.TestRequest
{
    public class TestRequestRepository : BaseRepository<Domain.Entities.TestRequest, TestRequestDbContext>, ITestRequestRepository
    {
        private readonly TestRequestDbContext _context;
        public TestRequestRepository(TestRequestDbContext context) : base(context)
        {
            _context = context;
        }

        public IList<Domain.Entities.TestRequest>? GetUserTestRequests(string? userName)
        {
            if (String.IsNullOrEmpty(userName))
            {
                return new List<Domain.Entities.TestRequest>();
            }
            return _context.TestRequest?.Where(x => x.CreatedBy == userName).OrderByDescending(x=>x.Id).ToList();
        }

        public IList<Domain.Entities.TestRequest>? GetApprovedTestRequests()
        {
            return _context.TestRequest?.Where(x => x.StatusString  == TestRequestStatus.APPROVED.ToString()).OrderByDescending(x => x.Id).ToList();
        }

        public IList<Domain.Entities.TestRequest>? GetUnfinishedTestRequests()
        {
            return _context.TestRequest?.Where(x => x.StatusString == TestRequestStatus.UNFINISHED.ToString()).OrderByDescending(x => x.Id).ToList();
        }
    }
}

