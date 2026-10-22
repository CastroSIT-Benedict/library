
using library.Data;
using library.Models;
using Microsoft.AspNetCore.Mvc;

namespace library.Controllers;

public class FeedbacksController : Controller
{
    private readonly FeedbackRepository _feedbacks;

    // ASP.NET Core provides the repository registered in Program.cs.
    public FeedbacksController(FeedbackRepository feedbacks)
    {
        _feedbacks = feedbacks;
    }

    // GET /Feedbacks
    // GET /Feedbacks
    // GET /Feedbacks
    public async Task<IActionResult> Index()
    {
        var feedbacks = await _feedbacks.GetAllAsync();
        return View(feedbacks);
    }

    // GET /Feedbacks/Create
    public IActionResult Create()
    {
        return View(new Feedback());
    }

    // POST /Feedbacks/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Feedback feedback)
    {
        if (!ModelState.IsValid)
        {
            return View(feedback);
        }

        await _feedbacks.AddAsync(feedback);
        return RedirectToAction(nameof(Index));
    }

    // GET /Feedbacks/Edit/5
    public async Task<IActionResult> Edit(long id)
    {
        var feedback = await _feedbacks.GetByIdAsync(id);

        if (feedback == null)
        {
            return NotFound();
        }

        return View(feedback);
    }

    // POST /Feedbacks/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Feedback feedback)
    {
        if (!ModelState.IsValid)
        {
            return View(feedback);
        }

        await _feedbacks.UpdateAsync(feedback);
        return RedirectToAction(nameof(Index));
    }

    // GET /Feedbacks/Delete/5
    public async Task<IActionResult> Delete(long id)
    {
        var feedback = await _feedbacks.GetByIdAsync(id);

        if (feedback == null)
        {
            return NotFound();
        }

        return View(feedback);
    }

    // POST /Feedbacks/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(long id)
    {
        await _feedbacks.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
