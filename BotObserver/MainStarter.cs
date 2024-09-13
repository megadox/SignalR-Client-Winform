using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Freeper.Beacon;

namespace BotObserver
{
    public partial class MainStarter : Form
    {
        BeaconTray beaconTray;
        public MainStarter()
        {
            InitializeComponent();

            beaconTray = new BeaconTray(this, Application.ProductName, Properties.Resources.Logo);

            this.FormClosing += MainStarter_FormClosing;
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            this.ShowIcon = false;
            this.Opacity = 0;

            int scWid = Screen.PrimaryScreen.WorkingArea.Width;
            int scHei = Screen.PrimaryScreen.WorkingArea.Height;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, scHei + this.Height);
            try
            {                

                InitContextMenu();

                ShowSetting();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);

            }
        }

        private void MainStarter_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure BA-Assist Observer Exit ?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
            {
                beaconTray.Hide();
                beaconTray.Stop();
            }
            else
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void InitContextMenu()
        {
            int idx = 0;

            MenuItem Mi_Setting = new MenuItem();
            Mi_Setting.Index = idx++;
            Mi_Setting.Text = "&Bot Observer";
            Mi_Setting.Name = "menu_setting";
            Mi_Setting.Click += Mi_Setting_Click;
            Mi_Setting.Visible = true;
            Mi_Setting.Enabled = true;


            MenuItem menuSeparator = new MenuItem("-");
            menuSeparator.Index = idx++;

            MenuItem Mi_exit = new MenuItem();
            Mi_exit.Index = idx++;
            Mi_exit.Name = "menu_exit";
            Mi_exit.Text = "Exit";
            Mi_exit.Click += Menu_item2_Click;


            MenuItem[] menuItems = new MenuItem[] { Mi_Setting, menuSeparator, Mi_exit };
            beaconTray.ContextMenu.MenuItems.AddRange(menuItems);
            //beaconTray.ContextMenu.Popup += TrayContextMenu_Popup;
        }

        #region *********** Tray Context Menu Event *****************
        private void Mi_Setting_Click(object sender, EventArgs e)
        {
            ShowSetting(true);
        }

        /// <summary>
        /// Application Exit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_item2_Click(object sender, EventArgs e)
        {
            ClsStruct.Application_Exit = true;
            Application.Exit();
        }

        private void ShowSetting(bool isshow = false)
        {

            if (!ClsStruct.SETTING_VIEW)
            {
                if (isshow)
                {
                    Observer frm = new Observer(this.beaconTray);
                    frm.Show();
                    ClsStruct.SETTING_VIEW = true;
                }
                else
                {
                    Observer frm = new Observer(this.beaconTray);
                    frm.Show();
                    Thread.Sleep(10);
                    ClsStruct.SETTING_VIEW = false;
                    frm.Hide();
                }
            }
        }

        #endregion
    }
}
