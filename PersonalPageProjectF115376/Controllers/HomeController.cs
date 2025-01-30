using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalPageProjectF115376.Data;
using PersonalPageProjectF115376.Models;
using static PersonalPageProjectF115376.Models.Comment;

namespace PersonalPageProjectF115376.Controllers;

public class HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Gallery()
    {
        return View();
    }

    public IActionResult Downloads()
    {
        return View();
    }


    public IActionResult Comments()
    {
        var viewModel = new CommentsViewModel();
        viewModel.ExistingComments = context.Comments.OrderByDescending(c => c.Date).Take(20).ToList();
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> SubmitComment(CommentsViewModel viewModel)
    {
        if (!User.Identity.IsAuthenticated && string.IsNullOrWhiteSpace(viewModel.NewComment.Author))
        {
            viewModel.NewComment.Author = "Anonymous";
        }

        if (ModelState.IsValid)
        {
            var newComment = viewModel.NewComment;
            newComment.Date = DateTime.Now;

            if (User.Identity.IsAuthenticated)
            {
                newComment.UserId =userManager.GetUserId(User);
                newComment.IsAnonymous = false;
            }
            else
            {
                newComment.IsAnonymous = true;
            }

            context.Comments.Add(newComment);
            await context.SaveChangesAsync();
            return RedirectToAction("Comments");
        }

        viewModel.ExistingComments = context.Comments.OrderByDescending(c => c.Date).Take(20).ToList();
        return View("Comments", viewModel);
    }
}