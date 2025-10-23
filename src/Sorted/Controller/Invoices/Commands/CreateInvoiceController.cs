using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
public class CreateInvoiceController : Controller
{
    private readonly IMediator _mediator;

    public CreateInvoiceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewInvoice([FromBody] CreateInvoiceCommand request)
    {
        string value = await _mediator.Send(request);
        return Json(value);
    }
}