using FSH.Starter.Application.Catalog.Assets;

namespace FSH.Starter.Host.Controllers.Catalog;

public class AssetsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Assets)]
    [OpenApiOperation("Search assets using available filters.", "")]
    public Task<PaginationResponse<AssetDto>> SearchAsync(SearchAssetsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Assets)]
    [OpenApiOperation("Get asset details.", "")]
    public Task<AssetDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetAssetRequest(id));
    }

    [HttpGet("dapper")]
    [MustHavePermission(FSHAction.View, FSHResource.Assets)]
    [OpenApiOperation("Get asset details via dapper.", "")]
    public Task<AssetDto> GetDapperAsync(Guid id)
    {
        return Mediator.Send(new GetAssetViaDapperRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Assets)]
    [OpenApiOperation("Create a new asset.", "")]
    public Task<Guid> CreateAsync(CreateAssetRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Assets)]
    [OpenApiOperation("Update a asset.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateAssetRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Assets)]
    [OpenApiOperation("Delete a asset.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteAssetRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.Assets)]
    [OpenApiOperation("Export a assets.", "")]
    public async Task<FileResult> ExportAsync(ExportAssetsRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "AssetExports");
    }
}