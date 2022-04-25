namespace FSH.Starter.Application.Catalog.Categories;

public class DeleteRandomCategoryRequest : IRequest<string>
{
}

public class DeleteRandomCategoryRequestHandler : IRequestHandler<DeleteRandomCategoryRequest, string>
{
    private readonly IJobService _jobService;

    public DeleteRandomCategoryRequestHandler(IJobService jobService) => _jobService = jobService;

    public Task<string> Handle(DeleteRandomCategoryRequest request, CancellationToken cancellationToken)
    {
        string jobId = _jobService.Schedule<ICategoryGeneratorJob>(x => x.CleanAsync(default), TimeSpan.FromSeconds(5));
        return Task.FromResult(jobId);
    }
}