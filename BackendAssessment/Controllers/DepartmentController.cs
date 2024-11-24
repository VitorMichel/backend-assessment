using BackendAssessment.Data;
using BackendAssessment.DTOs;
using BackendAssessment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Department
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDTO>>> GetAllDepartments()
        {
            var departments = await _context.Departments.ToListAsync();

            var departmentDTOs = departments.Select(d => new DepartmentDTO
            {
                Id = d.Id,
                Name = d.Name
            }).ToList();

            return Ok(departmentDTOs);
        }

        #region Other methods that were not asked for.
        //// GET: api/Department/{id}
        //[HttpGet("{id}")]
        //public async Task<ActionResult<DepartmentDTO>> GetDepartmentById(int id)
        //{
        //    var department = await _context.Departments.FindAsync(id);

        //    if (department == null)
        //    {
        //        return NotFound();
        //    }

        //    var departmentDTO = new DepartmentDTO
        //    {
        //        Id = department.Id,
        //        Name = department.Name
        //    };

        //    return Ok(departmentDTO);
        //}

        //// POST: api/Department
        //[HttpPost]
        //public async Task<ActionResult<DepartmentDTO>> CreateDepartment(DepartmentDTO departmentDTO)
        //{
        //    var department = new Department
        //    {
        //        Name = departmentDTO.Name
        //    };

        //    _context.Departments.Add(department);
        //    await _context.SaveChangesAsync();

        //    departmentDTO.Id = department.Id;

        //    return CreatedAtAction("GetDepartmentById", new { id = departmentDTO.Id }, departmentDTO);
        //}

        //// PUT: api/Department/{id}
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateDepartment(int id, DepartmentDTO departmentDTO)
        //{
        //    if (id != departmentDTO.Id)
        //    {
        //        return BadRequest();
        //    }

        //    var department = await _context.Departments.FindAsync(id);
        //    if (department == null)
        //    {
        //        return NotFound();
        //    }

        //    department.Name = departmentDTO.Name;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!DepartmentExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// DELETE: api/Department/{id}
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteDepartment(int id)
        //{
        //    var department = await _context.Departments.FindAsync(id);
        //    if (department == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Departments.Remove(department);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}
        #endregion
    }
}
