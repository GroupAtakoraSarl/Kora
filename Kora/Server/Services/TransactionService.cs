using AutoMapper;
using Kora.Server.Data;
using Kora.Server.ModelsDto;

namespace Kora.Server.Services;

public class TransactionService : ITransactionService
{

    private readonly KoraDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ICompteService _compteService;

    public TransactionService(KoraDbContext dbContext, IMapper mapper, ICompteService compteService)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _compteService = compteService;
    }


    public async Task<TransactionDto> GetInfoTransac(DateTime date, decimal montant, string numExp, string type)
    {

        var info = await _compteService.DepotCompte(numExp, montant);

        var infoDepot = new TransactionDto
        {
            DateTransaction = DateTime.Now,
            Montant = montant,
            Type = "Depot",
            NumExp = numExp
        };
        
        return infoDepot;
    }
}