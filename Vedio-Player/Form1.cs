using AxWMPLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using QuartzTypeLib;

namespace vedioPlayer
{

    public partial class Form1 : Form
    {
        public int maxvalue;
        private const int WS_CHILD = 0x40000000;
        private const int WS_CLIPCHILDREN = 0x2000000;
        private bool playStatus = false;
        public Boolean check = true;
        functions FunObj = new functions();

        public Form1()
        {
            InitializeComponent();
            //axWindowsMediaPlayer1.PositionChange += test;
        }
        private void test(object sender, _WMPOCXEvents_PositionChangeEvent e)
        {
            // if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            //  axWindowsMediaPlayer1.Ctlcontrols.currentPosition = trackBar1.Value;
            //UpdateStatusBarTime();
        }
        private void handlepos(object sender, EventArgs e)
        {
        }
        private void Form1_Load(object sender, EventArgs e)
        {


        }
      

        private void timer1_Tick(object sender, EventArgs e)
        {
            trackBar1.Value = (int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
            UpdateStatusBarTime();
        }

        private void UpdateTotalTime(int s)
        {
            trackBar1.Maximum = s;
            int h = s / 3600;
            int m = (s - (h * 3600)) / 60;
            s = s - (h * 3600 + m * 60);

            string duration = String.Format("{0:D2}:{1:D2}:{2:D2}", h, m, s);
            statusBarPanel2.Text = duration;
        }

        private void UpdateStatusBarTime()
        {

            if (axWindowsMediaPlayer1.Ctlcontrols.currentPosition >= 0)
            {
                int s, h, m;
                s = (int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                h = s / 3600;
                m = (s - (h * 3600)) / 60;
                s = s - (h * 3600 + m * 60);

                string curr = String.Format("{0:D2}:{1:D2}:{2:D2}", h, m, s);
                statusBarPanel1.Text = curr;


            }
        }

        private int GetMediaTimeLenSecond(string path)
        {
            try
            {
                Shell32.Shell shell = new Shell32.Shell();
                //文件路径              
                Shell32.Folder folder = shell.NameSpace(path.Substring(0, path.LastIndexOf("\\")));
                //文件名称            
                Shell32.FolderItem folderitem = folder.ParseName(path.Substring(path.LastIndexOf("\\") + 1));
                string len;
                if (Environment.OSVersion.Version.Major >= 6)
                {
                    len = folder.GetDetailsOf(folderitem, 27);
                }
                else
                {
                    len = folder.GetDetailsOf(folderitem, 21);
                }

                string[] str = len.Split(new char[] { ':' });
                int sum = 0;
                sum = int.Parse(str[0]) * 60 * 60 + int.Parse(str[1]) * 60 + int.Parse(str[2]);

                return sum;
            }
            catch (Exception ex) { return 0; }
        }


        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {
            //if(playStatus)
            //{
            //    this.Btn_Pause_Click_1(null,null);
            //}
            //else
            //{
            //    this.Btn_Play_Click_1(null, null);
            //}
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("深圳维周机器人科技有限公司，请联系QQ619576444");
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

            openFileDialog1.Filter = "(mp3,wav,mp4,mov,wmv,mpg)|*.mp3;*.wav;*.mp4;*.mov;*.wmv;*.mpgn|all files|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;
                axWindowsMediaPlayer1.URL = fileName;
                int s = this.GetMediaTimeLenSecond(fileName);
                this.UpdateTotalTime(s);
                statusBarPanel3.Text = "播放";
                UpdateStatusBarTime();
                timer1.Start();

                playStatus = true;
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "(srt)|*.srtn|all files|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                axWindowsMediaPlayer1.URL = openFileDialog1.FileName;
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void Btn_Play_Click_1(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.play();
            statusBarPanel3.Text = "播放";
            playStatus = true;

        }

        private void Btn_Previous_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.previous();

        }

        private void Btn_Next_Click_1(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.next();

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.fastForward();
            statusBarPanel3.Text = "快进";
        }

        private void Btn_FastBackward_Click_1(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.fastReverse();
            statusBarPanel3.Text = "快退";
        }

        private void Btn_Stop_Click_1(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
            statusBarPanel3.Text = "停止";
            playStatus = false;
        }

        private void Btn_Pause_Click_1(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
            statusBarPanel3.Text = "暂停";
            playStatus = false;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
                axWindowsMediaPlayer1.Ctlcontrols.currentPosition = trackBar1.Value;
            UpdateStatusBarTime();
        }

        private void axWindowsMediaPlayer1_Enter_1(object sender, EventArgs e)
        {

        }

        private void Btn_Pause_Click(object sender, EventArgs e)
        {

        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}