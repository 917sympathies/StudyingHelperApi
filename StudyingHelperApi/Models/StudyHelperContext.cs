using Microsoft.EntityFrameworkCore;

namespace StudyingHelperApi.Models
{
    public class StudyHelperContext : DbContext
    {
        public StudyHelperContext(DbContextOptions options) : base(options)
        {
        }
    }
}
