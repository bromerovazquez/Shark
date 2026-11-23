using Drim.Interfaces.MVC.Connection;
using Drim.Interfaces.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace Drim.Interfaces.MVC.Controllers
{
    public class BaseController : Controller
    {


        private readonly IBaseModel _BaseModel;

        public BaseController(IBaseModel baseModel)
        {
            _BaseModel = baseModel;
        }




        [HttpGet]
        public async Task<ActionResult> SearchArticuloKeyWord(string keyword)
        {
            var articulos = await _BaseModel.SearchArticuloKeyWord(keyword);
            string articulosResponseJson = JsonConvert.SerializeObject(articulos);
            return Json(new { Result = articulosResponseJson });
        }


        [HttpGet]
        public async Task<ActionResult> SearchProveedorKeyWord(string keyword)
        {
            var articulos = await _BaseModel.SearchProveedorKeyWord(keyword);
            string articulosResponseJson = JsonConvert.SerializeObject(articulos);
            return Json(new { Result = articulosResponseJson });
        }


        [HttpGet]
        public async Task<ActionResult> GetColumnsQuerySql(string query, CommandType type, int top = 5)
        {

            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();

            int rowsCount = 0;

            using (DAL dal = new DAL(ConnectionCatalogDB.G001))
            {

                using (var rdr = dal.DataReader(query, type))
                {

                    while (rdr.Read())
                    {
                        Dictionary<string, object> newRow = new Dictionary<string, object>();

                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            if (!rdr.IsDBNull(i))
                            {
                                string fieldName = rdr.GetName(i);
                                newRow.Add(fieldName, rdr.GetValue(i));
                            }
                        }

                        result.Add(newRow);


                        rowsCount++;
                    }
                }
            }

            string columnsResponseJson = JsonConvert.SerializeObject(result);


            return Json(new { Result = columnsResponseJson });
        }

    }
}
