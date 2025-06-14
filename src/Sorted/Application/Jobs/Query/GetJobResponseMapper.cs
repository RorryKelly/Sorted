using Sorted.Domain;

public static class GetJobResponseMapper
{
    public static GetJobResponse MapToJobResponse(this Job job)
    {
        return new GetJobResponse()
        {
            Title = job.Title,
            OwnerName = job.OwnerId,
            StartDate = job.StartDate,
            EndDate = job.EndDate,
            Creation = job.Creation,
            AgreedPrice = job.AgreedPrice
        };
    }
}