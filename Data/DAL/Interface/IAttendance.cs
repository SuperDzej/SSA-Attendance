using SSAAttenderAPI.Data.DAL.Model;
using System;
using System.Collections.Generic;

namespace SSAAttenderAPI.Data.DAL.Interface
{
    public interface IAttendance
    {
        List<AttendanceAPIModel> getAllAttendanceInfo();
        bool insertUserAttendance(string userName, DateTime timeAdded, bool didAttend, string training);
        bool checkIfUserAttended(string username, DateTime dayAttended);
        bool insertNewUserAttending(string userName);
    }
}
