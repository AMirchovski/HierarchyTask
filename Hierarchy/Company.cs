using System.Collections.Generic;

namespace HierarchyRefactored.Hierarchy;

public class Company
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public List<int> SectorsIds { get; private set; }
    public double LowSickDaysBonus { get; private set; } = 0.02;
    public double HighSickDaysBonus { get; private set; } = -0.02;
    public double LowExperienceBonus { get; private set; } = 0.1;
    public double HighExperienceBonus { get; private set; } = 0.15;

    public Company(int id, string name, List<int> sectorsIds)
    {
        Id = id;
        Name = name;
        SectorsIds = sectorsIds;
    }

    public void SetNewLowSickDaysBonus(double newValue)
    {
        LowSickDaysBonus = newValue;
    }
    
    public void SetHighLowSickDaysBonus(double newValue)
    {
        HighSickDaysBonus = newValue;
    }
    
    public void SetLowExperienceBonus(double newValue)
    {
        LowExperienceBonus = newValue;
    }
    
    public void SetHighExperienceBonus(double newValue)
    {
        HighExperienceBonus = newValue;
    }
}