namespace FSH.Starter.Application.Catalog.Tags;

public class DeleteRandomTagRequest : IRequest<string>
{
}

public class DeleteRandomTagRequestHandler : IRequestHandler<DeleteRandomTagRequest, string>
{
    private readonly IJobService _jobService;

    public DeleteRandomTagRequestHandler(IJobService jobService) => _jobService = jobService;

    public Task<string> Handle(DeleteRandomTagRequest request, CancellationToken cancellationToken)
    {
        string jobId = _jobService.Schedule<ITagGeneratorJob>(x => x.CleanAsync(default), TimeSpan.FromSeconds(5));
        return Task.FromResult(jobId);
    }
}