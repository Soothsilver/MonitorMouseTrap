using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonitorMouseTrap
{
    public partial class Form1 : Form
    {
        IKeyboardMouseEvents g;
        public Form1()
        {
            InitializeComponent();
            g = Hook.GlobalEvents();
            g.MouseMoveExt += G_MouseMoveExt;
            g.KeyDown += G_KeyDown;
            g.KeyUp += G_KeyUp;
            this.Visible = false;
            this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            this.Owner = new Form();
            this.WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
            notifyIcon1.ShowBalloonTip(3000, "Mouse trap active", "To user the top monitor, hold Shift or lock Scroll lock.", ToolTipIcon.Info);
        }

        private void G_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.LShiftKey || e.KeyCode == Keys.RShiftKey)
            {
                shift = false;
            }
        }

        bool shift;

        private void G_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.LShiftKey || e.KeyCode == Keys.RShiftKey)
            {
                shift = true;
            }
        }

        private void G_MouseMoveExt(object sender, MouseEventExtArgs e)
        {
            //  this.Text = e.X + ":" + e.Y + ":" + e.Location;
            if (e.Y < 0 && !shift && !Control.IsKeyLocked(Keys.Scroll))
            {
                Cursor.Position = new Point(e.X, 0);
                e.Handled = true;
            }
        }       

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            g.MouseMoveExt -= G_MouseMoveExt;
            g.KeyUp -= G_KeyUp;
            g.KeyDown -= G_KeyDown;
            g.Dispose();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
