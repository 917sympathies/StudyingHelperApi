﻿namespace StudyingHelperApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public virtual IEnumerable<Workspace> Workspaces { get; set; } = new List<Workspace>();
    }
}
