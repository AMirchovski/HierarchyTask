using System;
using System.Collections.Generic;
using HierarchyRefactored.Assets;
using HierarchyRefactored.Utils;

namespace HierarchyRefactored.Hierarchy;

public abstract class Employee
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string LastName { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public DateTime DateStartedWorking { get; private set; }
    public int SectorId { get; private set; }
    public int WorkingHours { get; protected set; }
    public double PayPerHour { get; protected set; }
    public double Salary { get; private set; }
    public Currency Currency { get; private set; }
    public List<int> Items { get; protected set; }
    public List<DateTime> SickDays { get; private set; }
    
    public Employee(int id, string name, string lastName, DateTime dateOfBirth, int sectorId, DateTime dateStartedWorking, 
        Currency currency)
    {
        Id = id;
        Name = name;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        DateStartedWorking = dateStartedWorking;
        SectorId = sectorId;
        Currency = currency;
        WorkingHours = 8;
        Items = new List<int>();
        SickDays = new List<DateTime>();
    }
    public void ChangePayPerHour(double newValue)
    {
        PayPerHour = newValue;
    }
    
    public void ChangeWorkingHours(int newValue)
    {
        WorkingHours = newValue;
    }

    public void ChangeSectorId(int newSectorId)
    {
        SectorId = newSectorId;
    }
    public void RequestSickDay(DateTime sickDay)
    {
        SickDays.Add(sickDay);
        Console.WriteLine("Successfully requested sick day");
    }

    public double? CalculateSalary()
    {
        var sector = Program.Sectors.Find(sec => sec.Id == SectorId);
        if (sector is null)
        {
            Console.WriteLine("Sector not found");
            return null;
        }
        var company = Program.Companies.Find(comp => comp.SectorsIds.Contains(SectorId));

        if (company is null)
        {
            Console.WriteLine("Company not found");
            return null;
        }
        
        Salary = SSalary.CalculateSalary(PayPerHour, WorkingHours, DateStartedWorking, SickDays, company);
        return Salary;
        
    }
    public bool IsNewComer()
    {
        int monthsWorked = DateCalculations.CalculateMonthsRangeBetweenDates(DateStartedWorking, DateTime.Now);
        return monthsWorked < 3;
    }
}