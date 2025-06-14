using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
public class CreateUserController : Controller
{
    private readonly IMediator _mediator;

    public CreateUserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewUser([FromBody] CreateUserCommand request)
    {

        string value = await _mediator.Send(request);
        return Json(value);
    }
}