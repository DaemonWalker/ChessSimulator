using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnShot_Click(object sender, EventArgs e)
        {
            IntPtr handle = IntPtr.Zero;
            try
            {
                handle = (comboBox1.SelectedItem as ProcessInfo).Hwnd;
                imgShot.Image = Snapshot.GetWindow(handle);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"handle: {handle.ToString()} {ex.Message}");
                MessageBox.Show("Error");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                while (true)
                {
                    var process = Process.GetProcesses();
                    var list = new List<ProcessInfo>();
                    foreach (var p in process)
                    {
                        
                        if (p.MainWindowHandle != IntPtr.Zero)
                        {
                            list.Add(new ProcessInfo(p));
                        }
                    }
                    SetProcesses(list);
                    Thread.Sleep(1000);
                }
            });
        }
        private void SetProcesses(List<ProcessInfo> processInfos)
        {
            if (this.comboBox1.InvokeRequired)
            {
                this.comboBox1.Invoke(new Action(() => SetProcesses(processInfos)));
            }
            else
            {
                for (int i = this.comboBox1.Items.Count - 1; i >= 0; i--)
                {
                    if (processInfos.Contains(this.comboBox1.Items[i]) == false)
                    {
                        this.comboBox1.Items.RemoveAt(i);
                    }
                }
                this.comboBox1.Items.AddRange(processInfos.Where(p => this.comboBox1.Items.Contains(p) == false).ToArray());
            }
        }
        private class ProcessInfo
        {
            public string Title { get; }
            public IntPtr Hwnd { get; }
            public ProcessInfo(Process p)
            {
                this.Title = p.MainWindowTitle;
                this.Hwnd = p.MainWindowHandle;
            }
            public override string ToString() => this.Title;
            public override bool Equals(object obj)
            {
                var info = obj as ProcessInfo;
                if (info is null && this != null)
                {
                    return false;
                }
                return this.Title == info.Title && this.Hwnd.ToInt32() == info.Hwnd.ToInt32();
            }
        }
    }
}
