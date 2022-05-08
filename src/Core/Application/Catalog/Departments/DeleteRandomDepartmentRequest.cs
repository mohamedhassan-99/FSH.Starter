using FSH.Starter.Application.Catalog.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Starter.Application.Catalog.Departments;

public class DeleteRandomDepartmentRequest : IRequest<string>
{
}

public class DeleteRandomDepartmentRequestHandler : IRequestHandler<DeleteRandomDepartmentRequest, string>
{
    private readonly IJobService _jobService;

    public DeleteRandomDepartmentRequestHandler(IJobService jobService) => _jobService = jobService;

    public Task<string> Handle(DeleteRandomDepartmentRequest request, CancellationToken cancellationToken)
    {
        string jobId = _jobService.Schedule<IDepartmentGeneratorJob>(x => x.CleanAsync(default), TimeSpan.FromSeconds(5));
        return Task.FromResult(jobId);
    }
}