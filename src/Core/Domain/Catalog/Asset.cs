namespace FSH.Starter.Domain.Catalog;

public class Asset : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public decimal? Rate { get; private set; }
    public string? ImagePath { get; private set; }
    public Guid? BrandId { get; private set; }
    public Guid? CategoryId { get; private set; }
    public Guid? ProjectId { get; private set; }
    public Guid? DepartmentId { get; private set; }
  
    public virtual Brand Brand { get; private set; } = default!;
    public virtual Category Category { get; private set; } = default!;
    public virtual Project Project { get; private set; } = default!;
    public virtual Department Department { get; private set; } = default!;
     
    public Asset(string name, string? description, decimal rate, string? imagePath, Guid? brandId, Guid? categoryId, Guid? projectId, Guid? departmentId)
    {
        Name = name;
        Description = description;
        Rate = rate;
        ImagePath = imagePath;
        BrandId = brandId;
        CategoryId = categoryId;
        ProjectId = projectId;
        DepartmentId = departmentId;
    }

    public Asset Update(string? name, string? description, decimal? rate, string? imagePath,  Guid? brandId, Guid? categoryId, Guid? projectId, Guid? departmentId)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (rate.HasValue && Rate != rate) Rate = rate.Value;
        if (brandId.HasValue && brandId.Value != Guid.Empty && !BrandId.Equals(brandId.Value)) BrandId = brandId.Value;
        if (categoryId.HasValue && categoryId.Value != Guid.Empty && !CategoryId.Equals(categoryId.Value)) CategoryId = categoryId.Value;
        if (projectId.HasValue && projectId.Value != Guid.Empty && !ProjectId.Equals(projectId.Value)) ProjectId = projectId.Value;
        if (departmentId.HasValue && departmentId.Value != Guid.Empty && !DepartmentId.Equals(departmentId.Value)) DepartmentId = departmentId.Value;
        if (imagePath is not null && ImagePath?.Equals(imagePath) is not true) ImagePath = imagePath;
        return this;
    }

    public Asset ClearImagePath()
    {
        ImagePath = string.Empty;
        return this;
    }
}