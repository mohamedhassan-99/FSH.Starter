namespace FSH.Starter.Application.Catalog.Tags;
public class CreateTagRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public string? Color { get; set; }
    //public IList<Asset>? Assets { get; set; }


}
public class CreateTagRequestValidator : CustomValidator<CreateTagRequest>
{

    public CreateTagRequestValidator(IReadRepository<Tag> repository, IStringLocalizer<CreateTagRequestValidator> localizer)
    {
       // RuleFor(p => p.Name)
       //     .NotEmpty()
       //     .MaximumLength(75)
       //     .MustAsync(async (name, ct) => await repository.GetBySpecAsync(new TagByNameSpec(name), ct) is not null)
       //     .WithMessage((_, name) => string.Format(localizer["tag.alreadyexists"], name));
    }
}

public class CreateTagRequestHandler : IRequestHandler<CreateTagRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Tag> _repository;

    public CreateTagRequestHandler(IRepositoryWithEvents<Tag> repository) => _repository = repository;

    public async Task<Guid> Handle(CreateTagRequest request, CancellationToken cancellationToken)
    {
        var tag = new Tag(request.Name, request.Color,null );

        await _repository.AddAsync(tag, cancellationToken);

        return tag.Id;
    }
}