using System.ComponentModel;

namespace CoreBox.Extensions;

public static class EnumExtensions
{
    public static string GetDescription<T>(this T enumValue)
    {
        var description = enumValue.ToString();
        var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

        if (fieldInfo is not null)
        {
            var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
            
            if (attrs != null && attrs.Length > 0)
                description = ((DescriptionAttribute)attrs[0]).Description;
        }

        return description;
    }

    public static List<string> GetDescriptions(this Type type)
    {
        if (!type.IsEnum)
            return null;

        List<string> descriptions = new();

        foreach (var enumItem in Enum.GetValues(type))
            descriptions.Add(GetDescription(enumItem));

        return descriptions;
    }
}