namespace FSH.Starter.Application.Catalog.Projects;

public class GenerateRandomProjectRequest : IRequest<string>
{
    public int NSeed { get; set; }
}

public class GenerateRandomProjectRequestHandler : IRequestHandler<GenerateRandomProjectRequest, string>
{
    private readonly IJobService _jobService;

    public GenerateRandomProjectRequestHandler(IJobService jobService) => _jobService = jobService;

    public Task<string> Handle(GenerateRandomProjectRequest request, CancellationToken cancellationToken)
    {
        string jobId = _jobService.Enqueue<IProjectGeneratorJob>(x => x.GenerateAsync(request.NSeed, default));
        return Task.FromResult(jobId);
    }
}