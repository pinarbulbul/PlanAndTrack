using System;
using PlanAndTrack.Application.Exceptions;
using PlanAndTrack.Domain.Entities;
using PlanAndTrack.Domain.Enums;
using TestRequest = PlanAndTrack.Domain.Entities.TestRequest;

namespace PlanAndTrack.Application.Validations
{
    public interface IValidations
    {
        void IsRequestExist(int id, TestRequest? request);
        void IsRequestEditAuthorized(int id, string creatorUserName, string? userName, TestRequestStatus status);
        void IsRequestApprovedAuthorized(int id, string creatorUserName, TestRequestStatus status, string? userName, bool isUserManager);
        bool CanEdit(string creatorUserName, TestRequestStatus status, string? userName);
        bool CanDelete(string creatorUserName, TestRequestStatus status, string? userName);
        bool CanApprove(TestRequestStatus status, bool isUserManager);

        void IsPlanExist(int id, PlanPeriod? plan);
        bool CanPlanBeCreated(bool isThereAnyContinuePlan);
        void IsPlanAbleToCreate(bool isThereAnyContinuePlan);
        void IsUserAuthorizedForCreatePlan(string? userName, bool isUserManager);

        void IsUserAuthorizedForFinishPlan(string? userName, bool isUserManager, DateOnly? ActualEndDate);
        bool CanFinishPlan(bool isUserManager, DateOnly? ActualEndDate);
    }
}

