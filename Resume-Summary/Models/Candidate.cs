using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Resume_Summary.Models
{

    public class CandidateContext : DbContext
    {

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Resume> Resumes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(Properties.Resources.CandidateDataSource);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidate>()
                .HasMany(c => c.Resumes);
        }
    }

    public class Candidate
    {
        public int CandidateId { get; set; }
        public List<Resume> Resumes { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CurrentRole { get; set; }
        public int YearsExperience { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
