using System;
using Microsoft.EntityFrameworkCore;

namespace Resume_Summary.Models
{
    public class ResumeContext : DbContext
    {

        public DbSet<Resume> Resumes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(Properties.Resources.ResumesDataSource);
        }
    }
   

    public class Resume
    {
        public int ResumeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ResumeText { get; set; }
        public byte[] ResumeRaw { get; set; }
        public DateTime LastModTime { get; set; }
    }
}
