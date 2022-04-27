using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Starter.Application.Catalog.Notes;

public class GetNoteRequest : IRequest<NoteDto>
{
    public Guid Id { get; set; }

    public GetNoteRequest(Guid id) => Id = id;
}

public class NoteByIdSpec : Specification<Note, NoteDto>, ISingleResultSpecification
{
    public NoteByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetNoteRequestHandler : IRequestHandler<GetNoteRequest, NoteDto>
{
    private readonly IRepository<Note> _repository;
    private readonly IStringLocalizer<GetNoteRequestHandler> _localizer;

    public GetNoteRequestHandler(IRepository<Note> repository, IStringLocalizer<GetNoteRequestHandler> localizer) => (_repository, _localizer) = (repository, localizer);

    public async Task<NoteDto> Handle(GetNoteRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<Note, NoteDto>)new NoteByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["brand.notfound"], request.Id));
}
