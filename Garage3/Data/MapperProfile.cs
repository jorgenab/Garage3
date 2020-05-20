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

            CreateMap<Student, StudentDetailsViewModel>()
                .ForMember(
                       dest => dest.Attending,
                       from => from.MapFrom(s => s.Enrollments.Count))
                .ForMember(
                       dest => dest.Courses,
                       from => from.MapFrom(s => s.Enrollments.Select(e => e.Course).ToList()));

        }
    }
}
