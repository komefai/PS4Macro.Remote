// PS4Macro.Remote (File: Script.cs)
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

using PS4MacroAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS4Macro.Remote
{
    public class Script : ScriptBase
    {
        public MainForm MainForm { get; private set; }

        public Script()
        {
            Config.Name = "Remote";
            Config.LoopDelay = 0;
            Config.EnableCapture = false;

            ScriptForm = MainForm = new MainForm();
        }

        public override void OnMacroLapEnter(object sender)
        {
            StopMacro();
        }

        public override void OnStopped()
        {
            MainForm.Stopped();
        }

        public override void Start()
        {
            MainForm.Start();
        }

        public override void Update()
        {
            // Key is down
            if (MainForm.IsKeyDown())
            {
                try
                {
                    List<Keys> keys = MainForm.PressedKeys.Keys.ToList();
                    MainForm.KeyboardMap.ExecuteActionsByKey(this, keys);
                }
                catch (ArgumentException) { }
            }
            // Key is up
            else
            {
                ClearButtons();
            }
        }
    }
}
