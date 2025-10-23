

using MediatR;
using Sorted.Domain;

namespace Sorted.Application.Commands;

public class CreateJobHandler : IRequestHandler<CreateJobCommand, string>
{
    private ICreateRepository<Job, string> _repository;
    private IIdentityService _identityService;

    public CreateJobHandler(ICreateRepository<Job, string> repository, IIdentityService identityService)
    {
        _repository = repository;
        _identityService = identityService;
    }

    public async Task<string> Handle(CreateJobCommand request, CancellationToken cancellationToken)
    {
        Result<string> userIdResult = await _identityService.GetUserId();

        if (!userIdResult.IsSuccess)
        {
            throw new Exception(userIdResult.ErrorsToString());
        }

        JobBuilder jobBuilder = new JobBuilder();
        Job newJob = jobBuilder
            .Title(request.title)
            .OwnerId(userIdResult.Value)
            .StartDate(request.startDate)
            .EndDate(request.EndDate)
            .Creation(request.creation)
            .AgreedPrice(request.agreedPrice)
            .Build();

        Result<string> result = await _repository.Create(newJob, cancellationToken);

        return result.Value;
    }
}