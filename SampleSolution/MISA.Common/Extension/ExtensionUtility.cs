using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Model.Attributes;

namespace Model.Extension;

public static class ExtensionUtility
{
    public static string GetTableNameOnly(this Type type)
    {
        return ((ConfigTableAttribute)type.GetCustomAttributes(typeof(ConfigTableAttribute), true)?
                .FirstOrDefault())?
            .TableName;
    }

    public static string GetPrimaryKeyType(this Type type)
    {
        var primaryKeyName = string.Empty;
        PropertyInfo[] properties = type.GetProperties();
        if (properties != null)
        {
            var propertyInfoKey =
                properties.SingleOrDefault((p => p.GetCustomAttributes(typeof(KeyAttribute), true) != null));
            if (propertyInfoKey != null)
            {
                primaryKeyName = propertyInfoKey.Name;
            }
        }

        return primaryKeyName;
    }

    /// <summary>
    /// Lay ra thong tin toan bo cac cot khong bao gom NotMapped
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static List<string> GetAllColumns(this Type type)
    {
        List<string> columns = new List<string>();
        PropertyInfo[] properties = type.GetProperties();
        if (properties != null)
        {
            columns = properties.Where(p => p.GetCustomAttributes(typeof(NotMappedAttribute), true) == null)
                .Select(p => p.Name)
                .ToList();
        }

        return columns;
    }

    public static object Get(this Dictionary<string, object> dic, string key)
    {
        if (dic.ContainsKey(key))
        {
            return dic[key];
        }

        return null;
    }

    public static Dictionary<string, object> ToDictionary(this object obj)
    {
        if (obj is null)
        {
            return new Dictionary<string, object>();
        }
        else
        {
            return (Dictionary<string, object>)obj;
        }
    }

    public static object? GetValue(this object objEntity, string propertyName)
    {
        if (objEntity != null && !string.IsNullOrEmpty(propertyName))
        {
            if (objEntity.GetType() == typeof(Dictionary<string, object>))
            {
                return (objEntity as Dictionary<string, object>).GetValue(propertyName);
            }
            else
            {
                // PropertyInfo[] info = objEntity.GetType().GetProperty(propertyName);
                PropertyInfo? info = objEntity.GetType().GetProperty(propertyName);
                if (info is not null)
                {
                    return info.GetValue(objEntity);
                }
                else
                {
                    return objEntity.ToDictionary().Get(propertyName);
                }
            }
        }

        return null;
    }
}