namespace FSH.Starter.Application.Catalog.Categories;

public class UpdateCategoryRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}

public class UpdateCategoryRequestValidator : CustomValidator<UpdateCategoryRequest>
{
    public UpdateCategoryRequestValidator(IRepository<Category> repository, IStringLocalizer<UpdateCategoryRequestValidator> localizer) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (Category, name, ct) =>
                    await repository.GetBySpecAsync(new CategoryByNameSpec(name), ct)
                        is not Category existingCategory || existingCategory.Id == Category.Id)
                .WithMessage((_, name) => string.Format(localizer["Category.alreadyexists"], name));
}

public class UpdateCategoryRequestHandler : IRequestHandler<UpdateCategoryRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Category> _repository;
    private readonly IStringLocalizer<UpdateCategoryRequestHandler> _localizer;

    public UpdateCategoryRequestHandler(IRepositoryWithEvents<Category> repository, IStringLocalizer<UpdateCategoryRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = category ?? throw new NotFoundException(string.Format(_localizer["category.notfound"], request.Id));

        category.Update(request.Name, request.Description);

        await _repository.UpdateAsync(category, cancellationToken);

        return request.Id;
    }
}