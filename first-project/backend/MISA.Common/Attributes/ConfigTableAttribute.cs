namespace MISA.Common.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class ConfigTableAttribute(string tableName) : Attribute
{
    public readonly string TableName = tableName;
}