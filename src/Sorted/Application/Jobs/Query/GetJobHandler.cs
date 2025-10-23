using MediatR;
using Sorted.Application.Common;
using Sorted.Domain;

namespace Sorted.Application.Query;

public class GetJobHandler : IQueryHandler<GetJobQuery, List<GetJobResponse>>
{
    private IReadRepository<GetJobQuery, Job> _readRepository;
    private IIdentityService _identityService;

    public GetJobHandler(IReadRepository<GetJobQuery, Job> readRepository, IIdentityService identityService)
    {
        _readRepository = readRepository;
        _identityService = identityService;
    }

    public async Task<List<GetJobResponse>> Handle(GetJobQuery request, CancellationToken cancellationToken)
    {
        Result<List<Job>> result = await _readRepository.FindAll(request, cancellationToken);

        List<Job> jobList = result.Value;
        List<GetJobResponse> jobDto = new List<GetJobResponse>();

        foreach (Job job in jobList)
        {
            Result<Access> access = await _identityService.GetAccess(job, cancellationToken);
            if (access.IsSuccess && access.Value.Read)
            {
                jobDto.Add(job.MapToJobResponse());
            }
        }

        return jobDto;
    }
}
