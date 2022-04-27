namespace FSH.Starter.Application.Catalog.Projects;

public class CreateProjectRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}

public class CreateProjectRequestValidator : CustomValidator<CreateProjectRequest>
{
    public CreateProjectRequestValidator(IReadRepository<Project> repository, IStringLocalizer<CreateProjectRequestValidator> localizer) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.GetBySpecAsync(new ProjectByNameSpec(name), ct) is null)
                .WithMessage((_, name) => string.Format(localizer["project.alreadyexists"], name));
}

public class CreateProjectRequestHandler : IRequestHandler<CreateProjectRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Project> _repository;

    public CreateProjectRequestHandler(IRepositoryWithEvents<Project> repository) => _repository = repository;

    public async Task<Guid> Handle(CreateProjectRequest request, CancellationToken cancellationToken)
    {
        var project = new Project(request.Name, request.Description);

        await _repository.AddAsync(project, cancellationToken);

        return project.Id;
    }
}