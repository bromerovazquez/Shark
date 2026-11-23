using Drim.Interfaces.MVC.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Drim.Interfaces.MVC.Repositorys
{
    public interface IWebDatosRepository
    {

        Task<IEnumerable<SelectListItem>> GetArticulosSelectByKeyWord(string keyword);

        dtArticulosPorIdioma GetArticuloByCodArticulo(int codArticulo);
    }
}
