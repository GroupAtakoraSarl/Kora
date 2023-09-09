using Kora.Shared.Models;
using Kora.Shared.ModelsDto;

namespace Kora.Server.Services;

public interface IKiosqueService
{
    Task<List<Kiosque>> GetAllKiosque();
    Task<string> GenerateRandomCode();
    Task<string> GenerateRandomKey();
    Task<List<Kiosque>> GetKiosqueByAdresse(string adresseKiosque);
    Task<Kiosque> AddKiosque(Kiosque kiosque);

    bool EnregistrerKiosque(string nomKiosque, string keyKiosque, string password);
    KiosqueTrans ConnecterKiosque(string codeKiosque, string password);
    Task<bool> ChargeSolde(decimal solde, string contactAgence);
    Task<decimal?> GetKiosqueSolde(string code);


    Task<bool> DeleteKiosque(string contactKiosque);
}