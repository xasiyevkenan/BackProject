﻿using System;
namespace BackProject.Data
{
    public class RequestEmail
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
