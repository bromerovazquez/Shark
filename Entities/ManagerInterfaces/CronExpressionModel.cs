using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Drim.Interfaces.MVC.Entities
{
    public class CronExpressionModel
    {
        public int Minutos { get; set; }
        public int Horas { get; set; }
        public int Meses { get; set; }
        public int DiasDeLaSemana { get; set; }
        public int DiasDelMes { get; set; }
        public string TipoFrecuencia { get; set; }
        public SelectList TiposFrecuencias { get; set; }
    }

}
