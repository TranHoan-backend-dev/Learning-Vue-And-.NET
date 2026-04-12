namespace MISA.Common.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class ConfigTableAttribute(string tableName) : Attribute
{
    public string TableName { get; set; } = tableName;
}