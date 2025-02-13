using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SPHealthSupportSystem_Repositories.DBContext;
using SPHealthSupportSystem_Repositories.Models;

namespace SPHealthSupportSystem_MVCWebApp.Controllers
{
    public class PsychologyTheoriesController : Controller
    {
        private readonly NET1720_PRN231_PRJ_G1_SchoolPsychologicalHealthSupportSystemContext _context;

        public PsychologyTheoriesController(NET1720_PRN231_PRJ_G1_SchoolPsychologicalHealthSupportSystemContext context)
        {
            _context = context;
        }

        // GET: PsychologyTheories
        public async Task<IActionResult> Index()
        {
            var nET1720_PRN231_PRJ_G1_SchoolPsychologicalHealthSupportSystemContext = _context.PsychologyTheories.Include(p => p.Topic);
            return View(await nET1720_PRN231_PRJ_G1_SchoolPsychologicalHealthSupportSystemContext.ToListAsync());
        }

        // GET: PsychologyTheories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var psychologyTheory = await _context.PsychologyTheories
                .Include(p => p.Topic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (psychologyTheory == null)
            {
                return NotFound();
            }

            return View(psychologyTheory);
        }

        // GET: PsychologyTheories/Create
        public IActionResult Create()
        {
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Id");
            return View();
        }

        // POST: PsychologyTheories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,TopicId,Author,YearPublished,TheoryType,Principle,Application,RelatedTheory,Criticism,CreateAt,UpdateAt")] PsychologyTheory psychologyTheory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(psychologyTheory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Id", psychologyTheory.TopicId);
            return View(psychologyTheory);
        }

        // GET: PsychologyTheories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var psychologyTheory = await _context.PsychologyTheories.FindAsync(id);
            if (psychologyTheory == null)
            {
                return NotFound();
            }
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Id", psychologyTheory.TopicId);
            return View(psychologyTheory);
        }

        // POST: PsychologyTheories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,TopicId,Author,YearPublished,TheoryType,Principle,Application,RelatedTheory,Criticism,CreateAt,UpdateAt")] PsychologyTheory psychologyTheory)
        {
            if (id != psychologyTheory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(psychologyTheory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PsychologyTheoryExists(psychologyTheory.Id))
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
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Id", psychologyTheory.TopicId);
            return View(psychologyTheory);
        }

        // GET: PsychologyTheories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var psychologyTheory = await _context.PsychologyTheories
                .Include(p => p.Topic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (psychologyTheory == null)
            {
                return NotFound();
            }

            return View(psychologyTheory);
        }

        // POST: PsychologyTheories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var psychologyTheory = await _context.PsychologyTheories.FindAsync(id);
            if (psychologyTheory != null)
            {
                _context.PsychologyTheories.Remove(psychologyTheory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PsychologyTheoryExists(int id)
        {
            return _context.PsychologyTheories.Any(e => e.Id == id);
        }
    }
}
