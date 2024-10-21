using a3ERPActiveX;
using CamposAdicionales.Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinControls.RichTextBox;
using Telerik.WinControls.UI;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace CamposAdicionales.Vistas
{
    public partial class Campos : RadForm
    {
        private string conexion, idOpcion;
        private double IDoc;
        private List<Control> controlesInput;
        // Contadores para asignar ADI correctamente
        private int contadorVarchar = 1;
        private int contadorDecimal = 11;
        private int contadorDatetime = 15;
        private bool textAsignado = false;
        private bool imageAsignado = false;
        // Contadores para verificar que no se pasan los limites de cantidades de valores
        int numVarchar;
        int numDecimal;
        int numDatetime;
        bool numText;
        bool numImage;
        // El numero de ADI actual (ej: ADI1,ADI2...ADI20)
        string actualADI;

        public Campos(string conexion, double IDoc, string idOpcion)
        {
            InitializeComponent();
            this.conexion = conexion;
            this.IDoc = IDoc;
            this.idOpcion = idOpcion;

            // Inicializo los contadores a sus valores correspondientes
            this.numVarchar = 0;
            this.numDecimal = 0;
            this.numDatetime = 0;
            this.numText = false;
            this.numImage = false;

            this.actualADI = "";

            // Inicializo una nueva lista de los controles de input
            controlesInput = new List<Control>();

            // Cargo los campos del formulario y lo muestro
            this.CargarCampos(); 
        }

        /*
         * Método al clicar el botón de 'Guardar Cambios'.
         */
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Llamar al método de actualización de campos
                Dictionary<string, string> diccionario = leerFichero();
                AsignarColumnas(diccionario);

                // Mensaje de confirmación
                MessageBox.Show("Los campos han sido guardados correctamente.");
            }
            catch (Exception excepcion)
            {
                MessageBox.Show(excepcion.Message);
            }
        }

        /*
         * Método que carga los campos del txt correspondiente en el formulario.
         */
        public void CargarCampos()
        {
            tableLayoutPanel.Refresh();
            // Limpia el TableLayoutPanel antes de agregar nuevos controles
            tableLayoutPanel.Controls.Clear();

            // Limpiar los estilos de fila existentes
            tableLayoutPanel.RowStyles.Clear();

            // Obtener la cantidad de campos del diccionario
            Dictionary<string, string> diccionarioNombreTipo = leerFichero();
            int numberOfRows = diccionarioNombreTipo.Count; // Número de filas a agregar
            int rowHeight = 50; // Altura de las filas en píxeles

            

            // Primero, verifica si se sobrepasan los límites
            foreach (KeyValuePair<string, string> elemento in diccionarioNombreTipo)
            {
                if (elemento.Value == "VARCHAR")
                {
                    if (numVarchar >= 10) // Verifica si se supera el límite
                    {
                        MessageBox.Show("Se ha superado el número máximo de columnas para VARCHAR (10).");
                        return; // Sale del método si se supera el límite
                    }
                    numVarchar++;
                }
                else if (elemento.Value == "DECIMAL")
                {
                    if (numDecimal >= 4) // Verifica si se supera el límite
                    {
                        MessageBox.Show("Se ha superado el número máximo de columnas para DECIMAL (4).");
                        return; // Sale del método si se supera el límite
                    }
                    numDecimal++;
                }
                else if (elemento.Value == "DATETIME")
                {
                    if (numDatetime >= 4) // Verifica si se supera el límite
                    {
                        MessageBox.Show("Se ha superado el número máximo de columnas para DATETIME (4).");
                        return; // Sale del método si se supera el límite
                    }
                    numDatetime++;
                }
                else if (elemento.Value == "TEXT")
                {
                    if (numText) // Verifica si ya se asignó un campo de tipo TEXT
                    {
                        MessageBox.Show("Solo se puede asignar un campo de tipo TEXT.");
                        return; // Sale del método si ya se asignó un TEXT
                    }
                    numText = true; // Marca que el texto ha sido asignado
                }
                else if (elemento.Value == "IMAGE")
                {
                    if (numImage) // Verifica si ya se asignó un campo de tipo IMAGE
                    {
                        MessageBox.Show("Solo se puede asignar un campo de tipo IMAGE.");
                        return; // Sale del método si ya se asignó una imagen
                    }
                    numImage = true; // Marca que la imagen ha sido asignada
                }
            }

            // Si la validación se pasa, procede a agregar los controles
            foreach (KeyValuePair<string, string> elemento in diccionarioNombreTipo)
            {
                // Creo la label con el texto correspondiente
                Label label = new Label();
                label.Text = elemento.Key;
                label.AutoSize = true;  // Activar AutoSize para que el tamaño se ajuste automáticamente
                label.Margin = new Padding(90, 5, 5, 5); // Margen para el label

                // Agregar la label al TableLayoutPanel en la primera columna
                tableLayoutPanel.Controls.Add(label);

                // Actualiza actualADI antes de crear el campo
                string asignacionColumna = ""; // Para almacenar la columna asignada
                if (elemento.Value == "VARCHAR" && contadorVarchar < 11)
                {
                    asignacionColumna = "ADI" + (contadorVarchar); // ADI1 a ADI10
                    actualADI = "ADI" + contadorVarchar;
                    contadorVarchar++;
                }
                else if (elemento.Value == "DECIMAL" && contadorDecimal < 15)
                {
                    asignacionColumna = "ADI" + (contadorDecimal); // ADI11 a ADI14
                    actualADI = "ADI" + contadorDecimal;
                    contadorDecimal++;
                }
                else if (elemento.Value == "DATETIME" && contadorDatetime < 19)
                {
                    asignacionColumna = "ADI" + (contadorDatetime); // ADI15 a ADI18
                    actualADI = "ADI" + contadorDatetime;
                    contadorDatetime++;
                }
                else if (elemento.Value == "TEXT" && !textAsignado)
                {
                    asignacionColumna = "ADI19"; // ADI19 para TEXT
                    actualADI = "ADI19";
                    textAsignado = true;
                }
                else if (elemento.Value == "IMAGE" && !imageAsignado)
                {
                    asignacionColumna = "ADI20"; // ADI20 para IMAGE
                    actualADI = "ADI20";
                    imageAsignado = true;
                }

                // Creo el campo para que el usuario pueda meter el valor correspondiente
                Control inputControl = addElementoPanelSegunTipo(elemento.Value);

                // Cargo el valor de la base de datos si es que existe
                object valor = ObtenerValorDesdeBaseDeDatos(actualADI);
                AsignarValorControl(inputControl, valor, IDoc.ToString());

                // Agregar el control de entrada al TableLayoutPanel en la segunda columna
                tableLayoutPanel.Controls.Add(inputControl);
            }

            this.Show();
        }

        /*
         * Método que asigna el valor obtenido desde la base de datos al control correspondiente.
         */
        private void AsignarValorControl(Control control, object valor, string IDoc)
        {
            Queries queries = new Queries(conexion);

            string nombreTabla = "";
            // Obtengo el nombre de la tabla
            if (idOpcion == "CLIENTES")
                nombreTabla = "__CLIENTES";
            else if (idOpcion == "ARTICULOS")
                nombreTabla = "ARTICULO";
            else if (idOpcion == "PROVEEDORES")
                nombreTabla = "__PROVEED";

            // Verificamos si el valor es DBNull.Value o null
            if (valor == null || valor == DBNull.Value)
            {
                // Si el valor es nulo o DBNull, asigna un valor predeterminado según el tipo de control
                if (control is NumericUpDown numericUpDown)
                {
                    if (valor != DBNull.Value) // Verifica si el valor es nulo
                    {
                        decimal decimalValue = Convert.ToDecimal(valor);
                        numericUpDown.Value = decimalValue;
                    }
                    else
                    {
                        numericUpDown.Value = 0.0m; // Asigna un valor predeterminado o deja el actual
                    }
                }
                else if (control is TextBox textBox)
                {
                    textBox.Text = string.Empty; // Texto vacío si es null
                }
                else if (control is DateTimePicker dateTimePicker)
                {
                    dateTimePicker.Value = DateTime.Now; // Asignar fecha actual si es null
                }
                else if (control is RichTextBox richTextBox)
                {
                    richTextBox.Text = string.Empty; // Texto vacío si es null
                }
                else if (control is FlowLayoutPanel imagePanel)
                {
                    // Maneja la imagen si es null o DBNull (deja el PictureBox vacío)
                    var pictureBox = (PictureBox)imagePanel.Controls[1];
                    pictureBox.Image = null;
                }

                // Actualizamos el campo en la base de datos con DBNull.Value si es necesario
                queries.ActualizarCampo(actualADI, DBNull.Value, IDoc, nombreTabla);
            }
            else
            {
                // Asignamos el valor correspondiente al control si no es null
                if (control is TextBox textBox)
                {
                    textBox.Text = valor.ToString();
                    queries.ActualizarCampo(actualADI, valor, IDoc, nombreTabla);
                }
                else if (control is DateTimePicker dateTimePicker)
                {
                    if (valor is DateTime dateTimeValue)
                    {
                        dateTimePicker.Value = dateTimeValue;
                        queries.ActualizarCampo(actualADI, dateTimePicker.Value, IDoc, nombreTabla);
                    }
                    else
                    {
                        dateTimePicker.Value = DateTime.Now;
                        queries.ActualizarCampo(actualADI, DBNull.Value, IDoc, nombreTabla);
                    }
                }
                else if (control is RichTextBox richTextBox)
                {
                    richTextBox.Text = valor.ToString();
                    queries.ActualizarCampo(actualADI, valor, IDoc, nombreTabla);
                }
                else if (control is NumericUpDown numericUpDown)
                {
                    try
                    {
                        // Asegúrate de convertir solo si el valor no es null o DBNull
                        decimal decimalValue = Convert.ToDecimal(valor);
                        numericUpDown.Value = decimalValue;
                        queries.ActualizarCampo(actualADI, numericUpDown.Value, IDoc, nombreTabla);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Error al asignar el valor al campo decimal: " + e.Message);
                    }
                }
                else if (control is FlowLayoutPanel imagePanel)
                {
                    byte[] imageBytes = valor as byte[];
                    if (imageBytes != null)
                    {
                        using (var ms = new MemoryStream(imageBytes))
                        {
                            var pictureBox = (PictureBox)imagePanel.Controls[1];
                            pictureBox.Image = Image.FromStream(ms);
                        }
                        queries.ActualizarCampo(actualADI, imageBytes, IDoc, nombreTabla);
                    }
                }
            }
        }

        /*
         * Añade un elemento al panel según el tipo de valor.
         */
        public Control addElementoPanelSegunTipo(string tipoValor)
        {
            // Para almacenar el control creado
            Control control = null; 

            // Creo el campo para introducir datos correspondiente
            if (tipoValor == "VARCHAR")
            {
                TextBox textBox = new TextBox();
                textBox.Margin = new Padding(5);
                textBox.Dock = DockStyle.Fill;
                control = textBox;
            }
            else if (tipoValor == "DATETIME")
            {
                DateTimePicker dateTimePicker = new DateTimePicker();
                dateTimePicker.Margin = new Padding(5);
                dateTimePicker.Dock = DockStyle.Fill;
                control = dateTimePicker;
            }
            else if (tipoValor == "TEXT")
            {
                RichTextBox richTextBox = new RichTextBox();
                richTextBox.Margin = new Padding(5);
                richTextBox.Dock = DockStyle.Fill;
                richTextBox.Size = new Size(200, 100);
                control = richTextBox;
            }
            else if (tipoValor == "IMAGE")
            {
                // Panel para contener el PictureBox y el botón
                FlowLayoutPanel imagePanel = new FlowLayoutPanel();
                imagePanel.AutoSize = true;

                PictureBox pictureBox = new PictureBox();
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom; // Mantiene la relación de aspecto
                pictureBox.Width = 150; // Establecer ancho fijo
                pictureBox.Height = 100; // Establecer alto fijo
                pictureBox.Margin = new Padding(5);

                RadButton btnLoadImage = new RadButton();
                btnLoadImage.Text = "Cargar Imagen";
                btnLoadImage.AutoSize = true;

                // Evento para cargar la imagen
                btnLoadImage.Click += (sender, e) =>
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (var img = Image.FromFile(openFileDialog.FileName))
                        {
                            // Escalar la imagen manteniendo la relación de aspecto
                            var scaledImage = new Bitmap(pictureBox.Width, pictureBox.Height);
                            using (var g = Graphics.FromImage(scaledImage))
                            {
                                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                g.DrawImage(img, new Rectangle(0, 0, pictureBox.Width, pictureBox.Height));
                            }
                            pictureBox.Image = scaledImage;
                        }
                    }
                };
                btnLoadImage.Margin = new Padding(5);

                // Agregar el botón y el PictureBox al panel
                imagePanel.Controls.Add(btnLoadImage);
                imagePanel.Controls.Add(pictureBox);

                control = imagePanel; // Asignar el panel como inputControl
            }
            else if (tipoValor == "DECIMAL")
            {
                NumericUpDown numericUpDown = new NumericUpDown();
                numericUpDown.DecimalPlaces = 2;
                numericUpDown.Margin = new Padding(5);
                numericUpDown.Dock = DockStyle.Fill;
                control = numericUpDown;
            }
            else
            {
                MessageBox.Show("Tipo de campo incorrecto: " + tipoValor);
            }

            // Añadir el control creado a la lista
            if (control != null)
            {
                controlesInput.Add(control);
            }

            return control; // Retorna el control creado
        }

        /*
         * Método para obtener el valor correspondiente desde la base de datos.
         */
        private object ObtenerValorDesdeBaseDeDatos(string ADI)
        {
            using (var conexion = new SqlConnection(this.conexion))
            {
                conexion.Open();

                string nombreTabla = "";
                // Obtengo el nombre de la tabla
                if (idOpcion == "CLIENTES")
                    nombreTabla = "__CLIENTES";
                else if (idOpcion == "ARTICULOS")
                    nombreTabla = "ARTICULO";
                else if (idOpcion == "PROVEEDORES")
                    nombreTabla = "__PROVEED";

                // Obtengo el nombre del codigo
                string nombreCodigo = "";
                if (nombreTabla == "__CLIENTES")
                    nombreCodigo = "CODCLI";
                else if (nombreTabla == "__PROVEED")
                    nombreCodigo = "CODPRO";
                else if (nombreTabla == "ARTICULO")
                    nombreCodigo = "CODART";

                string query = $"SELECT {ADI} FROM {nombreTabla} WHERE {nombreCodigo} = @IDoc";
                using (var comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@IDoc", this.IDoc);
                    var resultado = comando.ExecuteScalar();

                    if (resultado == DBNull.Value)
                    {
                        return null;
                    }

                    if (resultado is byte[] byteArray)
                    {
                        return byteArray;
                    }

                    if (resultado is decimal decimalValue)
                    {
                        return decimalValue;
                    }

                    if (resultado is double doubleValue)
                    {
                        return Convert.ToDecimal(doubleValue);
                    }

                    return resultado;
                }
            }
        }

        /*
         * Lee el fichero .txt y almacena los valores en un diccionario.
         */
        public Dictionary<string, string> leerFichero()
        {
            string rutaArchivo = "";

            // Dependiendo de la opción que sea se utilizará un archivo u otro
            if (this.idOpcion == "CLIENTES")
                rutaArchivo = Path.GetFullPath("..\\..\\MostrarCamposClientes.txt");
            else if(this.idOpcion == "ARTICULOS")
                rutaArchivo = Path.GetFullPath("..\\..\\MostrarCamposArticulos.txt");
            else if(this.idOpcion == "PROVEEDORES")
                rutaArchivo = Path.GetFullPath("..\\..\\MostrarCamposProveedores.txt");

            // Creo el diccionario
            Dictionary<string, string> nombreTipoDiccionario = new Dictionary<string, string>();

            // Solo leo el .txt si el archivo existe
            if (File.Exists(rutaArchivo))
            {
                var lineas = File.ReadAllLines(rutaArchivo);

                // Salta las líneas del encabezado del .txt
                for (int i = 2; i < lineas.Length; i++) 
                {
                    // Ignorar líneas vacías
                    if (string.IsNullOrWhiteSpace(lineas[i]))
                        continue;

                    var linea = lineas[i].Split('=');
                    if (linea.Length == 2)
                    {
                        string nombreCampo = linea[0].Trim();
                        string tipoCampo = linea[1].Trim().ToUpper();
                        // Compruebo que no haya otro elemento en el .txt con el mismo nombreCampo
                        if (nombreTipoDiccionario.ContainsKey(nombreCampo))
                            MessageBox.Show($"El campo con nombre '{nombreCampo}' en la línea {i + 1} ya existe. Se ha ignorado.");
                        else // Añado al Diccionario el nombre del campo y el tipo de campo si nombreCampo no existe en el .txt
                            nombreTipoDiccionario.Add(nombreCampo, tipoCampo);
                    }
                    else
                    {
                        MessageBox.Show($"La línea {i + 1} del archivo está mal formateada: {lineas[i]}");
                    }
                }
                return nombreTipoDiccionario;
            }
            return nombreTipoDiccionario;
        }

        /*
         * Asigna las columnas (ADI1,ADI2...ADI20) según el tipo del campo.
         */
        public void AsignarColumnas(Dictionary<string, string> diccionarioNombreTipo)
        {

        int contadorVarchar = 1;
        int contadorDecimal = 11;
        int contadorDatetime = 15;
        bool textAsignado = false;
        bool imageAsignado = false;

        // Asignar las columnas
        int index = 0; 
            foreach (KeyValuePair<string, string> elemento in diccionarioNombreTipo)
            {
                string asignacionColumna = ""; // Para almacenar la columna asignada
                actualADI = "";
                // Verifica el tipo de elemento y asigna la columna correspondiente
                if (elemento.Value == "VARCHAR" && contadorVarchar<11)
                {
                    asignacionColumna = "ADI" + (contadorVarchar); // ADI1 a ADI10
                    actualADI = "ADI" + (contadorVarchar);
                    contadorVarchar++;
                }
                else if (elemento.Value == "DECIMAL" && contadorDecimal < 15)
                {
                    asignacionColumna = "ADI" + (contadorDecimal); // ADI11 a ADI14
                    actualADI = "ADI" + (contadorDecimal);
                    contadorDecimal++;
                }
                else if (elemento.Value == "DATETIME" && contadorDatetime < 19)
                {
                    asignacionColumna = "ADI" + (contadorDatetime); // ADI15 a ADI18
                    actualADI = "ADI" + (contadorDatetime);
                    contadorDatetime++;
                }
                else if (elemento.Value == "TEXT" && textAsignado==false)
                {
                    asignacionColumna = "ADI19"; // ADI19 para TEXT
                    actualADI = "ADI19";
                    textAsignado = true;
                }
                else if (elemento.Value == "IMAGE" && imageAsignado==false)
                {
                    asignacionColumna = "ADI20"; // ADI20 para IMAGE
                    actualADI = "ADI20";
                    imageAsignado = true;
                }

                // Obtiene el valor del control correspondiente
                Control control = controlesInput[index];
                object valorCampo = null;

                // Obtener el valor según el tipo de control
                if (control is TextBox textBox)
                {
                    valorCampo = textBox.Text; // Para VARCHAR
                }
                else if (control is DateTimePicker dateTimePicker)
                {
                    valorCampo = dateTimePicker.Value.ToString("yyyy-MM-dd HH:mm:ss"); // Para DATETIME
                }
                else if (control is RichTextBox richTextBox)
                {
                    valorCampo = richTextBox.Text; // Para TEXT
                    
                }
                else if (control is NumericUpDown numericUpDown)
                {
                    valorCampo = numericUpDown.Value.ToString(); // Para DECIMAL
                }
                else if (control is FlowLayoutPanel flowLayoutPanel)
                {
                    // Convierte imagen en byte[]
                    // Buscar el PictureBox dentro del FlowLayoutPanel
                    foreach (Control innerControl in flowLayoutPanel.Controls)
                    {
                        if (innerControl is PictureBox pictureBox && pictureBox.Image != null)
                        {
                            // Convierte la imagen a byte[] usando un método auxiliar
                            valorCampo = ConvertImageToBytes(pictureBox.Image);
                            break;
                        }
                    }
                }

                // Actualiza la base de datos con el campo asignado usando el método ActualizarCampo
                Queries queries = new Queries(conexion);
                // Obtengo el nombre de la tabla
                string nombreTabla = "";
                if (idOpcion == "CLIENTES")
                    nombreTabla = "__CLIENTES";
                else if (idOpcion == "ARTICULOS")
                    nombreTabla = "ARTICULO";
                else if (idOpcion == "PROVEEDORES")
                    nombreTabla = "__PROVEED";
                bool actualizado = queries.ActualizarCampo(asignacionColumna, valorCampo, this.IDoc.ToString(), nombreTabla);

                if (actualizado)
                {
                    Console.WriteLine($"El campo '{elemento.Key}' de tipo '{elemento.Value}' se asignó y actualizó en la columna {asignacionColumna} para el cliente {this.IDoc}.");
                }
                else
                {
                    Console.WriteLine($"Error al actualizar el campo '{elemento.Key}' en la columna {asignacionColumna}.");
                }

                index++; // Incrementa el índice para el siguiente control
            }
        }

        /*
         * Guarda los datos que el usuario ha introducido en los inputs en sus ADIs correspondientes en la base de datos.
         */
        private void radButtonGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, string> diccionario = leerFichero();
                AsignarColumnas(diccionario);

                // Mensaje de confirmación
                MessageBox.Show("Los campos han sido guardados correctamente.");
            }
            catch (Exception excepcion)
            {
                MessageBox.Show(excepcion.Message);
            }
        }

        /*
         * Cierra el formulario.
         */
        private void radButtonCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /*
         * Acción al clicar el botón de 'Vaciar todo'
         */
        private void radButton1_Click(object sender, EventArgs e)
        {
            BorrarCamposDeInput(this);
        }

        /*
         * Borra todos los campos de input del formulario.
         */
        private void BorrarCamposDeInput(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Clear(); // Borra el texto del TextBox
                }
                else if (control is RichTextBox richTextBox)
                {
                    richTextBox.Clear(); // Borra el texto del RichTextBox
                }
                else if (control is ComboBox comboBox)
                {
                    comboBox.SelectedIndex = -1; // Restablece la selección del ComboBox
                }
                else if (control is NumericUpDown numericUpDown)
                {
                    numericUpDown.Value = 0.0M;
                }
                else if (control is DateTimePicker dateTimePicker)
                {
                    dateTimePicker.Value = DateTime.Now; // O establece un valor por defecto, como la fecha actual
                }
                else if (control is PictureBox pictureBox)
                {
                    pictureBox.Image = null; // Elimina la imagen del PictureBox
                }

                // Si el control tiene otros controles dentro de él, llama recursivamente
                if (control.HasChildren)
                {
                    BorrarCamposDeInput(control); // Llama a la función recursivamente
                }
            }
        }

        /*
         * Convierte una imagen en un array de bytes.
         */
        private byte[] ConvertImageToBytes(Image image)
        {
            if (image == null) return null;

            using (var ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png); 
                return ms.ToArray();
            }
        }

    }
}