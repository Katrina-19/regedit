using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace lab3OSSP5sem
{
    public partial class ItemForm : Form
    {
        public string ValueName { get; set; }
        public RegistryValueKind ValueType { get; set; }
        public object ValueValue { get; set; }
        private bool IsEdit { get; set; }
        private List<char> hexSymbols = new List<char>() { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

        private RegistryValueKind[] regTypes =
        {
            RegistryValueKind.Binary,
            RegistryValueKind.DWord,
            RegistryValueKind.ExpandString,
            RegistryValueKind.MultiString,
            RegistryValueKind.None,
            RegistryValueKind.QWord,
            RegistryValueKind.String,
            RegistryValueKind.Unknown
        };

        private ErrorProvider ep = new ErrorProvider();
        public ItemForm()
        {
            InitializeComponent();
            this.Load += ItemForm_Load;
            this.FormClosing += ItemForm_FormClosing;

            foreach (RegistryValueKind item in this.regTypes)
            {
                this.cbType.Items.Add(item);
                this.cbType.SelectedIndex = 0;
            }
        }
        public ItemForm(string name, RegistryValueKind type, object value)
           : this()
        {
            this.IsEdit = true;
            this.ValueName = name;
            this.ValueType = type;
            this.ValueValue = value;
        }

        private void ItemForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (this.tbName.Text.Trim().Length == 0)
                {
                    this.ep.SetError(this.tbName, "Введите имя");
                    e.Cancel = true;
                }
                else
                {
                    this.ep.SetError(this.tbName, "");
                }
                this.ValueName = this.tbName.Text.Trim();
                this.ValueType = (RegistryValueKind)cbType.SelectedItem;

                if (this.ValueType == RegistryValueKind.Binary)
                {
                    List<byte> byteArr = new List<byte>();
                    string byteStr = this.tbValue.Text.Replace(" ", "");

                    if (byteStr.Length % 2 != 0) { byteStr = "0" + byteStr; }

                    for (int i = 0; i < byteStr.Length; i += 2)
                    {
                        try
                        {
                            byte current = Convert.ToByte(byteStr.Substring(i, 2), 16);
                            byteArr.Add(current);
                            this.ValueValue = byteArr.ToArray();
                        }
                        catch
                        {
                            this.ep.SetError(this.tbValue, "Не корректное значение");
                            e.Cancel = true;
                        }
                    }
                }
                else if (this.ValueType == RegistryValueKind.DWord)
                {
                    try
                    {
                        int.Parse(this.tbValue.Text);
                        this.ep.SetError(this.tbValue, "");
                        this.ValueValue = this.tbValue.Text.Trim();
                    }
                    catch
                    {
                        e.Cancel = true;
                        this.ep.SetError(this.tbValue, "Не корректное значение");
                    }
                }
                else
                {
                    this.ValueValue = this.tbValue.Text.Trim();
                }
            }
        }

        private void ItemForm_Load(object sender, EventArgs e)
        {
            if (this.IsEdit)
            {
                this.tbName.Text = this.ValueName;
                this.tbName.ReadOnly = true;

                this.cbType.SelectedItem = this.ValueType;
                this.cbType.Enabled = false;

                if (this.ValueType == RegistryValueKind.Binary)
                {
                    byte[] values = this.ValueValue as byte[];
                    string strValues = "";
                    foreach (byte item in values)
                    {
                        strValues += item.ToString("X2") + " ";
                    }
                    tbValue.Text = strValues.Trim();
                }
                else
                {
                    tbValue.Text = this.ValueValue.ToString();
                }
            }
            else
            {
                this.cbType.SelectedItem = RegistryValueKind.String;
            }
        }

        private void tbValue_TextChanged(object sender, EventArgs e)
        {
            if ((RegistryValueKind)this.cbType.SelectedItem == RegistryValueKind.Binary)
            {
                string currentString = tbValue.Text.Replace(" ", "").ToUpper();
                for (int i = 0; i < currentString.Length; i++)
                {
                    if (!this.hexSymbols.Contains(currentString[i]))
                    {
                        currentString = currentString.Replace(currentString[i], ' ');
                    }
                }
                this.tbValue.Text = currentString;
                this.tbValue.Select(tbValue.Text.Length, 0);
            }
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.tbValue.Text = "";
        }
    }
}
