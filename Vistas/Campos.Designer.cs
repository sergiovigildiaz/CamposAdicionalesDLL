namespace CamposAdicionales.Vistas
{
    partial class Campos
    {
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.office2007SilverTheme1 = new Telerik.WinControls.Themes.Office2007SilverTheme();
            this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
            this.radButtonCancelar = new Telerik.WinControls.UI.RadButton();
            this.radButtonGuardar = new Telerik.WinControls.UI.RadButton();
            this.radPanel4 = new Telerik.WinControls.UI.RadPanel();
            this.radPanel3 = new Telerik.WinControls.UI.RadPanel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.radPanel5 = new Telerik.WinControls.UI.RadPanel();
            this.radButton1 = new Telerik.WinControls.UI.RadButton();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            this.radPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonCancelar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonGuardar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel4)).BeginInit();
            this.radPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel5)).BeginInit();
            this.radPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radPanel2
            // 
            this.radPanel2.Controls.Add(this.radButtonCancelar);
            this.radPanel2.Controls.Add(this.radButtonGuardar);
            this.radPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radPanel2.Location = new System.Drawing.Point(0, 443);
            this.radPanel2.Name = "radPanel2";
            this.radPanel2.Size = new System.Drawing.Size(768, 49);
            this.radPanel2.TabIndex = 1;
            // 
            // radButtonCancelar
            // 
            this.radButtonCancelar.Location = new System.Drawing.Point(656, 3);
            this.radButtonCancelar.Name = "radButtonCancelar";
            this.radButtonCancelar.Size = new System.Drawing.Size(109, 40);
            this.radButtonCancelar.TabIndex = 4;
            this.radButtonCancelar.Text = "Cancelar";
            this.radButtonCancelar.Click += new System.EventHandler(this.radButtonCancelar_Click);
            // 
            // radButtonGuardar
            // 
            this.radButtonGuardar.Location = new System.Drawing.Point(541, 3);
            this.radButtonGuardar.Name = "radButtonGuardar";
            this.radButtonGuardar.Size = new System.Drawing.Size(109, 40);
            this.radButtonGuardar.TabIndex = 3;
            this.radButtonGuardar.Text = "Guardar";
            this.radButtonGuardar.Click += new System.EventHandler(this.radButtonGuardar_Click);
            // 
            // radPanel4
            // 
            this.radPanel4.Controls.Add(this.radPanel3);
            this.radPanel4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radPanel4.Location = new System.Drawing.Point(0, 0);
            this.radPanel4.Name = "radPanel4";
            this.radPanel4.Size = new System.Drawing.Size(768, 50);
            this.radPanel4.TabIndex = 3;
            this.radPanel4.Text = "Introduzca los valores para los campos deseados";
            // 
            // radPanel3
            // 
            this.radPanel3.Location = new System.Drawing.Point(13, 13);
            this.radPanel3.Name = "radPanel3";
            this.radPanel3.Size = new System.Drawing.Size(200, 100);
            this.radPanel3.TabIndex = 0;
            this.radPanel3.Text = "radPanel3";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(768, 443);
            this.dataGridView1.TabIndex = 4;
            // 
            // radPanel5
            // 
            this.radPanel5.Controls.Add(this.radButton1);
            this.radPanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radPanel5.Location = new System.Drawing.Point(0, 0);
            this.radPanel5.Name = "radPanel5";
            this.radPanel5.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.radPanel5.Size = new System.Drawing.Size(768, 43);
            this.radPanel5.TabIndex = 5;
            this.radPanel5.Text = "Introduzca los valores para los campos deseados";
            // 
            // radButton1
            // 
            this.radButton1.Location = new System.Drawing.Point(646, 12);
            this.radButton1.Name = "radButton1";
            this.radButton1.Size = new System.Drawing.Size(110, 24);
            this.radButton1.TabIndex = 0;
            this.radButton1.Text = "Vaciar todo";
            this.radButton1.Click += new System.EventHandler(this.radButton1_Click);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.AutoScroll = true;
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 43);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(768, 400);
            this.tableLayoutPanel.TabIndex = 6;
            // 
            // Campos
            // 
            this.ClientSize = new System.Drawing.Size(768, 492);
            this.Controls.Add(this.tableLayoutPanel);
            this.Controls.Add(this.radPanel5);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.radPanel4);
            this.Controls.Add(this.radPanel2);
            this.Name = "Campos";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Añadir campos adicionales al diccionario";
            this.ThemeName = "Office2007Silver";
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            this.radPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radButtonCancelar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonGuardar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel4)).EndInit();
            this.radPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radPanel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel5)).EndInit();
            this.radPanel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Telerik.WinControls.Themes.Office2007SilverTheme office2007SilverTheme1;
        private Telerik.WinControls.UI.RadPanel radPanel2;
        private Telerik.WinControls.UI.RadPanel radPanel4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private Telerik.WinControls.UI.RadPanel radPanel3;
        private Telerik.WinControls.UI.RadPanel radPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private Telerik.WinControls.UI.RadButton radButtonGuardar;
        private Telerik.WinControls.UI.RadButton radButtonCancelar;
        private Telerik.WinControls.UI.RadButton radButton1;
    }
}