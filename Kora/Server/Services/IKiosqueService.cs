using Kora.Models;
using Kora.Server.ModelsDto;

namespace Kora.Server.Services;

public interface IKiosqueService
{
    Task<List<KiosqueDto>> GetAllKiosque();
    Task<Kiosque> AddKiosque(Kiosque kiosque);
    Task<bool> DeleteKiosque(int contactKiosque);
}