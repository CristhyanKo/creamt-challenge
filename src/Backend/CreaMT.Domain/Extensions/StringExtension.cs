using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace CreaMT.Domain.Extensions;
public static class StringExtension
{
    public static bool NotEmpty([NotNullWhen(true)] this string? value) => string.IsNullOrWhiteSpace(value).IsFalse();

    public static string RemoveMascara(this string value) => Regex.Replace(value, "[^0-9]", "");

}
