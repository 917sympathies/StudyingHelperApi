namespace StudyingHelperApi.Models
{
    public class Workspace
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Task>? Tasks { get; set; }
    }
}
