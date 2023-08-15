using Kora.Shared.Models;
using Kora.Shared.ModelsDto;

namespace Kora.Client.Services;

public interface IKiosqueService
{
    Task<List<Kiosque>> GetAllKiosque();
    Task<List<KiosqueDto>> GetKiosqueByAdresse(string adresseKiosque);
    Task<Kiosque> AddKiosque(Kiosque kiosque);
    Task<bool> ChargeSolde(double solde, string contactAgence);
    Task<bool> DeleteKiosque(int contactKiosque);
}