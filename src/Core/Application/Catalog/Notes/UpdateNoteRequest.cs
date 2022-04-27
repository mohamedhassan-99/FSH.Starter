using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Starter.Application.Catalog.Notes;

public class UpdateNoteRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string NoteContent { get; set; }
}

public class UpdateNoteRequestValidator : CustomValidator<UpdateNoteRequest>
{
    public UpdateNoteRequestValidator(IRepository<Note> repository, IStringLocalizer<UpdateNoteRequestValidator> localizer) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (note, name, ct) =>
                    await repository.GetBySpecAsync(new NoteByNameSpec(name), ct)
                        is not Note existingNote || existingNote.Id == note.Id)
                .WithMessage((_, name) => string.Format(localizer["brand.alreadyexists"], name));
}

public class UpdateNoteRequestHandler : IRequestHandler<UpdateNoteRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Note> _repository;
    private readonly IStringLocalizer<UpdateNoteRequestHandler> _localizer;

    public UpdateNoteRequestHandler(IRepositoryWithEvents<Note> repository, IStringLocalizer<UpdateNoteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(UpdateNoteRequest request, CancellationToken cancellationToken)
    {
        var brand = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = brand ?? throw new NotFoundException(string.Format(_localizer["brand.notfound"], request.Id));

        brand.Update(request.Name, request.NoteContent);

        await _repository.UpdateAsync(brand, cancellationToken);

        return request.Id;
    }
}