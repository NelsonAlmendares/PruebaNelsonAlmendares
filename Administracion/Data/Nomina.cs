using Administracion.Models;
using System.Data.SqlClient;
using System.Data;
using System;

namespace Administracion.Data
{
    public class Nomina
    {
        // Creamos una lista da datos, en esta se almacenaran todos los datos de la tabla [dbo].[DataEmpleado]
        public List<NominaModelo> Shown()
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
                SqlCommand GetEmployees = new SqlCommand("spShowDataNomina", connect);
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
                            DatePicked = rd["Fecha"].ToString(),
                            ID_Empleado = rd["Empleado"].ToString(),
                            Position = rd["Puesto"].ToString(),
                            Payment = rd["Salario Final"].ToString(),
                            DaysNotWorked = rd["Días Laburados"].ToString()
                        });
                    }
                }
            }
            return ObjList;
        }

        /*
         * Quiero llamar a los datos pero este método necesita ser instanciado en el controlador
         * Estoy pensando como si fuera forms :(
         */
        public static DataTable LoadEmployee()
        {
            DataTable Data;
            var cn = new Connection();
            try
            {
                using (var connect = new SqlConnection(cn.getConnectSQL()))
                {
                    // Abrimos la conexión
                    connect.Open();
                    string RunQuery = "SELECT ID_Empleado AS ID, CONCAT(primer_nombre, '', segundo_nombre)  AS Empleado\r\n\tFROM [dbo].[DataEmpleado]";
                    SqlCommand CmbSelect = new SqlCommand(string.Format(RunQuery), connect);
                    SqlDataAdapter ADP = new SqlDataAdapter(CmbSelect);
                    Data = new DataTable();
                    ADP.Fill(Data);
                    return Data;

                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at: " + ex);
                return null;
            }
        }

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


        public bool Save(NominaModelo nompReq)
        {
            bool response;
            try
            {
                // Instancimos un objeto para la  cadena de conexión con la base de datos:
                var cn = new Connection();
                // Mandamos a llamar el método que contiene el fragmento tomado del appseting con la información de la conexión
                using (var connect = new SqlConnection(cn.getConnectSQL()))
                {
                    // Abrimos la conexión
                    connect.Open();

                    // TODO: Estruncturas de los comandos de SQL =>
                    SqlCommand InsertData = new SqlCommand("spInsertData", connect);
                    // Enviamos el parámetro que recibimos en la función al parámetro del Procedimiento
                    InsertData.Parameters.AddWithValue("@primer_nombre", nompReq.ID_Empleado);
                    InsertData.CommandType = CommandType.StoredProcedure;
                    // Realizamos la ejecución de la consulta con los parámetros
                    InsertData.ExecuteNonQuery();
                }
                response = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at: " + ex);
                response = false;
            }
            return response;
        }
    }
}
