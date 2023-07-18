using System;
using System.Linq;
using HierarchyRefactored.Assets;
using HierarchyRefactored.Utils;

namespace HierarchyRefactored.Hierarchy;

public class RegularEmployee : Employee
{
    public RegularEmployee(int id, string name, string lastName, DateTime dateOfBirth, int sectorId, DateTime dateStartedWorking, 
        Currency currency) : 
        base(id, name, lastName, dateOfBirth, sectorId, dateStartedWorking, currency)
    {
    }
    public void Resign()
    {
        var monthsWorked = DateCalculations.CalculateMonthsRangeBetweenDates(DateStartedWorking, DateTime.Now);
        if (monthsWorked < 3 && RemoveEmployee())
            Console.WriteLine($"Employee with id: {Id} has resigned");
        else if (monthsWorked >= 3)
            Console.WriteLine("You need to send one week notice to your supervisor");
    }

    public void SendEmail(string emailAddress, string emailText)
    {
        var supervisors = Program.Employees.Where(employee => employee.GetType() == typeof(SupervisorEmployee)).Cast<SupervisorEmployee>().ToList();
        var supervisor = supervisors.Find(employee => employee.Email.Equals(emailAddress));
        if (supervisor is null)
        {
            Console.WriteLine($"Supervisor with email {emailAddress} not found");
            return;
        }

        if (!RemoveEmployee()) return;
        Console.WriteLine($"Email successfully sent to supervisor: {emailAddress}\n{emailText}");
        var mail = new Mail(emailText, Id, supervisor.Id);
        supervisor.AddMail(mail);
    }
    public void TakeItem(int itemId)
    {
        if (!Program.Items.Exists(it => it.Id == itemId))
        {
            Console.WriteLine($"Item with id {itemId} not found");
            return;
        }
        var sector = Program.Sectors.Find(sec => sec.Id == SectorId);
        if (sector is null)
        {
            Console.WriteLine($"Sector with id: {SectorId} not found");
            return;
        }
        var supervisor = Program.Employees.Find(employee => employee.Id == sector.SupervisorEmployeeId) as SupervisorEmployee;
        if (supervisor is not null && supervisor.BorrowItem(Id, itemId))
            Items.Add(itemId);
        else
            Console.WriteLine("Supervisor employee or item in his inventory not found");
    }
    
    public void ReturnItem(int itemId)
    {
        if (!Program.Items.Exists(it => it.Id == itemId))
        {
            Console.WriteLine($"The item with id {itemId} does not exist");
            return;
        }
        if (!Items.Exists(i => i == itemId))
        {
            Console.WriteLine("You can't return item you haven't borrowed");
            return;
        }
        
        var sector = Program.Sectors.Find(sec => sec.Id == SectorId);
        if (sector is null)
        {
            Console.WriteLine($"Sector with id: {SectorId} not found");
            return;
        }

        var supervisor =
            Program.Employees.Find(employee => employee.Id == sector.SupervisorEmployeeId) as SupervisorEmployee;
        if (supervisor is null)
        {
            Console.WriteLine($"Supervisor with id {sector.SupervisorEmployeeId} not found"); 
            return;
        }
        Items.Remove(itemId);
        supervisor.TakeItem(Id, itemId);
    }
    private bool RemoveEmployee()
    {
        var sector = Program.Sectors.Find(sec => sec.Id == SectorId);
        if (sector == null)
        {
            Console.WriteLine("The sector does not exists");
            return false;
        }
        sector.Employees.Remove(Id);
        return true;
    }

    public override string ToString()
    {
        var yearsOfExperience = DateCalculations.CalculateYearsRangeBetweenDates(DateStartedWorking, DateTime.Now);
        var monthsOfExperience = DateCalculations.CalculateMonthsRangeBetweenDates(DateStartedWorking, DateTime.Now);
        return $"Regular Employee with id: {Id}, name: {Name}, last name: {LastName}, salary: {CalculateSalary()}, " +
               $"years of experience {yearsOfExperience}, months of experience {monthsOfExperience}, sector: {SectorId}";
    }
}