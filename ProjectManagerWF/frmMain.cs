using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Model;

namespace ProjectManagerWF
{
    public partial class frmMain : Form
    {
        private Model.ProjectManager projects;
        private Users users;

        public frmMain()
        {
            InitializeComponent();

            Model.ProjectManager.Load("projects", out projects);
            Users.Load("users", out users);

            for(int i = 0; i < projects.Count; i++)
            {
                TreeNode root = new TreeNode(projects[i].Name);
                root.Tag = projects[i];
                FillNode(root, (ITaskContainer)projects[i]);
                treeView1.Nodes.Add(root);
            }
        }

        private void FillNode(TreeNode root, ITaskContainer task)
        {
            foreach(IAssignable child in task.SubTasks)
            {
                TreeNode node = new TreeNode(child.Name);
                node.Tag = child;
                if ((child as ITaskContainer) != null)
                    FillNode(node, (ITaskContainer)child);
                root.Nodes.Add(node);
            }
        }


        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            Model.ProjectManager.Save("projects", projects);
            Users.Save("users", users);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAddProject frmAddProject = new frmAddProject();
            if (frmAddProject.ShowDialog() != DialogResult.OK)
                return;
            Project project = new Project(frmAddProject.MaxTaskCount)
            {
                Name = frmAddProject.ProjectName
            };
            TreeNode root = new TreeNode(project.Name);
            root.Tag = project;
            treeView1.Nodes.Add(root);
            projects.Add(project);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
                return;

            ITaskContainer container = treeView1.SelectedNode.Tag as ITaskContainer;
            if(container == null)
            {
                MessageBox.Show("В выбранный объект невозможно добавить задачу");
                return;
            }

            frmAddTask frmAddTask = new frmAddTask();
            if (frmAddTask.ShowDialog() != DialogResult.OK)
                return;
            try
            {
                container.AddSubTask(frmAddTask.Task);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            TreeNode node = new TreeNode(frmAddTask.Task.Name);
            node.Tag = frmAddTask.Task;
            treeView1.SelectedNode.Nodes.Add(node);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
                return;

            TreeNode node = treeView1.SelectedNode.Parent;
            if(node != null)
            {
                ITaskContainer parent = (ITaskContainer)node.Tag;
                parent.RemoveTask((IAssignable)treeView1.SelectedNode.Tag);
                node.Nodes.Remove(treeView1.SelectedNode);
            }
            else
            {
                projects.Remove((Project)treeView1.SelectedNode.Tag);
                treeView1.Nodes.Remove(treeView1.SelectedNode);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void addUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUser frmAddUser = new frmAddUser();
            if (frmAddUser.ShowDialog() != DialogResult.OK)
                return;
            users.Add(frmAddUser.User);
        }

        private void showAllUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (users.Count == 0)
                MessageBox.Show("Список пользователей пуст");
            else
                MessageBox.Show(string.Join("\n", users));
        }

        private void removeUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRemoveUser frmRemoveUser = new frmRemoveUser(users);
            if (frmRemoveUser.ShowDialog() != DialogResult.OK)
                return;
            foreach (User user in frmRemoveUser.SelectedUsers)
            {
                users.Remove(user);
                foreach (Project project in projects)
                {
                    foreach (IAssignable task in project.SubTasks)
                        removeUser(task, user);
                }
            }
        }

        private void removeUser(IAssignable task, User user)
        {
            task.DischargePerformer(user);
            ITaskContainer container = task as ITaskContainer;
            if (container == null)
                return;
            foreach (IAssignable t in container.SubTasks)
                removeUser(t, user);                
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            button4.Enabled = treeView1.SelectedNode.Tag as Project == null;
            button5.Enabled = treeView1.SelectedNode.Tag as Project == null;
            button6.Enabled = treeView1.SelectedNode.Tag as Project == null;

            textBox1.Text = treeView1.SelectedNode.Tag.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmSetStatus frmSetStatus = new frmSetStatus();
            if (frmSetStatus.ShowDialog() != DialogResult.OK)
                return;

            IAssignable task = treeView1.SelectedNode.Tag as IAssignable;
            if (task == null)
                return;

            task.Status = frmSetStatus.SelectedStatus;
            textBox1.Text = task.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
                return;
            frmRemoveUser frmRemoveUser = new frmRemoveUser(users);
            if (frmRemoveUser.ShowDialog() != DialogResult.OK)
                return;

            IAssignable task = (IAssignable)treeView1.SelectedNode.Tag;
            if((task as Bug != null || task as Task != null) && 
                frmRemoveUser.SelectedUsers.Length > 1)
            {
                MessageBox.Show("Для данного типа задачи можно определить только одного исполнителя!");
                return;
            }
            foreach (User user in frmRemoveUser.SelectedUsers)
                try
                {
                    task.AssignPerformer(user);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            textBox1.Text = task.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
                return;
            IAssignable task = (IAssignable)treeView1.SelectedNode.Tag;

            frmRemoveUser frmRemoveUser = new frmRemoveUser(task.Performers());
            if (frmRemoveUser.ShowDialog() != DialogResult.OK)
                return;

            foreach (User user in frmRemoveUser.SelectedUsers)
                task.DischargePerformer(user);

            textBox1.Text = task.ToString();
        }
    }
}
