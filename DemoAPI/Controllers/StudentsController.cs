using DemoAPI.Data;
using DemoAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private ApplicationDbContext _db;
        public StudentsController(ApplicationDbContext context)
        {
            _db = context;
                
        }
        [HttpGet]
        public List<StudentEntity> GetAllStudents()
        {
            return _db.StudentRegister.ToList();
        }
        [HttpGet("GetStudentsById")]
        public ActionResult<StudentEntity> GetStudentDetails(Int32 Id) 
        {
            if(Id == 0 )
            {
                return BadRequest();
            }


            var StudentDetails = _db.StudentRegister.FirstOrDefault(x => x.Id == Id);

            if (StudentDetails == null)
            {
                return NotFound();
            }


            return StudentDetails;

        }
        [HttpPost]
        public ActionResult<StudentEntity> AddStudent([FromBody]StudentEntity StudentDetails) 
        {
        if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
        _db.StudentRegister.Add(StudentDetails);
        _db.SaveChanges();
        return Ok(StudentDetails);
        }
        [HttpPost("UpdateStudentDetails")]
        public ActionResult<StudentEntity> UpdateStudent(Int32 Id, [FromBody] StudentEntity studendetails)
        {
            if (studendetails == null)
            {
                return BadRequest();       
            }       
            var StudentDetails = _db.StudentRegister.FirstOrDefault(x => x.Id == Id);
            if(StudentDetails == null)
            {
                return NotFound();
            }

            StudentDetails.Name= StudentDetails.Name;
            StudentDetails.Age = StudentDetails.Age;
            StudentDetails.Standard= StudentDetails.Standard;
            StudentDetails.EmailAddress= StudentDetails.EmailAddress;
            _db.SaveChanges();
            return Ok(StudentDetails);
        }
    }
}
