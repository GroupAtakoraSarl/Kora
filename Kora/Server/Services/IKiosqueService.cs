using Kora.Shared.Models;
using Kora.Shared.ModelsDto;

namespace Kora.Server.Services;

public interface IKiosqueService
{
    Task<List<Kiosque>> GetAllKiosque();
    Task<List<Kiosque>> GetKiosqueByAdresse(string adresseKiosque);
    Task<Kiosque> AddKiosque(Kiosque kiosque);
    Task<bool> DeleteKiosque(string contactKiosque);
}