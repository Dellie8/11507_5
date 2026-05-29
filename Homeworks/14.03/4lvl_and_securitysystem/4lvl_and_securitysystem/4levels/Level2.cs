using System;
using System.Collections.Generic;

namespace Levels
{
    class Employee
    {
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public int Experience { get; set; }
    }

    class EmployeeManager
    {
        public event Action<Employee> OnEmployeeFound;

        public void FilterEmployees(
            List<Employee> employees,
            Predicate<Employee> predicate)
        {
            foreach (var employee in employees)
            {
                if (predicate(employee))
                {
                    OnEmployeeFound?.Invoke(employee);
                }
            }
        }
    }
    
}