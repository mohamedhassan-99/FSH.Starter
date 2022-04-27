using FSH.Starter.Application.Catalog.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Starter.Application.Catalog.Notes;

public class DeleteNoteRequest: IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteNoteRequest(Guid id) => Id = id;
}

public class DeleteNoteRequestHandler : IRequestHandler<DeleteNoteRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Asset> _assetRepo;
    private readonly IReadRepository<Note> _NoteRepo;
    private readonly IStringLocalizer<DeleteNoteRequestHandler> _localizer;

    public DeleteNoteRequestHandler(IRepositoryWithEvents<Asset> assetRepo, IReadRepository<Note> NoteRepo, IStringLocalizer<DeleteNoteRequestHandler> localizer) =>
        (_assetRepo, _NoteRepo, _localizer) = (assetRepo, NoteRepo, localizer);

    public async Task<Guid> Handle(DeleteNoteRequest request, CancellationToken cancellationToken)
    {
        if (await _NoteRepo.AnyAsync(new NotesByAssetSpecs(request.Id), cancellationToken))
        {
            throw new ConflictException(_localizer["brand.cannotbedeleted"]);
        }

        var brand = await _assetRepo.GetByIdAsync(request.Id, cancellationToken);

        _ = brand ?? throw new NotFoundException(_localizer["brand.notfound"]);

        await _assetRepo.DeleteAsync(brand, cancellationToken);

        return request.Id;
    }
}

