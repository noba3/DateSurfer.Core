using Microsoft.AspNetCore.Mvc;
using MediatR;
using DateSurfer.Core.Application.Features.Membership.Commands;

namespace DateSurfer.Core.Web.Controllers;

public class HomeController : Controller
{
    private readonly IMediator _mediator;

    public HomeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CalculateFee([FromBody] CalculateMembershipFeeCommand request)
    {
        try
        {
            var fee = await _mediator.Send(request);
            return Json(new { success = true, fee });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, error = ex.Message });
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }
}