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
using RemoteDebugger.Docks;
using RemoteDebugger.Main;

namespace RemoteDebugger.Dialogs
{
	public partial class Watches : Form
	{
		BindingList<VarWatchData> varWatchData;

		public class VarWatchData: INotifyPropertyChanged
		{
			public int lastval;

			public bool local;				//is this a local var
			public int localcount = 10;		//how many steps local var stays active for

			public Labels.Label label = null;
			public string display = "";

			public string Variable
			{
				get { return label.label +" ($"+label.address.ToString("X4")+")"; }
			}

			public string Value
			{
				get { return (display); }

			}



			public event PropertyChangedEventHandler PropertyChanged;
			private void NotifyPropertyChanged(string p)
			{
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(p));
			}

		}
		


		/// -------------------------------------------------------------------------------------------------
		/// <summary> Default constructor. </summary>
		///
		/// <remarks> 12/09/2018. </remarks>
		/// -------------------------------------------------------------------------------------------------
		public Watches()
		{
			InitializeComponent();
			varWatchData = new BindingList<VarWatchData>();

			watchesGrid.AutoGenerateColumns = false;

			//watchesGrid.Columns.Add(new DataGridViewColumn("Variable"));
			//watchesGrid.Columns.Add(new DataGridViewColumn("Value"));
			//watchesGrid.Columns.Add(new DataGridViewColumn("Uninstall", typeof(System.Windows.Forms.Button)));

			watchesGrid.Columns[0].ReadOnly = true;
			watchesGrid.Columns[1].ReadOnly = true;

			watchesGrid.CellClick += dataGridViewSoftware_CellClick;
			
			watchesGrid.AllowUserToAddRows = false;
			watchesGrid.RowHeadersVisible = false;

			watchesGrid.DataSource = varWatchData;

		}


		private void dataGridViewSoftware_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == watchesGrid.Columns["Delete"].Index)
			{
				varWatchData.RemoveAt( e.RowIndex );
				Update();

				//Do something with your button.
			}
		}

		/// -------------------------------------------------------------------------------------------------
		/// <summary> Updates this object. </summary>
		///
		/// <remarks> 12/09/2018. </remarks>
		/// -------------------------------------------------------------------------------------------------
		public void Update()
		{
			for (int i = (varWatchData.Count - 1); i >= 0; i--)
			{
				if (varWatchData[i].local && varWatchData[i].localcount < 0)
				{
					varWatchData.RemoveAt(i);
				}

			}

			
			
			int index = 0;
			foreach (VarWatchData vmd in varWatchData)
			{
				if (vmd.local)
				{
					vmd.localcount--;
				}


				Program.telnetConnection.SendCommand("read-memory "+vmd.label.address.ToString()+" 2", Callback,index);
				index++;

			}



		}

		/// -------------------------------------------------------------------------------------------------
		/// <summary> Callbacks. </summary>
		///
		/// <remarks> 12/09/2018. </remarks>
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
		/// <remarks> 12/09/2018. </remarks>
		///
		/// <param name="response"> The response. </param>
		/// <param name="tag">	    The tag. </param>
		/// -------------------------------------------------------------------------------------------------
		private void UIUpdate(string[] response, int tag)
		{
			int value;

			if (int.TryParse(response[0], NumberStyles.HexNumber, null, out value))
			{
				value = MainForm.Endian(value);
				int val8 = (value & 0xff);
				int val16 = (value & 0xffff);
				
				varWatchData[tag].display = "M: "+response[0]+"   8: $"+val8.ToString("X2")+" / "+val8+"    16: $"+val16.ToString("X4")+" / "+val16;
				watchesGrid.InvalidateRow(tag);

				if (varWatchData[tag].lastval != val16)
					watchesGrid.Rows[tag].DefaultCellStyle.BackColor = Color.LightBlue;
				else
					watchesGrid.Rows[tag].DefaultCellStyle.BackColor = Color.White;


				varWatchData[tag].lastval = val16;
			}





		}


		/// -------------------------------------------------------------------------------------------------
		/// <summary> Event handler. Called by addbutton for click events. </summary>
		///
		/// <remarks> 12/09/2018. </remarks>
		///
		/// <param name="sender"> Source of the event. </param>
		/// <param name="e">	  Event information. </param>
		/// -------------------------------------------------------------------------------------------------
		private void addbutton_Click(object sender, EventArgs e)
		{
			string s = labeladdtext.Text.TrimStart(' ').TrimEnd(' ');
			Labels.Label l = Labels.FindLabel(s);
			if (l != null)
			{
				AddWatchLabel(l);
			}

		}

		/// -------------------------------------------------------------------------------------------------
		/// <summary> Adds a watch label. </summary>
		///
		/// <remarks> 19/09/2018. </remarks>
		///
		/// <param name="l"> The l control. </param>
		/// -------------------------------------------------------------------------------------------------
		public void AddWatchLabel(Labels.Label l)
		{
			if (HasLabel(l) == null)
			{

				VarWatchData vwd = new VarWatchData();
				vwd.label = l;
				varWatchData.Insert(0,vwd);
				vwd.local = false;

				Update();
			}


		}

		/// -------------------------------------------------------------------------------------------------
		/// <summary> Query if 'l' has label. </summary>
		///
		/// <remarks> 12/09/2018. </remarks>
		///
		/// <param name="l"> The l control. </param>
		///
		/// <returns> True if label, false if not. </returns>
		/// -------------------------------------------------------------------------------------------------
		private VarWatchData HasLabel(Labels.Label l)
		{
			foreach (VarWatchData vmd in varWatchData)
			{
				if (vmd.label == l) return vmd;
			}

			return null;


		}

		/// -------------------------------------------------------------------------------------------------
		/// <summary> Adds a local watch. </summary>
		///
		/// <remarks> 12/09/2018. </remarks>
		///
		/// <param name="l"> The l control. </param>
		/// -------------------------------------------------------------------------------------------------
		public void AddLocalWatch(Labels.Label l)
		{
			VarWatchData vmd = HasLabel(l);
			if (vmd != null)
			{
				vmd.localcount = 1;
			}
			else
			{	 
				vmd = new VarWatchData();
				vmd.localcount = 1;
				vmd = new VarWatchData();
				vmd.label = l;
				varWatchData.Add(vmd);
				vmd.local = true;

				
			}


		}




	}
}
