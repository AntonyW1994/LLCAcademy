using System;

namespace LLCAcademy.Models
{
    public class SoccerSchool
    {
        public int ID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int StartHour { get; set; }
        public int EndHour { get; set; }
        public string SpecialText { get; set; }
        public decimal CostPerDay { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public int AddressID { get; set; }
        public string Name { get; set; }
        public string CustomClass { get; set; }
    }
}