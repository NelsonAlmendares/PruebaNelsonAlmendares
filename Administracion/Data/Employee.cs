using Administracion.Models;
using System.Data.SqlClient;
using System.Data;

namespace Administracion.Data
{
    public class Employee
    {
        public List<NominaModelo> ShownEmployees()
        {
            // En este objeto hacemos una instancia a la lista creada
            var ObjList = new List<NominaModelo>();

            // Instanciamos un objeto para la  cadena de conexión con la base de datos:
            var cn = new Connection();
            // Mandamos a llamar el método que contiene el fragmento tomado del appseting con la información de la conexión
            using (var connect = new SqlConnection(cn.getConnectSQL()))
            {
                // Abrimos la conexión
                connect.Open();

                // TODO: Estruncturas de los comandos de SQL =>
                SqlCommand GetEmployees = new SqlCommand("spShoOneData", connect);
                GetEmployees.CommandType = CommandType.StoredProcedure;

                // Vamos a leer los datos que provienen de
                using (var rd = GetEmployees.ExecuteReader())
                {
                    // Con el método Read vamos a recolectar cada uno de los datos que están en la consulta
                    while (rd.Read())
                    {
                        // Los datos se agregan a la lista creada como un objeto en la línea 17
                        ObjList.Add(new NominaModelo()
                        {
                            /*
                             * Accedemos a los atributos del modelo,
                             * el objeto rd necesita del nombre del campo en la base
                             * dado que estamos llamando una vista, será el nombre de los ALIAS
                             */
                            ID_Nomina = Convert.ToInt32(rd["ID"]),
                            Position = rd["Empleado"].ToString(),
                        });
                    }
                }
            }
            return ObjList;
        }
    }
}
