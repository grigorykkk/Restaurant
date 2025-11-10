using Microsoft.EntityFrameworkCore.Metadata.Internal;

public class TableType
{
    public int IdTableType { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public List<Table> Tables { get; set; } = new();
}