using System;
using System.Collections.Generic;
using HierarchyRefactored.Assets;
using HierarchyRefactored.Utils;

namespace HierarchyRefactored.Hierarchy;

public class SupervisorEmployee : Employee
{
    public string Email { get; private set; }
    public List<Mail> Mails { get; }
    public Dictionary<int, List<int>> BorrowedItems { get; private set; }

    public SupervisorEmployee(int id, string name, string lastName, DateTime dateOfBirth, int sectorId,
        DateTime dateStartedWorking,
        Currency currency, string email) :
        base(id, name, lastName, dateOfBirth, sectorId, dateStartedWorking, currency)
    {
        Email = email;
        Mails = new List<Mail>();
        BorrowedItems = new Dictionary<int, List<int>>();
    }

    public void ChangeEmployeePayPerHour(int employeeId, double newValue)
    {
        var employee = Program.Employees.Find(emp => emp.Id == employeeId);
        if (employee is null)
        {
            Console.WriteLine($"Employee with id: {employeeId} not found");
            return;
        }

        if (employee.SectorId != SectorId)
        {
            Console.WriteLine("The employee and the supervisor are not in the same sector");
            return;
        }

        employee.ChangePayPerHour(newValue);
        Console.WriteLine("Successfully changed employee pay per hour");
    }

    public void ChangeEmployeeWorkingHours(int employeeId, int newValue)
    {
        var employee = Program.Employees.Find(emp => emp.Id == employeeId);
        if (employee is null)
        {
            Console.WriteLine($"Employee with id: {employeeId} not found");
            return;
        }

        if (employee.SectorId != SectorId)
        {
            Console.WriteLine("The employee and the supervisor are not in the same sector");
            return;
        }

        if (employee.IsNewComer())
        {
            Console.WriteLine("The employee is a newcomer so the supervisor cannot change his/her working hours");
            return;
        }

        employee.ChangeWorkingHours(newValue);
        Console.WriteLine($"Successfully changed employee working hours");

    }

    public List<Employee> ViewEmployees()
    {
        return Program.Employees.FindAll(employee => employee.SectorId == SectorId && employee != this);
    }

    public List<Employee> ViewAllNewComers()
    {
        return Program.Employees.FindAll(employee =>
            employee.SectorId == SectorId && employee.IsNewComer() && employee != this);
    }

    public void ChangeEmployeeSector(int employeeId, int newSectorId)
    {
        var newSector = Program.Sectors.Find(sec => sec.Id == newSectorId);
        if (newSector is null)
        {
            Console.WriteLine($"Sector with id: {newSectorId} not found");
            return;
        }

        var employee = Program.Employees.Find(emp => emp.Id == employeeId);
        if (employee is null)
        {
            Console.WriteLine($"Employee with id: {employeeId} not found");
            return;
        }

        if (employee.SectorId != SectorId)
        {
            Console.WriteLine("Employee and Supervisor Employee are not in the same sector");
            return;
        }

        newSector.AddNewEmployee(employeeId);
        employee.ChangeSectorId(newSectorId);
        Console.WriteLine($"Successfully changed employee sector from: {SectorId} to: {newSectorId}");
    }

    public void AddMail(Mail mail)
    {
        Mails.Add(mail);
    }

    public bool BorrowItem(int employeeId, int itemId)
    {
        if (!Items.Exists(it => it == itemId))
            return false;
        Items.Remove(itemId);
        var items = new List<int>();
        if (BorrowedItems.TryGetValue(employeeId, out var existingItems))
            items = existingItems;
        items.Add(itemId);
        BorrowedItems.Add(employeeId, items);
        Console.WriteLine("Successfully borrowed item to the employee");
        return true;
    }

    public void TakeItem(int employeeId, int itemId)
    {
        Items.Add(itemId);
        if (BorrowedItems.TryGetValue(employeeId, out var existingItems))
        {
            existingItems.Remove(itemId);
            BorrowedItems[employeeId] = existingItems;
            Console.WriteLine("Successfully taken back item from the employee");
        }
        else
        {
            Console.WriteLine("The supervisor cannot take the item");
        }
        
    }

    public void SetItems(List<int> itemsIds)
    {
        Items = itemsIds;
    }

    public override string ToString()
    {
        {
            var yearsOfExperience = DateCalculations.CalculateYearsRangeBetweenDates(DateStartedWorking, DateTime.Now);
            var monthsOfExperience =
                DateCalculations.CalculateMonthsRangeBetweenDates(DateStartedWorking, DateTime.Now);
            return
                $"Supervisor Employee with id: {Id}, name: {Name}, last name: {LastName}, salary: {CalculateSalary()}, " +
                $"years of experience {yearsOfExperience}, months of experience {monthsOfExperience}, sector: {SectorId}";
        }
    }
}