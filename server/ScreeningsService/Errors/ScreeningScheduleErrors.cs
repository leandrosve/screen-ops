namespace ScreeningsService.Errors
{
    public class ScreeningScheduleErrors
    {
        public static class Create
        {
            public const string MovieIdRequired = "movie_id_required";
            public const string MovieNotFound = "movie_not_found";

            public const string RoomIdRequired = "room_id_required";
            public const string RoomOccupied = "room_occupied:{date}-{start_time}-{end_time}";

            public const string StartDateRequired = "start_date_required";
            public const string StartDateBustBeTodayOrFuture = "start_date_must_be_today_or_future";

            public const string EndDateRequired = "start_date_required";
            public const string EndDateBustBeAfterStartDate = "end_date_must_be_after_start_date";

            public const string TimesRequired = "times_required";
            public const string TimesOverlap = "times_overlap";

            public const string StartTimeRequired = "start_time_required";
            public const string StartTimeInvalid = "start_time_invalid";
            public const string StartTimeMustBeBeforeEndTime = "start_time_must_be_before_end_time";

            public const string EndTimeRequired = "end_time_required";
            public const string EndTimeInvalid = "end_time_invalid";
            public const string EndTimeBeforeMovieEnds = "end_time_must_be_after_movie_ends";


            public const string DaysOfWeekRequired = "days_of_week_required";
            public const string DayOfWeekInvalid = "day_of_week_invalid";

            public const string FeatureInvalid = "feature_invalid";
        }

        public static class Update
        {
         
        }

        public static class Get
        {
            public const string ScreeningScheduleNotFound = "screening_schedule_not_found";
        }

        public static class GetByFilters
        {
            public const string ToDateAfterFromDate = "to_date_must_be_after_from_date";
            public const string StatusInvalid = "status_invalid";
        }

        public static class Delete
        {
           

        }

        public static class UpdateStatus
        {
            public const string ScreeningNotFound = "screening_not_found";
            public const string DateBustBeTodayOrFuture = "date_must_be_today_or_future";
        }

    }
}
