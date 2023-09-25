using BLL;
using ENTITY;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion
{
    internal class Presentacion
    {
        static void Main(string[] args)
        {
            LiquidacionCuotaModeradoraGUI liquidacionCuotaModeradoraGUI = new LiquidacionCuotaModeradoraGUI();
            liquidacionCuotaModeradoraGUI.Menu();


        }

    }
}
