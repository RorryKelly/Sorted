using MediatR;
using Sorted.Domain;

namespace Sorted.Application.Jobs;

public record CreateJobCommand(string title, decimal agreedPrice, DateTime creationDate, DateTime startDate) : IRequest<Guid>;
public class CreateJobHandler : IRequestHandler<CreateJobCommand, Guid>
{
    private IJobRepository _jobRepository;
    private IIdentityService _identityService;

    public CreateJobHandler(IJobRepository jobRepository, IIdentityService identityService)
    {
        _jobRepository = jobRepository;
        _identityService = identityService;
    }

    public async Task<Guid> Handle(CreateJobCommand request, CancellationToken cancellationToken)
    {
        return Guid.NewGuid();
    }
}