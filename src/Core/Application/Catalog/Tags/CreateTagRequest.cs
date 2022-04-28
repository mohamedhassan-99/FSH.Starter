namespace FSH.Starter.Application.Catalog.Tags;
public class CreateTagRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public string? Color { get; set; }
}
public class CreateTagRequestValidator : CustomValidator<CreateTagRequest>
{

    public CreateTagRequestValidator(IReadRepository<Tag> repository, IStringLocalizer<CreateTagRequestValidator> localizer)
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.GetBySpecAsync(new TagByNameSpec(name), ct) is not null)
            .WithMessage((_, name) => string.Format(localizer["tag.alreadyexists"], name));
    }
}
