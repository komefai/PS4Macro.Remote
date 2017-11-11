namespace PS4Macro.Remote
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mappingsGroupBox = new System.Windows.Forms.GroupBox();
            this.mappingsDataGridView = new System.Windows.Forms.DataGridView();
            this.Button = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Key = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.macrosGroupBox = new System.Windows.Forms.GroupBox();
            this.macrosDataGridView = new System.Windows.Forms.DataGridView();
            this.Browse = new System.Windows.Forms.DataGridViewButtonColumn();
            this._Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.focusTextBox = new System.Windows.Forms.TextBox();
            this.tutorialLabel = new System.Windows.Forms.Label();
            this.mappingsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mappingsDataGridView)).BeginInit();
            this.macrosGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.macrosDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // mappingsGroupBox
            // 
            this.mappingsGroupBox.Controls.Add(this.mappingsDataGridView);
            this.mappingsGroupBox.Location = new System.Drawing.Point(12, 12);
            this.mappingsGroupBox.Name = "mappingsGroupBox";
            this.mappingsGroupBox.Size = new System.Drawing.Size(339, 204);
            this.mappingsGroupBox.TabIndex = 0;
            this.mappingsGroupBox.TabStop = false;
            this.mappingsGroupBox.Text = "Mappings";
            // 
            // mappingsDataGridView
            // 
            this.mappingsDataGridView.AllowUserToAddRows = false;
            this.mappingsDataGridView.AllowUserToDeleteRows = false;
            this.mappingsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mappingsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Button,
            this.Key});
            this.mappingsDataGridView.Location = new System.Drawing.Point(6, 19);
            this.mappingsDataGridView.Name = "mappingsDataGridView";
            this.mappingsDataGridView.Size = new System.Drawing.Size(327, 179);
            this.mappingsDataGridView.TabIndex = 0;
            this.mappingsDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.mappingsDataGridView_CellContentClick);
            this.mappingsDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.mappingsDataGridView_CellValueChanged);
            // 
            // Button
            // 
            this.Button.DataPropertyName = "Name";
            this.Button.HeaderText = "Button";
            this.Button.Name = "Button";
            this.Button.ReadOnly = true;
            // 
            // Key
            // 
            this.Key.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Key.DataPropertyName = "Key";
            this.Key.HeaderText = "Key";
            this.Key.Name = "Key";
            // 
            // macrosGroupBox
            // 
            this.macrosGroupBox.Controls.Add(this.macrosDataGridView);
            this.macrosGroupBox.Location = new System.Drawing.Point(12, 222);
            this.macrosGroupBox.Name = "macrosGroupBox";
            this.macrosGroupBox.Size = new System.Drawing.Size(339, 153);
            this.macrosGroupBox.TabIndex = 1;
            this.macrosGroupBox.TabStop = false;
            this.macrosGroupBox.Text = "Macros";
            // 
            // macrosDataGridView
            // 
            this.macrosDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.macrosDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Browse,
            this._Name,
            this.dataGridViewTextBoxColumn1,
            this.Path});
            this.macrosDataGridView.Location = new System.Drawing.Point(6, 19);
            this.macrosDataGridView.Name = "macrosDataGridView";
            this.macrosDataGridView.Size = new System.Drawing.Size(327, 128);
            this.macrosDataGridView.TabIndex = 0;
            this.macrosDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.macrosDataGridView_CellContentClick);
            this.macrosDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.macrosDataGridView_CellValueChanged);
            // 
            // Browse
            // 
            this.Browse.FillWeight = 40F;
            this.Browse.HeaderText = "...";
            this.Browse.Name = "Browse";
            this.Browse.Text = "...";
            this.Browse.UseColumnTextForButtonValue = true;
            this.Browse.Width = 40;
            // 
            // _Name
            // 
            this._Name.DataPropertyName = "Name";
            this._Name.HeaderText = "Name";
            this._Name.Name = "_Name";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Key";
            this.dataGridViewTextBoxColumn1.HeaderText = "Key";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // Path
            // 
            this.Path.DataPropertyName = "Path";
            this.Path.HeaderText = "Path";
            this.Path.Name = "Path";
            // 
            // focusTextBox
            // 
            this.focusTextBox.BackColor = System.Drawing.Color.LightBlue;
            this.focusTextBox.Location = new System.Drawing.Point(12, 381);
            this.focusTextBox.Name = "focusTextBox";
            this.focusTextBox.Size = new System.Drawing.Size(339, 20);
            this.focusTextBox.TabIndex = 2;
            this.focusTextBox.TextChanged += new System.EventHandler(this.focusTextBox_TextChanged);
            // 
            // tutorialLabel
            // 
            this.tutorialLabel.Location = new System.Drawing.Point(9, 410);
            this.tutorialLabel.Name = "tutorialLabel";
            this.tutorialLabel.Size = new System.Drawing.Size(342, 13);
            this.tutorialLabel.TabIndex = 3;
            this.tutorialLabel.Text = "Click on the blue textbox to focus and control PS4 Remote Play";
            this.tutorialLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 432);
            this.Controls.Add(this.tutorialLabel);
            this.Controls.Add(this.focusTextBox);
            this.Controls.Add(this.macrosGroupBox);
            this.Controls.Add(this.mappingsGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "PS4 Macro Remote - v1.1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.mappingsGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mappingsDataGridView)).EndInit();
            this.macrosGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.macrosDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox mappingsGroupBox;
        private System.Windows.Forms.DataGridView mappingsDataGridView;
        private System.Windows.Forms.GroupBox macrosGroupBox;
        private System.Windows.Forms.DataGridView macrosDataGridView;
        private System.Windows.Forms.TextBox focusTextBox;
        private System.Windows.Forms.DataGridViewButtonColumn Browse;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Path;
        private System.Windows.Forms.Label tutorialLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Button;
        private System.Windows.Forms.DataGridViewTextBoxColumn Key;
    }
}