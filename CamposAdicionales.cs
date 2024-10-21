using a3ERPActiveX;
using CamposAdicionales.Vistas;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CamposAdicionales
{
    public class CamposAdicionales
    {
        private static Enlace a3enlace = null;

        public static string conexion = "Data Source=AUXILIAR-MSI\\A3ERP;Initial Catalog=EjemploDDBB;User Id=Sa;Password=demo";

        //public static string conexion = "Data Source=PABLO-ERP\\A3ERP;Initial Catalog=DEMO;User Id=Sa;Password=demo";

        public static bool ventanaActiva = false;
        public static bool ActivarVentana = true;
        public List<string[]> refe;

        private SqlConnection connEmpresa = new SqlConnection();
        SqlCommand cmd;

        string Usuario = "";
        string Servidor = "";
        string BaseDeDatos = "";
        string Password = "";


        //COMPARTIDO ENTRE EVENTOS DE DLL Y EVENTOS DE MENÚ
        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        private string buildConnection(string conexionNexus)
        {
            string[] Copia;
            Copia = conexionNexus.Split(new Char[] { ';' });
            foreach (string item in Copia)
            {
                string[] items = item.Split(new Char[] { '=' });
                if (items[0].ToUpper() == "USER ID")
                    Usuario = items[1];
                else if (items[0].ToUpper() == "INITIAL CATALOG")
                    BaseDeDatos = items[1];
                else if (items[0].ToUpper() == "DATA SOURCE")
                    Servidor = items[1];
                else if (items[0].ToUpper() == "PASSWORD")
                    Password = items[1];
            }
            return string.Format(conexion, Servidor, BaseDeDatos, Usuario, Password);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        private const UInt32 WM_CLOSE = 0x0010;

        public object[] ListaProcedimientos()
        {
            object[] result = { "INICIAR", "FINALIZAR","Opcion", "AntesDeGuardarDocumentoV2", "DespuesDeNuevoFormulario", "AntesDeDestruirFormulario", "DespuesDeCargarDocumentoV2", "DespuesDeGuardarDocumentoV2" };

            return result;
        }

        // Une nuestra programacion con la base de datos y el programa
        public void Iniciar(string connEmpresaNexus, string connSistemaNexus)
        {
            connEmpresa.ConnectionString = buildConnection(connEmpresaNexus);
            connEmpresa.Open();

            a3enlace = new Enlace();
            a3enlace.RaiseOnException = true;
            a3enlace.VerBarraDeProgreso = true;
            a3enlace.Iniciar("");

            cmd = new SqlCommand();
            cmd.Connection = connEmpresa;
        }

        [SuppressUnmanagedCodeSecurity]
        internal static class UnsafeNativeMethods
        {
            [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
            internal static extern int GetWindowText(IntPtr hWnd, [Out] StringBuilder lpString, int nMaxCount);

            [DllImport("user32.dll", SetLastError = true)]
            internal static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        }

        public void DespuesDeGuardarDocumentoV2(string documento, double idDoc, int estado)
        {
            
        }

        public bool AntesDeGuardarDocumentoV2(string Documento, double IdDoc, ref object Cabecera, ref object Lineas, int estado)
        {
            try
            {
                if (estado != 2)
                {
                    
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        /**
         *  Recibe como parámetro el id de la opción y el id del documento.
         */
        public void Opcion( string IdOpcion, double idDoc )
        {
            
            // La opción de guardar campos adicionales solo tiene que aparecer en Clientes, Artículos y Proveedores.
            if ((IdOpcion.ToUpper() == "CLIENTES") || (IdOpcion.ToUpper() == "ARTICULOS") || (IdOpcion.ToUpper() == "PROVEEDORES"))
            {
                // Creo el formulario (se muestra al inicializarse)
                var camposForm = new Campos(conexion, idDoc, IdOpcion);
            }
        }


        public void DespuesDeNuevoFormulario(ref string ClaseFormulario, int HandleFormulario)
        {
            
            
        }

        public void AntesDeDestruirFormulario(string ClaseFormulario, int HandleFormulario)
        {
            
        }

        public object DespuesDeCargarDocumentoV2(string Documento, decimal IdDoc, object Cabecera, object Lineas, int Estado)
        {
            try
            {
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en DespuesDeCargarDocumentoV2: " + ex.Message);
            }
            return null;
        }

        private object GetValorAntes(object[] Datos, string Campo)
        {
            object result = null;
            for (int i = 1; i <= Convert.ToInt16((Datos[1] as object[])[0]); i++)
            {
                object[] item = (Datos[1] as object[])[i] as object[];
                if (item[0].ToString().ToUpper() == Campo.ToUpper())
                    result = item[1];
            }
            return result;
        }
    }
}