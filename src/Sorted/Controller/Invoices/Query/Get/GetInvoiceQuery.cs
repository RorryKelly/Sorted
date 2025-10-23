using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
public class GetInvoiceController : Controller
{
    private readonly IMediator _mediator;

    public GetInvoiceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> GetNewInvoice([FromBody] GetInvoiceQuery request)
    {
        List<GetInvoiceResponse> value = await _mediator.Send(request);
        return Json(value);
    }
}