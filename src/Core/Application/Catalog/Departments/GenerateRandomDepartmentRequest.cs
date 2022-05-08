using FSH.Starter.Application.Catalog.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Starter.Application.Catalog.Departments;

public class GenerateRandomDepartmentRequest : IRequest<string>
{
    public int NSeed { get; set; }
}

public class GenerateRandomDepartmentRequestHandler : IRequestHandler<GenerateRandomDepartmentRequest, string>
{
    private readonly IJobService _jobService;

    public GenerateRandomDepartmentRequestHandler(IJobService jobService) => _jobService = jobService;

    public Task<string> Handle(GenerateRandomDepartmentRequest request, CancellationToken cancellationToken)
    {
        string jobId = _jobService.Enqueue<IDepartmentGeneratorJob>(x => x.GenerateAsync(request.NSeed, default));
        return Task.FromResult(jobId);
    }
}