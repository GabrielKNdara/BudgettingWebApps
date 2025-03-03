﻿namespace BudgettingWebApps.Models.DbModel
{
    public class DbUserDto
    {
        public int id { get; set; }
        public string username { get; set; } = string.Empty;
        public string firstname { get; set; } = string.Empty;
        public string lastname { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string passwordhash { get; set; } = string.Empty;
        public bool active { get; set; }
        public string role { get; set; } = string.Empty;
        public DateTime createdate { get; set; } = DateTime.Now;
    }
}
