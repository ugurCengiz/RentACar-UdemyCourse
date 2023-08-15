using Application.Features.Brands.Queries.GetList;
using Application.Models.Queries.GetList;
using Application.Models.Queries.GetListByDynamic;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Models.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        //Manuel Join 
        CreateMap<Model, GetListModelListItemDto>()
            .ForMember(destinationMember: b => b.BrandName, memberOptions: opt => opt.MapFrom(b => b.Brand.Name))
            .ForMember(destinationMember: f => f.FuelName, memberOptions: opt => opt.MapFrom(f => f.Fuel.Name))
            .ForMember(destinationMember: t => t.TransmissionName, memberOptions: opt => opt.MapFrom(t => t.Transmission.Name))
            .ReverseMap();
        CreateMap<Paginate<Model>, GetListResponse<GetListModelListItemDto>>().ReverseMap();


        CreateMap<Paginate<Model>, GetListResponse<GetListByDynamicModelListItemDto>>().ReverseMap();
        CreateMap<Model, GetListByDynamicModelListItemDto>()
            .ForMember(destinationMember: b => b.BrandName, memberOptions: opt => opt.MapFrom(b => b.Brand.Name))
            .ForMember(destinationMember: f => f.FuelName, memberOptions: opt => opt.MapFrom(f => f.Fuel.Name))
            .ForMember(destinationMember: t => t.TransmissionName, memberOptions: opt => opt.MapFrom(t => t.Transmission.Name))
            .ReverseMap();

    }
}