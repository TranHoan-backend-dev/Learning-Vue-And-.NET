using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using MISA.Common.Attributes;

namespace MISA.Common.Extension;

/// <summary>
/// Class Utility, dung de mo rong hanh vi cho cac class khac
/// </summary>
public static class ExtensionUtility
{
    /// <summary>
    /// Cho phep lay ra ten bang
    /// </summary>
    /// <param name="type">
    /// Cho phep 1 doi tuong kieu Type goi method nay nhu the no thuoc ve Type
    /// VD: typeof(Employee). Khi do co the goi typeof(Employee).GetTableNameOnly()
    /// </param>
    /// <returns></returns>
    public static string GetTableNameOnly(this Type type)
    {
        return ((ConfigTableAttribute)type.GetCustomAttributes(
                typeof(ConfigTableAttribute),
                true
            )
            .FirstOrDefault()!).TableName;
    }

    /// <summary>
    /// Cho phep lay ra thuoc tinh duoc danh dau la khoa chinh ([Key]) cua bang
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static string GetPrimaryKeyType(this Type type)
    {
        var primaryKeyName = string.Empty;
        // lay ra toan bo property
        PropertyInfo[] properties = type.GetProperties();
        if (properties.Length > 0)
        {
            // tim doi tuong co attribute [Key]
            var propertyInfoKey = properties.SingleOrDefault(p => p
                .GetCustomAttributes<KeyAttribute>(true).Any()
            );
            if (propertyInfoKey != null)
            {
                primaryKeyName = propertyInfoKey.Name;
            }
        }

        return primaryKeyName;
    }

    /// <summary>
    /// Lay ra toan bo cac thuoc tinh duoc map xuong DB (tru NotMapped)
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static List<string> GetAllColumns(this Type type)
    {
        List<string> columns = new List<string>();
        PropertyInfo[] properties = type.GetProperties();
        if (properties.Length > 0)
        {
            // Tim bat cu ban ghi nao khong co NotMapped
            columns = properties.Where(p => !p
                    .GetCustomAttributes<NotMappedAttribute>(true).Any()
                ).Select(p => p.Name)
                .ToList();
        }

        return columns;
    }

    /// <summary>
    /// Tra ve value tuong ung voi key trong Dictionary (HashMap)
    /// </summary>
    /// <param name="dic">Tap hop kieu du lieu key-value (Giong Hashmap trong java)</param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static object? Get(this Dictionary<string, object> dic, string key)
    {
        return dic.GetValueOrDefault(key);
    }

    /// <summary>
    /// Ep kieu obj sang Dictionary
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static Dictionary<string, object> ToDictionary(this object? obj)
    {
        // neu obj cast sang Dictionary<string, object> thanh cong thi lay, con khong thi lay doi tuong Dictionary moi
        return obj as Dictionary<string, object> ?? new Dictionary<string, object>();
    }

    /// <summary>
    /// Lay value theo propertyName
    /// <ul>
    ///     <li>Neu la Dictionary, thi lay tu key</li>
    ///     <li>Neu la object binh thuong thi lay tu property name</li>
    /// </ul>
    /// </summary>
    /// <param name="objEntity"></param>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    public static object? GetValue(this object? objEntity, string propertyName)
    {
        if (objEntity is null || string.IsNullOrEmpty(propertyName))
            return null;

        // neu la Dictionary / Expando, IDictionary
        if (objEntity is IDictionary<string, object> objects)
        {
            return objects.TryGetValue(propertyName, out var value) ? value : null;
        }

        // neu la obj binh thuong
        var info = objEntity.GetType().GetProperty(propertyName);
        return info?.GetValue(objEntity);
    }
}