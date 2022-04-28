namespace FSH.Starter.Domain.Catalog;

public class Asset : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; } = default!;
    public string? Summary { get; private set; }
    public string? Description { get; private set; }
    public string? Location { get; private set; }
    public string? Longitude { get; private set; }
    public string? Latitude { get; private set; }
    public string? Barcode { get; private set; }
    public string? QrCode { get; private set; }
    public string? Model { get; private set; }
    public string? Vendor { get; private set; }
    public decimal? Rate { get; private set; }
    public string? ImagePath { get; private set; }
    public Guid? BrandId { get; private set; }
    public Guid? CategoryId { get; private set; }
    public Guid? ProjectId { get; private set; }
    public Guid? DepartmentId { get; private set; }
    public IList<Tag>? Tags { get; private set; }
    public virtual Brand Brand { get; private set; } = default!;
    public virtual Category Category { get; private set; } = default!;
    public virtual Project Project { get; private set; } = default!;
    public virtual Department Department { get; private set; } = default!;
    public Asset(string name, string? summary, string? description, string? location, string? longitude, string? latitude, string? barcode, string? qrCode, string? model, string? vendor, decimal? rate, string? imagePath, Guid? brandId, Guid? categoryId, Guid? projectId, Guid? departmentId, List<Tag>? tags)
    {
        Name = name;
        Description = description;
        Summary = summary;
        Location = location;
        Longitude = longitude;
        Latitude = latitude;
        Barcode = barcode;
        QrCode = qrCode;
        Model = model;
        Vendor = vendor;
        Rate = rate;
        ImagePath = imagePath;
        BrandId = brandId;
        CategoryId = categoryId;
        ProjectId = projectId;
        DepartmentId = departmentId;
        Tags = tags;
    }

    public Asset Update(string? name, string? summary, string? description, string? location, string? longitude, string? latitude, string? barcode, string? qrCode, string? model, string? vendor, decimal? rate, string? imagePath, Guid? brandId, Guid? categoryId, Guid? projectId, Guid? departmentId, List<Tag>? tags)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (summary is not null && Summary?.Equals(summary) is not true) Summary = summary;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (location is not null && Location?.Equals(location) is not true) Location = location;
        if (longitude is not null && Longitude?.Equals(longitude) is not true) Longitude = longitude;
        if (latitude is not null && Latitude?.Equals(latitude) is not true) Latitude = latitude;
        if (barcode is not null && Barcode?.Equals(barcode) is not true) Barcode = barcode;
        if (qrCode is not null && QrCode?.Equals(qrCode) is not true) QrCode = qrCode;
        if (model is not null && Model?.Equals(model) is not true) Model = model;
        if (vendor is not null && Vendor?.Equals(vendor) is not true) Vendor = vendor;
        if (rate.HasValue && Rate != rate) Rate = rate.Value;
        if (brandId.HasValue && brandId.Value != Guid.Empty && !BrandId.Equals(brandId.Value)) BrandId = brandId.Value;
        if (categoryId.HasValue && categoryId.Value != Guid.Empty && !CategoryId.Equals(categoryId.Value)) CategoryId = categoryId.Value;
        if (projectId.HasValue && projectId.Value != Guid.Empty && !ProjectId.Equals(projectId.Value)) ProjectId = projectId.Value;
        if (departmentId.HasValue && departmentId.Value != Guid.Empty && !DepartmentId.Equals(departmentId.Value)) DepartmentId = departmentId.Value;
        if (tags is not null && Tags?.Equals(tags) is not true) Tags = tags;
        if (imagePath is not null && ImagePath?.Equals(imagePath) is not true) ImagePath = imagePath;
        return this;
    }

    public Asset ClearImagePath()
    {
        ImagePath = string.Empty;
        return this;
    }
}