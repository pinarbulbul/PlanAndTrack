using System;
using System.Net.NetworkInformation;
using PlanAndTrack.Application.Exceptions;
using PlanAndTrack.Domain.Entities;
using PlanAndTrack.Domain.Enums;

namespace PlanAndTrack.Application.Validations
{
    public class Validations : IValidations
    {
        public void IsRequestExist(int id, TestRequest? request)
        {
            if (request == null || request?.Id != id)
                throw new NotFoundException(id);
        }

        public void IsRequestEditAuthorized(int id, string creatorUserName, string? userName, TestRequestStatus status)
        {
            if (!IsUserCreator(creatorUserName, userName))
                throw new NotAuthorizedException(userName, id);
            else if (!IsStatusUpdatable(status))
                throw new NotAppropriateStatusException(id);
        }

        public void IsRequestApprovedAuthorized(int id, string creatorUserName, TestRequestStatus status, string? userName, bool isUserManager)
        {
            if (!isUserManager)
                throw new NotAuthorizedException(userName, id);
            else if (!IsStatusUpdatable(status))
                throw new NotAppropriateStatusException(id);
        }

        public bool CanEdit(string creatorUserName, TestRequestStatus status, string? userName)
        {
            if (IsUserCreator(creatorUserName, userName) && IsStatusUpdatable(status))
                return true;
            return false;
        }

        public bool CanDelete(string creatorUserName, TestRequestStatus status, string? userName)
        {
            if (IsUserCreator(creatorUserName, userName) && IsStatusUpdatable(status))
                return true;
            return false;
        }

        public bool CanApprove(TestRequestStatus status, bool isUserManager)
        {
            if (isUserManager && IsStatusUpdatable(status))
                return true;
            return false;
        }

        private bool IsUserCreator(string creatorUserName, string? userName)
        {
            if (!String.IsNullOrEmpty(userName)
               && creatorUserName == userName)
                return true;
            return false;
        }

        private bool IsStatusUpdatable(TestRequestStatus status)
        {
            if (status == TestRequestStatus.NEW || status == TestRequestStatus.UPDATED || status == TestRequestStatus.APPROVED || status == TestRequestStatus.REJECTED)
                return true;
            return false;
        }

        public void IsPlanExist(int id, PlanPeriod? plan)
        {
            if (plan == null || plan?.Id != id)
                throw new NotFoundException(id);
        }

        public bool CanPlanBeCreated(bool isThereAnyContinuePlan)
        {
            return !isThereAnyContinuePlan;
        }

        public void IsPlanAbleToCreate(bool isThereAnyContinuePlan)
        {
            if (!CanPlanBeCreated(isThereAnyContinuePlan))
                throw new NotAppropriateForCreatePlan();
        }

        public void IsUserAuthorizedForCreatePlan(string? userName, bool isUserManager)
        {
            if (!isUserManager)
                throw new NotAuthorizedException(userName, null);
        }

        public void IsUserAuthorizedForFinishPlan(string? userName, bool isUserManager, DateOnly? ActualEndDate)
        {
            if (!CanFinishPlan(isUserManager, ActualEndDate))
                throw new NotAuthorizedException(userName, null);
        }

        public bool CanFinishPlan(bool isUserManager, DateOnly? ActualEndDate)
        {
            if (isUserManager && ActualEndDate==null)
                return true;
            return false;
        }
    }
}

