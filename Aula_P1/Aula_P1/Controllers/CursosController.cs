using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Aula_P1.Data;
using Aula_P1.Models;
using System.Xml.Linq;
using Aula_P1.Data.Migrations;

namespace Aula_P1.Controllers
{
    public class CursosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CursosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cursos
        public async Task<IActionResult> Index(bool? Disponivel)
        {
            if (Disponivel == null)
                return View(await _context.Cursos.ToListAsync());
            else
                return View(await _context.Cursos.Where(c => c.Disponivel == Disponivel).ToListAsync());

        }
        [HttpPost]
        public async Task<IActionResult> Index(string cname)
        {
             var result = from s in _context.Cursos
                          where s.Nome.Contains(cname)
                          select s;

             return View(await result.ToListAsync());
            

            //return View(await _context.Cursos.Where(c => c.Nome.Contains(cname)).ToListAsync());

        }

        // GET: Search
        public async Task<IActionResult> Search()
        {
            ViewData["ListaDeCategorias"] = new SelectList(_context.Categoria.ToList(), "Id", "Nome");
            PesquisaCursoViewModel pesquisaCurso = new PesquisaCursoViewModel();
            pesquisaCurso.ListaDeCursos = await _context.Cursos.Include("categoria").ToListAsync();
            pesquisaCurso.NumResultados = pesquisaCurso.ListaDeCursos.Count;

            return View(pesquisaCurso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search([Bind("TextoAPesquisar")] PesquisaCursoViewModel pesquisaCurso)
        {
            ViewData["ListaDeCategorias"] = new SelectList(_context.Categoria.ToList(), "Id", "Nome");
            pesquisaCurso.ListaDeCursos = await _context.Cursos.Where(c => c.Nome.Contains(pesquisaCurso.TextoAPesquisar))  .ToListAsync();
            pesquisaCurso.NumResultados = pesquisaCurso.ListaDeCursos.Count();

            return View(pesquisaCurso);

        }



        // GET: Cursos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cursos == null)
            {
                return NotFound();
            }

            var curso = await _context.Cursos.Include("categoria")
                .FirstOrDefaultAsync(m => m.Id == id);
            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        // GET: Cursos/Create
        public IActionResult Create()
        {
            ViewData["ListaDeCategorias"] = new SelectList(_context.Categoria.ToList(),"Id","Nome");
            return View();
        }

        // POST: Cursos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Disponivel,Descricao,DescricaoResumida,Rquisitos,IdadeMinima,Price,CategoriaId")] Curso curso)
        {
            ModelState.Remove(nameof(curso.categoria));
            if (ModelState.IsValid)
            {
                _context.Add(curso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(curso);
        }

        // GET: Cursos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cursos == null)
            {
                return NotFound();
            }

            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
            {
                return NotFound();
            }

            ViewData["ListaDeCategorias"] = new SelectList(_context.Categoria.ToList(), "Id", "Nome",curso.CategoriaId);

            return View(curso);
        }

        // POST: Cursos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Disponivel,Descricao,DescricaoResumida,Rquisitos,IdadeMinima,Price,CategoriaId")] Curso curso)
        {
            if (id != curso.Id)
            {
                return NotFound();
            }

            ModelState.Remove(nameof(curso.categoria));
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(curso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CursoExists(curso.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(curso);
        }

        // GET: Cursos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cursos == null)
            {
                return NotFound();
            }

            var curso = await _context.Cursos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        // POST: Cursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cursos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Cursos'  is null.");
            }
            var curso = await _context.Cursos.FindAsync(id);
            if (curso != null)
            {
                _context.Cursos.Remove(curso);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CursoExists(int id)
        {
          return _context.Cursos.Any(e => e.Id == id);
        }

        
    }
}
