using SecureApi.Models;

namespace SecureApi.Data;

public static class DbSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (context.Employees.Any()) return;

        context.Employees.AddRange(
            new Employee { Name = "John Doe", Role = "Developer", Department = "IT" },
            new Employee { Name = "Sara Smith", Role = "Manager", Department = "HR" }
        );

        context.SaveChanges();
    }
}