using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DataLayer.Models;
using DataLayer.Repository;
using WebAPI.Dto;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IRepository<Author> _authorRepository;

        public AuthorController(IRepository<Author> authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Author>> GetAllAuthors()
        {
            var authors = _authorRepository.GetAll();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public ActionResult<Author> GetAuthorById(Guid id)
        {
            var author = _authorRepository.GetById(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpPost]
        public ActionResult<Author> AddAuthor(AuthorDto authorDto)
        {
            var newAuthor = new Author
            {
                Id = Guid.NewGuid(),
                Name = authorDto.Name,
                Description = authorDto.Description
            };

            _authorRepository.Add(newAuthor);
            _authorRepository.SaveChanges();

            return CreatedAtAction(nameof(GetAuthorById), new { id = newAuthor.Id }, newAuthor);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(Guid id, AuthorDto authorDto)
        {
            var existingAuthor = _authorRepository.GetById(id);
            if (existingAuthor == null)
            {
                return NotFound();
            }

            existingAuthor.Name = authorDto.Name;
            existingAuthor.Description = authorDto.Description;

            _authorRepository.Update(existingAuthor);
            _authorRepository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(Guid id)
        {
            var existingAuthor = _authorRepository.GetById(id);
            if (existingAuthor == null)
            {
                return NotFound();
            }

            _authorRepository.Remove(existingAuthor);
            _authorRepository.SaveChanges();

            return NoContent();
        }
    }
}
