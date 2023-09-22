using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Establecimiento
    {
        public String identificacion { get; set; }
        public String nombre { get; set; }
        public Double valorIngresosAnuales { get; set; }
        public Double valorGastosAnuales { get; set; }
        public int tiempoFuncionamiento { get; set; }
        public String tipoResponsabilidad { get; set; }
        public Double gananciasPesos { get; set; }
        public Double gananciasUVT { get; set; }
        public Double valorImpuesto { get; set; }

      
        

  

        public Establecimiento()
        {
        }

        public Establecimiento(string identificacion, string nombre, double valorIngresosAnuales, double valorGastosAnuales, int tiempoFuncionamiento, string tipoResponsabilidad, double gananciasPesos, double gananciasUVT)
        {
            this.identificacion = identificacion;
            this.nombre = nombre;
            this.valorIngresosAnuales = valorIngresosAnuales;
            this.valorGastosAnuales = valorGastosAnuales;
            this.tiempoFuncionamiento = tiempoFuncionamiento;
            this.tipoResponsabilidad = tipoResponsabilidad;
            this.gananciasPesos = gananciasPesos;
            this.gananciasUVT = gananciasUVT;
            this.valorImpuesto = CalcularImpuesto();
        }

        public Double CalcularImpuesto()
        {
            if(tipoResponsabilidad == "S" || tipoResponsabilidad == "s")
            {
                if(gananciasPesos < 25000)
                {
                    return gananciasPesos * 0;
                }
                if(gananciasUVT < 100 && gananciasPesos > 25000)
                {
                    return gananciasPesos * 0.05;
                }
                if(gananciasUVT >= 100 && gananciasUVT < 200)
                {
                    return gananciasPesos * 0.10;
                }
                if(gananciasUVT >= 200)
                {
                    return gananciasPesos * 0.15;
                }
            }
            else if(tipoResponsabilidad == "N" || tipoResponsabilidad == "n")
            {
                if (gananciasUVT < 100)
                {
                    return gananciasPesos * 0;
                }
                if(tiempoFuncionamiento < 6)
                {
                    return gananciasPesos * 0.01;
                }
                if (tiempoFuncionamiento >= 6 && tiempoFuncionamiento < 10)
                {
                    return gananciasPesos * 0.02;
                }
                if (tiempoFuncionamiento >= 10)
                {
                    return gananciasPesos * 0.03;
                }
            }
        
            return 0;
        }

    }
}
