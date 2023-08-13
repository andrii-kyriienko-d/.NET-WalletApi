using Microsoft.AspNetCore.Mvc;
using WalletApi.Core.Models.WalletTransaction;
using WalletApi.Core.Services.Interfaces;

namespace WalletApi.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TransactionsController : ControllerBase
{
    private readonly IWalletTransactionService _walletTransactionService;

    public TransactionsController(IWalletTransactionService walletTransactionService)
    {
        _walletTransactionService = walletTransactionService;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTransactionAsync([FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var model = await _walletTransactionService.GetAsync(id, cancellationToken);

        return Ok(model);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransactionAsync([FromBody] WalletTransactionUpsertModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var id = await _walletTransactionService.CreateTransactionAsync(model, cancellationToken);

        return CreatedAtAction("GetTransaction", new { id }, id);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateTransactionAsync([FromRoute] Guid id,
        [FromBody] WalletTransactionUpsertModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _walletTransactionService.UpdateTransactionAsync(id, model, cancellationToken);

        return NoContent();
    }
}