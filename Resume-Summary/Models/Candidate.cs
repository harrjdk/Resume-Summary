using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Resume_Summary.Models
{

    public class CandidateContext : DbContext
    {

        public DbSet<Candidate> Candidates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(Properties.Resources.CandidateDataSource);
        }
    }

    public class Candidate
    {
        public int CandidateId { get; set; }
        public ICollection<Resume> Resumes { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CurrentRole { get; set; }
        public int YearsExperience { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
