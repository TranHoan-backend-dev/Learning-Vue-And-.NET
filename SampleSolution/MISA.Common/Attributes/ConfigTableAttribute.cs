namespace Model.Attributes;

public class ConfigTableAttribute(string tableName) : Attribute
{
    public string TableName { get; set; } = tableName;
}