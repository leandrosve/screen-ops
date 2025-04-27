namespace CinemasService.Errors
{
    public static class LayoutErrors
    {
        public static class Create
        {
            public const string NameRequired = "name_required";
            public const string NameMaxLength = "name_max_length";
            public const string NameAlreadyExists = "name_already_exists";

            public const string RowsRequired = "rows_required";
            public const string ColumnsRequired= "columns_required";

            public const string ElementsRequired = "elements_requried";

            public const string ElementsTooMany = "elements_too_many";

            public const string SeatLabelRequired = "seat_label_required";
            public const string SeatLabelDuplicated = "seat_label_duplicated";
            public const string DimensionsTooSmall = "dimensions_too_small";
            public const string NotEnoughSeats = "not_enough_seats";
            public const string DuplicatePositions = "duplicate_positions";
            public const string MissingSeatPositions = "missing_seat_positions";

        }

        public static class Update
        {
            public const string LayoutNotFound = "layout_not_found";

        }

        public static class Get
        {
            public const string LayoutNotFound = "layout_not_found";

        }

        public static class Delete
        {
            public const string LayoutNotFound = "layout_not_found";
        }

    }
}
