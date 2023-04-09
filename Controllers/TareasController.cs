﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionDeTareas.Models;
using Azure.Core;

namespace GestionDeTareas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private readonly TareasDBDbContext _context;

        public TareasController(TareasDBDbContext context)
        {
            _context = context;
        }

        // GET: api/Tareas
        [HttpGet]
        public async Task<IActionResult> GetTareas()
        {
            List<Tarea> list = await _context.Tareas.OrderBy(x => x.Name).ThenBy(x => x.Date).ToListAsync();
            return StatusCode(StatusCodes.Status200OK, list);
        }

        // GET: api/Tareas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarea>> GetTarea(int id)
        {
          if (_context.Tareas == null)
          {
              return NotFound();
          }
            var tarea = await _context.Tareas.FindAsync(id);

            if (tarea == null)
            {
                return NotFound();
            }

            return tarea;
        }

        // PUT: api/Tareas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarea(int id, Tarea tarea)
        {
            if (id != tarea.Id)
            {
                return BadRequest();
            }

            _context.Entry(tarea).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TareaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tareas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tarea>> PostTarea([FromForm]Tarea tarea)
        {
            await _context.Tareas.AddAsync(tarea);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, "ok");
        }

        // DELETE: api/Tareas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarea(int id)
        {
            if (_context.Tareas == null)
            {
                return NotFound();
            }
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null)
            {
                return NotFound();
            }

            _context.Tareas.Remove(tarea);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, "ok");
        }

        private bool TareaExists(int id)
        {
            return (_context.Tareas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}