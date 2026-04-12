namespace MISA.Common.Procedures;

public class ProcedureNames
{
    private const string Prefix = "Proc_{0}_";
    public static string Insert = string.Concat(Prefix, "Insert");
    public static string Update = string.Concat(Prefix, "Update");
    public static string Delete = string.Concat(Prefix, "Delete");
    public static string GetAll = string.Concat(Prefix, "GetAll");
}