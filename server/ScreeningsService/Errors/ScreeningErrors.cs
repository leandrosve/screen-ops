namespace ScreeningsService.Errors
{
    public class ScreeningErrors
    {
        public static class Create
        {
            public const string MovieIdRequired = "movie_id_required";
            public const string MovieNotFound = "movie_not_found";

            public const string RoomIdRequired = "room_id_required";
            public const string RoomOccupied = "room_occupied";

            public const string DateRequired = "date_required";
            public const string DateBustBeTodayOrFuture = "date_must_be_today_or_future";

            public const string StartTimeRequired = "start_time_required";
            public const string StartTimeMustBeBeforeEndTime = "start_time_must_be_before_end_time";
            public const string EndTimeMustBeAfterStartTime = "end_time_must_be_after_start_time";
            public const string EndTimeRequired = "end_time_required";
            public const string EndTimeBeforeMovieEnds = "end_time_must_be_after_movie_ends";

            public const string FeatureInvalid = "feature_invalid";
        }

        public static class Update
        {
         
        }

        public static class Get
        {
            public const string ScreeningNotFound = "screening_not_found";
        }

        public static class Delete
        {
           

        }

        public static class UpdateStatus
        {
            public const string ScreeningNotFound = "screening_not_found";
        }

    }
}
