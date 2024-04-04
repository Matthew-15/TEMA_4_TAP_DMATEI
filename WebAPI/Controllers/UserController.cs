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
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;

        public UserController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("get")]
        public IEnumerable<User> Get()
        {
            return _userRepository.GetAll();
        }

        [HttpPost("add")]
        public IActionResult Add(UserDto userDto)
        {
            if (string.IsNullOrEmpty(userDto.Name) || string.IsNullOrEmpty(userDto.Email) || string.IsNullOrEmpty(userDto.Password))
            {
                return BadRequest("Toate câmpurile trebuie completate.");
            }


            _userRepository.Add(new User(userDto.Name, userDto.Email, userDto.Password, userDto.TypeId));
            _userRepository.SaveChanges();
            return Ok();
        }

        [HttpPut("update")]
        public void Update(User user)
        {
            _userRepository.Update(user);
            _userRepository.SaveChanges();
        }

        [HttpDelete("delete/{id}")]
        public void Delete(Guid id)
        {
            _userRepository.Remove(_userRepository.GetById(id));
            _userRepository.SaveChanges();
        }
    }
}
