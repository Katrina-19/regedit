
namespace lab3OSSP5sem
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Tree = new System.Windows.Forms.TreeView();
            this.listReg = new System.Windows.Forms.ListView();
            this.cmnTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmnAddTree = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnDeleteTree = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmnAddList = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnEditList = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnDeleteList = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnTree.SuspendLayout();
            this.cmnList.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tree
            // 
            this.Tree.Location = new System.Drawing.Point(0, 0);
            this.Tree.Name = "Tree";
            this.Tree.Size = new System.Drawing.Size(342, 438);
            this.Tree.TabIndex = 0;
            this.Tree.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Tree_MouseDown);
            // 
            // listReg
            // 
            this.listReg.HideSelection = false;
            this.listReg.Location = new System.Drawing.Point(348, 0);
            this.listReg.Name = "listReg";
            this.listReg.Size = new System.Drawing.Size(452, 438);
            this.listReg.TabIndex = 1;
            this.listReg.UseCompatibleStateImageBehavior = false;
            this.listReg.View = System.Windows.Forms.View.Details;
            // 
            // cmnTree
            // 
            this.cmnTree.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmnTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnAddTree,
            this.cmnDeleteTree});
            this.cmnTree.Name = "cmnTree";
            this.cmnTree.Size = new System.Drawing.Size(146, 52);
            // 
            // cmnAddTree
            // 
            this.cmnAddTree.Name = "cmnAddTree";
            this.cmnAddTree.Size = new System.Drawing.Size(145, 24);
            this.cmnAddTree.Text = "Добавить";
            this.cmnAddTree.Click += new System.EventHandler(this.cmnAddTree_Click);
            // 
            // cmnDeleteTree
            // 
            this.cmnDeleteTree.Name = "cmnDeleteTree";
            this.cmnDeleteTree.Size = new System.Drawing.Size(145, 24);
            this.cmnDeleteTree.Text = "Удалить";
            this.cmnDeleteTree.Click += new System.EventHandler(this.cmnDeleteTree_Click);
            // 
            // cmnList
            // 
            this.cmnList.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmnList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnAddList,
            this.cmnEditList,
            this.cmnDeleteList});
            this.cmnList.Name = "cmnList";
            this.cmnList.Size = new System.Drawing.Size(148, 76);
            // 
            // cmnAddList
            // 
            this.cmnAddList.Name = "cmnAddList";
            this.cmnAddList.Size = new System.Drawing.Size(147, 24);
            this.cmnAddList.Text = "Добавить";
            this.cmnAddList.Click += new System.EventHandler(this.cmnAddList_Click);
            // 
            // cmnEditList
            // 
            this.cmnEditList.Name = "cmnEditList";
            this.cmnEditList.Size = new System.Drawing.Size(147, 24);
            this.cmnEditList.Text = "Изменить";
            this.cmnEditList.Click += new System.EventHandler(this.cmnEditList_Click);
            // 
            // cmnDeleteList
            // 
            this.cmnDeleteList.Name = "cmnDeleteList";
            this.cmnDeleteList.Size = new System.Drawing.Size(147, 24);
            this.cmnDeleteList.Text = "Удалить";
            this.cmnDeleteList.Click += new System.EventHandler(this.cmnDeleteList_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listReg);
            this.Controls.Add(this.Tree);
            this.Name = "Form1";
            this.Text = "Form1";
            this.cmnTree.ResumeLayout(false);
            this.cmnList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView Tree;
        private System.Windows.Forms.ListView listReg;
        private System.Windows.Forms.ContextMenuStrip cmnTree;
        private System.Windows.Forms.ContextMenuStrip cmnList;
        private System.Windows.Forms.ToolStripMenuItem cmnAddTree;
        private System.Windows.Forms.ToolStripMenuItem cmnDeleteTree;
        private System.Windows.Forms.ToolStripMenuItem cmnAddList;
        private System.Windows.Forms.ToolStripMenuItem cmnEditList;
        private System.Windows.Forms.ToolStripMenuItem cmnDeleteList;
    }
}

