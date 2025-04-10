namespace CinemasService.Errors
{
    public static class RoomErrors
    {
        public static class Create
        {
            public const string CinemaNotFound = "cinema_not_found";
            public const string CinemaIdRequired = "cinema_id_required";

            public const string NameRequired = "name_required";
            public const string NameMaxLength = "name_too_long";
            public const string DescriptionMaxLength = "description_max_length";

        }

        public static class Update
        {
            public const string RoomNotFound = "room_not_found";
            public const string NameMinLength = "name_min_length";
            public const string NameMaxLength = "name_max_length";

            public const string DescriptionMinLength = "description_min_length";
            public const string DescriptionMaxLength = "description_max_length";
        }

        public static class Get
        {
            public const string RoomNotFound = "room_not_found";


        }

        public static class Delete
        {
            public const string RoomNotFound = "room_not_found";

        }

        public static class Publish
        {
            public const string RoomNotFound = "room_not_found";
            public const string LayoutMissing = "layout_missing";

        }


    }
}
