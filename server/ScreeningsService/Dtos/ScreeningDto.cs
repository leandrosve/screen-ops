﻿using ScreeningsService.Enums;
using System.Text.Json.Serialization;

namespace ScreeningsService.Dtos
{
    public class ScreeningDto
    {
        public Guid Id { get; set; }
        public Guid MovieId { get; set; }
        public Guid RoomId { get; set; }
        public Guid CinemaId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public ICollection<ScreeningFeatureEnum> Features { get; set; } = new List<ScreeningFeatureEnum>();
        public ScreeningStatusEnum Status { get; set; }
    }
}
