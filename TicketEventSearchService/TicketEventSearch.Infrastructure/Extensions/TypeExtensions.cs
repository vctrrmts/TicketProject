﻿namespace TicketEventSearch.Infrastructure.Extensions;

public static class TypeExtensions
{
    /// <summary>
    /// Returns the type name. If this is a generic type, appends
    /// the list of generic type arguments between angle brackets.
    /// (Does not account for embedded / inner generic arguments.)
    /// </summary>
    public static string GetFormattedName(this Type type)
    {
        if (type.IsGenericType)
        {
            string genericArguments = type.GetGenericArguments()
                                .Select(x => x.Name)
                                .Aggregate((x1, x2) => $"{x1}, {x2}");
            return $"{type.Name.Substring(0, type.Name.IndexOf("`"))}"
                 + $"<{genericArguments}>";
        }
        return type.Name;
    }
}
