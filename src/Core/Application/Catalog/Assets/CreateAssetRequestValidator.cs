namespace FSH.Starter.Application.Catalog.Assets;

public class CreateAssetRequestValidator : CustomValidator<CreateAssetRequest>
{
    public CreateAssetRequestValidator(IReadRepository<Asset> assetRepo, IReadRepository<Brand> brandRepo, IReadRepository<Category> categoryRepo, IReadRepository<Project> projectRepo, IStringLocalizer<CreateAssetRequestValidator> localizer)
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await assetRepo.GetBySpecAsync(new AssetByNameSpec(name), ct) is null)
                .WithMessage((_, name) => string.Format(localizer["asset.alreadyexists"], name));

        RuleFor(p => p.Rate)
            .GreaterThanOrEqualTo(1);

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new FileUploadRequestValidator());

        RuleFor(p => p.BrandId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await brandRepo.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => string.Format(localizer["brand.notfound"], id));
        RuleFor(p => p.CategoryId)
                .NotEmpty()
                .MustAsync(async (id, ct) => await categoryRepo.GetByIdAsync(id, ct) is not null)
                    .WithMessage((_, id) => string.Format(localizer["category.notfound"], id));
        RuleFor(p => p.ProjectId)
                    .NotEmpty()
                    .MustAsync(async (id, ct) => await projectRepo.GetByIdAsync(id, ct) is not null)
                        .WithMessage((_, id) => string.Format(localizer["project.notfound"], id));
    }
}