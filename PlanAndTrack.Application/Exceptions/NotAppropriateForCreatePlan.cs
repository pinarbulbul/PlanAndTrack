using System;
namespace PlanAndTrack.Application.Exceptions
{
    public class NotAppropriateForCreatePlan : Exception
    {
        public NotAppropriateForCreatePlan()
            : base($"Bitirilmemiş planlama dönemi olduğundan yeni planlama dönemi oluşturamazsınız.")
        {
        }
    }
}

