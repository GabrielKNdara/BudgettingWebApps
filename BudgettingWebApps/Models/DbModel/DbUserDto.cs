﻿namespace BudgettingWebApps.Models.DbModel
{
    public class DbUserDto
    {
        public int id { get; set; }
        public string username { get; set; } = string.Empty;
        public string passwordhash { get; set; } = string.Empty;
        public string role { get; set; } = string.Empty;
        public DateTime createdate { get; set; } = DateTime.Now;
    }
}