using System;
using PlanAndTrack.Application.Models;
using PlanAndTrack.Application.Models.TestRequest;

namespace PlanAndTrack.Application.Services.Interfaces
{
    public interface ITestRequestService
    {
        Task<List<TestRequestVm>> GetAllAsync();
        Task<UpdateVm> GetUpdateVmAsync(int id);
        Task<int> CreateAsync(CreateVm createVm);
        Task UpdateAsync(UpdateVm updateVm);
        Task DeleteAsync(int id);
        Task<byte[]?> GetFileBytesAsync(int id);
        Task<ApproveVm> GetApproveVmAsync(int id);
        Task ApproveAsync(ApproveVm approveVm);
        Task RejectAsync(int id);

        Task<DetailVm> GetDetailVmAsync(int id);

        CreateVm GetCreateVm();
    }
}

