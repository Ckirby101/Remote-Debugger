﻿/*
 
The MIT License (MIT) 
 
Copyright (c) 2017 Savoury SnaX 
 
Permission is hereby granted, free of charge, to any person obtaining a copy 
of this software and associated documentation files (the "Software"), to deal 
in the Software without restriction, including without limitation the rights 
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
copies of the Software, and to permit persons to whom the Software is 
furnished to do so, subject to the following conditions: 
 
The above copyright notice and this permission notice shall be included in all 
copies or substantial portions of the Software. 
 
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE 
SOFTWARE. 

*/

using System;

namespace RemoteDebugger
{
    public partial class LogView : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        string viewName;
        public LogView(string name, string viewname)
        {
            viewName = viewname;
            InitializeComponent();
            Text = name;
        }
        override protected string GetPersistString()
        {
            return viewName;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string res;
            while (Program.t.messages.TryDequeue(out res))
            {
                while (res.Length>0)
                {
                    // Limit line length
                    int len = Math.Min(1024, res.Length);
                    string safeLine = res.Substring(0, len);
                    res = res.Substring(len);
                    listBox1.Items.Add(safeLine);
                }
                listBox1.TopIndex = listBox1.Items.Count - 1;
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            ActiveForm.AcceptButton = button1;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            ActiveForm.AcceptButton = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.t.SendCommand(textBox1.Text,null);
            textBox1.Text = "";
        }
    }

}