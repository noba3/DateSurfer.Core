using Microsoft.AspNetCore.Mvc;
using MediatR;
using DateSurfer.Core.Application.Features.Membership.Commands;
using DateSurfer.Core.Domain.Enums;

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
    public async Task<IActionResult> CalculateFee([FromBody] CalculateFeeRequest request)
    {
        try
        {
            var fee = await _mediator.Send(new CalculateMembershipFeeCommand(
                request.UserId,
                (MembershipType)request.MembershipType));

            return Json(new { success = true, fee });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, error = ex.Message });
        }
    }

    public class CalculateFeeRequest
    {
        public int UserId { get; set; }
        public int MembershipType { get; set; }
    }
}