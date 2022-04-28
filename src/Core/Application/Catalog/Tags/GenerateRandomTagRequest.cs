namespace FSH.Starter.Application.Catalog.Tags;

public class GenerateRandomTagRequest : IRequest<string>
{
    public int NSeed { get; set; }
}

public class GenerateRandomTagRequestHandler : IRequestHandler<GenerateRandomTagRequest, string>
{
    private readonly IJobService _jobService;

    public GenerateRandomTagRequestHandler(IJobService jobService) => _jobService = jobService;

    public Task<string> Handle(GenerateRandomTagRequest request, CancellationToken cancellationToken)
    {
        string jobId = _jobService.Enqueue<ITagGeneratorJob>(x => x.GenerateAsync(request.NSeed, default));
        return Task.FromResult(jobId);
    }
}