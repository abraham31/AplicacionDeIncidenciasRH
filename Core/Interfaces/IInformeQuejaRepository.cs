using Core.Entities;

namespace Core.Interfaces
{
    public interface IInformeQuejaRepository :IRepository<InformeQueja>
    {
        Task<InformeQueja> Actualizar(InformeQueja Entity);
    }
}
