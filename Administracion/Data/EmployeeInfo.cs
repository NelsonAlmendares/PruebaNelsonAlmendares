using Administracion.Models;
using System.Data.SqlClient;
using System.Data;

namespace Administracion.Data
{
    /*
     * Esta clase será utilizada para poder almacenar las consultas hacia la base de datos
     * (En esta caso procedimientos almacenados para el llenado de las tablas)
     */
    public class EmployeeInfo
    {
        // Creamos una lista da datos, en esta se almacenaran todos los datos de la tabla [dbo].[DataEmpleado]
        public List<EmpleadoModelo> Shown() 
        {
            // En este objeto hacemos una instancia a la lista creada
            var ObjList = new List<EmpleadoModelo>();

            // Instanciamos un objeto para la  cadena de conexión con la base de datos:
            var cn = new Connection();
            // Mandamos a llamar el método que contiene el fragmento tomado del appseting con la información de la conexión
            using (var connect = new SqlConnection(cn.getConnectSQL())) 
            {
                // Abrimos la conexión
                connect.Open();

                // TODO: Estruncturas de los comandos de SQL =>
                SqlCommand GetEmployees = new SqlCommand("spShowData", connect);
                GetEmployees.CommandType = CommandType.StoredProcedure;

                // Vamos a leer los datos que provienen de
                using (var rd = GetEmployees.ExecuteReader()) 
                {
                    // Con el método Read vamos a recolectar cada uno de los datos que están en la consulta
                    while (rd.Read()) {
                        // Los datos se agregan a la lista creada como un objeto en la línea 17
                        ObjList.Add(new EmpleadoModelo()
                        {
                            /*
                             * Accedemos a los atributos del modelo,
                             * el objeto rd necesita del nombre del campo en la base
                             * dado que estamos llamando una vista, será el nombre de los ALIAS
                             */
                            ID_empleado = Convert.ToInt32(rd["ID"]),
                            FirstName = rd["Nombre"].ToString(),
                            LastName = rd["Apellidos"].ToString(),
                            DateBirth = rd["Nacimiento"].ToString(),
                            Position = rd["Posicion"].ToString(),
                            Amount = Convert.ToDouble(rd["Salario"])
                        });
                    }
                }
            }
            return ObjList;
        }

        public EmpleadoModelo GetDataInfo(int IdEmployee)
        {
            // Creamos un objeto para acceder a la clase del Modelo de datos
            var ObjList = new EmpleadoModelo();

            // Instancimos un objeto para la  cadena de conexión con la base de datos:
            var cn = new Connection();
            // Mandamos a llamar el método que contiene el fragmento tomado del appseting con la información de la conexión
            using (var connect = new SqlConnection(cn.getConnectSQL()))
            {
                // Abrimos la conexión
                connect.Open();

                // TODO: Estruncturas de los comandos de SQL =>
                SqlCommand GetEmployeeById = new SqlCommand("spShowByID", connect);
                // Enviamos el parámetro que recibimos en la función al parámetro del Procedimiento
                GetEmployeeById.Parameters.AddWithValue("@ID_Empleado", IdEmployee);
                GetEmployeeById.CommandType = CommandType.StoredProcedure;

                // Vamos a leer los datos que provienen de
                using (var rd = GetEmployeeById.ExecuteReader())
                {
                    // Con el método Read vamos a recolectar cada uno de los datos que están en la consulta
                    while (rd.Read())
                    {
                        ObjList.ID_empleado = Convert.ToInt32(rd["ID_Empleado"]);
                        ObjList.FirstName = rd["primer_nombre"].ToString();
                        ObjList.LastName = rd["segundo_nombre"].ToString();
                        ObjList.DateBirth = rd["fecha_nacimiento"].ToString();
                        ObjList.Position = rd["id_Puesto"].ToString();
                        ObjList.Amount = Convert.ToDouble(rd["salario"]);
                    }
                }
            }
            return ObjList;
        }

        /*
         * Con este metodo se van a guardar los datos por medio de un procedimiento almacenado
         * Creamos un objeto para llenar los parámetros de una manara más eficiente
         */
        public bool SaveData(EmpleadoModelo empReq) 
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
                    InsertData.Parameters.AddWithValue("@primer_nombre", empReq.FirstName);
                    InsertData.Parameters.AddWithValue("@segundo_nombre", empReq.LastName);
                    InsertData.Parameters.AddWithValue("@fecha_nacimiento", empReq.DateBirth);
                    InsertData.Parameters.AddWithValue("@id_Puesto", empReq.Position);
                    InsertData.Parameters.AddWithValue("@salario", empReq.Amount);
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

        public bool UpdateData(EmpleadoModelo empReq)
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
                    SqlCommand UpdateData = new SqlCommand("spUpdateData", connect);
                    // Enviamos el parámetro que recibimos en la función al parámetro del Procedimiento
                    UpdateData.Parameters.AddWithValue("@ID_Empleado", empReq.ID_empleado);
                    UpdateData.Parameters.AddWithValue("@primer_nombre", empReq.FirstName);
                    UpdateData.Parameters.AddWithValue("@segundo_nombre", empReq.LastName);
                    UpdateData.Parameters.AddWithValue("@fecha_nacimiento", empReq.DateBirth);
                    UpdateData.Parameters.AddWithValue("@id_Puesto", empReq.Position);
                    UpdateData.Parameters.AddWithValue("@salario", empReq.Amount);
                    UpdateData.CommandType = CommandType.StoredProcedure;
                    // Realizamos la ejecución de la consulta con los parámetros
                    UpdateData.ExecuteNonQuery();
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

        public bool DeleteData(EmpleadoModelo empReq)
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
                    SqlCommand DeleteData = new SqlCommand("DELETE FROM [dbo].[DataEmpleado] WHERE ID_Empleado = ?;", connect);
                    // Enviamos el parámetro que recibimos en la función al parámetro del Procedimiento
                    DeleteData.Parameters.AddWithValue("ID_Empleado", empReq.ID_empleado);
                    DeleteData.CommandType = CommandType.Text;
                    // Realizamos la ejecución de la consulta con los parámetros
                    DeleteData.ExecuteNonQuery();
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
