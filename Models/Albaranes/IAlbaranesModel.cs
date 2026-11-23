using Drim.Interfaces.MVC.ViewModels;

namespace Drim.Interfaces.MVC.Models
{
    public interface  IAlbaranesModel
    {

        string GeneraAlbaranesDevolucionTraspasos(List<TraspasoProveedorOrigenDestino> traspasosY,bool? IsSeparadoPorTienda);

    }
}
