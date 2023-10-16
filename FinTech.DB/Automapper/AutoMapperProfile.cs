using AutoMapper;
using FinTech.DB.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTech.DB.Automapper
{
    public  class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TransactionDTO, Transaction>();
            CreateMap<AccountDTo, Account>();
            CreateMap< Account,GetBankAccountDTo>().ReverseMap();
            CreateMap<UpdateAccountDTO , Account>();
            //CreateMap<>();
        }
    }
}
