using AutoMapper;

namespace CA.API.General
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<MstDepartment, LogMstDepartment>()
               .ForMember(dest => dest.SourceId,
                          opt => opt.MapFrom(src => src.Id));
            CreateMap<MstDesignation, LogMstDesignation>()
               .ForMember(dest => dest.SourceId,
                          opt => opt.MapFrom(src => src.Id));
            CreateMap<MstGroupSetup, LogMstGroupSetup>()
               .ForMember(dest => dest.SourceId,
                          opt => opt.MapFrom(src => src.Id));
            CreateMap<MstCostDriversType, LogMstCostDriversType>()
               .ForMember(dest => dest.SourceId,
                          opt => opt.MapFrom(src => src.Id));
            CreateMap<MstCostType, LogMstCostType>()
               .ForMember(dest => dest.SourceId,
                          opt => opt.MapFrom(src => src.Id));
            CreateMap<MstActivityType, LogMstActivityType>()
               .ForMember(dest => dest.SourceId,
                          opt => opt.MapFrom(src => src.Id));
            CreateMap<MstSalesPriceList, LogMstSalesPriceList>()
               .ForMember(dest => dest.SourceId,
                          opt => opt.MapFrom(src => src.Id));
        }
    }
}
