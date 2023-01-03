using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskApi.Data;
using TaskApi.Models;

namespace TaskApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        public TaskDbContext _context { get; }

        public IssueController(TaskDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Issue>> Get()
        {
            return await _context.Issues
                .Include(c => c.Assign).ToListAsync();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Issue), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var issue = await _context.Issues
                .Include(c => c.Assign).SingleOrDefaultAsync(c => c.Id == id); //.FindAsync(id);
            return issue != null ? Ok(issue) : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(Issue issue)
        {
            if (issue.Id == Guid.Empty || !Enum.IsDefined(typeof(Priority), issue.Priority)
                || !Enum.IsDefined(typeof(IssueType), issue.IssueType)
                || !Enum.IsDefined(typeof(IssueStatus), issue.IssueStatus))
            {
                return BadRequest();
            }

            await _context.AddAsync(issue);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = issue.Id }, issue);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid id, Issue issue)
        {
            if (id != issue.Id || !Enum.IsDefined(typeof(Priority), issue.Priority)
                || !Enum.IsDefined(typeof(IssueType), issue.IssueType)
                || !Enum.IsDefined(typeof(IssueStatus), issue.IssueStatus))
            {
                return BadRequest();
            }

            _context.Entry(issue).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var issue = await _context.Issues.FindAsync(id);
            if (issue == null)
            {
                return NotFound();
            }

            _context.Issues.Remove(issue);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
