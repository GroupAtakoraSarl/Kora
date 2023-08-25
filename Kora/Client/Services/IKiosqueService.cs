using Kora.Shared.Models;
using Kora.Shared.ModelsDto;

namespace Kora.Client.Services;

public interface IKiosqueService
{
    Task<List<Kiosque>> GetAllKiosque();
    Task<List<KiosqueDto>> GetKiosqueByAdresse(string adresseKiosque);
    Task<Kiosque> AddKiosque(Kiosque kiosque);
    Task<bool> ChargeSolde(ChargeCompteDto chargeCompteDto);
    Task<bool> DeleteKiosque(KiosqueDeleteDto kiosqueDeleteDto);
}