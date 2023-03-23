using System;
using System.Threading.Tasks;
using Manager.API.ViewModels;
using Manager.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Manager.Services.Interfaces;
using AutoMapper;
using Manager.Services.DTO;
using Manager.API.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace Manager.API.Controllers
{

    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        [Route("/api/v1/users/create")]

        public async Task<IActionResult> Create([FromBody] CreateUserViewModel userViewModel)
        {
            try
            {
                var userDTO = _mapper.Map<UserDTO>(userViewModel);
                var userCreated = await _userService.Create(userDTO);

                return Ok(new ResultViewModel
                {
                    Message = "Usuario criado com sucesso",
                    Success = true,
                    Data = userCreated
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpPut]
        [Authorize]
        [Route("/api/v1/users/update")]

        public async Task<IActionResult> Update([FromBody] UpdateUserViewModel userViewModel)
        {
            try
            {
                var userDTO = _mapper.Map<UserDTO>(userViewModel);

                var userUpdated = await _userService.Update(userDTO);

                return Ok(new ResultViewModel
                {
                    Message = "Usuario atualizado om sucesso!",
                    Success = true,
                    Data = userUpdated
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("/api/v1/users/remove")]

        public async Task<IActionResult> Remove(long id)
        {
            try
            {
                await _userService.Remove(id);

                return Ok(new ResultViewModel
                {
                    Message = "Usuario removido om sucesso!",
                    Success = true,
                    Data = null
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpGet]
        [Authorize]
        [Route("/api/v1/users/get/{id}")]

        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var user = await _userService.Get(id);

                if (user == null)
                    return Ok(new ResultViewModel
                    {
                        Message = "Usuario não encontrado!",
                        Success = true,
                        Data = user
                    });

                return Ok(new ResultViewModel
                {
                    Message = "Usuario encontrado om sucesso!",
                    Success = true,
                    Data = user
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpGet]
        [Authorize]
        [Route("/api/v1/users/get-all")]

        public async Task<IActionResult> GetAll()
        {
            try
            {
                var allUsers = await _userService.Get();
                

                return Ok(new ResultViewModel
                {
                    Message = "Usuarios encontrados om sucesso!",
                    Success = true,
                    Data = allUsers
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpGet]
        [Authorize]
        [Route("/api/v1/users/get-by-email")]

        public async Task<IActionResult> GetByEmail([FromQuery] string email)
        {
            try
            {
                var user = await _userService.GetByEmail(email);

                if (user == null)
                    return Ok(new ResultViewModel
                    {
                        Message = "Nenhum Usuario encontrado com esse email!",
                        Success = true,
                        Data = user
                    });

                return Ok(new ResultViewModel
                {
                    Message = "Usuario encontrado com sucesso!",
                    Success = true,
                    Data = user
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }


        [HttpGet]
        [Authorize]
        [Route("/api/v1/users/search-by-name")]

        public async Task<IActionResult> SearchByName([FromQuery] string name)
        {
            try
            {
                var allUsers = await _userService.SearchByName(name);

                if (allUsers.Count == 0)
                    return Ok(new ResultViewModel
                    {
                        Message = "Nenhum Usuario encontrado com esse nome!",
                        Success = true,
                        Data = null
                    });

                return Ok(new ResultViewModel
                {
                    Message = "Usuario encontrado com sucesso!",
                    Success = true,
                    Data = allUsers
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpGet]
        [Authorize]
        [Route("/api/v1/users/search-by-email")]

        public async Task<IActionResult> SearchByEmail([FromQuery] string email)
        {
            try
            {
                var allUsers = await _userService.SearchByEmail(email);

                if (allUsers.Count == 0)
                    return Ok(new ResultViewModel
                    {
                        Message = "Nenhum Usuario encontrado com esse email!",
                        Success = true,
                        Data = null
                    });

                return Ok(new ResultViewModel
                {
                    Message = "Usuario encontrado com sucesso!",
                    Success = true,
                    Data = allUsers
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

    }
}