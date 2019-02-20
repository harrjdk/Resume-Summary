using Resume_Summary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume_Summary.Data
{
    public class DbInitializer
    {
        public static void Initialize(CandidateContext candidateContext)
        {
            candidateContext.Database.EnsureCreated();

            if (candidateContext.Candidates.Any())
            {
                candidateContext.RemoveRange(candidateContext.Candidates);
                candidateContext.RemoveRange(candidateContext.Resumes);
            }

            Resume resume = new Resume { FirstName = "John", LastName = "Doe", ResumeText="Test Job" };

            Candidate candidate = new Candidate { FirstName = "John", LastName = "Doe", YearsExperience = 0, City = "Atlanta", State = "GA", Resumes = new List<Resume>() };
            candidateContext.Add<Candidate>(candidate);
            candidateContext.SaveChanges();
            candidate.Resumes.Add(resume);
            candidateContext.SaveChanges();
        }
    }
}