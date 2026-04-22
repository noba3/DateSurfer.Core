using DateSurfer.Core.Application.Features.Membership.Commands;
using DateSurfer.Core.Domain.Entities;
using DateSurfer.Core.Domain.Enums;
using DateSurfer.Core.Infrastructure.Data;
using DateSurfer.Core.Web.Models;
using DateSurfer.Core.Web.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DateSurfer.Core.Web.Controllers;

public class HomeController : Controller
{
    private readonly IMediator _mediator;
    private readonly ITestUserService _testUserService;
    private readonly ApplicationDbContext _context;

    public HomeController(IMediator mediator, ITestUserService testUserService, ApplicationDbContext context)
    {
        _mediator = mediator;
        _testUserService = testUserService;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var model = new HomeViewModel
        {
            TestUsers = await _testUserService.GetTestUsersAsync(),
            SelectedUserId = 1  // Standard
        };
        return View(model);
    }

    public IActionResult About()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Contact()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Contact(ContactModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var message = new ContactMessage
        {
            Name = model.Name,
            Email = model.Email,
            Message = model.Message,
            CreatedAt = DateTime.UtcNow
        };
        _context.ContactMessages.Add(message);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Vielen Dank für deine Nachricht!";
        return RedirectToAction("ThankYou");
    }

    [HttpPost]
    public async Task<IActionResult> Index(int userId)
    {
        var model = new HomeViewModel
        {
            TestUsers = await _testUserService.GetTestUsersAsync(),
            SelectedUserId = userId
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> CalculateFee(int userId, int membershipType)
    {
        var model = new HomeViewModel
        {
            TestUsers = await _testUserService.GetTestUsersAsync(),
            SelectedUserId = userId,
            SelectedMembershipType = membershipType
        };

        try
        {
            var fee = await _mediator.Send(new CalculateMembershipFeeCommand(
                userId,
                (MembershipType)membershipType));

            model.CalculatedFee = fee;
        }
        catch (Exception ex)
        {
            model.ErrorMessage = ex.Message;
        }

        return View("Index", model);
    }

    public IActionResult ThankYou()
    {
        return View();
    }

    public IActionResult Faq()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
}

public class CalculateFeeRequest
{
    public int UserId { get; set; }
    public int MembershipType { get; set; }
}