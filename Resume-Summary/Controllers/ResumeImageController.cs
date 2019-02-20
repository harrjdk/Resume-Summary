using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Resume_Summary.Models;
using System.Drawing;
using System.IO;
using Tesseract;

namespace Resume_Summary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeImageController : Controller
    {
        private readonly CandidateContext _candidateContext;
        public ResumeImageController(CandidateContext candidateContext)
        {
            _candidateContext = candidateContext;
        }

        /// <summary>
        /// Post a candidate resume as bmp (for now)
        /// </summary>
        /// <param name="candidateId">The candidate ID</param>
        /// <param name="resumeFormat">The resume format (always bmp)</param>
        /// <param name="resumeRaw">the resume bytes</param>
        [HttpPost("{candidateId}/{resumeFormat}")]
        public void Post(int candidateId, string resumeFormat, [FromBody] byte[] resumeRaw)
        {
            Candidate candidate = _candidateContext.Candidates
                .Where(c => c.CandidateId == candidateId)
                .FirstOrDefault();
            if (candidate == null)
            {
                return;
            }

            Bitmap bmp;
            var ocrText = string.Empty;
            using (var ms = new MemoryStream(resumeRaw))
            {
                bmp = new Bitmap(ms);
                var ocr = new TesseractEngine(Properties.Resources.TesseractData, "eng", EngineMode.Default);
                using (var img = PixConverter.ToPix(bmp))
                {
                    using (var page = ocr.Process(img))
                    {
                        ocrText = page.GetText();
                    }
                }
                if (ocrText != null)
                {
                    candidate.Resumes.Add(new Resume { FirstName = candidate.FirstName, LastName = candidate.LastName, ResumeText = ocrText });
                } else
                {
                    candidate.Resumes.Add(new Resume { FirstName = candidate.FirstName, LastName = candidate.LastName, ResumeRaw = resumeRaw });
                }
                
            }

            _candidateContext.SaveChanges();
        }
    }
}