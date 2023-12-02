using FinTech.DB;
using FinTech.DB.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTechCore.Interfaces
{
    public interface IAuthenticationService
    {
        Task<Responses<LoginUserDTO>> Login(LoginDTO model);
        Task<Responses<string>> Register(RegisterDTO user);
        Task<Responses<string>> RefreshToken();
        public Task<Responses<string>> ChangePassword(ChangePasswordDTO changePasswordDTO);
        public Task<object> ResetPassword(UpdatePasswordDTO resetPasswordDTO);
        public Task<Responses<string>> ForgottenPassword(ResetPasswordDTO model);
        Task Signout();
    }
}
