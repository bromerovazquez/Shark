using Drim.Interfaces.MVC.Data;
using Drim.Interfaces.MVC.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Drim.Interfaces.MVC.Repositorys
{
    public class WebDatosRepository : IWebDatosRepository
    {
        public readonly WebDatosContext _WebDatosContext;

        public WebDatosRepository(IDbContextFactory<WebDatosContext> WebDatosContext) 
        {
            _WebDatosContext = WebDatosContext.CreateDbContext();
        }


        async Task<IEnumerable<SelectListItem>> IWebDatosRepository.GetArticulosSelectByKeyWord(string keyword)
        {
           return  await _WebDatosContext.dtArticulosPorIdiomas.Where(x=> x.ID_IDIOMA == 1 && !string.IsNullOrEmpty(x.NOMBRE) && x.NOMBRE.Contains(keyword) ).Select(s => new SelectListItem()
            {
                Value = s.CODARTICULO.ToString(),
                Text = s.NOMBRE

            }).ToListAsync();
        }

        dtArticulosPorIdioma IWebDatosRepository.GetArticuloByCodArticulo(int codArticulo) 
        {
            return  _WebDatosContext.dtArticulosPorIdiomas.Where(x => x.ID_IDIOMA == 1 && x.CODARTICULO == codArticulo).FirstOrDefault();
        }
    }
}
