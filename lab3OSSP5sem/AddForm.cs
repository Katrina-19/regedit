using System;
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
    public partial class AddForm : Form
    {
        public string SubName { get; set; }

        private ErrorProvider ep = new ErrorProvider();

        public AddForm()
        {
            InitializeComponent();
            this.FormClosing += AddForm_FormClosing;
        }
        private void AddForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                if (tbName.Text.Trim().Length == 0)
                {
                    ep.SetError(tbName, "Введите имя");
                    e.Cancel = true;
                }
                else
                {
                    ep.SetError(tbName, "");
                    SubName = this.tbName.Text.Trim();
                }
            }
        }
    }
}
