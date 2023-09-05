using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task Crear(T entity);
        Task<List<T>> ObtenerTodos(Expression<Func<T, bool>>? filtro = null, string? incluirPropiedades = null);

        Task<T> Obtener(Expression<Func<T, bool>> filtro = null, bool tracked = true, string? incluirPropiedades = null);

        Task Remover(T entidad);

        Task Grabar();
    }
}
