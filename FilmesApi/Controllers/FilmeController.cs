using System;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FilmesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private static List<Filme> filmes = new List<Filme>();
        private static int id = 1;

        [HttpPost]
        public IActionResult AdicionarFilmes([FromBody] Filme filme)
        {
            filme.Id = id++;
            filmes.Add(filme);
            return CreatedAtAction(nameof(RecuperarFilmesPorId), new { Id = filme.Id }, filme);    
        }

        [HttpGet]
        public IActionResult RecuperarFilmes()
        {
            return Ok(filmes);
        }       

        [HttpGet("{id}")]
        public IActionResult RecuperarFilmesPorId(int id)
        {
            /*
            foreach (Filme filme in filmes)
            {
                if (filme.Id == id)
                {
                    return filme;
                }
            }

            return null;
            */

            Filme filme = filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme != null)
            {
                Ok(filme);
            }

            return NotFound();
        }
    }
}
