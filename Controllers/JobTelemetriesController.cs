using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CMPG323_PROJECT2_35407972.Models;
using JWTAuthentication.Authentication;
using Microsoft.AspNetCore.Authorization;


namespace CMPG323_PROJECT2_35407972.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class JobTelemetriesController : ControllerBase
    {
        private readonly NWUTechTrendsContext _context;

        public JobTelemetriesController(NWUTechTrendsContext context)
        {
            _context = context;
        }

        //gets all telemetries data
        // GET: api/JobTelemetries
        [HttpGet] 
        public async Task<ActionResult<IEnumerable<JobTelemetry>>> GetJobTelemetries()
        {
            return await _context.JobTelemetries.ToListAsync();
        }

        //gets telemetries data based on a specific id.
        // GET: api/JobTelemetries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobTelemetry>> GetJobTelemetry(int id)
        {
            var jobTelemetry = await _context.JobTelemetries.FindAsync(id);

            if (jobTelemetry == null)
            {
                return NotFound();
            }

            return jobTelemetry;
        }

        //patch method that updates an already existing telemetry entry 
        // PATCH: api/JobTelemetries/5
        /**[HttpPatch("{id}")]
        public async Task<IActionResult> PatchJobTelemetry(int id, [FromBody] JsonPatchDocument<JobTelemetry> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var jobTelemetry = await _context.JobTelemetries.FindAsync(id);
            if (jobTelemetry == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(jobTelemetry, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobTelemetryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        */

        //this DELETE method deletes an already existing Telemtry entry
        //DELETE: api/JobTelemetries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobTelemetry(int id)
        {
            var jobTelemetry = await _context.JobTelemetries.FindAsync(id);
            if (jobTelemetry == null)
            {
                return NotFound();
            }

            _context.JobTelemetries.Remove(jobTelemetry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/JobTelemetries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobTelemetry(int id, JobTelemetry jobTelemetry)
        {
            if (id != jobTelemetry.Id)
            {
                return BadRequest();
            }

            _context.Entry(jobTelemetry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobTelemetryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //private method that checks if a telemetry exists

        private bool JobTelemetryExists(int id)
        {
            return (_context.JobTelemetries?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        //POST: api/JobTelemetries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JobTelemetry>> PostJobTelemetry(JobTelemetry jobTelemetry)
        {
            _context.JobTelemetries.Add(jobTelemetry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJobTelemetry", new { id = jobTelemetry.Id }, jobTelemetry);
        }

        [HttpGet("savings")]
        public async Task<ActionResult<SavingsResult>> GetSavingsProject(
     [FromQuery] Guid projectId, // Use Guid type
     [FromQuery] DateTime startDate,
     [FromQuery] DateTime endDate)
        {
            if (startDate > endDate)
            {
                return BadRequest("Start date must be before end date.");
            }

            var savings = await _context.JobTelemetries
                .Where(t => t.ProjectId == projectId // Ensure ProjectID is of type Guid
                            && t.EntryDate >= startDate
                            && t.EntryDate <= endDate)
                .GroupBy(t => t.ProjectId)
                .Select(g => new SavingsResult
                {
                    ProjectId = g.Key, // Ensure consistency in types
                    TotalTimeSaved = g.Sum(t => t.HumanTime ?? 0), // Sum the time saved
                    //TotalCostSaved = g.Sum(t => CalculateCostSaved(t)) // Replace with your own logic
                })
                .FirstOrDefaultAsync();

            if (savings == null)
            {
                return NotFound();
            }

            return Ok(savings);
        }

        // get savings (client)
        [HttpGet("Getsavings")]
        public async Task<ActionResult<SavingsResult>> GetSavingsClient(
    [FromQuery] Guid ClientId, // Filter by Client ID
    [FromQuery] DateTime startDate,
    [FromQuery] DateTime endDate)
        {
            if (startDate > endDate)
            {
                return BadRequest("Start date must be before end date.");
            }

            var savings = await _context.JobTelemetries
                .Where(t => t.ClientId == ClientId // Filter by Client ID
                            && t.EntryDate >= startDate
                            && t.EntryDate <= endDate)
                .GroupBy(t => t.ClientId)
                .Select(g => new SavingsResult
                {
                    ProjectId = Guid.Empty, // No direct project savings, you might adjust as needed
                    TotalTimeSaved = g.Sum(t => t.HumanTime ?? 0), // Sum the time saved
                    //TotalCostSaved = g.Sum(t => CalculateCostSaved(t)) // Replace with your own logic
                })
                .FirstOrDefaultAsync();

            if (savings == null)
            {
                return NotFound();
            }

            return Ok(savings);
        }



    }


}

