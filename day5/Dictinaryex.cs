using System;
using System.Collections.Generic;

public class Student
{
    public int ID { get; set; }
    public string Name { get; set; }
    public Dictionary<string, int> SubjectMarks { get; set; }

    // Constructor with null check
    public Student(int id, string name, Dictionary<string, int> subjectMarks)
    {
        ID = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        SubjectMarks = subjectMarks ?? throw new ArgumentNullException(nameof(subjectMarks));
    }

    // Method to calculate average marks
    public double GetAverage()
    {
        if (SubjectMarks.Count == 0)
            return 0;

        int total = 0;
        foreach (var marks in SubjectMarks.Values)
        {
            total += marks;
        }

        return (double)total / SubjectMarks.Count;
    }

    // Method to display student details
    public void Display()
    {
        Console.WriteLine("Student ID: " + ID);
        Console.WriteLine("Student Name: " + Name);
        Console.WriteLine("Subject Marks:");

        foreach (var subject in SubjectMarks)
        {
            Console.WriteLine($"{subject.Key}: {subject.Value}");
        }

        Console.WriteLine("Average Marks: " + GetAverage());
    }
}
