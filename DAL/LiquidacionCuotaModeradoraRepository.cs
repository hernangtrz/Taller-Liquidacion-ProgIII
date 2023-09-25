using ENTITY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class LiquidacionCuotaModeradoraRepository
    {
        private readonly string FileName = "Liquidaciones.txt";
        public void Guardar(LiquidacionCuotaModeradora Liquidacion)
        {
            FileStream file = new FileStream(FileName, FileMode.Append);
            StreamWriter writer = new StreamWriter(file);
            writer.WriteLine($"{Liquidacion.numeroLiquidacion};{Liquidacion.fechaLiquidacion};{Liquidacion.idPaciente};{Liquidacion.tipoAfilacion};{Liquidacion.salarioDevengado};{Liquidacion.valorHospitalizacion};{Liquidacion.tarifa};{Liquidacion.valorLiquidoRealCuotaModeradora};{Liquidacion.pasoTopeMaximo};{Liquidacion.valorCuotaModeradora}");
            writer.Close();
            file.Close();

        }
        public List<LiquidacionCuotaModeradora> ConsultarTodos()
        {
            List<LiquidacionCuotaModeradora> liquidaciones = new List<LiquidacionCuotaModeradora>();
            FileStream file = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader reader = new StreamReader(file);
            string linea = string.Empty;
            while ((linea = reader.ReadLine()) != null)
            {

                LiquidacionCuotaModeradora liquidacion = Map(linea);
                liquidaciones.Add(liquidacion);
            }
            reader.Close();
            file.Close();
            return liquidaciones;
        }
        private LiquidacionCuotaModeradora Map(string linea)
        {
            LiquidacionCuotaModeradora liquidacion = new LiquidacionCuotaModeradora();
            char delimiter = ';';
            string[] matrizLiquidacion = linea.Split(delimiter);
            liquidacion.numeroLiquidacion = int.Parse(matrizLiquidacion[0]);
            liquidacion.fechaLiquidacion = matrizLiquidacion[1];
            liquidacion.idPaciente = int.Parse(matrizLiquidacion[2]);
            liquidacion.tipoAfilacion = matrizLiquidacion[3];
            liquidacion.salarioDevengado = Double.Parse(matrizLiquidacion[4]);
            liquidacion.valorHospitalizacion = Double.Parse(matrizLiquidacion[5]);
            liquidacion.tarifa = Convert.ToDouble(matrizLiquidacion[6]);
            liquidacion.valorLiquidoRealCuotaModeradora = Convert.ToDouble(matrizLiquidacion[7]);
            liquidacion.pasoTopeMaximo = matrizLiquidacion[8];
            liquidacion.valorCuotaModeradora = Convert.ToDouble(matrizLiquidacion[9]);


            return liquidacion;
        }
        private bool EsEncontrado(int numLiquidacion, int numBuscado)
        {
            return numLiquidacion == numBuscado;
        }
        public void Eliminar(int numeroLiquidacionEliminar)
        {
            List<LiquidacionCuotaModeradora> liquidaciones = new List<LiquidacionCuotaModeradora>();
            liquidaciones = ConsultarTodos();
            FileStream file = new FileStream(FileName, FileMode.Create);
            file.Close();
            foreach (var item in liquidaciones)
            {
                if (!EsEncontrado(item.numeroLiquidacion, numeroLiquidacionEliminar))
                {
                    Guardar(item);
                }

            }

        }
        public LiquidacionCuotaModeradora Buscar(int numLiquidacion)
        {
            List<LiquidacionCuotaModeradora> liquidaciones = ConsultarTodos();
            foreach (var item in liquidaciones)
            {
                if (EsEncontrado(item.numeroLiquidacion, numLiquidacion))
                {
                    return item;
                }
            }
            return null;
        }
        public LiquidacionCuotaModeradora Modificar(int numeroLiquidacion, Double valorHospitalizacion)
        {
            List<LiquidacionCuotaModeradora> liquidaciones = new List<LiquidacionCuotaModeradora>();
            liquidaciones = ConsultarTodos();
            FileStream file = new FileStream(FileName, FileMode.Create);
            file.Close();
            foreach (var item in liquidaciones)
            {
                if(numeroLiquidacion == item.numeroLiquidacion)
                {
                    item.valorHospitalizacion = valorHospitalizacion;
                    return item;
                }
            }
            return null;
        }
    }
}
