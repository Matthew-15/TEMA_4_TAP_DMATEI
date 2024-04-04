using DataLayer.Models;
using DataLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IRepository<Article> _articlesRepository;

        public ArticleController(IRepository<Article> articlesRepository)
        {
            _articlesRepository = articlesRepository;
        }

        [HttpGet("getall")]
        public IEnumerable<ArticleDto> GetAll()
        {
            return _articlesRepository
                .GetAll()
                .Select(article => new ArticleDto(article.Title, article.Description, article.Url));
        }

        [HttpPost("add")]
        public IActionResult Add(ArticleDto article)
        {
            if (string.IsNullOrEmpty(article.Title) || string.IsNullOrEmpty(article.Description) || string.IsNullOrEmpty(article.Url))
            {
                return BadRequest("Toate câmpurile trebuie completate.");
            }


            _articlesRepository.Add(new Article()
            {
                Title = article.Title,
                Description = article.Description,
                Url = article.Url
            });

            _articlesRepository.SaveChanges();
            return Ok();
        }

        [HttpPut("update")]
        public IActionResult Update(Guid articleId, ArticleDto article)
        {
            var articleFromDb = _articlesRepository.GetById(articleId);

            if (articleFromDb == null)
            {
                return NotFound("Articolul nu a fost găsit.");
            }

            if (string.IsNullOrEmpty(article.Title) || string.IsNullOrEmpty(article.Description) || string.IsNullOrEmpty(article.Url))
            {
                return BadRequest("Toate câmpurile trebuie completate.");
            }


            articleFromDb.Title = article.Title;
            articleFromDb.Description = article.Description;
            articleFromDb.Url = article.Url;

            _articlesRepository.SaveChanges();
            return Ok("Articolul a fost actualizat cu succes.");
        }

        [HttpDelete("delete")]
        public IActionResult Delete(Guid articleId)
        {
            var articleFromDb = _articlesRepository.GetById(articleId);

            if (articleFromDb == null)
            {
                return NotFound("Articolul nu a fost găsit.");
            }

            _articlesRepository.Remove(articleFromDb);
            _articlesRepository.SaveChanges();

            return Ok("Articolul a fost șters cu succes.");
        }
    }
}
