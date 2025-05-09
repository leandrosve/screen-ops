﻿using AutoMapper;
using CinemasService.Dtos;
using CinemasService.Models;
using Contracts.Rooms;
using GrpcRoomsService;

namespace CinemasService.Profiles
{
    public class CinemaProfile : Profile
    {

        public CinemaProfile() {

            CreateMap<Cinema, CinemaDto>();
            CreateMap<Cinema, CinemaSummaryDto>();
            CreateMap<Cinema, CinemaSummaryShortDto>();

            CreateMap<Room, RoomDto>();

            // To allow ignoring null integers for updates
            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);

            CreateMap<CinemaUpdateDto, Cinema>()
                .ForMember(dest => dest.Capacity, opt => opt.Condition(src => src.Capacity.HasValue))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<RoomUpdateDto, Room>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<LayoutElementCreateDto, LayoutElement>();
            CreateMap<LayoutCreateDto, Layout>();
            CreateMap<Layout, LayoutDto>();
            CreateMap<Layout, LayoutSummaryDto>();
            CreateMap<LayoutElement, LayoutElementDto>();

            CreateMap<Room, RoomSummaryContractDto>();
            CreateMap<Room, RoomSummaryDto>();
            CreateMap<Room, RoomSummaryShortDto>();
            CreateMap<RoomSummaryContractDto, GrpcRoomSummaryModel>();
        }

    }
}
