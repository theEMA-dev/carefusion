namespace Carefusion.Core.Utilities;

using System;
using System.ComponentModel;
using System.Reflection;

public static class EnumExtensions
{
    public static string ReadDesc(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        if (field == null) return value.ToString();
        var attribute = field.GetCustomAttribute<DescriptionAttribute>();

        return attribute == null ? value.ToString() : attribute.Description;
    }
}