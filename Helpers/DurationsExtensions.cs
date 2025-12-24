using TALENTSPHERE.Models;

namespace TALENTSPHERE.Helpers
{
    public static class DurationsExtensions
    {
        public static string ToRussianString(this Durations duration)
        {
            switch (duration)
            {
                case Durations.day1:
                    return "1 день";
                case Durations.days2:
                    return "2 дня";
                case Durations.days3:
                    return "3 дня";
                case Durations.days5:
                    return "5 дней";
                case Durations.week1:
                    return "1 неделя";
                case Durations.days10:
                    return "10 дней";
                case Durations.weeks2:
                    return "2 недели";
                case Durations.weeks3:
                    return "3 недели";
                case Durations.month1:
                    return "1 месяц";
                case Durations.month2:
                    return "2 месяца";
                case Durations.more_than_3_months:
                    return "Больше 3 месяцев";
                default:
                    return string.Empty;
            }
        }
    }
}
