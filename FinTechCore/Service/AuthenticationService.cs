using FinTech.DB;
using FinTech.DB.DTO;
using FinTechCore.Interfaces;
using FinTechCore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FinTechCore.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository repository;

        public AuthenticationService(IAuthenticationRepository repository)
        {
            this.repository = repository;
        }
        public async Task<Responses<string>> ChangePassword(ChangePasswordDTO model)
        {
            if (model.ConfirmNewPassword != model.NewPassword) return new Responses<string> { Succeeded = false, Data = "Password does not match", StatusCode = 404 };
            var response = await repository.ChangePassword(model);
            return response;
        }

        public async Task<Responses<string>> ForgottenPassword(ResetPasswordDTO model)
        {
            var response = await repository.ForgottenPassword(model);
            return response;
        }

        public async Task<Responses<LoginUserDTO>> Login(LoginDTO model)
        {
            return await repository.Login(model);
        }

        public async Task<Responses<string>> RefreshToken()
        {
            return await repository.RefreshToken();
        }

        public async Task<Responses<string>> Register(RegisterDTO user)
        {
            var result = await repository.Register(user);
            var response = new Responses<string>();
            if (result)
            {
                response.Succeeded = true;
                response.StatusCode = (int)HttpStatusCode.Created;
                response.Message = "Successfully registered";
            }
            else
            {
                response.Succeeded = false;
                response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                response.Message = "Failed to register, please change check the email, username and password.";
            }

            return  response;
        }

        public async Task<object> ResetPassword(UpdatePasswordDTO model)
        {
            var response = await repository.ResetPassword(model);
            return response;
        }

        public async Task Signout()
        {
            await repository.Signout();
        }
    }
}
