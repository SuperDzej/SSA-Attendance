using SSAAttenderAPI.Data.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using SSAAttenderAPI.Data.DAL.Model;

namespace SSAAttenderAPI.Data.DAL.AttendanceRepository
{
    public class AttendanceRepository:IAttendance
    {
        private ApplicationDbContext context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public bool checkIfUserAttended(string username, DateTime dayAttended)
        {
            try
            {
                var user = context.Attendance
                    .Where(r => r.userName.Equals(username))
                    .FirstOrDefault();
                if (user == null)
                    return false;
                else
                    return true;
            }catch(Exception ex)
            {
                Console.Write(ex);
                return false;
            }
        }

        public List<AttendanceAPIModel> getAllAttendanceInfo()
        {
            try
            {
                var attendancesUserName = context.UserAttending
                    .Select(r => r.userName)
                    .Distinct()
                    .ToList();

                List<AttendanceAPIModel> returnAttends = new List<AttendanceAPIModel>();

                foreach (var attendUser in attendancesUserName)
                {
                    var allAttendanceForUser = context.Attendance
                        .Where(r => r.userName.Equals(attendUser))
                        .ToList();

                    List<long> userTimes = allAttendanceForUser
                        .OrderBy(x => x.dayAttended)
                        .Select(x => (long)(x.dayAttended.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds)
                        .ToList();
                    List<bool> userAttended = allAttendanceForUser
                        .OrderBy(x => x.dayAttended)
                        .Select(x => x.dayDidAttend)
                        .ToList();
                    List<string> userTraining = allAttendanceForUser
                        .OrderBy(x => x.dayAttended)
                        .Select(x => x.trainingAttended)
                        .ToList();
                    
                    returnAttends.Add(new AttendanceAPIModel
                    {
                        daysAttended = userTimes,
                        daysDidAttend = userAttended,
                        userName = attendUser,
                        numberOfAttendance = allAttendanceForUser.Count,
                        trainingAttended = userTraining
                    });
                }

                return returnAttends;
            }
            catch(Exception ex)
            {
                Console.Write(ex);
                return null;
            }
        }

        public bool insertUserAttendance(string userName, DateTime timeAdded, bool didAttend, string training)
        {
            try
            {
                var loc = context.Attendance
                    .Where(r => r.userName.Equals(userName)
                     && r.dayAttended.Day == timeAdded.Day
                     && r.dayAttended.Month == timeAdded.Month)
                    .ToList();

                if (loc == null || loc.Count < 4)
                {
                    if (loc != null)
                    {
                        for (int i = 0; i < loc.Count; i++)
                            if (training.Equals(loc.ElementAt(i).trainingAttended))
                                return false;
                    }

                    var ad = new AttendanceModel()
                    {
                        userName = userName,
                        dayAttended = timeAdded,
                        dayDidAttend = didAttend,
                        trainingAttended = training
                    };

                    context.Attendance.Add(ad);
                    context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                Console.Write(ex);
                return false;
            }
        }

        public bool insertNewUserAttending(string userName)
        {
            try
            {
                if (userName.Contains(","))
                {
                    string[] userNames = userName.Split(',');

                    foreach (var user in userNames)
                    {
                        insertUser(user);
                    }
                    return true;
                }
                else
                {
                    if (userName.Equals(""))
                        return false;

                    return insertUser(userName);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return false;
            }
        }

        private bool insertUser(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return false;

            var loc = context.UserAttending
                .Where(r => r.userName.Equals(userName))
                .FirstOrDefault();
         
            if (loc == null)
            {
                var ad = new UserModel()
                {
                    userName = userName
                };

                context.UserAttending.Add(ad);
                context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
