﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using PWEB_AulasP_2223.Data;
using PWEB_AulasP_2223.Helpers;
using PWEB_AulasP_2223.Models;
using PWEB_AulasP_2223.ViewModels;

namespace PWEB_AulasP_2223.Controllers
{
    public class CursosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CursosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cursos
        public async Task<IActionResult> Index(bool? disponivel)
        {

            ViewData["ListaDeCategorias"] = new SelectList(_context.Categorias.ToList(), "Id", "Nome");

            if (disponivel != null)
            {
                if (disponivel==true)
                    ViewData["Title"] = "Lista de cursos activos";
                else
                    ViewData["Title"] = "Lista de cursos Inactivos";

                return View(await _context.Cursos.Include("categoria").
                    Where(c => c.Disponivel == disponivel).ToListAsync()
                    );
            }
            else
            {
                ViewData["Title"] = "Lista de cursos";
                return View(await _context.Cursos.Include("categoria").ToListAsync());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(string TextoAPesquisar, int CategoriaId)
        {
            ViewData["ListaDeCategorias"] = new SelectList(_context.Categorias.ToList(), "Id", "Nome");

            if (string.IsNullOrWhiteSpace(TextoAPesquisar))
                return View(_context.Cursos.Where(c=>c.CategoriaId == CategoriaId));
            else
            {
                var resultado =  from c in _context.Cursos
                                where c.Nome.Contains(TextoAPesquisar) && c.CategoriaId==CategoriaId
                                select c;
                return View(resultado);
            }                            
        }
        
        public async Task<IActionResult> Search(string? TextoAPesquisar)
        {
            PesquisaCursoViewModel pesquisaVM = new PesquisaCursoViewModel();

            if (string.IsNullOrWhiteSpace(TextoAPesquisar))            
                pesquisaVM.ListaDeCursos =  await _context.Cursos.Include("categoria").ToListAsync();
            else
            {
                pesquisaVM.ListaDeCursos = await _context.Cursos.Include("categoria").
                    Where(c =>  c.Nome.Contains(TextoAPesquisar) || 
                                c.DescricaoResumida.Contains(TextoAPesquisar) || 
                                c.Descricao.Contains(TextoAPesquisar)
                         ).ToListAsync();

                pesquisaVM.TextoAPesquisar = TextoAPesquisar;
            }    
            
            pesquisaVM.NumResultados = pesquisaVM.ListaDeCursos.Count();
            

            return View(pesquisaVM);

        }

        [HttpPost]  
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(
            [Bind("TextoAPesquisar")] 
            PesquisaCursoViewModel pesquisaCurso
            )
        {

            if (string.IsNullOrEmpty(pesquisaCurso.TextoAPesquisar))
            {
                pesquisaCurso.ListaDeCursos = 
                    await _context.Cursos.Include("categoria").ToListAsync();

                pesquisaCurso.NumResultados = pesquisaCurso.ListaDeCursos.Count();
            }                
            else
            {
                pesquisaCurso.ListaDeCursos = 
                    await _context.Cursos.Include(c=>c.categoria).Where(
                        c => c.Nome.Contains(pesquisaCurso.TextoAPesquisar)
                        ).ToListAsync();

                pesquisaCurso.NumResultados = pesquisaCurso.ListaDeCursos.Count();
                
            }

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
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {

            ViewData["ListaDeCategorias"] = 
                new SelectList(_context.Categorias.ToList(), "Id", "Nome");
            

            return View();
        }

        // POST: Cursos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Nome,Descricao,DescricaoResumida,Requisitos,IdadeMinima,Disponivel,Preco,EmDestaque,CategoriaId")] Curso curso)
        {
            ViewData["ListaDeCategorias"] =
                new SelectList(_context.Categorias.ToList(), "Id", "Nome");
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
        [Authorize(Roles = "Admin")]
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

            ViewData["ListaDeCategorias"] = new SelectList(_context.Categorias.ToList(), "Id", "Nome", curso.CategoriaId);

            string coursePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CursosImagens/" + id.ToString());

            if (!Directory.Exists(coursePath))
                Directory.CreateDirectory(coursePath);

            var files = from file in Directory.EnumerateFiles(coursePath)
                        select string.Format("/CursosImagens/{0}/{1}",id,Path.GetFileName(file));

            ViewData["CursosImagens"] = files;

         

            

            return View(curso);
        }

        // POST: Cursos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao,DescricaoResumida,Requisitos,IdadeMinima,Disponivel,Preco,EmDestaque,CategoriaId")] Curso curso,
           [FromForm] List<IFormFile> ImagemCurso)
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

                    string path = Directory.GetCurrentDirectory();
                    Path.Combine( path, "wwwroot/CursosImagens");

                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);


                    string coursePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CursosImagens/" + id.ToString());

                    if(!Directory.Exists(coursePath))
                        Directory.CreateDirectory(coursePath);

                    foreach (var formFile in ImagemCurso)
                    {
                        if(formFile.Length > 0)
                        {
                            var filePath = Path.Combine(coursePath,Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName));

                            while (System.IO.File.Exists(filePath))
                            {
                                filePath= Path.Combine(coursePath,Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName));
                            }
                            using (var stream = System.IO.File.Create(filePath))
                            {
                                await formFile.CopyToAsync(stream);
                            }
                        }
                    }


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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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

        public async Task<IActionResult> deleteImage(int id, string image)
        {
            if(id == null || _context.Cursos == null)
                return NotFound();

            var curso = await _context.Cursos.FirstOrDefaultAsync(m => m.Id == id);

            if (curso == null)
                return NotFound();

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/" + image);

            System.IO.File.Delete(filePath);
            return RedirectToAction("Edit", new { Id = id });
        }

        public async Task<IActionResult> Comprar(int? id)
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

            var CarrinhoDeCompras = HttpContext.Session.GetJson<Carrinho>("CarrinhoDeCompras") ?? new Carrinho();

            CarrinhoDeCompras.AddItem(new CarrinhoItem { CursoId = curso.Id, CursoNome = curso.Nome, PrecoUnit = (decimal)curso.Preco },1);

            HttpContext.Session.SetJson("CarrinhoDeCompras", CarrinhoDeCompras);

            return RedirectToAction("Carrinho");

        }

        public async Task<IActionResult> Carrinho()
        {
            var CarrinhoDeCompras = HttpContext.Session.GetJson<Carrinho>("CarrinhoDeCompras") ?? new Carrinho();
            return View(CarrinhoDeCompras);
        }
    }
}
