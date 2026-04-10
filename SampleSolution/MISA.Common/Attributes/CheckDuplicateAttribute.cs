namespace Model.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class CheckDuplicateAttribute(string errorMessage) : System.Attribute
{
    public readonly string ErrorMessage = errorMessage;
}