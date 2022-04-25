namespace FSH.Starter.Application.Catalog.Categories;

public class GenerateRandomCategoryRequest : IRequest<string>
{
    public int NSeed { get; set; }
}

public class GenerateRandomCategoryRequestHandler : IRequestHandler<GenerateRandomCategoryRequest, string>
{
    private readonly IJobService _jobService;

    public GenerateRandomCategoryRequestHandler(IJobService jobService) => _jobService = jobService;

    public Task<string> Handle(GenerateRandomCategoryRequest request, CancellationToken cancellationToken)
    {
        string jobId = _jobService.Enqueue<ICategoryGeneratorJob>(x => x.GenerateAsync(request.NSeed, default));
        return Task.FromResult(jobId);
    }
}