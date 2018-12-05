using System;
using System.Collections.Generic;
using System.Text;

namespace FastDeepCloner.tests.Entitys.testClasses
{
    public class OpeningHour
    {
        public int OpeningHourId { get; set; }

        public string Day { get; set; }

        public DateTime Opening { get; set; }

        public DateTime Closing { get; set; }

        public bool Closed { get; set; }


        public OpeningHour()
        {

        }
        public int PositionInList()
        {
            switch (Day.ToLower())
            {
                case "mon": return 1;
                case "tue": return 2;
                case "wed": return 3;
                case "thurs": return 4;
                case "fri": return 5;
                case "sat": return 6;
                case "sun": return 7;
                default: return 1;
            }
        }

        public string PartOne()
        {
            switch (Day.ToLower())
            {
                case "mon": return "Maandag:";
                case "tue": return "Dinsdag:";
                case "wed": return "Woensdag:";
                case "thurs": return "Donderdag:";
                case "fri": return "Vrijdag:";
                case "sat": return "Zaterdag:";
                case "sun": return "Zondag:";
                default: return "Default:";
            }
        }

        public string PartTwo()
        {
            if (Closed)
            {
                return "";
            }
            return Opening.ToString("H:mm");
        }
        public string PartThree()
        {
            if (Closed)
            {
                return "Gesloten";
            }
            return "-";
        }
        public string PartFour()
        {
            if (Closed)
            {
                return "";
            }
            return Closing.ToString("H:mm");
        }

        public override string ToString()
        {
            return String.Format(PartTwo() + PartThree() + PartFour());
        }

    }
}
