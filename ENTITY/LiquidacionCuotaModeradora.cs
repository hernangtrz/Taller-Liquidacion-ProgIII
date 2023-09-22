using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class LiquidacionCuotaModeradora
    {
        public int numeroLiquidacion {  get; set; }
        public int idPaciente { get; set; } 
        public int tipoAfilacion { get; set; }
        public Double salarioDevengado { get; set; }    
        public Double valorHospitalizacion { get; set; }    
        public Double valorCuotaModeradora { get; set; }
        public Double Tarifa { get; set; }



        public LiquidacionCuotaModeradora(int numeroLiquidacion, int idPaciente, int tipoAfilacion, double salarioDevengado, double valorHospitalizacion, double tarifa)
        {
            this.numeroLiquidacion = numeroLiquidacion;
            this.idPaciente = idPaciente;
            this.tipoAfilacion = tipoAfilacion;
            this.salarioDevengado = salarioDevengado;
            this.valorHospitalizacion = valorHospitalizacion;
            this.Tarifa = tarifa;
        }





    }
}
