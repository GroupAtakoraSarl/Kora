using System.Net.Http.Json;
using Kora.Shared.Models;
using Kora.Shared.ModelsDto;

namespace Kora.Client.Services;

public class TransactionService : ITransactionService
{
    private readonly HttpClient _httpClient;

    public TransactionService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    
    public async Task<List<TransactionDto>> GetAllTransaction()
    {
        return await _httpClient.GetFromJsonAsync<List<TransactionDto>>("api/transaction/GetAllTransaction");
    }
}