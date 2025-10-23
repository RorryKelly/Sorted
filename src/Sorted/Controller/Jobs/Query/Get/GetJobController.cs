using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
public class GetJobController : Controller
{
    private readonly IMediator _mediator;

    public GetJobController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> GetNewJob([FromBody] GetJobQuery request)
    {
        List<GetJobResponse> value = await _mediator.Send(request);
        return Json(value);
    }
}