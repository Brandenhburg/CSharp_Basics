using CSharp_Basics_HW.Enums;

namespace CSharp_Basics_HW.Models
{
    internal class Task
    {
        public string TaskId { get; set; }
        public string TaskName { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime DateOfEffectuation { get; set; }
        public string Description { get; set; }
        public TaskPriority TaskPriority { get; set; }
        public bool State { get; set; }
        public bool IsAssigned { get; set; }
    }
}
