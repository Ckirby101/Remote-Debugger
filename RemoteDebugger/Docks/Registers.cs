/*
 
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using RemoteDebugger.Main;

namespace RemoteDebugger
{
    public partial class Registers : Form
    {
	    public enum Z80Register
	    {
		    a = 0,
		    hl,
		    bc,
		    de,

		    a_e,
		    hl_e,
			bc_e,
			de_e,

		    ix,
		    iy,

		    sp,
		    pc,

		    i,
		    r,

		    f,
		    f_e,

		    memptr,

			numRgisters

	    }


	    class RegisterItem
	    {
		    public Z80Register reg;

		    public string DisplayString;
		    public string RegisterName;
		    public int Value;

		    public string labelstring;		//if the register points to a memeory location we put the label here
		    public TextBox uiTextBox;

		    public Regex regex;
	    }





        delegate bool RegisterValidate(string toValidate,out string error);

	    private Regex BankReg;
        RegisterItem[] registerData;
        Dictionary<string, int> nameMap;

        string viewName;

        void InitialiseRegister(string name,string display,string regex,Z80Register index,TextBox UiTextBox)
        {
	        RegisterItem reg = new RegisterItem()
		        {RegisterName = name, Value = 0, regex = new Regex(regex), DisplayString = display,uiTextBox = UiTextBox,reg = index};

	        registerData[(int) index] = reg;

            //registerData.Add(new RegisterItem() { RegisterName = name, Value = 0 ,regex = new Regex(regex), DisplayString = display });
            //nameMap.Add(name, registerData.Count - 1);
        }

        bool VOneByte(string input, out string error)
        {
            error = "";
            int num = Convert.ToInt32(input, 16);
            if (num<0 || num>255)
            {
                error = "Hexadecimal input out of range (00-FF)";
                return false;
            }
            return true;
        }

        public Registers(string name, string viewname)
        {
            viewName = viewname;
            InitializeComponent();
            //dataGridView1.CausesValidation = false;
            registerData = new RegisterItem[ (int)Z80Register.numRgisters ];
            //regexList = new List<Regex>();
            nameMap = new Dictionary<string, int>();
            //validateList = new List<RegisterValidate>();


	        BankReg = new Regex(@"([O:A])(\d+)\s([O:A])(\d+)\s([O:A])(\d+)\s([O:A])(\d+)\s([O:A])(\d+)\s([O:A])(\d+)\s([O:A])(\d+)\s([O:A])(\d+)\s");
            InitialiseRegister("A", "X2", @"AF\s*=\s*([a-fA-F0-9]{2})[a-fA-F0-9]{2}\s+",Z80Register.a,RegA);
	        InitialiseRegister("HL", "X4", @"HL\s*=\s*([a-fA-F0-9]{4})\s+",Z80Register.hl,RegHL);
	        InitialiseRegister("BC", "X4", @"BC\s*=\s*([a-fA-F0-9]{4})\s+",Z80Register.bc,RegBC);
	        InitialiseRegister("DE", "X4", @"DE\s*=\s*([a-fA-F0-9]{4})\s+",Z80Register.de,RegDE);


	        InitialiseRegister("A'", "X2", @"A\'\s*=\s*([a-fA-F0-9]{2})\s+",Z80Register.a_e,RegExA);
	        InitialiseRegister("HL'", "X4", @"HL\'\s*=\s*([a-fA-F0-9]{4})\s+",Z80Register.hl_e,RegExHL);
	        InitialiseRegister("BC'", "X4", @"BC\'\s*=\s*([a-fA-F0-9]{4})\s+",Z80Register.bc_e,RegExBC);
	        InitialiseRegister("DE'", "X4", @"DE\'\s*=\s*([a-fA-F0-9]{4})\s+",Z80Register.de_e,RegExDE);

	        InitialiseRegister("IX", "X4", @"IX\s*=\s*([a-fA-F0-9]{4})\s+",Z80Register.ix,RegIX);
	        InitialiseRegister("IY", "X4", @"IY\s*=\s*([a-fA-F0-9]{4})\s+",Z80Register.iy,RegIY);

	        InitialiseRegister("SP", "X4", @"SP\s*=\s*([a-fA-F0-9]{4})\s+",Z80Register.sp,RegSP);
	        InitialiseRegister("PC", "X4", @"PC\s*=\s*([a-fA-F0-9]{4})\s+",Z80Register.pc,RegPC);

	        InitialiseRegister("I", "X2", @"I\s*=\s*([a-fA-F0-9]{2})\s+",Z80Register.i,RegI);
	        InitialiseRegister("R", "X2", @"R\s*=\s*([a-fA-F0-9]{2})\s+",Z80Register.r,RegR);

	        InitialiseRegister("F", "X2", @"AF\s*=\s*[a-fA-F0-9]{2}([a-fA-F0-9]{2})\s+",Z80Register.f,RegF);
	        InitialiseRegister("F'", "X2", @"F\'\s*=\s*([SZ5P3HNC ]+)\s+",Z80Register.f_e,RegExF);
	        InitialiseRegister("MEMPTR", "X2", @"MEMPTR\s*=\s*([a-fA-F0-9]{4})\s+",Z80Register.memptr,null);


            //InitialiseRegister("MEMPTR", "0000",@"MEMPTR\s*=\s*([a-fA-F0-9]{4})\s+",VUnhandled);
            //InitialiseRegister("INT", "DI", @"\s+(DI|EI)\s+",VUnhandled);
            //InitialiseRegister("IMode", "IM1", @"\s+(IM0|IM1|IM2)\s+",VUnhandled);
            //InitialiseRegister("VPS", "0", @"VPS\s*:\s*(\w)",VUnhandled);


            // Add registers
            //dataGridView1.DataSource = registerData;

            //dataGridView1.Columns[0].ReadOnly = true;
            //dataGridView1.AllowUserToAddRows = false;
            //dataGridView1.RowHeadersVisible = true;
            //dataGridView1.CausesValidation = true;
        }
        //override protected string GetPersistString()
        //{
        //    return viewName;
        //}

        public void RequestUpdate(CommandResponse cb)
        {
	        Program.telnetConnection.SendCommand("get-memory-pages", BankCallback);
	        //Program.telnetConnection.SendCommand("tbblue-get-register 81", BankCallback,81);
	        //Program.telnetConnection.SendCommand("tbblue-get-register 82", BankCallback,82);
	        //Program.telnetConnection.SendCommand("tbblue-get-register 83", BankCallback,83);
	        //Program.telnetConnection.SendCommand("tbblue-get-register 84", BankCallback,84);
	        //Program.telnetConnection.SendCommand("tbblue-get-register 85", BankCallback,85);
	        //Program.telnetConnection.SendCommand("tbblue-get-register 86", BankCallback,86);
	        //Program.telnetConnection.SendCommand("tbblue-get-register 87", BankCallback,87);
	        Program.telnetConnection.SendCommand("get-registers", cb);

        }



        public string GetRegisterValue(string reg)
        {
            //return registerData[nameMap[reg]].Value;
	        return "";
        }

	    /// -------------------------------------------------------------------------------------------------
	    /// <summary> Gets register valueint. </summary>
	    ///
	    /// <remarks> 07/09/2018. </remarks>
	    ///
	    /// <param name="reg"> The register. </param>
	    ///
	    /// <returns> The register valueint. </returns>
	    /// -------------------------------------------------------------------------------------------------
	    public int GetRegisterValueint(Z80Register reg)
	    {
		    return registerData[(int)reg].Value;
	    }

	    public string GetRegisterLabelString(Z80Register reg)
	    {
		    return registerData[(int)reg].labelstring;
	    }
	    /// -------------------------------------------------------------------------------------------------
	    /// <summary> Updates the registers described by items. </summary>
	    ///
	    /// <remarks> 07/09/2018. </remarks>
	    ///
	    /// <param name="items"> The items. </param>
	    /// -------------------------------------------------------------------------------------------------
	    public void UpdateRegisters(string[] items)
	    {
            if (items.Count() != 1)
                return;

		    for (int r = 0; r < registerData.Length; r++)
		    {
			    Match m = registerData[r].regex.Match(items[0]);
			    if (m.Success)
			    {

				    if (!string.IsNullOrEmpty(m.Groups[1].Value))
					    int.TryParse(m.Groups[1].Value, NumberStyles.HexNumber, null, out registerData[r].Value);

				    
				    //memptr is off by one!!
				    if (registerData[r].reg == Z80Register.memptr)
				    {
					    registerData[r].Value--;

				    }


				    if (registerData[r].reg == Z80Register.hl ||
				        registerData[r].reg == Z80Register.de ||
				        registerData[r].reg == Z80Register.bc ||
				        registerData[r].reg == Z80Register.ix ||
				        registerData[r].reg == Z80Register.iy ||
				        registerData[r].reg == Z80Register.hl_e ||
				        registerData[r].reg == Z80Register.de_e ||
				        registerData[r].reg == Z80Register.bc_e ||
					    registerData[r].reg == Z80Register.memptr)
				    {
					    int offset;
					    Labels.Label l;
					    if (Labels.GetLabelWithOffset(registerData[r].Value,out l,out offset))
					    {
							if (offset!=0)
							    registerData[r].labelstring = l.label+"+"+offset;
							else
							    registerData[r].labelstring = l.label;
					    }
					    else
					    {
						    registerData[r].labelstring = "";
					    }

				    }


			    }
		    }



		    UIUpdate();

	    }

        /// -------------------------------------------------------------------------------------------------
        /// <summary> Updates this object. </summary>
        ///
        /// <remarks> 07/09/2018. </remarks>
        /// -------------------------------------------------------------------------------------------------
        void UIUpdate()
        {

	        for (int r = 0; r < registerData.Length; r++)
	        {
		        if (registerData[r].uiTextBox != null)
		        {
			        registerData[r].uiTextBox.Text = "$"+registerData[r].Value.ToString(registerData[r].DisplayString) + " / " +
			                                         registerData[r].Value.ToString();

		        }

	        }


	        BankData.Text = String.Format("${0:X2}  ${1:X2}  ${2:X2}  ${3:X2}  ${4:X2}  ${5:X2}  ${6:X2}  ${7:X2}", MainForm.banks[0],
		        MainForm.banks[1], MainForm.banks[2], MainForm.banks[3], MainForm.banks[4], MainForm.banks[5],
		        MainForm.banks[6], MainForm.banks[7]);


	        int flg = registerData[(int) Z80Register.f].Value;
	        string flags = " ";


	        string[] flagsoff = { "- ","- ","- ","- ","- ","- ","- ","- " };
	        string[] flagson = { "C ","N ","P ","3 ","H ","5 ","Z ","S " };

	        for (int i = 7; i >= 0; i--)
	        {
		        if ( (flg & (1 << i)) !=0 )
		        {
			        flags = flags + flagson[i];
		        }
		        else
		        {
			        flags = flags + flagsoff[i];

		        }

	        }

	        RegFlags.Text = flags;





/*            if (items.Count() != 1)
                return;
            bool updated = false;
            dataGridView1.CausesValidation = false;
            for (int r = 0; r < regexList.Count; r++)
            {
                Match m = regexList[r].Match(items[0]);
                if (m.Success)
                {
                    string newValue = m.Groups[1].Value;
                    if (registerData[r].Value != newValue)
                    {
                        registerData[r].Value = newValue;
                        updated = true;
                    }
                }
            }
            if (updated)
            {
                dataGridView1.Invalidate(true);
            }
            dataGridView1.CausesValidation = true;*/
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary> Callbacks. </summary>
        ///
        /// <remarks> 07/09/2018. </remarks>
        ///
        /// <param name="response"> The response. </param>
        /// <param name="tag">	    The tag. </param>
        /// -------------------------------------------------------------------------------------------------
        void Callback(string[] response,int tag)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke((MethodInvoker)delegate { UpdateRegisters(response); });
                }
                else
                {
	                UpdateRegisters(response);
                }
            }
            catch
            {

            }
        }



	    /// -------------------------------------------------------------------------------------------------
	    /// <summary> Callback, called when the bank. </summary>
	    ///
	    /// <remarks> 07/09/2018. </remarks>
	    ///
	    /// <param name="response"> The response. </param>
	    /// <param name="tag">	    The tag. </param>
	    /// -------------------------------------------------------------------------------------------------
	    void BankCallback(string[] response,int tag)
	    {
		    if (response.Count() != 1) return;

		    Match m = BankReg.Match(response[0]);


		    for (int i = 0; i < (16/2); i++)
		    {
			    if (m.Groups[1 + (i*2)].ToString() == "O")
			    {
				    MainForm.banks[ i ] = 0xff;
			    }
			    else
			    {
				    int bank;
				    if (int.TryParse(m.Groups[2 + (i*2)].ToString(), NumberStyles.Integer, null, out bank))
					    MainForm.banks[ i ] = bank;
				    
			    }




		    }

	    }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
/*            if (e.ColumnIndex == 1)
            {
                dataGridView1.Rows[e.RowIndex].ErrorText = "";
                string regName = dataGridView1.Rows[e.RowIndex].Cells[0].Value as string;
                string cValue = dataGridView1.Rows[e.RowIndex].Cells[1].Value as string;
                if (cValue != e.FormattedValue.ToString())
                {
                    string err;
                    if (validateList[e.RowIndex](e.FormattedValue.ToString(), out err))
                    {
                        Program.telnetConnection.SendCommand("set-register " + regName + "=" + e.FormattedValue.ToString()+"h", null);
                    }
                    dataGridView1.Rows[e.RowIndex].ErrorText = err;
                }
            }*/
        }


	    void RegChangeCallback(string[] response,int tag)
	    {
		    try
		    {
			    if (InvokeRequired)
			    {
				    Invoke((MethodInvoker)delegate { Program.myMainForm.UpdateAllWindows(Program.InStepMode); });
			    }
			    else
			    {
				    Program.myMainForm.UpdateAllWindows(Program.InStepMode);
			    }
		    }
		    catch
		    {

		    }
	    }


	    private void UpdateRegister(KeyEventArgs e, string s, Z80Register reg, string assign)
	    {
		    if (e.KeyCode == Keys.Enter)
		    {

			    int addr = 0;
			    if (MainForm.ParseExpression(s, ref addr))
			    {
				    registerData[(int) reg].Value = addr;


				    Program.telnetConnection.SendCommand("set-register "+assign+""+addr, RegChangeCallback);

				    UIUpdate();
			    }



		    }

	    }

		private void RegA_KeyDown(object sender, KeyEventArgs e)
		{
			UpdateRegister(e, RegA.Text,Z80Register.a,"a=");
		}


		private void RegExA_KeyDown(object sender, KeyEventArgs e)
		{
			UpdateRegister(e, RegExA.Text,Z80Register.a_e,"a'=");

		}

		private void RegHL_KeyDown(object sender, KeyEventArgs e)
		{
			UpdateRegister(e, RegHL.Text,Z80Register.hl,"hl=");

		}

		private void RegDE_KeyDown(object sender, KeyEventArgs e)
		{
			UpdateRegister(e, RegDE.Text,Z80Register.de,"de=");

		}

		private void RegBC_KeyDown(object sender, KeyEventArgs e)
		{
			UpdateRegister(e, RegBC.Text,Z80Register.bc,"bc=");

		}

		private void RegPC_KeyDown(object sender, KeyEventArgs e)
		{
			UpdateRegister(e, RegPC.Text,Z80Register.pc,"pc=");

		}

		private void RegExHL_KeyDown(object sender, KeyEventArgs e)
		{
			UpdateRegister(e, RegExHL.Text,Z80Register.hl_e,"hl'=");

		}

		private void RegExDE_KeyDown(object sender, KeyEventArgs e)
		{
			UpdateRegister(e, RegExDE.Text,Z80Register.de_e,"de'=");

		}

		private void RegExBC_KeyDown(object sender, KeyEventArgs e)
		{
			UpdateRegister(e, RegExBC.Text,Z80Register.bc_e,"bc'=");

		}

		private void RegIX_KeyDown(object sender, KeyEventArgs e)
		{
			UpdateRegister(e, RegIX.Text,Z80Register.ix,"ix=");

		}

		private void RegIY_KeyDown(object sender, KeyEventArgs e)
		{
			UpdateRegister(e, RegIY.Text,Z80Register.iy,"iy=");

		}

		private void RegSP_KeyDown(object sender, KeyEventArgs e)
		{
			UpdateRegister(e, RegSP.Text,Z80Register.sp,"sp=");

		}
	}


}
