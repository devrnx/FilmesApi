using System;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using AutoMapper;

namespace FilmesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public FilmeController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionarFilmes([FromBody] CreateFilmeDto filmeDto)
        {
            Filme filme = _mapper.Map<Filme>(filmeDto);

            _context.Filmes.Add(filme);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperarFilmesPorId), new { Id = filme.Id }, filme);    
        }

        [HttpGet]
        public IEnumerable<Filme> RecuperarFilmes()
        {
            return _context.Filmes;
        }       

        [HttpGet("{id}")]
        public IActionResult RecuperarFilmesPorId(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if (filme != null)
            {
                ReadFilmeDto filmeDto = _mapper.Map<ReadFilmeDto>(filme);

                return Ok(filmeDto);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
        {


            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if (filme == null)
            {
                return NotFound();
            }

            _mapper.Map(filmeDto, filme);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarFilme(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if (filme == null)
            {
                return NotFound();
            }

            _context.Remove(filme);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
