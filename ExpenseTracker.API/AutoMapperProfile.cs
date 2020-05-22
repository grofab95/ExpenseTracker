using AutoMapper;
using ExpenseTracker.API.Dto.CostDto;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddCostDto, Cost>();
        }
    }
}
