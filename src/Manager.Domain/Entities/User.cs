using System.Text;
using System;
using System.Collections.Generic;
using Manager.Core.Exceptions;
using Manager.Domain.Validators;

namespace Manager.Domain.Entities
{
    public class User : Base
    {
        public string Name { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }


        protected User() { }

        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
            _errors = new List<string>();

            Validate();
        }

        public void ChangeName(string name){
            Name = name;
            Validate();
        }

        public void ChangePassword(string password){
            Password = password;
            Validate();
        }

        public void ChangeEmail(string email){
            Email = email;
            Validate();
        }

        public bool Validate() 
            => base.Validate<UserValidator, User>(new UserValidator(), this);

        public string ErrorsToString()
        {
            var builder = new StringBuilder();

            foreach(var error in _errors)
                builder.AppendLine(error);

            return builder.ToString();
        }
        
    }

}