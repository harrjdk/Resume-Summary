using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Resume_Summary.Models;
using Microsoft.EntityFrameworkCore;

namespace Resume_Summary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : Controller
    {
        private readonly CandidateContext _candidateContext;
        public CandidateController(CandidateContext candidateContext)
        {
            _candidateContext = candidateContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Candidate>> Get()
        {
            return _candidateContext.Candidates.Include(c => c.Resumes).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Candidate> Get(int id)
        {
            return _candidateContext.Candidates
                .Where(c => c.CandidateId == id)
                .Include(c => c.Resumes)
                .FirstOrDefault();
        }

        [HttpPost]
        public void Post([FromBody] Candidate candidate)
        {
            _candidateContext.Add<Candidate>(candidate);
            _candidateContext.SaveChanges();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Candidate candidate)
        {
            var entity = _candidateContext.Find<Candidate>(id);
            if (entity == null)
            {
                return;
            }
            _candidateContext.Entry(entity).CurrentValues.SetValues(candidate);
            _candidateContext.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _candidateContext.Remove<Candidate>(_candidateContext.Find<Candidate>(id));
            _candidateContext.SaveChanges();
        }
    }
}