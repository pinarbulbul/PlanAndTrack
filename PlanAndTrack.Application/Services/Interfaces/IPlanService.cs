using System;
using PlanAndTrack.Application.Models;
using PlanAndTrack.Application.Models.Plan;

namespace PlanAndTrack.Application.Services.Interfaces
{
    public interface IPlanService
    {
        Task<List<PlanHeaderVm>> GetAllAsync();
        CreateVm GetCreateVmAsync();
        Task<CreateVm> GetPlanTasksAsync(CreateVm createVm);
        Task<int> CreateAsync(CreateVm createVm);

        Task<CurrentPlanVm> GetCurrentPlan();
        Task<UpdateTestVm> GetUpdateTestVmAsync(int planRequestId);
        Task UpdateTestAsync(UpdateTestVm updateVm);
        
        Task FinishAsync(int id);
        Task<DetailVm> GetDetailVmAsync(int id);
    }
}

