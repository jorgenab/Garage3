using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Garage3.Models;
using Garage3.Models.ViewModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Garage3.Controllers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Members, MemberViewModel>()
                .ForMember(
                dest => dest.NumberOfVehicles,
                from => from.MapFrom(s =>s.Vehicles.Count));
                    
            CreateMap<Vehicles, DetailsViewModel>()
                .ForMember(
                       dest => dest.TypeOfVehicle,
                       from => from.MapFrom(s => s.VehicleTypes.TypeOfVehicle))
                .ForMember(
                       dest => dest.FullName,
                       from => from.MapFrom(s => s.Members.FullName));

            CreateMap<Members, MemberDetailsViewModel>()
                .ForMember(
                dest => dest.VehicleTypes,
                from => from.MapFrom(s => s.Vehicles.Select(e => e.VehicleTypes).ToList())
                );
               

        }
    }
}
