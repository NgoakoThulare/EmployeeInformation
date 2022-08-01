using static Model.Enums;

namespace Model
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public Address Address { get; set; }
        public Project Project { get; set; }
        public string ReportingLine { get; set; }
        public string BusinessRole { get; set; }
        public string Image { get; set; }
    }
}
