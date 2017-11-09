// PS4Macro.Remote (File: MainForm.cs)
//
// Copyright (c) 2017 Komefai
//
// Visit http://komefai.com for more information
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS4Macro.Remote
{
    public partial class MainForm : Form
    {
        public bool FormLoaded { get; private set; }
        public bool IsKeyDown { get; private set; }
        public Keys CurrentKeyDown { get; private set; }

        public KeyboardMap KeyboardMap { get; private set; }
        private List<MappingAction> MappingsDataBinding { get; set; }
        private List<MacroAction> MacrosDataBinding { get; set; }
        private BindingList<MappingAction> MappingsBindingList { get; set; }
        private BindingList<MacroAction> MacrosBindingList { get; set; }


        public MainForm()
        {
            InitializeComponent();

            KeyboardMap = new KeyboardMap();
            MappingsDataBinding = new List<MappingAction>()
            {
                new MappingAction("L Left", Keys.A, "LX", 0),
                new MappingAction("L Right", Keys.D, "LX", 255),
                new MappingAction("L Up", Keys.W, "LY", 0),
                new MappingAction("L Down", Keys.S, "LY", 255),

                new MappingAction("R Left", Keys.J, "RX", 0),
                new MappingAction("R Right", Keys.L, "RX", 255),
                new MappingAction("R Up", Keys.I, "RY", 0),
                new MappingAction("R Down", Keys.K, "RY", 255),

                new MappingAction("R1", Keys.Q, "R1", true),
                new MappingAction("L1", Keys.E, "L1", true),
                new MappingAction("L2", Keys.U, "L2", 255),
                new MappingAction("R2", Keys.O, "R2", 255),

                new MappingAction("Triangle", Keys.C, "Triangle", true),
                new MappingAction("Circle", Keys.Escape, "Circle", true),
                new MappingAction("Cross", Keys.Enter, "Cross", true),
                new MappingAction("Square", Keys.V, "Square", true),

                new MappingAction("DPad Up", Keys.Up, "DPad_Up", true),
                new MappingAction("DPad Down", Keys.Down, "DPad_Down", true),
                new MappingAction("DPad Left", Keys.Left, "DPad_Left", true),
                new MappingAction("DPad Right", Keys.Right, "DPad_Right", true),

                new MappingAction("L3", Keys.N, "L3", true),
                new MappingAction("R3", Keys.M, "R3", true),

                new MappingAction("Share", Keys.LControlKey, "Share", true),
                new MappingAction("Options", Keys.Alt, "Options", true),
                new MappingAction("PS", Keys.LShiftKey, "PS", true),

                new MappingAction("Touch Button", Keys.T, "TouchButton", true)
            };

            MacrosDataBinding = new List<MacroAction>();

            CreateActions();
        }

        private void BindMappingsDataGrid()
        {
            mappingsDataGridView.AutoGenerateColumns = false;

            MappingsBindingList = new BindingList<MappingAction>(MappingsDataBinding);
            mappingsDataGridView.DataSource = MappingsBindingList;
        }

        private void BindMacrosDataGrid()
        {
            macrosDataGridView.AutoGenerateColumns = false;

            MacrosBindingList = new BindingList<MacroAction>(MacrosDataBinding);
            macrosDataGridView.DataSource = MacrosBindingList;
        }

        private void CreateActions()
        {
            var dict = new Dictionary<Keys, BaseAction>();

            foreach (var item in MappingsDataBinding)
            {
                if (item.Key == Keys.None) continue;
                dict.Add(item.Key, item);
            }
            foreach (var item in MacrosDataBinding)
            {
                if (item.Key == Keys.None) continue;
                dict.Add(item.Key, item);
            }

            KeyboardMap.KeysDict = dict;
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            BindMappingsDataGrid();
            BindMacrosDataGrid();

            FormLoaded = true;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            CurrentKeyDown = e.KeyCode;
            IsKeyDown = true;
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            IsKeyDown = false;
        }

        private void mappingsDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!FormLoaded) return;
            CreateActions();
        }

        private void macrosDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!FormLoaded) return;
            CreateActions();
        }

        private void mappingsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var dataGridView = sender as DataGridView;
            if (dataGridView.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {

            }
        }

        private void macrosDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var dataGridView = sender as DataGridView;
            var rowIndex = e.RowIndex;

            if (dataGridView.Columns[e.ColumnIndex] is DataGridViewButtonColumn && rowIndex >= 0)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.InitialDirectory = Helper.GetScriptFolder() + @"";
                openFileDialog.Filter = "XML Files (*.xml)|*.xml|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 0;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = openFileDialog.FileName;
                    string fileName = System.IO.Path.GetFileName(openFileDialog.FileName);

                    if (rowIndex >= MacrosDataBinding.Count)
                    {
                        MacrosDataBinding.Add(new MacroAction());
                    }

                    var item = MacrosDataBinding[rowIndex];
                    item.Name = fileName;
                    //item.Key = Keys.None;
                    item.Path = path;

                    MacrosBindingList.ResetBindings();
                }
            }
        }

        private void focusTextBox_TextChanged(object sender, EventArgs e)
        {
            focusTextBox.Text = string.Empty;
        }
    }
}
