using FSH.Starter.Application.Catalog.Projects;

namespace FSH.Starter.Host.Controllers.Catalog;

public class ProjectsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Projects)]
    [OpenApiOperation("Search Projects using available filters.", "")]
    public Task<PaginationResponse<ProjectDto>> SearchAsync(SearchProjectsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Projects)]
    [OpenApiOperation("Get project details.", "")]
    public Task<ProjectDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetProjectRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Projects)]
    [OpenApiOperation("Create a new project.", "")]
    public Task<Guid> CreateAsync(CreateProjectRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Projects)]
    [OpenApiOperation("Update a project.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateProjectRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Projects)]
    [OpenApiOperation("Delete a project.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteProjectRequest(id));
    }

    [HttpPost("generate-random")]
    [MustHavePermission(FSHAction.Generate, FSHResource.Projects)]
    [OpenApiOperation("Generate a number of random Projects.", "")]
    public Task<string> GenerateRandomAsync(GenerateRandomProjectRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpDelete("delete-random")]
    [MustHavePermission(FSHAction.Clean, FSHResource.Projects)]
    [OpenApiOperation("Delete the Projects generated with the generate-random call.", "")]
    [ApiConventionMethod(typeof(FSHApiConventions), nameof(FSHApiConventions.Search))]
    public Task<string> DeleteRandomAsync()
    {
        return Mediator.Send(new DeleteRandomProjectRequest());
    }
}