using ENTITY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EstablecimientoRepository
    {
        private readonly string FileName = "Establecimientos.txt";
        public void Guardar(Establecimiento establecimiento)
        {
            FileStream file = new FileStream(FileName, FileMode.Append);
            StreamWriter writer = new StreamWriter(file);
            writer.WriteLine($"{establecimiento.nombre};{establecimiento.identificacion};{establecimiento.valorIngresosAnuales};{establecimiento.valorGastosAnuales};{establecimiento.tiempoFuncionamiento};{establecimiento.tipoResponsabilidad};{establecimiento.gananciasPesos};{establecimiento.gananciasUVT};{establecimiento.valorImpuesto} ");
            writer.Close();
            file.Close();

        }
        public List<Establecimiento> ConsultarTodos()
        {
            List<Establecimiento> establecimientos = new List<Establecimiento>();
            FileStream file = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader reader = new StreamReader(file);
            string linea = string.Empty;
            while ((linea = reader.ReadLine()) != null)
            {

                Establecimiento establecimiento = Map(linea);
                establecimientos.Add(establecimiento);
            }
            reader.Close();
            file.Close();
            return establecimientos;
        }
        private Establecimiento Map(string linea)
        {
            Establecimiento establecimiento = new Establecimiento();
            char delimiter = ';';
            string[] matrizPersona = linea.Split(delimiter);
            establecimiento.nombre = matrizPersona[0];
            establecimiento.identificacion = matrizPersona[1];
            establecimiento.valorIngresosAnuales = Convert.ToDouble(matrizPersona[2]);
            establecimiento.valorGastosAnuales = Convert.ToDouble(matrizPersona[3]);
            establecimiento.tiempoFuncionamiento = int.Parse(matrizPersona[4]);
            establecimiento.tipoResponsabilidad = matrizPersona[5];

            return establecimiento;
        }
        private bool EsEncontrado(string identificacioRegistrada, string identificacionBuscada)
        {
            return identificacioRegistrada == identificacionBuscada;
        }
        public void Eliminar(string identificacion)
        {
            List<Establecimiento> establecimientos = new List<Establecimiento>();
            establecimientos = ConsultarTodos();
            FileStream file = new FileStream(FileName, FileMode.Create);
            file.Close();
            foreach (var item in establecimientos)
            {
                if (!EsEncontrado(item.identificacion, identificacion))
                {
                    Guardar(item);
                }

            }

        }
        public Establecimiento Buscar(string identificacion)
        {
            List<Establecimiento> establecimientos = ConsultarTodos();
            foreach (var item in establecimientos)
            {
                if (EsEncontrado(item.identificacion, identificacion))
                {
                    return item;
                }
            }
            return null;
        }
    }
}
