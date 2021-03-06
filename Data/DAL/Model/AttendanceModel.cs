﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SSAAttenderAPI.Data.DAL.Model
{
    public class AttendanceModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string userName { get; set; }
        public DateTime dayAttended { get; set; }
        public bool dayDidAttend { get; set; }
        public string trainingAttended { get; set; }
    }
}
