using FSH.Starter.Application.Catalog.Tags;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FSH.Starter.Host.Controllers.Catalog;
[Route("api/[controller]")]
[ApiController]
public class TagController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Categories)]
    [OpenApiOperation("Search tags using available filters.", "")]
    public Task<PaginationResponse<TagDto>> SearchAsync(SearchTagsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Categories)]
    [OpenApiOperation("Get tag details.", "")]
    public Task<TagDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetTagRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Categories)]
    [OpenApiOperation("Create a new tag.", "")]
    public Task<Guid> CreateAsync(CreateTagRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Categories)]
    [OpenApiOperation("Update a tag.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateTagRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Categories)]
    [OpenApiOperation("Delete a tag.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteTagRequest(id));
    }

    [HttpPost("generate-random")]
    [MustHavePermission(FSHAction.Generate, FSHResource.Categories)]
    [OpenApiOperation("Generate a number of random tags.", "")]
    public Task<string> GenerateRandomAsync(GenerateRandomTagRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpDelete("delete-random")]
    [MustHavePermission(FSHAction.Clean, FSHResource.Categories)]
    [OpenApiOperation("Delete the tags generated with the generate-random call.", "")]
    [ApiConventionMethod(typeof(FSHApiConventions), nameof(FSHApiConventions.Search))]
    public Task<string> DeleteRandomAsync()
    {
        return Mediator.Send(new DeleteRandomTagRequest());
    }
}
