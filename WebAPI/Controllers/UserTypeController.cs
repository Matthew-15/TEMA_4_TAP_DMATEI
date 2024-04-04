using DataLayer.Models;
using DataLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;
using System;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserTypeController : ControllerBase
    {
        private readonly IRepository<UserType> _userTypeRepository;

        public UserTypeController(IRepository<UserType> userTypeRepository)
        {
            _userTypeRepository = userTypeRepository;
        }

        [HttpGet("get")]
        public IEnumerable<UserType> Get()
        {
            return _userTypeRepository.GetAll();
        }

        [HttpPost("add")]
        public IActionResult Add(UserTypeDto userTypeDto)
        {
            if (string.IsNullOrEmpty(userTypeDto.Name))
            {
                return BadRequest("Numele tipului de utilizator nu poate fi gol.");
            }

            _userTypeRepository.Add(new UserType(userTypeDto.Name));
            _userTypeRepository.SaveChanges();
            return Ok();
        }

        [HttpPut("update")]
        public void Update(UserType userType)
        {
            _userTypeRepository.Update(userType);
            _userTypeRepository.SaveChanges();
        }

        [HttpDelete("delete/{id}")]
        public void Delete(Guid id)
        {
            _userTypeRepository.Remove(_userTypeRepository.GetById(id));
            _userTypeRepository.SaveChanges();
        }
    }
}
