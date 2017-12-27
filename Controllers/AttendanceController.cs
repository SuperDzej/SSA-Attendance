using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SSAAttenderAPI.Data.DAL.Interface;
using SSAAttenderAPI.Data.DAL.Model;

namespace SSAAttenderAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AttendanceController : Controller
    {
        IAttendance _attendance = null;

        public AttendanceController(IAttendance attendance)
        {
            _attendance = attendance;
        }

        //This is accessed as url/api/Attendance/{username}
        [HttpGet("{userName}/{training}", Name = "InsertUserAttended")]
        public IActionResult InsertUserAttended(string userName, string training)
        {
            var attended = _attendance.insertUserAttendance(userName, DateTime.Now, true, training);
            if (attended)
                return Ok();
            else
                return NotFound();
        }

        //This is accessed as url/api/Attendance
        [HttpGet(Name = "GetAllUsersInfo")]
        public List<AttendanceAPIModel> GetAllUsersInfo()
        {
            ViewBag.Message = "The current time is " + DateTime.Now.ToString();
            return _attendance.getAllAttendanceInfo();
        }

        //This is accessed as url/api/Attendance post data
        [HttpPost(Name = "AddNewUserAttending")]
        public IActionResult AddNewUserAttending([FromBody] AttendanceAPIModel model)
        {
            var inserted = _attendance.insertNewUserAttending(model.userName);
            if (inserted)
                return Ok("Inserted successfully");
            else
                return NotFound();
        }
    }
}