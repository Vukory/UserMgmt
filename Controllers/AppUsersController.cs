using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using UserMgmt.Data;
using UserMgmt.Dtos;
using UserMgmt.Models;
using UserMgmt.Services;

namespace UserMgmt.Controllers
{
    [Route("api/appusers")]
    [ApiController]
    public class AppUsersController : ControllerBase
    {
        private readonly IUserMgmtRepo _repository;
        private readonly IMapper _mapper;
        private readonly AppUserService _service;

        public AppUsersController(IUserMgmtRepo repository, IMapper mapper, AppUserService service)
        {
            _repository = repository;
            _mapper = mapper;
            _service = service;
        }

        //GET api/appusers
        [HttpGet]
        public ActionResult<IEnumerable<AppUserReadDto>> GetaAllAppUsers()
        {
            var appUserItems = _repository.GetAllAppUsers();

            return Ok(_mapper.Map<IEnumerable<AppUserReadDto>>(appUserItems));
        }

        //GET api/appusers/{id}
        [HttpGet("{id}", Name = "GetAppUserById")]
        public ActionResult<AppUserReadDto> GetAppUserById(int id)
        {
            var appUserItem = _repository.GetAppUserById(id);

            if (appUserItem != null)
            {
                return Ok(_mapper.Map<AppUserReadDto>(appUserItem));
            }

            return NotFound();
        }

        //POST api/appusers
        [HttpPost]
        public ActionResult<AppUserReadDto> CreateAppUser(AppUserCreateDto appUserCreateDto)
        {
            var appUserModel = _mapper.Map<AppUser>(appUserCreateDto);
            var username = _service.GetAvailableUsername(appUserModel.FirstName, appUserModel.LastName);
            appUserModel.Username = username;
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(appUserCreateDto.Password);
            appUserModel.PasswordHash=passwordHash;
            appUserModel.SystemRole=SystemRole.User;
            _repository.CreateAppUser(appUserModel);
            _repository.SaveChanges();

            var appUserReadDto = _mapper.Map<AppUserReadDto>(appUserModel);

            return CreatedAtRoute(nameof(GetAppUserById), new { Id = appUserReadDto.Id }, appUserReadDto);

        }

        //PATCH api/appusers/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialAppUserUpdate(int id, JsonPatchDocument<AppUserUpdateDto> patchDoc)
        {
            var appUserModelFromRepo = _repository.GetAppUserById(id);
            if (appUserModelFromRepo == null)
            {
                return NotFound();
            }


            Console.WriteLine("FUCK");
            var appUserToPatch = _mapper.Map<AppUserUpdateDto>(appUserModelFromRepo);
            patchDoc.ApplyTo(appUserToPatch, ModelState);

            if (!TryValidateModel(appUserToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(appUserToPatch, appUserModelFromRepo);
            _repository.UpdateAppUser(appUserModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        //DELETE api/appusers/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteAppUser(int id)
        {
            var appUserModelFromRepo = _repository.GetAppUserById(id);
            if (appUserModelFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeleteAppUser(appUserModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}