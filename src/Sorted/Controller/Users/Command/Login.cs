using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
public class LoginController : Controller
{
    private readonly IMediator _mediator;

    public LoginController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewUser([FromBody] LoginCommand request)
    {
        string value = await _mediator.Send(request);
        return Json(value);
    }
}