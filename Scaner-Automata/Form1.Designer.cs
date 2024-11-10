namespace Scaner_Automata
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            txtTexto = new TextBox();
            dgvLexica = new DataGridView();
            num = new DataGridViewTextBoxColumn();
            linea = new DataGridViewTextBoxColumn();
            token = new DataGridViewTextBoxColumn();
            tipo = new DataGridViewTextBoxColumn();
            codigo = new DataGridViewTextBoxColumn();
            label1 = new Label();
            dgvId = new DataGridView();
            identificadores = new DataGridViewTextBoxColumn();
            valorTI = new DataGridViewTextBoxColumn();
            lineaTI = new DataGridViewTextBoxColumn();
            dgvCons = new DataGridView();
            constantes = new DataGridViewTextBoxColumn();
            valorTC = new DataGridViewTextBoxColumn();
            lineaTC = new DataGridViewTextBoxColumn();
            label2 = new Label();
            label3 = new Label();
            lblMensaje = new Label();
            btnLeer = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvLexica).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvId).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvCons).BeginInit();
            SuspendLayout();
            // 
            // txtTexto
            // 
            txtTexto.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtTexto.Location = new Point(10, 28);
            txtTexto.Margin = new Padding(3, 2, 3, 2);
            txtTexto.Multiline = true;
            txtTexto.Name = "txtTexto";
            txtTexto.Size = new Size(1030, 180);
            txtTexto.TabIndex = 0;
            // 
            // dgvLexica
            // 
            dgvLexica.AllowUserToAddRows = false;
            dgvLexica.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvLexica.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvLexica.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLexica.Columns.AddRange(new DataGridViewColumn[] { num, linea, token, tipo, codigo });
            dgvLexica.Location = new Point(10, 274);
            dgvLexica.Margin = new Padding(3, 2, 3, 2);
            dgvLexica.Name = "dgvLexica";
            dgvLexica.ReadOnly = true;
            dgvLexica.RowHeadersWidth = 51;
            dataGridViewCellStyle2.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dgvLexica.RowsDefaultCellStyle = dataGridViewCellStyle2;
            dgvLexica.Size = new Size(576, 369);
            dgvLexica.TabIndex = 1;
            // 
            // num
            // 
            num.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            num.HeaderText = "No.";
            num.MinimumWidth = 6;
            num.Name = "num";
            num.ReadOnly = true;
            // 
            // linea
            // 
            linea.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            linea.HeaderText = "Linea";
            linea.MinimumWidth = 6;
            linea.Name = "linea";
            linea.ReadOnly = true;
            // 
            // token
            // 
            token.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            token.HeaderText = "Token";
            token.MinimumWidth = 6;
            token.Name = "token";
            token.ReadOnly = true;
            // 
            // tipo
            // 
            tipo.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            tipo.HeaderText = "Tipo";
            tipo.MinimumWidth = 6;
            tipo.Name = "tipo";
            tipo.ReadOnly = true;
            // 
            // codigo
            // 
            codigo.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            codigo.HeaderText = "Código";
            codigo.MinimumWidth = 6;
            codigo.Name = "codigo";
            codigo.ReadOnly = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(10, 253);
            label1.Name = "label1";
            label1.Size = new Size(96, 20);
            label1.TabIndex = 2;
            label1.Text = "Tabla léxica";
            // 
            // dgvId
            // 
            dgvId.AllowUserToAddRows = false;
            dgvId.AllowUserToDeleteRows = false;
            dgvId.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvId.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvId.Columns.AddRange(new DataGridViewColumn[] { identificadores, valorTI, lineaTI });
            dgvId.Location = new Point(624, 274);
            dgvId.Margin = new Padding(3, 2, 3, 2);
            dgvId.Name = "dgvId";
            dgvId.ReadOnly = true;
            dgvId.RowHeadersWidth = 51;
            dgvId.RowsDefaultCellStyle = dataGridViewCellStyle2;
            dgvId.Size = new Size(416, 165);
            dgvId.TabIndex = 3;
            // 
            // identificadores
            // 
            identificadores.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            identificadores.HeaderText = "Identificadores";
            identificadores.MinimumWidth = 6;
            identificadores.Name = "identificadores";
            identificadores.ReadOnly = true;
            // 
            // valorTI
            // 
            valorTI.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            valorTI.HeaderText = "Valor";
            valorTI.MinimumWidth = 6;
            valorTI.Name = "valorTI";
            valorTI.ReadOnly = true;
            // 
            // lineaTI
            // 
            lineaTI.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            lineaTI.HeaderText = "Linea";
            lineaTI.MinimumWidth = 6;
            lineaTI.Name = "lineaTI";
            lineaTI.ReadOnly = true;
            // 
            // dgvCons
            // 
            dgvCons.AllowUserToAddRows = false;
            dgvCons.AllowUserToDeleteRows = false;
            dgvCons.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvCons.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCons.Columns.AddRange(new DataGridViewColumn[] { constantes, valorTC, lineaTC });
            dgvCons.Location = new Point(624, 478);
            dgvCons.Margin = new Padding(3, 2, 3, 2);
            dgvCons.Name = "dgvCons";
            dgvCons.ReadOnly = true;
            dgvCons.RowHeadersWidth = 51;
            dgvCons.RowsDefaultCellStyle = dataGridViewCellStyle2;
            dgvCons.Size = new Size(416, 165);
            dgvCons.TabIndex = 4;
            // 
            // constantes
            // 
            constantes.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            constantes.HeaderText = "Constantes";
            constantes.MinimumWidth = 6;
            constantes.Name = "constantes";
            constantes.ReadOnly = true;
            // 
            // valorTC
            // 
            valorTC.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            valorTC.HeaderText = "Valor";
            valorTC.MinimumWidth = 6;
            valorTC.Name = "valorTC";
            valorTC.ReadOnly = true;
            // 
            // lineaTC
            // 
            lineaTC.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            lineaTC.HeaderText = "Linea";
            lineaTC.MinimumWidth = 6;
            lineaTC.Name = "lineaTC";
            lineaTC.ReadOnly = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(624, 457);
            label2.Name = "label2";
            label2.Size = new Size(159, 20);
            label2.TabIndex = 5;
            label2.Text = "Tabla de constantes";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(624, 253);
            label3.Name = "label3";
            label3.Size = new Size(188, 20);
            label3.TabIndex = 6;
            label3.Text = "Tabla de identificadores";
            // 
            // lblMensaje
            // 
            lblMensaje.AutoSize = true;
            lblMensaje.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMensaje.Location = new Point(10, 208);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(84, 20);
            lblMensaje.TabIndex = 7;
            lblMensaje.Text = "Mensaje | ";
            // 
            // btnLeer
            // 
            btnLeer.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLeer.Location = new Point(936, 211);
            btnLeer.Margin = new Padding(3, 2, 3, 2);
            btnLeer.Name = "btnLeer";
            btnLeer.Size = new Size(104, 26);
            btnLeer.TabIndex = 8;
            btnLeer.Text = "Leer";
            btnLeer.UseVisualStyleBackColor = true;
            btnLeer.Click += btnLeer_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1063, 652);
            Controls.Add(btnLeer);
            Controls.Add(lblMensaje);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(dgvCons);
            Controls.Add(dgvId);
            Controls.Add(label1);
            Controls.Add(dgvLexica);
            Controls.Add(txtTexto);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Scaner";
            ((System.ComponentModel.ISupportInitialize)dgvLexica).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvId).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvCons).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtTexto;
        private DataGridView dgvLexica;
        private DataGridViewTextBoxColumn num;
        private DataGridViewTextBoxColumn linea;
        private DataGridViewTextBoxColumn token;
        private DataGridViewTextBoxColumn tipo;
        private DataGridViewTextBoxColumn codigo;
        private Label label1;
        private DataGridView dgvId;
        private DataGridViewTextBoxColumn identificadores;
        private DataGridViewTextBoxColumn valorTI;
        private DataGridViewTextBoxColumn lineaTI;
        private DataGridView dgvCons;
        private DataGridViewTextBoxColumn constantes;
        private DataGridViewTextBoxColumn valorTC;
        private DataGridViewTextBoxColumn lineaTC;
        private Label label2;
        private Label label3;
        private Label lblMensaje;
        private Button btnLeer;
    }
}
