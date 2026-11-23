using Microsoft.AspNetCore.Mvc.Rendering;

namespace Drim.Interfaces.MVC.Models
{
    public interface IBaseModel
    {

        Task<IEnumerable<SelectListItem>> SearchArticuloKeyWord(string keyword);

        Task<IEnumerable<SelectListItem>> SearchProveedorKeyWord(string keyword);

    }
}
