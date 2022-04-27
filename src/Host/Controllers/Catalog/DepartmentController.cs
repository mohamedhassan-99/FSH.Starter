using FSH.Starter.Application.Catalog.Departments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FSH.Starter.Host.Controllers.Catalog;
[Route("api/[controller]")]
[ApiController]
public class DepartmentController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Departments)]
    [OpenApiOperation("Search department using available filters.", "")]
    public Task<PaginationResponse<DepartmentDto>> SearchAsync(SearchDepartmentRequest request)
    {
        return Mediator.Send(request);
    }
    
    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Departments)]
    [OpenApiOperation("Get department details.", "")]
    public Task<DepartmentDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetDepartmentRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Departments)]
    [OpenApiOperation("Create a new department.", "")]
    public Task<Guid> CreateAsync(CreateDepartmentRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Departments)]
    [OpenApiOperation("Update a department.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateDepartmentRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Departments)]
    [OpenApiOperation("Delete a department.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteDepartmentRequest(id));
    }

    [HttpPost("generate-random")]
    [MustHavePermission(FSHAction.Generate, FSHResource.Departments)]
    [OpenApiOperation("Generate a number of random department.", "")]
    public Task<string> GenerateRandomAsync(GenerateRandomDepartmentRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpDelete("delete-random")]
    [MustHavePermission(FSHAction.Clean, FSHResource.Departments)]
    [OpenApiOperation("Delete the department generated with the generate-random call.", "")]
    [ApiConventionMethod(typeof(FSHApiConventions), nameof(FSHApiConventions.Search))]
    public Task<string> DeleteRandomAsync()
    {
        return Mediator.Send(new DeleteRandomDepartmentRequest());
    }
}
