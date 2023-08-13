using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WalletApi.Core.Models.WalletUser;
using WalletApi.Core.Repositories.Interfaces;
using WalletApi.Core.Services.Interfaces;
using WalletApi.Data.Entities;

namespace WalletApi.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class UsersController : ControllerBase
{
    private readonly IWalletUserRepository _walletUserRepository;
    private readonly IWalletTransactionService _walletTransactionService;
    private readonly IMapper _mapper;

    public UsersController(IWalletUserRepository walletUserRepository, IMapper mapper,
        IWalletTransactionService walletTransactionService)
    {
        _walletUserRepository = walletUserRepository;
        _mapper = mapper;
        _walletTransactionService = walletTransactionService;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserAsync([FromRoute] Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var model = await _walletUserRepository.GetAsync(id);

        return Ok(_mapper.Map<WalletUserViewModel>(model));
    }

    [HttpPost]
    public async Task<IActionResult> CreateUserAsync([FromBody] WalletUserUpsertModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var record = _mapper.Map<WalletUser>(model);
        await _walletUserRepository.CreateAsync(record, cancellationToken);
        await _walletUserRepository.SubmitAsync(cancellationToken);

        return CreatedAtAction("GetUser", new { id = record.Id }, record.Id);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateUserAsync([FromRoute] Guid id,
        [FromBody] WalletUserUpsertModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var record = _mapper.Map<WalletUser>(model);
        record.Id = id;
        _walletUserRepository.Update(record);
        await _walletUserRepository.SubmitAsync();

        return NoContent();
    }

    [HttpGet("{id:guid}/transactions")]
    public async Task<IActionResult> GetTransactionsByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var transactions =
            await _walletTransactionService.GetByUserIdAsync(id, cancellationToken);

        return Ok(transactions);
    }
}