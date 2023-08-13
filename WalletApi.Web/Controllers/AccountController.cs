using Microsoft.AspNetCore.Mvc;
using WalletApi.Core.Models.WalletAccount;
using WalletApi.Core.Services.Interfaces;

namespace WalletApi.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AccountController : ControllerBase
{
    private readonly IWalletTransactionService _walletTransactionService;
    private readonly IWalletAccountService _walletAccountService;

    public AccountController(IWalletTransactionService walletTransactionService,
        IWalletAccountService walletAccountService)
    {
        _walletTransactionService = walletTransactionService;
        _walletAccountService = walletAccountService;
    }

    [HttpGet("{id:guid}/transactions")]
    public async Task<IActionResult> GetTransactionsByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var transactions =
            await _walletTransactionService.GetByAccountIdAsync(id, cancellationToken);

        return Ok(transactions);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAccountAsync([FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var model = await _walletAccountService.GetAsync(id, cancellationToken);

        return Ok(model);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccountAsync([FromBody] WalletAccountUpsertModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var id = await _walletAccountService.CreateAccountAsync(model, cancellationToken);

        return CreatedAtAction("GetAccount", new { id }, id);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAccountAsync([FromRoute] Guid id,
        [FromBody] WalletAccountUpsertModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _walletAccountService.UpdateAccountAsync(id, model, cancellationToken);

        return NoContent();
    }
}