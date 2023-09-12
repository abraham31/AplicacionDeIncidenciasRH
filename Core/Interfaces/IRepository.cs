using Core.Entities;
using System.Linq.Expressions;

namespace Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task Crear(T entity);
        Task<List<T>> ObtenerTodos(Expression<Func<T, bool>>? filtro = null, string? incluirPropiedades = null);
        Task<List<T>> ObtenerTodosAsync(Expression<Func<T, bool>>? filtro = null, string? incluirPropiedades = null);
        Task<IEnumerable<T>> ObtenerPorPeriodoAsync(Expression<Func<T, DateTime>> fechaSelector, DateTime fechaInicio, DateTime fechaFin);
        Task<T> GetByIdAsync(int id);
        Task<T> Obtener(Expression<Func<T, bool>> filtro = null, bool tracked = true, string? incluirPropiedades = null);
        Task Remover(T entidad);
        Task Grabar();
    }
}
