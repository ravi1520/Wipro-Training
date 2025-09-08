using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize(Roles = "User")]
public class UserController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: /User/TaskList
   public async Task<IActionResult> TaskList()
{
    var user = await _userManager.GetUserAsync(User);
    if (user == null)
    {
        return Challenge(); // redirect to login if not authenticated
    }

    var tasks = await _context.Tasks
        .Where(t => t.UserId == user.Id)
        .ToListAsync();

    return View(tasks);
}


    // GET: /User/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: /User/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TaskItem task)
    {
        var user = await _userManager.GetUserAsync(User);

        if (ModelState.IsValid)
        {
            task.UserId = user.Id;
            _context.Add(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(TaskList));
        }

        return View(task);
    }

    // GET: /User/Edit/5
    [Authorize(Policy = "CanEditTask")]
    public async Task<IActionResult> Edit(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == user.Id);

        if (task == null)
        {
            return NotFound();
        }

        return View(task);
    }

    // POST: /User/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "CanEditTask")]
    public async Task<IActionResult> Edit(int id, TaskItem task)
    {
        var user = await _userManager.GetUserAsync(User);

        if (id != task.Id)
            return BadRequest();

        var existingTask = await _context.Tasks.AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == user.Id);

        if (existingTask == null)
            return NotFound();

        if (ModelState.IsValid)
        {
            task.UserId = user.Id;
            _context.Update(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(TaskList));
        }

        return View(task);
    }

    // POST: /User/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == user.Id);

        if (task == null)
        {
            return NotFound();
        }

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(TaskList));
    }
}
