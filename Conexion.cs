using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EC3_LLATAS_PARIGUANA_ANDY_MARCELO
{
    internal class Conexion
    {
        private static string cadena = @"Server=(local);Database=VideoJuegoEC3;User Id=sa;Password=12345;Trusted_Connection=True;";

        public static SqlConnection Conectar()
        {
            return new SqlConnection(cadena);
        }

        public static void Registrar(string genero, string nombres, decimal precio, string idioma, int año)
        {
            try
            {
                using (SqlConnection conexion = Conectar())
                {
                    string query = "INSERT INTO Videojuego (GENERO, NOMBRES, PRECIO, IDIOMA, AÑO) VALUES (@GENERO, @NOMBRES, @PRECIO, @IDIOMA, @AÑO)";
                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@GENERO", genero);
                        cmd.Parameters.AddWithValue("@NOMBRES", nombres);
                        cmd.Parameters.AddWithValue("@PRECIO", precio);
                        cmd.Parameters.AddWithValue("@IDIOMA", idioma);
                        cmd.Parameters.AddWithValue("@AÑO", año);

                        conexion.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar: {ex.Message}");
            }
        }

        public static void Actualizar(int id, string genero, string nombres, decimal precio, string idioma, int año)
        {
            try
            {
                using (SqlConnection conexion = Conectar())
                {
                    string query = "UPDATE Videojuego SET GENERO = @GENERO, NOMBRES = @NOMBRES, PRECIO = @PRECIO, IDIOMA = @IDIOMA, AÑO = @AÑO WHERE ID = @ID";
                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@GENERO", genero);
                        cmd.Parameters.AddWithValue("@NOMBRES", nombres);
                        cmd.Parameters.AddWithValue("@PRECIO", precio);
                        cmd.Parameters.AddWithValue("@IDIOMA", idioma);
                        cmd.Parameters.AddWithValue("@AÑO", año);

                        conexion.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar: {ex.Message}");
            }
        }

        public static List<object[]> Mostrar()
        {
            List<object[]> videojuegos = new List<object[]>();

            try
            {
                using (SqlConnection conexion = Conectar())
                {
                    string query = "SELECT * FROM Videojuego";
                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        conexion.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                object[] videojuego = new object[reader.FieldCount];
                                reader.GetValues(videojuego);
                                videojuegos.Add(videojuego);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al mostrar datos: {ex.Message}");
            }

            return videojuegos;
        }

        public static object[] BuscarPorID(int id)
        {
            object[] videojuego = null;

            try
            {
                using (SqlConnection conexion = Conectar())
                {
                    string query = "SELECT * FROM Videojuego WHERE ID = @ID";
                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);

                        conexion.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                videojuego = new object[reader.FieldCount];
                                reader.GetValues(videojuego);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar por ID: {ex.Message}");
            }

            return videojuego;
        }
    }
}
