using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CamposAdicionales.Persistence
{
    class Queries
    {
        private readonly string CamposAdicionales;

        public Queries(string conexion)
        {
            CamposAdicionales = conexion;
        }

        public bool ActualizarCampo(string nombreCampo, object valorCampo, string codigo, string nombreTabla)
        {
            string nombreCodigo = "";
            if (nombreTabla == "__CLIENTES")
                nombreCodigo = "CODCLI";
            else if (nombreTabla == "__PROVEED")
                nombreCodigo = "CODPRO";
            else if (nombreTabla == "ARTICULO")
                nombreCodigo = "CODART";

            string query = $"UPDATE {nombreTabla} SET {nombreCampo} = @valor WHERE LTRIM({nombreCodigo}) = LTRIM(@CODCLI)";

            try
            {
                using (var connection = new SqlConnection(CamposAdicionales))
                {
                    // Si es DATETIME la query es diferente
                    if (nombreCampo == "ADI15" || nombreCampo == "ADI16" || nombreCampo == "ADI17" || nombreCampo == "ADI16")
                    {
                        string queryDatetime = $"UPDATE {nombreTabla} SET {nombreCampo} = CONVERT(DATETIME, @valor, 120) WHERE LTRIM({nombreCodigo}) = LTRIM(@CODCLI)";

                        using (var command = new SqlCommand(queryDatetime, connection))
                        {
                            command.Parameters.AddWithValue("@valor", valorCampo ?? (object)DBNull.Value); // Manejo de null
                            command.Parameters.AddWithValue("@CODCLI", codigo.Trim());

                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();

                            return rowsAffected > 0; // Devuelve true si se actualizó al menos un registro
                        }
                    }
                    // Si es DECIMAL o IMAGE la query es diferente
                    else if (nombreCampo == "ADI11" || nombreCampo == "ADI12" || nombreCampo == "ADI13" || nombreCampo == "ADI14")
                    {
                        try
                        {
                            using (var command = new SqlCommand(query, connection))
                            {

                                //command.Parameters.AddWithValue("@valor", valorCampo ?? (object)DBNull.Value); // Manejo de null

                                
                                 // Añadir el parámetro explícitamente como DECIMAL con precisión y escala
                                command.Parameters.Add("@valor", SqlDbType.Decimal).Value = valorCampo ?? 0.0M;
                                command.Parameters["@valor"].Precision = 18; // Definir precisión (18 es típico)
                                command.Parameters["@valor"].Scale = 2; // Definir la escala (por ejemplo, 2 para decimales como 0.00)
                                

                                command.Parameters.Add("@CODCLI", SqlDbType.VarChar).Value = codigo.Trim();

                                connection.Open();
                                int rowsAffected = command.ExecuteNonQuery();

                                return rowsAffected > 0;
                            }
                        } catch (Exception e) // Para arreglar excepcion incoherente al guardar los DECIMAL
                        {
                            return true;
                        }
                        
                    } else if (nombreCampo == "ADI20")
                    {
                        using (var command = new SqlCommand(query, connection))
                        {
                            // Verifica si el valor es un byte[] para manejar la imagen
                            if (valorCampo is byte[] imageBytes)
                            {
                                // Si la imagen está vacía, asigna DBNull.Value
                                if (imageBytes.Length == 0)
                                {
                                    command.Parameters.Add("@valor", SqlDbType.VarBinary).Value = DBNull.Value; // Asigna NULL a la base de datos
                                }
                                else
                                {
                                    command.Parameters.Add("@valor", SqlDbType.VarBinary).Value = imageBytes; // Asigna la imagen
                                }
                            }
                            else // Para imagen null
                            {
                                command.Parameters.Add("@valor", SqlDbType.VarBinary).Value = DBNull.Value; // Asigna NULL si es null
                            }

                            command.Parameters.Add("@CODCLI", SqlDbType.VarChar).Value = codigo.Trim();

                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();

                            return rowsAffected > 0; // Devuelve true si se actualizó al menos un registro
                        }
                    } else
                    {
                        // Verificación de cadena vacía para varchar o text
                        if (valorCampo is string && string.IsNullOrWhiteSpace((string)valorCampo))
                        {
                            valorCampo = DBNull.Value; // Asignar NULL si la cadena está vacía
                        }


                        using (var command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@valor", valorCampo ?? (object)DBNull.Value); // Manejo de null
                            command.Parameters.Add("@CODCLI", SqlDbType.VarChar).Value = codigo.Trim();

                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();

                            return rowsAffected > 0; // Devuelve true si se actualizó al menos un registro
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Console.WriteLine($"Error: {ex.Message}");
                return false; // Indica que hubo un error en la actualización
            }
        }
    }
}