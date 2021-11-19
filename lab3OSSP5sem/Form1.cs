using Microsoft.Win32;
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
    public partial class Form1 : Form
    {
        private Dictionary<string, RegistryKey> rootRegistry = new Dictionary<string, RegistryKey>()
        {
            { "HKEY_LOCAL_MACHINE", Registry.LocalMachine },
            { "HKEY_CLASSES_ROOT", Registry.ClassesRoot },
            { "HKEY_CURRENT_CONFIG", Registry.CurrentConfig },
            { "HKEY_CURRENT_USER", Registry.CurrentUser },
            { "HKEY_DYN_DATA",Registry.DynData},
            { "HKEY_USERS", Registry.Users },
            { "HKEY_PERFORMANCE_DATA", Registry.PerformanceData}
        };

        public Form1()
        {
            InitializeComponent();
            this.Tree.BeforeExpand += Tree_BeforeExpand;
            this.Tree.AfterSelect += Tree_AfterSelect;
            Tree.ContextMenuStrip = cmnTree;
            listReg.ContextMenuStrip = cmnList;

            ColumnHeader ch = new ColumnHeader();
            ch.Text = "Имя";
            this.listReg.Columns.Add(ch);

            ch = new ColumnHeader();
            ch.Text = "Тип";
            this.listReg.Columns.Add(ch);

            ch = new ColumnHeader();
            ch.Text = "Значение";
            this.listReg.Columns.Add(ch);
            for (int i = 0; i < this.listReg.Columns.Count; i++)
            {
                listReg.Columns[i].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            }

            this.FillRoot();
        }
        #region TREE

        private void Tree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode t = e.Node;
            RegistryKey rKey = t.Tag as RegistryKey;
            if (rKey == null) { return; }

            this.UpdateList(rKey);
        }

        private void Tree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode t = e.Node;

            if (t.Text == "REGISTRY") { return; }

            t.Nodes.Clear();
            RegistryKey rKey = t.Tag as RegistryKey;
            if (rKey == null) { return; }

            string[] names = rKey.GetSubKeyNames();

            foreach (string name in names)
            {
                try
                {
                    RegistryKey tmpKey = rKey.OpenSubKey(name, true);
                    TreeNode tmpNode = new TreeNode(name, 0, 1);
                    tmpNode.Tag = tmpKey;
                    tmpNode.Nodes.Add("^_^");
                    t.Nodes.Add(tmpNode);
                }
                catch { }
            }
        }

        private void Tree_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left) { return; }

            TreeNode tn = this.Tree.GetNodeAt(e.Location);
            if (tn != null)
            {
                this.Tree.SelectedNode = tn;
            }
        }

        private void cmnAddTree_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.Tree.SelectedNode;
            if (selectedNode == null || selectedNode.Parent == null) { return; }

            RegistryKey currentRegKey = this.Tree.SelectedNode.Tag as RegistryKey;

            if (currentRegKey == null || rootRegistry.Values.Contains(currentRegKey)) { return; }

            AddForm addForm = new AddForm();
            if (addForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    currentRegKey.CreateSubKey(addForm.SubName);
                    if (!selectedNode.IsExpanded) { selectedNode.Expand(); }
                    this.UpdateNode(selectedNode, currentRegKey);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void cmnDeleteTree_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.Tree.SelectedNode;
            TreeNode parentNode = selectedNode.Parent;
            if (selectedNode == null || parentNode == null || parentNode.Parent == null) { return; }

            RegistryKey currentRegKey = parentNode.Tag as RegistryKey;

            if (currentRegKey == null) { return; }

            try
            {
                currentRegKey.DeleteSubKey(selectedNode.Text, true);
                this.UpdateNode(parentNode, currentRegKey);
                this.UpdateList(currentRegKey);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void UpdateNode(TreeNode selectedNode, RegistryKey currentRegKey)
        {
            selectedNode.Nodes.Clear();
            string[] names = currentRegKey.GetSubKeyNames();
            foreach (string name in names)
            {
                try
                {
                    RegistryKey tmpKey = currentRegKey.OpenSubKey(name, true);
                    TreeNode tmpNode = new TreeNode(name);
                    tmpNode.Tag = tmpKey;
                    tmpNode.Nodes.Add("^_^");
                    selectedNode.Nodes.Add(tmpNode);
                }
                catch { }
            }
        }

        #endregion
         #region LISTVIEW

        private void cmnAddList_Click(object sender, EventArgs e)
        {
            TreeNode currentNode = this.Tree.SelectedNode;
            if (currentNode == null || currentNode.Parent == null) { return; }

            RegistryKey currentRegKey = currentNode.Tag as RegistryKey;
            if (currentRegKey == null || this.rootRegistry.Values.Contains(currentRegKey)) { return; }

            ItemForm form = new ItemForm();

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    currentRegKey.SetValue(form.ValueName, form.ValueValue, form.ValueType);
                    this.UpdateList(currentRegKey);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void cmnDeleteList_Click(object sender, EventArgs e)
        {
            if (this.listReg.SelectedIndices.Count == 0) { return; }

            RegistryKey currentRegKey = this.Tree.SelectedNode.Tag as RegistryKey;
            if (currentRegKey == null) { return; }

            string name = listReg.SelectedItems[0].Text;
            try
            {
                currentRegKey.DeleteValue(name);
                this.UpdateList(currentRegKey);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmnEditList_Click(object sender, EventArgs e)
        {
            if (this.listReg.SelectedIndices.Count == 0) { return; }

            RegistryKey currentRegKey = this.Tree.SelectedNode.Tag as RegistryKey;
            if (currentRegKey == null) { return; }

            string name = listReg.SelectedItems[0].Text;
            RegistryValueKind type = currentRegKey.GetValueKind(name);
            object value = currentRegKey.GetValue(name);

            ItemForm form = new ItemForm(name, type, value);
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                currentRegKey.SetValue(form.ValueName, form.ValueValue, form.ValueType);
                this.UpdateList(currentRegKey);
            }
        }

        private void UpdateList(RegistryKey rKey)
        {
            this.listReg.Items.Clear();
            string[] names = rKey.GetValueNames();
            foreach (string name in names)
            {
                ListViewItem lvi = new ListViewItem(name);
                RegistryValueKind rType = rKey.GetValueKind(name);
                lvi.Tag = rType;

                string value = null;
                if (rType == RegistryValueKind.Binary)
                {
                    byte[] valueObj = rKey.GetValue(name) as byte[];
                    foreach (byte item in valueObj)
                    {
                        value += item.ToString("X2") + " ";
                    }
                }
                else
                {
                    value = rKey.GetValue(name).ToString();
                }

                string type = rType.ToString();
                lvi.SubItems.Add(type);
                lvi.SubItems.Add(value.Trim());
                this.listReg.Items.Add(lvi);
            }

            for (int i = 0; i < this.listReg.Columns.Count; i++)
            {
                if (this.listReg.Items.Count > 0)
                {
                    this.listReg.Columns[i].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                }
                else
                {
                    this.listReg.Columns[i].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                }
            }
        }
        
        #endregion

        private void FillRoot()
        {
            TreeNode root = new TreeNode("REGISTRY");
            foreach (KeyValuePair<string, RegistryKey> item in rootRegistry)
            {
                TreeNode t = new TreeNode(item.Key, 0, 1);
                t.Tag = item.Value;
                t.Nodes.Add("^_^");
                root.Nodes.Add(t);
            }
            this.Tree.Nodes.Add(root);
        }
    }
}
