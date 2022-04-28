namespace FSH.Starter.Application.Catalog.Tags;

public class GetTagRequest : IRequest<TagDto>
{
    public Guid Id { get; set; }

    public GetTagRequest(Guid id) => Id = id;
}

public class TagByIdSpec : Specification<Tag, TagDto>, ISingleResultSpecification
{
    public TagByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetTagRequestHandler : IRequestHandler<GetTagRequest, TagDto>
{
    private readonly IRepository<Tag> _repository;
    private readonly IStringLocalizer<GetTagRequestHandler> _localizer;

    public GetTagRequestHandler(IRepository<Tag> repository, IStringLocalizer<GetTagRequestHandler> localizer) => (_repository, _localizer) = (repository, localizer);

    public async Task<TagDto> Handle(GetTagRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<Tag, TagDto>)new TagByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["tags.notfound"], request.Id));
}