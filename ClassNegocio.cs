using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public class ClassNegocio
    {
        ClassDatos objd = new ClassDatos();

        public DataTable N_listar_clientes()
        {
            return objd.D_listar_clientes();
        }

        public DataTable N_buscar_cliente(ClassEntidad obje)
        {
            return objd.D_buscar_clientes(obje);
        }

        public string N_mantenimiento_clientes(ClassEntidad obje)
        {
            return objd.D_mantenimiento(obje);
        }
    }
}
