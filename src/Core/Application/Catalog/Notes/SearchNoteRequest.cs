using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Starter.Application.Catalog.Notes;

public class SearchNoteRequest : PaginationFilter, IRequest<PaginationResponse<NoteDto>>
{

}
public class NotesBySearchRequestSpec : EntitiesByPaginationFilterSpec<Note, NoteDto>
{
    public NotesBySearchRequestSpec(SearchNoteRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class SearchNotesRequestHandler : IRequestHandler<SearchNoteRequest, PaginationResponse<NoteDto>>
{
    private readonly IReadRepository<Note> _repository;

    public SearchNotesRequestHandler(IReadRepository<Note> repository) => _repository = repository;

    public async Task<PaginationResponse<NoteDto>> Handle(SearchNoteRequest request, CancellationToken cancellationToken)
    {
        var spec = new NotesBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}