using System;
namespace PlanAndTrack.Application.Exceptions
{
    public class NotAuthorizedException : Exception
    {
        public NotAuthorizedException(string? userName, object key)
           : base($"{userName} is not authorized for entity {key}.")
        {
        }
    }
}

