using AutoMapper;
using SalesSystem.Core.Models;
using SalesSystem.Sales.DTOs;

namespace BookingSystem.ApiSales.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Sale, ReadSalesDTO>();
            CreateMap<Sale, SaveSalesDTO>();


            CreateMap<ReadSalesDTO, Sale>();
            CreateMap<SaveSalesDTO, Sale>();
        }
    }
}