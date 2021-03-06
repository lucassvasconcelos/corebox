namespace CoreBox.Extensions;

public static class DateTimeExtensions
{
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
}