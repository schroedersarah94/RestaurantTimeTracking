﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTrackingApplication.Models;

namespace TimeTrackingApplication.Data
{
    public class DbInitializer
    {
        public static void Initialize(TimeTrackingContext context)
        {
            context.Database.EnsureCreated();

            if (context.Roles.Any())
            {
                return;
            }

            //MAIN ROLES
            context.Contacts.RemoveRange(context.Contacts);
            context.Roles.RemoveRange(context.Roles);
            context.Employees.RemoveRange(context.Employees);
            context.Statuses.RemoveRange(context.Statuses);

            var roles = new Role[]
            {
                new Role{ Name="Employee"},
                new Role{ Name="Manager"},
                new Role{ Name="Admin"}
            };

            foreach (Role role in roles)
            {
                context.Roles.Add(role);
            }
            context.SaveChanges();

            //MAIN REQUEST STATUSES
            var statuses = new Status[]
            {
                new Status{ Name="In Process"},
                new Status{ Name="Accepted"},
                new Status{ Name="Rejected"},
            };

            foreach (Status status in statuses)
            {
                context.Statuses.Add(status);
            }
            context.SaveChanges();

            //ADMIN EMPLOYEE
            var contact = new Contact { Address = "123 Road Drive", City = "cityville", State = "MI", Phone = "1234567890" };
            context.Contacts.Add(contact);

            var employee = new Employee{
                FirstName="Admin",
                LastName="Istrator",
                PTO=80,
                Role = context.Roles.Find(3), //3 = admin role
                Contact = contact,
                ClockedIn = false
            };

            context.Employees.Add(employee);

            context.SaveChanges();


        }
    }
}
