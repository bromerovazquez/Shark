using Drim.Interfaces.MVC.Repositorys;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Drim.Interfaces.MVC.Models
{
    public class BaseModel : IBaseModel
    {

        public readonly IManagerRepository _managerRepository;
        public readonly IWebDatosRepository _webDatosRepository;

        public BaseModel(IManagerRepository managerRepository, IWebDatosRepository webDatosRepository) 
        {
            _managerRepository = managerRepository;
            _webDatosRepository = webDatosRepository;
        }


        async Task<IEnumerable<SelectListItem>> IBaseModel.SearchArticuloKeyWord(string keyword)
        {

            var result_webdatos =  await _webDatosRepository.GetArticulosSelectByKeyWord(keyword);

            var result_manager = await  _managerRepository.GetArticulosSelectByKeyWord(keyword, result_webdatos.Select(s=> Convert.ToInt32(s.Value)).ToList());
            

            return result_manager;

        }
        async Task<IEnumerable<SelectListItem>> IBaseModel.SearchProveedorKeyWord(string keyword)
        {

            var result_manager = await _managerRepository.GetProveedoresSelectByKeyWord(keyword);


            return result_manager;

        }
    }
}
