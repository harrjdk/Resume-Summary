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
                return;
            }

            Candidate candidate = new Candidate { FirstName = "John", LastName = "Doe", YearsExperience = 0, City = "Atlanta", State = "GA" };
            candidateContext.Add<Candidate>(candidate);
            candidateContext.SaveChanges();
        }
    }
}