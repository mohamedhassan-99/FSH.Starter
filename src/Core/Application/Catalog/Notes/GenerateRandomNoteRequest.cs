using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Starter.Application.Catalog.Notes;

public class GenerateRandomNoteRequest : IRequest<string>
{
    public int NSeed { get; set; }
}

public class GenerateRandomNoteRequestHandler : IRequestHandler<GenerateRandomNoteRequest, string>
{
    private readonly IJobService _jobService;

    public GenerateRandomNoteRequestHandler(IJobService jobService) => _jobService = jobService;

    public Task<string> Handle(GenerateRandomNoteRequest request, CancellationToken cancellationToken)
    {
        string jobId = _jobService.Enqueue<INoteGeneratorJob>(x => x.GenerateAsync(request.NSeed, default));
        return Task.FromResult(jobId);
    }
}
