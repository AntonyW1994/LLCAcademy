using System;

namespace LLCAcademy.Models
{
    public class Session
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int StartHour { get; set; }

        public int EndHour { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public double Price { get; set; }

        public int SpecialPrice { get; set; }

        public int DayOfWeek { get; set; }

        public int AddressID { get; set; }
    }
}