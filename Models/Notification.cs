﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWProject.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public bool IsSent { get; set; }

        public Notification(int id, string message)
        {
            Id = id;
            Message = message;
            IsSent = false;
        }
    }
}
