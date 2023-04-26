using System.Diagnostics.CodeAnalysis;

namespace CoreBox.Extensions;

public static class DateTimeExtensions
{
    public static DateTime GetFirstDayOfMonth(this DateTime date)
        => new DateTime(date.Year, date.Month, 1);

    public static DateTime GetLastDayOfMonth(this DateTime date)
        => GetFirstDayOfMonth(date).AddMonths(1).AddDays(-1);

    public static DateTime SetMonth(this DateTime dt, int month)
    {
        if (dt.Month > month)
            return dt.AddMonths(-(dt.Month - month));
        else
            return dt.AddMonths(month - dt.Month);
    }

    public static DateTime SetYear(this DateTime dt, int year)
    {
        if (dt.Year > year)
            return dt.AddYears(-(dt.Year - year));
        else
            return dt.AddYears(year - dt.Year);
    }

    [ExcludeFromCodeCoverage]
    public static int GetAge(this DateTime dt)
    {
        var today = DateTime.UtcNow.Date;
        var age = today.Year - dt.Year;
        if (dt > today.AddYears(-age)) age--;
        return age;
    }
}