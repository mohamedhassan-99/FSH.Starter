namespace FSH.Starter.Application.Catalog.Projects;

public class DeleteRandomProjectRequest : IRequest<string>
{
}

public class DeleteRandomProjectRequestHandler : IRequestHandler<DeleteRandomProjectRequest, string>
{
    private readonly IJobService _jobService;

    public DeleteRandomProjectRequestHandler(IJobService jobService) => _jobService = jobService;

    public Task<string> Handle(DeleteRandomProjectRequest request, CancellationToken cancellationToken)
    {
        string jobId = _jobService.Schedule<IProjectGeneratorJob>(x => x.CleanAsync(default), TimeSpan.FromSeconds(5));
        return Task.FromResult(jobId);
    }
}