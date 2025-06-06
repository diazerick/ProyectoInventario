using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebProductos.Models;
using System.Data.SqlClient;

namespace WebProductos.Datos
{
    public class D_Producto
    {
        private string cadenaConexion = "server=localhost;database=generacion33;user=sa;password=devo123";
        public List<E_Producto> ObtenerTodos()
        {
            /* Pasos para ejecutar un query a la BD
             1. Crear la conexion (SqlConnection y cadenaConexion)
             2  Abrir la conexion
             3. Crear el query a ejecutar
             4. Crear el objeto para ejecutar el query (SqlCommand)
             5. Ejecutar el query
             6. Cerrar la conexion 
             */

            //Crear la lista de productos vacia
            List<E_Producto> lista = new List<E_Producto>();
            //Creamos la cadena de conexion con la informacion de la BD que nos vamos a conectar
            cadenaConexion = "server=localhost;database=generacion33;user=sa;password=devo123";
            //Cadena conexion con authenticacion de windows
            //string cadenaConexion = "server=localhost;database=generacion33;Integrated Security=true";

            //Creamos el objeto para conectarnos a la BD (SqlConnection)
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            try
            {
                //Abrir la conexion
                conexion.Open();
                //Declarar el query que queremos ejecutar
                string query = "SELECT idProducto,descripcion,precio,fechaIngreso,disponible FROM Productos";
                //Creamos objeto para ejecutar el query (SqlCommand)
                //Al constructor le debemos pasar el query y la conexion
                SqlCommand comando = new SqlCommand(query, conexion);
                //Creamos un objeto SqlDataReader para almacenar los resultos que devuelva el query
                SqlDataReader reader = comando.ExecuteReader();
                //Leer los resultados
                while (reader.Read())
                {
                    //Creamos un producto
                    E_Producto producto = new E_Producto();
                    //Le asigno valores al producto, hay que convertir al tipo de dato que corresponda
                    producto.IdProducto = Convert.ToInt32(reader["idProducto"]);
                    producto.Descripcion = Convert.ToString(reader["descripcion"]);
                    producto.Precio = Convert.ToDecimal(reader["precio"]);
                    producto.FechaIngreso = Convert.ToDateTime(reader["fechaIngreso"]);
                    producto.Disponible = Convert.ToBoolean(reader["disponible"]);

                    //Agregar el producto a la lista
                    lista.Add(producto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //Al final siempre cerramos la conexion
                conexion.Close();
            }

            //Terminamos el metodo regresando la lista
            return lista;
        }

        public void AgregarProducto(E_Producto producto)
        {
            /* Pasos para ejecutar un query a la BD
            1. Crear la conexion (SqlConnection y cadenaConexion)
            2  Abrir la conexion
            3. Crear el query a ejecutar
            4. Crear el objeto para ejecutar el query (SqlCommand)
            5. Asignarle valores a los parametros del query (si es que tiene)
            6. Ejecutar el query
            7. Cerrar la conexion 
            */

            cadenaConexion = "server=localhost;database=generacion33;user=sa;password=devo123";
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            try
            {
                //Abrimos conexion
                conexion.Open();
                //Query a ejecutar
                string query = "INSERT INTO Productos(descripcion,precio,fechaIngreso,disponible) " +
                                             "VALUES(@parametro1,@precio,@fechaIngreso,@disponible)";
                //Objeto para ejecutar el query
                SqlCommand comando = new SqlCommand(query, conexion);
                //Asignamos valores a los parametros del query
                comando.Parameters.AddWithValue("@parametro1", producto.Descripcion);
                comando.Parameters.AddWithValue("@precio", producto.Precio);
                comando.Parameters.AddWithValue("@fechaIngreso", producto.FechaIngreso);
                comando.Parameters.AddWithValue("@disponible", producto.Disponible);
                //Ejecutar el query
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //Cerrar la conexion
                conexion.Close();
            } 
        }

        public E_Producto ObtenerProductoPorId(int idProducto)
        {
            //Crear un producto vacio
            E_Producto producto = new E_Producto();
            cadenaConexion = "server=localhost;database=generacion33;user=sa;password=devo123";
            //Creo objeto para conectarme a la BD
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            //Abrir la conexion
            conexion.Open();
            //query a ejecutar
            string query = "SELECT idProducto,descripcion,precio,fechaIngreso,disponible FROM Productos " +
                             "WHERE idProducto = @id";
            //Crear el objeto para ejecutar el query
            SqlCommand comando = new SqlCommand(query, conexion);
            //Asignamos valor al parametro del query
            comando.Parameters.AddWithValue("@id", idProducto);
            //Ejecutamos el query y guardamos los resultados en reader
            SqlDataReader reader = comando.ExecuteReader();
            //Si encontro un registro lo leemos
            if (reader.Read())
            {
                //Le asigno valores al producto, hay que convertir al tipo de dato que corresponda
                producto.IdProducto = Convert.ToInt32(reader["idProducto"]);
                producto.Descripcion = Convert.ToString(reader["descripcion"]);
                producto.Precio = Convert.ToDecimal(reader["precio"]);
                producto.FechaIngreso = Convert.ToDateTime(reader["fechaIngreso"]);
                producto.Disponible = Convert.ToBoolean(reader["disponible"]);
            }
            //Cerrar la conexion
            conexion.Close();
            return producto;
        }

        public void EditarProducto(E_Producto producto)
        {
            cadenaConexion = "server=localhost;database=generacion33;user=sa;password=devo123";
            //Creo objeto para conectarme a la BD
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            //Abrir la conexion
            conexion.Open();
            //query a ejecutar
            string query = "UPDATE Productos SET descripcion=@descripcion,precio=@precio,fechaIngreso=@fechaIngreso, " +
                             "disponible=@disponible WHERE idProducto=@idProducto";
            //Crear objeto para ejecutar el query
            SqlCommand comando = new SqlCommand(query, conexion);
            //Asignar valores a los parametros del query
            comando.Parameters.AddWithValue("@descripcion", producto.Descripcion);
            comando.Parameters.AddWithValue("@precio", producto.Precio);
            comando.Parameters.AddWithValue("@fechaIngreso", producto.FechaIngreso);
            comando.Parameters.AddWithValue("@disponible", producto.Disponible);
            comando.Parameters.AddWithValue("@idProducto", producto.IdProducto);
            //Ejecutamos el query
            comando.ExecuteNonQuery();
            //Cierro la conexion
            conexion.Close();
        }

        public void EliminarProducto(int idProducto)
        {
            cadenaConexion = "server=localhost;database=generacion33;user=sa;password=devo123";
            //Creo objeto para conectarme a la BD
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            try
            {
                //Abrir la conexion
                conexion.Open();
                //query a ejecutar
                string query = "DELETE Productos WHERE idProducto=@idProducto";
                //Crear objeto para ejecutar el query
                SqlCommand comando = new SqlCommand(query, conexion);
                //Asignar valores a los parametros del query
                comando.Parameters.AddWithValue("@idProducto", idProducto);
                //Ejecutamos el query
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //Cierro la conexion
                conexion.Close();
            }            
        }


    }
}