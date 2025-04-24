namespace CinemasService.Errors
{
    public static class CinemaErrors
    {
        public static class Create
        {
            public const string NameRequired = "name_required";
            public const string NameMaxLength = "name_max_length";
            public const string NameAlreadyExists = "name_already_exists";


            public const string LocationRequired = "location_required";
            public const string LocationMaxLength = "location_max_length";

            public const string DescriptionRequired = "description_required";
            public const string DescriptionMaxLength = "description_max_length";

            public const string CapacityInvalid = "capacity_invalid";
            public const string ImageUrlInvalid = "image_url_invalid";
        }

        public static class Update
        {
            public const string CinemaNotFound = "cinema_not_found";

            public const string NameMinLength = "name_min_length";
            public const string NameMaxLength = "name_max_length";

            public const string LocationMinLength = "location_min_length";
            public const string LocationMaxLength = "location_max_length";

            public const string DescriptionMinLength = "description_min_length";
            public const string DescriptionMaxLength = "description_max_length";

            public const string ImageUrlInvalid = "image_url_invalid";

            public const string CapacityInvalid = "capacity_invalid";

            public const string StatusInvalid = "status_invalid";

        }

        public static class Get
        {
            public const string CinemaNotFound = "cinema_not_found";
        }

        public static class Delete
        {
            public const string CinemaNotFound = "cinema_not_found";
        }

    }
}
