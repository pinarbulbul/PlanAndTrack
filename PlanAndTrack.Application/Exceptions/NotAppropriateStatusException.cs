using System;
namespace PlanAndTrack.Application.Exceptions
{
    public class NotAppropriateStatusException : Exception
    {
        public NotAppropriateStatusException(object key)
            : base($"Entity {key} was not appropriate for this action.")
        {
        }
    }
}

