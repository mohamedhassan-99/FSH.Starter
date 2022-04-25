namespace FSH.Starter.Application.Dashboard;

public class StatsDto
{
    public int AssetCount { get; set; }
    public int ProductCount { get; set; }
    public int BrandCount { get; set; }
    public int CategoryCount { get; set; }
    public int UserCount { get; set; }
    public int RoleCount { get; set; }
    public List<ChartSeries> DataEnterBarChart { get; set; } = new();
    public Dictionary<string, double>? ProductByBrandTypePieChart { get; set; }
    public Dictionary<string, double>? AssetByBrandTypePieChart { get; set; }
    public Dictionary<string, double>? AssetByCategoryTypePieChart { get; set; }
}

public class ChartSeries
{
    public string? Name { get; set; }
    public double[]? Data { get; set; }
}