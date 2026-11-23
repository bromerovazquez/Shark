using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drim.Interfaces.MVC.Entities
{
    public class Interfaces_Steps_ResponseData
    {
        public long IdArchivo { get; set; }
        public long IdStep { get; set; }
        public long ParametroId { get; set; }
        public string Value { get; set; }
        public string TipoValueData { get; set; }
        public bool IsKey { get; set; }
        public int IndexValue { get; set; }
        public short? LineaValue { get; set; }
        public string NombreArchivo { get; set; }
        public int Row { get; set; }
    }
}
