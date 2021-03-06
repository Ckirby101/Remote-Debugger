﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteDebugger.Docks
{
	public partial class MemWatch : Form
	{
		private ByteProvider byteProvider;
		private int memaddress = 0;
		public MemWatch()
		{
			InitializeComponent();



			byteProvider = new ByteProvider();
			byteProvider.init(512,0);
			MEMPTRHexControl.Model.ByteProvider = byteProvider;
			MEMPTRHexControl.UpdateView();
		}



		/// -------------------------------------------------------------------------------------------------
		/// <summary> Updates the memory. </summary>
		///
		/// <remarks> 09/09/2018. </remarks>
		/// -------------------------------------------------------------------------------------------------
		public void UpdateMemory()
		{
			int v = memaddress;
			ByteProvider bp = byteProvider;
			bp.offset = v;
			Program.telnetConnection.SendCommand("read-memory "+v.ToString()+" 512", Callback,0);
			//label1.Text = "( MEMPTR ) $" + v.ToString("X4")+" "+MainForm.myNewRegisters.GetRegisterLabelString(Registers.Z80Register.memptr);
		}


		/// -------------------------------------------------------------------------------------------------
		/// <summary> Callbacks. </summary>
		///
		/// <remarks> 09/09/2018. </remarks>
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
					Invoke((MethodInvoker)delegate { UIUpdate(response,tag); });
				}
				else
				{
					UIUpdate(response,tag);
				}
			}
			catch
			{
                
			}
		}

		/// -------------------------------------------------------------------------------------------------
		/// <summary> Updates this object. </summary>
		///
		/// <remarks> 09/09/2018. </remarks>
		///
		/// <param name="response"> The response. </param>
		/// <param name="tag">	    The tag. </param>
		/// -------------------------------------------------------------------------------------------------
		private void UIUpdate(string[] response,int tag)
		{
			byteProvider.parseData(response[0]);
			MEMPTRHexControl.UpdateView();
		}

		private void AddrtextBox_TextChanged(object sender, EventArgs e)
		{
			string s = AddrtextBox.Text;

			MainForm.ParseExpression(s,ref memaddress);
			UpdateMemory();
		}


	}
}
