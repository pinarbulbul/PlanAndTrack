using System;
namespace PlanAndTrack.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(object key)
            : base($"Entity {key} was not found.")
        {
        }
    }
}

