namespace StudyingHelperApi.DataTransferObjects
{
    public class TaskToCreateDto
    {
        public string Name { get; set; }
        public string State { get; set; }
        public DateOnly Deadline { get; set; }
    }
}
