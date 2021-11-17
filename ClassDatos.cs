using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Capa_Entidad;

namespace Capa_Datos
{
   
    public class ClassDatos
    {
        SqlConnection Con = new SqlConnection(@"Server=DESKTOP-QE63DOI; Database=Tarea5; Integrated Security= true");
        public DataTable D_listar_clientes()
        {
            SqlCommand cmd = new SqlCommand("sp_listar_clientes", Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable D_buscar_clientes(ClassEntidad obje)
        {
            SqlCommand cmd = new SqlCommand("sp_buscar", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nombre", obje.Nombre);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
            
        }

        public string D_mantenimiento(ClassEntidad obje)
        {
            string accion;
            SqlCommand cmd = new SqlCommand("sp_mantenimiento_clientes", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@codigo", obje.Codigo);
            cmd.Parameters.AddWithValue("@nombre", obje.Nombre);
            cmd.Parameters.AddWithValue("@edad", obje.Edad);
            cmd.Parameters.AddWithValue("@telefono", obje.Telefono);
            cmd.Parameters.Add("@accion", SqlDbType.VarChar, 50).Value = obje.Accion;
            cmd.Parameters["@accion"].Direction = ParameterDirection.InputOutput;

            if (Con.State == ConnectionState.Open) Con.Close();
            Con.Open();
            int v = cmd.ExecuteNonQuery();
            accion = cmd.Parameters["@accion"].Value.ToString();
            Con.Close();
            return accion;
        }

    }
}
