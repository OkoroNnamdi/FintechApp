using FinTech.DB.DTO;
using FinTech.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTechCore.Repository
{
    public  interface IAuthenticationRepository
    {
        Task<Responses<LoginUserDTO>> Login(LoginDTO model);
        Task<bool> Register(RegisterDTO user);
        Task<Responses<string>> RefreshToken();
        public Task<Responses<string>> ChangePassword(ChangePasswordDTO changePasswordDTO);
        public Task<object> ResetPassword(UpdatePasswordDTO resetPasswordDTO);
        public Task<Responses<string>> ForgottenPassword(ResetPasswordDTO model);
        Task Signout();
    }
}
