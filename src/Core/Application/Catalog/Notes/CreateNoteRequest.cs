using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Starter.Application.Catalog.Notes;

public class CreateNoteRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public string? NoteContent { get; set; }
}
public class CreateNoteRequestValidator : CustomValidator<CreateNoteRequest>
{
    public CreateNoteRequestValidator(IReadRepository<Note> repository, IStringLocalizer<CreateNoteRequestValidator> localizer) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.GetBySpecAsync(new NoteByNameSpec(name), ct) is null)
                .WithMessage((_, name) => string.Format(localizer["note.alreadyexists"], name));
}

public class CreateNoteRequestHandler : IRequestHandler<CreateNoteRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Note> _repository;

    public CreateNoteRequestHandler(IRepositoryWithEvents<Note> repository) => _repository = repository;

    public async Task<Guid> Handle(CreateNoteRequest request, CancellationToken cancellationToken)
    {
        var note = new Note(request.Name, request.NoteContent);

        await _repository.AddAsync(note, cancellationToken);

        return note.Id;
    }
}
