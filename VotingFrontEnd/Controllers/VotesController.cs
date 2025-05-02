using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VotingFrontEnd.Data;
using VotingFrontEnd.Models;

namespace VotingFrontEnd.Controllers
{
    public class VotesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public VotesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // 投票首頁
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var hasVoted = await _context.Votes.AnyAsync(v => v.UserId == user.Id);
            ViewBag.HasVoted = hasVoted;
            var candidates = await _context.Candidates.ToListAsync();
            return View(candidates);
        }
        // AJAX 投票
        [HttpPost]
        public async Task<IActionResult> VoteAjax(int candidateId)
        {
            var user = await _userManager.GetUserAsync(User);
            // 防止重複投票
            if (await _context.Votes.AnyAsync(v => v.UserId == user.Id))
                return Json(new { success = false, message = "你已經投過票了" });

            var vote = new Vote
            {
                UserId = user.Id,
                CandidateId = candidateId
            };
            _context.Votes.Add(vote);
            var candidate = await _context.Candidates.FindAsync(candidateId);
            if (candidate != null)
            {
                candidate.VoteCount++;
            }
            await _context.SaveChangesAsync();
            return Json(new { success = true, newCount = candidate.VoteCount });
        }
        // AJAX 刪除投票
        [HttpPost]
        public async Task<IActionResult> DeleteVoteAjax()
        {
            var user = await _userManager.GetUserAsync(User);
            var vote = await _context.Votes.FirstOrDefaultAsync(v => v.UserId == user.Id);
            if (vote != null)
            {
                var candidate = await _context.Candidates.FindAsync(vote.CandidateId);
                if (candidate != null)
                {
                    candidate.VoteCount--;
                }
                _context.Votes.Remove(vote);
                await _context.SaveChangesAsync();
                return Json(new { success = true, candidateId = candidate.Id, newCount = candidate.VoteCount });
            }
            return Json(new { success = false, message = "尚未投票" });
        }

        // GET: Votes
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.Votes.Include(v => v.Candidate).Include(v => v.User);
        //    return View(await applicationDbContext.ToListAsync());
        //}

        // GET: Votes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vote = await _context.Votes
                .Include(v => v.Candidate)
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vote == null)
            {
                return NotFound();
            }

            return View(vote);
        }

        // GET: Votes/Create
        public IActionResult Create()
        {
            ViewData["CandidateId"] = new SelectList(_context.Candidates, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Votes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,CandidateId")] Vote vote)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vote);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CandidateId"] = new SelectList(_context.Candidates, "Id", "Id", vote.CandidateId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", vote.UserId);
            return View(vote);
        }

        // GET: Votes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vote = await _context.Votes.FindAsync(id);
            if (vote == null)
            {
                return NotFound();
            }
            ViewData["CandidateId"] = new SelectList(_context.Candidates, "Id", "Id", vote.CandidateId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", vote.UserId);
            return View(vote);
        }

        // POST: Votes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,CandidateId")] Vote vote)
        {
            if (id != vote.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoteExists(vote.Id))
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
            ViewData["CandidateId"] = new SelectList(_context.Candidates, "Id", "Id", vote.CandidateId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", vote.UserId);
            return View(vote);
        }

        // GET: Votes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vote = await _context.Votes
                .Include(v => v.Candidate)
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vote == null)
            {
                return NotFound();
            }

            return View(vote);
        }

        // POST: Votes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vote = await _context.Votes.FindAsync(id);
            if (vote != null)
            {
                _context.Votes.Remove(vote);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoteExists(int id)
        {
            return _context.Votes.Any(e => e.Id == id);
        }
    }
}
