namespace StudyingHelperApi.Models
{
    public enum TaskState
    {
        Acutal,
        Done
    }
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
    }
}
