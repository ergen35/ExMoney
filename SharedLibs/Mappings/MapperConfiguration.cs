using AutoMapper;
using ExMoney.SharedLibs.DTOs;

namespace ExMoney.SharedLibs.Mappings
{
    public class MapperConfiguration: Profile
    {
        public MapperConfiguration()
        {
            this.CreateMap<Currency, CurrencyCreateDTO>().ReverseMap();
        }
    }
}
