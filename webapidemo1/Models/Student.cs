namespace webapidemo1.Models
{
    public class Student
    {
        public int StudentId { get; set; }  // PK
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public StudentAddress Address { get; set; }
    }
}
