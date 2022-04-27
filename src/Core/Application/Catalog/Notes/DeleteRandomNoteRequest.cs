using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Starter.Application.Catalog.Notes;

public class DeleteRandomNoteRequest : IRequest<string>
{

}
public class DeleteRandomNoteRequestHandler : IRequestHandler<DeleteRandomNoteRequest, string>
{
    private readonly IJobService _jobService;

    public DeleteRandomNoteRequestHandler(IJobService jobService) => _jobService = jobService;

    public Task<string> Handle(DeleteRandomNoteRequest request, CancellationToken cancellationToken)
    {
        string jobId = _jobService.Schedule<INoteGeneratorJob>(x => x.CleanAsync(default), TimeSpan.FromSeconds(5));
        return Task.FromResult(jobId);
    }
}