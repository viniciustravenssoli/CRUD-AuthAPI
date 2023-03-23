using System.Collections.Generic;
using System.Security;
using Manager.API.ViewModels;

namespace Manager.API.Utilities{
    public static class Responses{
        public static ResultViewModel ApplicationErrorMessage()
        {
            return new ResultViewModel
            {
                Message = "Ocorreu algum erro interno na aplicaçao, por favor tente novamente",
                Success = false,
                Data = null
            };
        }

        public static ResultViewModel DomainErrorMessage(string message)
        {
            return new ResultViewModel
            {
                Message = message,
                Success = false,
                Data = null
            };
        }

        public static ResultViewModel DomainErrorMessage(string message, IReadOnlyCollection<string> errors)
        {
            return new ResultViewModel
            {
                Message = message,
                Success = false,
                Data = errors
            };
        }

        public static ResultViewModel UnauthorizedErrorMessage()
        {
            return new ResultViewModel
            {
                Message = "A Combinaçao de login ou senha esta incorreta",
                Success = false,
                Data = null
            };
        }
    }
}