using System;
using System.Collections.Generic;

namespace HierarchyRefactored.Hierarchy;

public class Sector
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public List<int> Employees { get; private set; }
    public int SupervisorEmployeeId { get; private set; }

    public Sector(int id, string name, int supervisorEmployeeId)
    {
        Id = id;
        Name = name;
        SupervisorEmployeeId = supervisorEmployeeId;
        Employees = new List<int>();
    }

    public void AddNewEmployee(int employeeId)
    {
        Employees.Add(employeeId);
    }

    public void ViewAllEmployees()
    {
        List<Employee> employees = new List<Employee>();
        foreach (var employeeId in Employees)
        {
            var employee = Program.Employees.Find(emp => emp.Id == employeeId);
            if (employee is null)
                continue;
            Console.WriteLine(employee);
        }
    }
}