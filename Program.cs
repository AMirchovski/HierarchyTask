// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using HierarchyRefactored.Assets;
using HierarchyRefactored.Hierarchy;

namespace HierarchyRefactored;

public class Program
{
    public static List<Item> Items = new List<Item>()
    {
        new Item(1, "Monitor"),
        new Item(2, "Headset"),
        new Item(3, "Mouse"),
        new Item(4, "Keyboard"),
        new Item(5, "Phone"),
        new Item(6, "Laptop"),
        new Item(7, "Calculator"),
        new Item(8, "Hub")
    };
    public static List<Sector> Sectors = new List<Sector>()
    {
        new Sector(1, "Finance", 1),
        new Sector(2, "Human Resources", 2),
        new Sector(3, "Information Technology", 3),
        new Sector(4, "Sales", 4)
    };

    public static List<Company> Companies = new List<Company>()
    {
        new Company(1, "CodeChem", new List<int>(){1, 2, 3, 4})
    };
    public static List<Employee> Employees = new List<Employee>()
    {
        new SupervisorEmployee(1, "John", "Doe", new DateTime(1985, 5, 10), 1, new DateTime(2012, 3, 10), Currency.USD, "john.doe@example.com"),
        new SupervisorEmployee(2, "Jane", "Smith", new DateTime(1990, 9, 15), 2, new DateTime(2013, 3, 10), Currency.EUR, "jane.smith@example.com"),
        new SupervisorEmployee(3, "Michael", "Brown", new DateTime(1988, 11, 18), 3, new DateTime(2011, 3, 10), Currency.USD, "michael.brown@example.com"),
        new SupervisorEmployee(4, "Emily", "Taylor", new DateTime(1992, 4, 27), 4, new DateTime(2014, 3, 10), Currency.EUR, "emily.taylor@example.com"),
        
        new RegularEmployee(5,"Alice", "Johnson", new DateTime(1993, 3, 22), 1, new DateTime(2020, 3, 10), Currency.USD),
        new RegularEmployee(6,"Bob", "Williams", new DateTime(1991, 7, 7), 2, new DateTime(2023, 6, 20), Currency.EUR),
        new RegularEmployee(7,"David", "Lee", new DateTime(1994, 9, 12), 3, new DateTime(2018, 8, 15), Currency.USD),
        new RegularEmployee(8,"Olivia", "Clark", new DateTime(1989, 8, 9), 4, new DateTime(2016, 10, 1), Currency.EUR),
        new RegularEmployee(9,"Sophia", "Wilson", new DateTime(1995, 2, 5), 1, new DateTime(2017, 3, 3), Currency.USD),
        new RegularEmployee(10,"Ethan", "Anderson", new DateTime(1990, 12, 1), 2, new DateTime(2015, 5, 5), Currency.EUR),
        new RegularEmployee(11,"Isabella", "Martinez", new DateTime(1993, 6, 18), 3, new DateTime(2019, 1, 12), Currency.USD),
        new RegularEmployee(12,"Mason", "Gonzalez", new DateTime(1988, 3, 29), 4, new DateTime(2019, 5, 30), Currency.EUR),
        new RegularEmployee(13,"Ava", "Perez", new DateTime(1991, 1, 7), 1, new DateTime(2023, 6, 20), Currency.USD),
        new RegularEmployee(14,"Liam", "Rivera", new DateTime(1994, 10, 16), 2, new DateTime(2021, 6, 2), Currency.EUR),
        new RegularEmployee(15, "Emma", "Davis", new DateTime(1992, 6, 8), 3, new DateTime(2020, 11, 7), Currency.USD),
        new RegularEmployee(16, "James", "Wilson", new DateTime(1995, 9, 23), 4, new DateTime(2021, 7, 13), Currency.EUR)

    };
    

    public static void Main()
    {
        Console.WriteLine("Printing All Employees");
        Employees.ForEach(Console.WriteLine);
        Console.WriteLine("-----------------------------------------------------------------------------------------");
        var supervisorEmployee = (SupervisorEmployee)Employees[0];
        //Valid: Testing for employee pay per hour set
        supervisorEmployee.ChangeEmployeePayPerHour(5, 10);
        Console.WriteLine("-----------------------------------------------------------------------------------------");
        //Valid: Testing for employee working hours set
        supervisorEmployee.ChangeEmployeeWorkingHours(5, 10);
        Console.WriteLine("-----------------------------------------------------------------------------------------");
        //Setting salary parameters for all employees
        SetSalaryParametersForAll();
        
        Console.WriteLine("Testing edge cases:");
        //Invalid: Setting pay per hour on a different sector employee
        supervisorEmployee.ChangeEmployeePayPerHour(6, 10);
        Console.WriteLine("-----------------------------------------------------------------------------------------");
        //Invalid: Not existing employee test
        supervisorEmployee.ChangeEmployeePayPerHour(20, 10);
        Console.WriteLine("-----------------------------------------------------------------------------------------");
        //Invalid: Setting working hours on a different sector employee
        supervisorEmployee.ChangeEmployeeWorkingHours(6, 10);
        Console.WriteLine("-----------------------------------------------------------------------------------------");
        //Invalid: Setting working hours on a newcomer
        supervisorEmployee.ChangeEmployeeWorkingHours(13, 10);
        Console.WriteLine("-----------------------------------------------------------------------------------------");
        //Invalid: Not existing employee test
        supervisorEmployee.ChangeEmployeeWorkingHours(20, 10);
        Console.WriteLine("-----------------------------------------------------------------------------------------");
        
        //Invalid: Not existing sector test
        supervisorEmployee.ChangeEmployeeSector(5, 6);
        Console.WriteLine("-----------------------------------------------------------------------------------------");
        //Invalid: Not existing employee test 
        supervisorEmployee.ChangeEmployeeSector(21, 3);
        Console.WriteLine("-----------------------------------------------------------------------------------------");
        //Invalid: Not in the same sector test
        supervisorEmployee.ChangeEmployeeSector(6, 3);
        Console.WriteLine("-----------------------------------------------------------------------------------------");
        //Valid: Test for employee sector change
        supervisorEmployee.ChangeEmployeeSector(5, 3);
        Console.WriteLine("-----------------------------------------------------------------------------------------");
        
        Console.WriteLine("Printing All Employees");
        Employees.ForEach(Console.WriteLine);
        Console.WriteLine("-----------------------------------------------------------------------------------------");
        
        Console.WriteLine("Printing All Employees by Supervisor Employee");
        supervisorEmployee.ViewEmployees().ForEach(Console.WriteLine);
        Console.WriteLine("-----------------------------------------------------------------------------------------");
        
        Console.WriteLine("Printing All Newcomers by Supervisor employee");
        supervisorEmployee.ViewAllNewComers().ForEach(Console.WriteLine);
        
        Console.WriteLine("-----------------------------------------------------------------------------------------");
        
        //Testing regular employee methods
        var regularEmployee = (RegularEmployee)Employees[4];
        var regularEmployee9 = (RegularEmployee)Employees[8];
        var regularEmployeeBob = (RegularEmployee)Employees[5];
        //Invalid: Testing resigning with more than 3 months experience
        regularEmployee.Resign();
        Console.WriteLine("-----------------------------------------------------------------------------------------");
        //Valid: Testing resigning with less than 3 months experience
        regularEmployeeBob.Resign();
        Console.WriteLine("-----------------------------------------------------------------------------------------");
        //Invalid: Testing email send invalid emailAddress
        regularEmployee.SendEmail("foo.email@example.com", "Testing resigning with email notice");
        Console.WriteLine("-----------------------------------------------------------------------------------------");
        //Valid: Testing email send valid emailAddress
        regularEmployee.SendEmail("john.doe@example.com", "Testing resigning with email notice");
        Console.WriteLine("-----------------------------------------------------------------------------------------");
        supervisorEmployee.SetItems(new List<int>(){1, 2, 3, 4});
        supervisorEmployee.Items.ForEach(Console.WriteLine);
        regularEmployee9.TakeItem(1);
        Console.WriteLine("-----------------------------------------------------------------------------------------");
        regularEmployee9.Items.ForEach(Console.WriteLine);
        Console.WriteLine("-----------------------------------------------------------------------------------------");
        supervisorEmployee.Items.ForEach(Console.WriteLine);
        Console.WriteLine("-----------------------------------------------------------------------------------------");
        regularEmployee9.ReturnItem(1);
        Console.WriteLine("-----------------------------------------------------------------------------------------");
        regularEmployee9.Items.ForEach(Console.WriteLine);
        Console.WriteLine("-----------------------------------------------------------------------------------------");
        supervisorEmployee.Items.ForEach(Console.WriteLine);
        Console.WriteLine("-----------------------------------------------------------------------------------------");
        regularEmployee9.TakeItem(5);
        Console.WriteLine("-----------------------------------------------------------------------------------------");
        regularEmployee9.ReturnItem(6);
        Console.WriteLine("-----------------------------------------------------------------------------------------");
        regularEmployee9.RequestSickDay(new DateTime(2023, 7, 18));
        Console.WriteLine("-----------------------------------------------------------------------------------------");
        regularEmployee9.SickDays.ForEach(dt => Console.WriteLine(dt));
        Console.WriteLine("-----------------------------------------------------------------------------------------");
        
        Console.WriteLine("Printing All Employees");
        Employees.ForEach(Console.WriteLine);
        Console.WriteLine("-----------------------------------------------------------------------------------------");

        SetEmployeesInSector();

        Console.WriteLine("Printing All Employees by Sector:");
        foreach (var sector in Sectors)
        {
            Console.WriteLine($"Sector: {sector.Id}");
            sector.ViewAllEmployees();
            Console.WriteLine("-----------------------------------------------------------------------------------------");
        }
        Console.WriteLine("Printing All Emails by Supervisor:");
        supervisorEmployee.Mails.ForEach(Console.WriteLine);
    }

    public static void SetSalaryParametersForAll()
    {
        var supervisors = new List<SupervisorEmployee>()
        {
            (SupervisorEmployee)Employees[0],
            (SupervisorEmployee)Employees[1],
            (SupervisorEmployee)Employees[2],
            (SupervisorEmployee)Employees[3]
        };
        var random = new Random();
        foreach (var supervisor in supervisors)
        {
            var employees = supervisor.ViewEmployees();
            supervisor.ChangeEmployeeWorkingHours(supervisor.Id, 10);
            supervisor.ChangeEmployeePayPerHour(supervisor.Id, 10);
            foreach (var employee in employees)
            {
                var rand1 = random.Next(8, 13);
                supervisor.ChangeEmployeePayPerHour(employee.Id, rand1);
                var rand2 = random.Next(8, 13);
                supervisor.ChangeEmployeeWorkingHours(employee.Id, rand2);
            }
        }
    }

    public static void SetEmployeesInSector()
    {
        foreach (var employee in Employees)
        {
            var sector = Sectors.Find(sec => sec.Id == employee.SectorId);
            if (sector is null)
                continue;
            sector.Employees.Add(employee.Id);
        }
    }
}