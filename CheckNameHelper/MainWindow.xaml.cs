using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CheckNameHelper
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        VoiceHelper vh = new VoiceHelper();
        ExcelStuListReader reader = new ExcelStuListReader("compiler-05.xls");
        IList<StuInfo> stus;
        IList<StuInfo> ranlist;
        IList<StuInfo> srclist;
        MediaPlayer mp = new MediaPlayer();
        int sidx = 0, ridx = 0;
        int nowi = 0;
        int gradeidx;
        bool isRandom = false;
        string path = AppDomain.CurrentDomain.BaseDirectory + "/compiler-05/";
        public MainWindow()
        {
            InitializeComponent();
            mp.MediaEnded += mp_MediaEnded;
        }

        private void btnTestVoice_Click(object sender, RoutedEventArgs e)
        {
            sidx = 0;
            ridx = 0;
            ranlist = null;
            srclist = null;
            nowi = 0;
            isRandom = false;
            stus = null;
            gradeidx = 0;
            btnYes.IsEnabled = false;
            btnYes.IsEnabled = false;
            //string txt = btnTestVoice.Content.ToString();

            //string path = AppDomain.CurrentDomain.BaseDirectory + "/test/" + txt + ".mp3";
            //if (!Directory.Exists(path))
            //{
            //    Directory.CreateDirectory(System.IO.Path.GetFullPath(path));
            //}
            //Stream st = vh.GetVoiceFile(txt);
            //FileStream fs = File.OpenWrite(path);

            //MediaPlayer mp = new MediaPlayer();
            //mp.Open(new Uri(path));
            //mp.Play();

        }

        private void btnLoadExcel_Click(object sender, RoutedEventArgs e)
        {

            srclist = reader.ReadStuList();
            string path = AppDomain.CurrentDomain.BaseDirectory + "/compiler-05/";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            foreach (StuInfo stu in srclist)
            {
                string filename = path + stu.Name + ".mp3";
                if (File.Exists(filename) && new FileInfo(filename).Length > 0)
                {
                    continue;
                }
                Stream st = vh.GetVoiceFile(stu.Name);
                FileStream fs = new FileStream(filename, FileMode.Create);

                // vh.CopyStream(fs, st);
                st.Seek(0, SeekOrigin.Begin);
                st.CopyTo(fs);
                fs.Close();
                st.Close();


            }

            if (ranlist == null)
            {
                ranlist = new List<StuInfo>(srclist);
                int seed = DateTime.Now.Year * 10000 + DateTime.Now.Month * 100 + DateTime.Now.Day + (int)DateTime.Now.DayOfWeek;
                Random random = new Random(seed);
                for (int i = 0; i < ranlist.Count * 2; i++)
                {
                    int r1 = random.Next(ranlist.Count);
                    StuInfo s = ranlist[0];
                    ranlist[0] = ranlist[r1];
                    ranlist[r1] = s;
                }
            }
        }

        private void btn_Start_Click(object sender, RoutedEventArgs e)
        {
            if (stus == null || stus.Count <= 0)
                return;
            btnYes.IsEnabled = true;
            btnYes.IsEnabled = true;

            if (isRandom == true)
            {
                ridx = nowi;
                isRandom = false;
                nowi = sidx;
                stus = srclist;
            }



            if (nowi < stus.Count)
            {
                txtName.Text = stus[nowi].Name + " - " + stus[nowi].StuNo;
                mp.Open(new Uri(path + stus[nowi].Name + ".mp3"));
                mp.Play();


            }
        }

        void mp_MediaEnded(object sender, EventArgs e)
        {



        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            stus[nowi].Grades[0] = 1;
            ++nowi;
            if (nowi < stus.Count)
            {

                txtName.Text = stus[nowi].Name + " - " + stus[nowi].StuNo;
                mp.Open(new Uri(path + stus[nowi].Name + ".mp3"));
                mp.Play();

            }
            else
            {
                txtName.Text = "已经完成";
            }
        }

        private void btnRandom_Click(object sender, RoutedEventArgs e)
        {
            if (ranlist == null || ranlist.Count <= 0)
                return;
            btnYes.IsEnabled = true;
            btnYes.IsEnabled = true;

            if (isRandom == false)
            {
                sidx = nowi;
                isRandom = true;
                nowi = ridx;
                stus = ranlist;
            }
            if (nowi < stus.Count)
            {
                txtName.Text = stus[nowi].Name + " - " + stus[nowi].StuNo;
                mp.Open(new Uri(path + stus[nowi].Name + ".mp3"));
                mp.Play();


            }

        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            stus[nowi].Grades[0] = 0;
            ++nowi;
            if (nowi < stus.Count)
            {

                txtName.Text = stus[nowi].Name + " - " + stus[nowi].StuNo;
                mp.Open(new Uri(path + stus[nowi].Name + ".mp3"));
                mp.Play();

            }
            else
            {
                txtName.Text = "已经完成";
            }
        }
    }
}
