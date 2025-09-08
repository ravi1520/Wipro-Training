using Microsoft.EntityFrameworkCore;
using webapidemo1;
using webapidemo1.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EFCodeFirstContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<EFCodeFirstContext>();

    var studentAddress = new StudentAddress
    {
        AddressLine1 = "Text1",
        AddressLine2 = "Text2",
        City = "Bhubaneswar",
        State = "Odisha",
        Country = "India",
        PinCode = 751001
    };

    var student = new Student
    {
        FirstName = "Pranaya",
        LastName = "Rout",
        Address = studentAddress
    };

    context.Students.Add(student);
    context.SaveChanges();

    Console.WriteLine("Student Added");
}

app.Run();
