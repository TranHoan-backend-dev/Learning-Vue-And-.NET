namespace MISA.Common.Attributes;

// Cau hinh de thuoc tinh nay ap dung len property 
[AttributeUsage(AttributeTargets.Property)]
public class CheckDuplicateAttribute(string errorMessage) : Attribute
{
    public readonly string ErrorMessage = errorMessage;
}