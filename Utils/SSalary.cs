using System;
using System.Collections.Generic;
using HierarchyRefactored.Hierarchy;

namespace HierarchyRefactored.Utils;

public static class SSalary
{
    public static double CalculateSalary(double payPerHour, int workingHours, DateTime dateStartedWorking, 
        List<DateTime> sickDays, Company company)
    {
        double salary = payPerHour * workingHours * 30;
        return salary + salary * CalculateExperienceBonus(dateStartedWorking, company.LowExperienceBonus, company.HighExperienceBonus) +
               salary * CalculateSickDayBonus(sickDays, company.LowSickDaysBonus, company.HighSickDaysBonus);
    }
    
    private static double CalculateExperienceBonus(DateTime dateStartedWorking, double lowExperienceBonus, double highExperienceBonus)
    {
        int monthsWorked = DateCalculations.CalculateMonthsRangeBetweenDates(dateStartedWorking, DateTime.Now);
        if (monthsWorked > 36)
        {
            return highExperienceBonus;
        }
        if (monthsWorked > 18)
        {
            return lowExperienceBonus;
        }
        return 0;
    }
    private static double CalculateSickDayBonus(List<DateTime> sickDays, double lowSickDaysBonus, 
        double highSickDaysBonus)
    {
        int numSickDays = 0;
        foreach (var sickDay in sickDays)
        {
            int sickDayMonthsRange = DateCalculations.CalculateMonthsRangeBetweenDates(sickDay, DateTime.Now);
            if (sickDayMonthsRange < 3)
                numSickDays++;
        }

        if (numSickDays < 3)
            return lowSickDaysBonus;
        if (numSickDays > 12)
            return highSickDaysBonus;
        return 0;
    }
}