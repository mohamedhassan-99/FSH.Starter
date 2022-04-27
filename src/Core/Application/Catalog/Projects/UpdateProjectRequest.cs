namespace FSH.Starter.Application.Catalog.Projects;

public class UpdateProjectRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}

public class UpdateProjectRequestValidator : CustomValidator<UpdateProjectRequest>
{
    public UpdateProjectRequestValidator(IRepository<Project> repository, IStringLocalizer<UpdateProjectRequestValidator> localizer) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (Project, name, ct) =>
                    await repository.GetBySpecAsync(new ProjectByNameSpec(name), ct)
                        is not Project existingProject || existingProject.Id == Project.Id)
                .WithMessage((_, name) => string.Format(localizer["Project.alreadyexists"], name));
}

public class UpdateProjectRequestHandler : IRequestHandler<UpdateProjectRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Project> _repository;
    private readonly IStringLocalizer<UpdateProjectRequestHandler> _localizer;

    public UpdateProjectRequestHandler(IRepositoryWithEvents<Project> repository, IStringLocalizer<UpdateProjectRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(UpdateProjectRequest request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = project ?? throw new NotFoundException(string.Format(_localizer["projects.notfound"], request.Id));

        project.Update(request.Name, request.Description);

        await _repository.UpdateAsync(project, cancellationToken);

        return request.Id;
    }
}