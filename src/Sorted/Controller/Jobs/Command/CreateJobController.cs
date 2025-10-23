using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
public class CreateJobController : Controller
{
    private readonly IMediator _mediator;

    public CreateJobController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewJob([FromBody] CreateJobCommand request)
    {
        string value = await _mediator.Send(request);
        return Json(value);
    }
}