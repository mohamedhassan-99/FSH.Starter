using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Starter.Application.Catalog.Departments;

public class UpdateDepartmentRequest : IRequest<Guid>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}

public class UpdateDepartmentRequestValidator : CustomValidator<UpdateDepartmentRequest>
{
    public UpdateDepartmentRequestValidator(IRepository<Department> repository, IStringLocalizer<UpdateDepartmentRequestValidator> localizer) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (department, name, ct) =>
                    await repository.GetBySpecAsync(new DepartmentByNameSpec(name), ct)
                        is not Department existingdep || existingdep.Id == department.Id)
                .WithMessage((_, name) => string.Format(localizer["Department.alreadyexists"], name));
}

public class UpdateDepartmentRequestHandler : IRequestHandler<UpdateDepartmentRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Department> _repository;
    private readonly IStringLocalizer<UpdateDepartmentRequestHandler> _localizer;

    public UpdateDepartmentRequestHandler(IRepositoryWithEvents<Department> repository, IStringLocalizer<UpdateDepartmentRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(UpdateDepartmentRequest request, CancellationToken cancellationToken)
    {
        var dep = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = dep ?? throw new NotFoundException(string.Format(_localizer["department.notfound"], request.Id));

        dep.Update(request.Name, request.Description);

        await _repository.UpdateAsync(dep, cancellationToken);

        return request.Id;
    }
}