namespace ScreeningsService.Errors
{
    public class ScreeningErrors
    {
        public static class Create
        {
            public const string MovieIdRequired = "movie_id_required";
            public const string MovieNotFound = "movie_not_found";


            public const string RoomIdRequired = "room_id_required";

            public const string StartTimeMustBeBeforeEndTime = "start_time_must_be_before_end_time";
            public const string EndTimeMustBeAfterStartTime = "end_time_must_be_after_start_time";

            public const string EndTimeBeforeMovieEnds = "end_time_must_be_after_movie_ends";

            public const string FeatureInvalid = "feature_invalid";
        }

        public static class Update
        {
         
        }

        public static class Get
        {
          


        }

        public static class Delete
        {
           

        }

        public static class Publish
        {

        }

    }
}
