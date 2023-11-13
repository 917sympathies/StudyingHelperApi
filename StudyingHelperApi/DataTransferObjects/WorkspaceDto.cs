﻿namespace StudyingHelperApi.DataTransferObjects
{
    public class WorkspaceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<Task> Tasks { get; set; } = new List<Task>();
    }
}
