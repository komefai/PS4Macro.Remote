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
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace PS4Macro.Remote
{
    public partial class MainForm : Form
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        private const string BINDINGS_FILE = "bindings.xml";

        public bool FormLoaded { get; private set; }
        public Dictionary<Keys, bool> PressedKeys { get; private set; }

        public KeyboardMap KeyboardMap { get; private set; }
        private List<MappingAction> MappingsDataBinding { get; set; }
        private List<MacroAction> MacrosDataBinding { get; set; }
        private BindingList<MappingAction> MappingsBindingList { get; set; }
        private BindingList<MacroAction> MacrosBindingList { get; set; }
        private GlobalKeyboardHook GlobalKeyboardHook { get; set; }
        private Process CurrentProcess { get; set; }


        public MainForm()
        {
            InitializeComponent();

            GlobalKeyboardHook = new GlobalKeyboardHook();
            GlobalKeyboardHook.KeyboardPressed += OnKeyPressed;
            PressedKeys = new Dictionary<Keys, bool>();

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

            // Load bindings if file exist
            if (System.IO.File.Exists(Helper.GetScriptFolder() + @"\" + BINDINGS_FILE))
            {
                LoadBindings();
            }

            CreateActions();
        }

        public bool IsKeyDown()
        {
            return PressedKeys.Count > 0;
        }

        public void Start()
        {
            //GlobalKeyboardHook = new GlobalKeyboardHook();
            //GlobalKeyboardHook.KeyboardPressed += OnKeyPressed;

            CurrentProcess = Helper.FindRemotePlayProcess();
        }

        public void Stopped()
        {
            //if (GlobalKeyboardHook != null)
            //{
            //    try
            //    {
            //        GlobalKeyboardHook.Dispose();
            //    }
            //    catch { }

            //    GlobalKeyboardHook = null;
            //}
        }

        private void SaveBindings()
        {
            var container = new BindingsContainer();
            container.Mappings = MappingsDataBinding;
            container.Macros = MacrosDataBinding;

            BindingsContainer.Serialize(BINDINGS_FILE, container);
        }

        private void LoadBindings()
        {
            var container = BindingsContainer.Deserialize(BINDINGS_FILE);
            MappingsDataBinding = container.Mappings;
            MacrosDataBinding = container.Macros;
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

        private void OnKeyPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/dd375731(v=vs.85).aspx

            // Remote Play not found
            if (CurrentProcess == null)
                return;

            // Check for active window
            var activeWindow = GetForegroundWindow();
            if (activeWindow != IntPtr.Zero)
            {
                if (activeWindow != CurrentProcess.MainWindowHandle)
                {
                    return;
                }
            }

            int vk = e.KeyboardData.VirtualCode;
            Keys key = (Keys)vk;
            //System.Diagnostics.Debug.WriteLine("KEY: " + vk);

            if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown)
            {
                //System.Diagnostics.Debug.WriteLine("KEY DOWN");

                if (!PressedKeys.ContainsKey(key))
                {
                    PressedKeys.Add(key, true);
                }

                e.Handled = true;
            }
            else if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyUp)
            {
                //System.Diagnostics.Debug.WriteLine("KEY UP");

                if (PressedKeys.ContainsKey(key))
                {
                    PressedKeys.Remove(key);
                }

                e.Handled = true;
            }
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            BindMappingsDataGrid();
            BindMacrosDataGrid();

            FormLoaded = true;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveBindings();
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
    }
}
