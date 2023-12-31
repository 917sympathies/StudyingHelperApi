﻿using StudyingHelperApi.Models;

namespace StudyingHelperApi.DataTransferObjects
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public virtual ICollection<Workspace> Workspaces { get; set; } = new List<Workspace>();
    }
}
