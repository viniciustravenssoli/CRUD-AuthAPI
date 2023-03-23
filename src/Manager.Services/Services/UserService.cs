using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Manager.Core.Exceptions;
using Manager.Domain.Entities;
using Manager.Infra.Interfaces;
using Manager.Services.DTO;
using Manager.Services.Interfaces;
using EscNet.Cryptography.Interfaces;

namespace Manager.Services.Services{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IRijndaelCryptography _rijndaelCryptography;

        public UserService(IMapper mapper, IUserRepository userRepository, IRijndaelCryptography rijndaelCryptography)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _rijndaelCryptography = rijndaelCryptography;
        }

        public async Task<UserDTO> Create(UserDTO userDTO)
        {
            var userExists = await _userRepository.GetByEmail(userDTO.Email);
            if(userExists!= null)
                throw new DomainException("Já existe um usuario cadastrado com esse email");

            var user = _mapper.Map<User>(userDTO);
            user.Validate();
            user.ChangePassword(_rijndaelCryptography.Encrypt(user.Password));

            var userCreated = await _userRepository.Create(user);

            return _mapper.Map<UserDTO>(userCreated);
        }

        public async Task<UserDTO> Auth(string email, string password)
        {
            var user = await _userRepository.GetByEmail(email);

            var hasedpassword = _rijndaelCryptography.Encrypt(password);

            if (user.Password != hasedpassword)
            {
                return null;
            }

            return _mapper.Map<UserDTO>(user);
        }
        

        public async Task<UserDTO> Get(long id)
        {
            var user = await _userRepository.Get(id);

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<List<UserDTO>> Get()
        {
           var allUsers = await _userRepository.Get();

           return _mapper.Map<List<UserDTO>>(allUsers);
        }

        public async Task<UserDTO> GetByEmail(string email)
        {
            var user = await _userRepository.GetByEmail(email);

            return _mapper.Map<UserDTO>(user);
        }

        

        public async Task Remove(long id)
        {
            await _userRepository.Remove(id);
        }

        public async Task<List<UserDTO>> SearchByEmail(string email)
        {
            var allUsers = await _userRepository.SearchByEmail(email);

            return _mapper.Map<List<UserDTO>>(allUsers);
        }

        public async Task<List<UserDTO>> SearchByName(string name)
        {
           var allUsers = await _userRepository.SearchByName(name);

            return _mapper.Map<List<UserDTO>>(allUsers);
        }

        public async Task<UserDTO> Update(UserDTO userDTO)
        {
            var userExists = await _userRepository.Get(userDTO.Id);

            if(userExists == null)
                throw new DomainException("Nao existe nenhum usuario com o Id informado");

            var user = _mapper.Map<User>(userDTO);
            user.Validate();
            user.ChangePassword(_rijndaelCryptography.Encrypt(user.Password));

            var userUpdated = await _userRepository.Update(user);

            return _mapper.Map<UserDTO>(userUpdated);
        }
    }
}