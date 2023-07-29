using AutoMapper;
using Kora.Models;
using Kora.Server.Data;
using Kora.Server.ModelsDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kora.Server.Services;

public class TransactionService
{

    private readonly KoraDbContext _dbContext;
    private readonly ICompteService _compteService;

    public TransactionService(KoraDbContext dbContext, ICompteService compteService)
    {

        _compteService = compteService;
    }
    
    public async Task<List<string>> GetTransactions()
    {
        return await _compteService.GetTransaction();
    }
}