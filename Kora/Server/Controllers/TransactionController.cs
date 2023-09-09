using AutoMapper;
using Kora.Server.Services;
using Kora.Shared.Models;
using Kora.Shared.ModelsDto;
using Microsoft.AspNetCore.Mvc;

namespace Kora.Server.Controllers;


[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;
    private readonly IMapper _mapper;
    
    public TransactionController(ITransactionService transactionService, IMapper mapper)
    {
        _transactionService = transactionService;
        _mapper = mapper;
    }

    
    [HttpGet("GetAllTransaction")]
    public async Task<ActionResult<List<TransactionDto>>> GetAllTransaction()
    {
        var transactions = await _transactionService.GetAllTransaction();
        var transactionDtos = _mapper.Map<List<TransactionDto>>(transactions);
        return Ok(transactionDtos);
    }

    [HttpPost("GetClientTransaction")]
    public async Task<ActionResult<List<TransactionDto>>> GetClientTransaction(ClientTrans clientTrans)
    {
        var transactions = await _transactionService.GetClientTransaction(clientTrans.Tel);
        var transactionDto = _mapper.Map<List<TransactionDto>>(transactions);
        return Ok(transactionDto);
    } 

    [HttpPost("GetKiosqueTransaction")]
    public async Task<ActionResult<List<TransactionDto>>> GetKiosqueTransaction(KiosqueCode kiosqueCode)
    {
        var transactions = await  _transactionService.GetKiosqueTransaction(kiosqueCode.Code);
        if (transactions != null)
        {
            var transDto = _mapper.Map<List<TransactionDto>>(transactions);
            return Ok(transDto);
        }
        else
        {
            return NotFound();
        }
    }
    
}