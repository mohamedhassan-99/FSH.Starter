namespace FSH.Starter.Application.Catalog.Tags;

public class UpdateTagRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Color { get; set; }
}

public class UpdateTagRequestValidator : CustomValidator<UpdateTagRequest>
{
    public UpdateTagRequestValidator(IRepository<Tag> repository, IStringLocalizer<UpdateTagRequestValidator> localizer) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (tag, name, ct) =>
                    await repository.GetBySpecAsync(new TagByNameSpec(name), ct)
                        is not Tag existingtag || existingtag.Id == tag.Id)
                .WithMessage((_, name) => string.Format(localizer["tag.alreadyexists"], name));
}

public class UpdateTagRequestHandler : IRequestHandler<UpdateTagRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Tag> _repository;
    private readonly IStringLocalizer<UpdateTagRequestHandler> _localizer;

    public UpdateTagRequestHandler(IRepositoryWithEvents<Tag> repository, IStringLocalizer<UpdateTagRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(UpdateTagRequest request, CancellationToken cancellationToken)
    {
        var tag = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = tag ?? throw new NotFoundException(string.Format(_localizer["tags.notfound"], request.Id));

        tag.Update(request.Name, request.Color,null);

        await _repository.UpdateAsync(tag, cancellationToken);

        return request.Id;
    }
}