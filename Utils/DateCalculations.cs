using System;

namespace HierarchyRefactored.Utils;

public static class DateCalculations
{
    public static int CalculateMonthsRangeBetweenDates(DateTime from, DateTime to)
    {
        int months = (to.Year - from.Year) * 12;
        months += to.Month - from.Month;
        if (to.Day < from.Day)
            months--;
        return months;
    }
    
    public static int CalculateYearsRangeBetweenDates(DateTime from, DateTime to)
    {
        int years = to.Year - from.Year;
        if (to.Month < from.Month)
            years--;
        else if (to.Month == from.Month)
        {
            if (to.Day < from.Day)
                years--;
        }
        return years;
    }
}