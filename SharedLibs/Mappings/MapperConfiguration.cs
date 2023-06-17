using AutoMapper;
using ExMoney.SharedLibs.DTOs;

namespace ExMoney.SharedLibs.Mappings
{
    public class MapperConfiguration: Profile
    {
        public MapperConfiguration()
        {
            this.CreateMap<CurrencyCreateDTO, Currency>().ReverseMap();
            this.CreateMap<TransactionCreateDTO, Transaction>().ReverseMap();
            this.CreateMap<WalletCreateDTO, Wallet>().ReverseMap();
            this.CreateMap<ExMoneySettingsUpdateDTO, ExMoneySettings>().ReverseMap();
            this.CreateMap<UserRegisterDTO, User>().ReverseMap();
        }
    }
}
