using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SutdManagmentSysAPI.Models;


namespace SutdManagmentSysAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly StudentsDbContext _res;
        public ApiController(StudentsDbContext context)
        {
            _res = context;
        } 

        [HttpGet]  // api/api reults in select all
        public async Task<ActionResult<IEnumerable<Tblstud>>> GetStud()
        {
            if (_res.Tblstuds == null)
            {
                return NotFound(); // 404 not found error
            }
            return await _res.Tblstuds.ToListAsync(); // returns the list of rows of tblstud
        }

        [HttpGet("{id}")] //api/api/2 results in select where sid=2
        public async Task<ActionResult<Tblstud>> GetStud(int id)
        {
            if (_res.Tblstuds == null)
            {
                return NotFound();
            }
            var test = await _res.Tblstuds.FindAsync(id);
            if(test == null)
            {
                return NotFound();
            }
            return test;
        }

        //[HttpGet("{str}")] // api/Api/21dcs results in select where rno like 21dcs
        [HttpGet("linq/{str}")]
        public ActionResult<int> GetStudlikeRno(string str)
        {
            if(_res.Tblstuds == null)
            {
                return NotFound();
            }
            
            var qry1 = _res.Tblstuds.Count( t => EF.Functions.Like(t.Name, str+'%'));
           
            if(qry1 == null)
            {
                return NotFound();
            }
            return Ok(qry1);
        }

        [HttpPut("{id}")] // api/api/3 results in update request to server
        public async Task<ActionResult<Tblstud>> PutStud(int id, Tblstud stud)
        {
            if(id!= stud.Sid)
            {
                return BadRequest();
            }
            _res.Entry(stud).State = EntityState.Modified; // for changing state of entry to modified

            try
            {
                await _res.SaveChangesAsync();
            }
            catch (Exception )
            {
                if( !studExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw; //if stud does not exit, it will return notfound else it will throw an exception
                }
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Tblstud>> PostStud(Tblstud stud)
        {
            if(_res.Tblstuds == null)
            {
                return Problem("The given request is null and cannot be added");
            }
            _res.Tblstuds.Add(stud);
            try
            {
                await _res.SaveChangesAsync();
            }
            catch (Exception ) 
            {
                if (studExists(stud.Sid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetStud", new { id = stud.Sid }, stud);

        }

        [HttpDelete("{id}")] //deletes the id row
        public async Task<ActionResult<Tblstud>> DeleteStud(int id)
        {
            if(_res.Tblstuds == null)
            {
                return NotFound();
            }
            var stud = await _res.Tblstuds.FindAsync(id);
            if(stud == null )
            {
                return NotFound();
            }
            _res.Tblstuds.Remove(stud);
            await _res.SaveChangesAsync();
            return NoContent();
        }

        private bool studExists(int id)
        {
            return (_res.Tblstuds?.Any(e => e.Sid == id)).GetValueOrDefault();
        }


        // FOR TBLCOURSES
        // ALL METHODS
        // route = api/api/course
        [HttpGet("course")]
        public async Task<ActionResult<IEnumerable<Tblcourse>>> GetCourses()
        {
            if(_res.Tblcourses == null)
            {
                return NotFound();
            }

            return await _res.Tblcourses.ToListAsync();
        }

        [HttpGet("course/{id}")]
        public async Task<ActionResult<Tblcourse>> GetCoursebyID(int id)
        {
            if(_res.Tblcourses == null)
            { 
                return NotFound(); 
            }

            var course =  await _res.Tblcourses.FindAsync(id);

            if(course == null )
            {
                return NotFound();
            }
            return course;
        }

        [HttpPost("course")]
        public async Task<ActionResult<Tblcourse>> PostCourse(Tblcourse cs)
        {
            if(_res.Tblcourses == null)
            {
                return Problem("TblCourse is null ");
            }
            _res.Tblcourses.Add(cs);
            try
            {
                await _res.SaveChangesAsync();
            }
            catch (Exception) {
                if (courseExists(cs.Cid))
                    return Conflict();
                else
                    throw;
            }
            return CreatedAtAction("GetCourses", new { id = cs.Cid }, cs);
        }


        private bool courseExists(int id)
        {
            return (_res.Tblcourses?.Any(e => e.Cid == id)).GetValueOrDefault();
        }



    }
}
