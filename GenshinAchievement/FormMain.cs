﻿using GenshinAchievement.Core;
using GenshinAchievement.Model;
using GenshinAchievement.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenshinAchievement
{
    public partial class FormMain : Form
    {

        private ImageCapture capture = new ImageCapture();

        private YuanShenWindow window = new YuanShenWindow();

        int x, y, w, h;
        string userDataPath, imgPagePath, imgSectionPath;

        PaimonMoeJson paimonMoeJson = PaimonMoeJson.Builder();

        public FormMain()
        {
            InitializeComponent();
            userDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UserData");
            imgPagePath = Path.Combine(userDataPath, cboEdition.Text + "_img_page");
            imgSectionPath = Path.Combine(userDataPath, cboEdition.Text + "_img_section");
        }

        private bool YSStatus()
        {
            if (window.FindYSHandle())
            {
                lblYSStatus.ForeColor = Color.Green;
                lblYSStatus.Text = "已启动";
                return true;
            }
            else
            {
                lblYSStatus.ForeColor = Color.Red;
                lblYSStatus.Text = "未启动";
                return false;
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            YSStatus();
            foreach (var item in paimonMoeJson.All)
            {
                cboEdition.Items.Add(item.Key);
            }
            cboEdition.Text = "天地万象";
        }



        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void btnOCR_Click(object sender, EventArgs e)
        {
            List<OcrAchievement> list = LoadImgSection();
            OcrUtils.Ocr(list);
        }

        private void btnAutoArea_Click(object sender, EventArgs e)
        {
            if (!YSStatus())
            {
                PrintMsg("未找到原神进程，请先启动原神！");
                return;
            }

            window.Focus();
            capture.Start();
            Thread.Sleep(500);
            Rectangle rc = window.GetSize();
            x = (int)Math.Ceiling(rc.X * PrimaryScreen.ScaleX);
            y = (int)Math.Ceiling(rc.Y * PrimaryScreen.ScaleY);
            w = (int)Math.Ceiling(rc.Width * PrimaryScreen.ScaleX);
            h = (int)Math.Ceiling(rc.Height * PrimaryScreen.ScaleY);
            Bitmap ysPic = capture.Capture(x, y, w, h);
            Rectangle rect = ImageRecognition.CalculateCatchArea(ysPic);
            //pictureBox1.Image = ysPic;
            //pictureBox1.Image = capture.Capture(x + rect.X, y + rect.Y, rect.Width, rect.Height);
            //InitAreaWindows(rect);
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (!YSStatus())
            {
                PrintMsg("未找到原神进程，请先启动原神！");
                return;
            }
            btnStart.Enabled = false;

            capture.Start();

            // 1.切换到原神窗口
            PrintMsg($"切换到原神窗口");
            window.Focus();
            Thread.Sleep(200);

            // 2. 定位截图选区
            Rectangle rc = window.GetSize();
            x = (int)Math.Ceiling(rc.X * PrimaryScreen.ScaleX);
            y = (int)Math.Ceiling(rc.Y * PrimaryScreen.ScaleY);
            w = (int)Math.Ceiling(rc.Width * PrimaryScreen.ScaleX);
            h = (int)Math.Ceiling(rc.Height * PrimaryScreen.ScaleY);
            Bitmap ysWindowPic = capture.Capture(x, y, w, h);
            // 使用新的坐标
            Rectangle rect = ImageRecognition.CalculateCatchArea(ysWindowPic);
            PrintMsg($"已定位成就栏位置");
            x += rect.X;
            y += rect.Y;
            w = rect.Width + 2;
            h = rect.Height;

            PrintMsg($"0.5s后开始自动滚动截图，按F12终止！");
            Thread.Sleep(500);
            YSClick();

            IOUtils.CreateFolder(userDataPath);
            IOUtils.CreateFolder(imgPagePath);
            IOUtils.DeleteFolder(imgPagePath);

            paimonMoeJson = PaimonMoeJson.Builder();

            await Task.Run(() =>
            {
                // 3. 滚动截图
                int rowIn = 0, rowOut = 0, n = 0;
                while (rowIn < 15 && rowOut < 15)
                {
                    try
                    {
                        Bitmap pagePic = capture.Capture(x, y, w, h);
                        if (n % 20 == 0)
                        {
                            pagePic.Save(Path.Combine(imgPagePath, n + ".png"));
                            //PrintMsg($"{n}：截图并保存");
                        }

                        Bitmap onePixHightPic = capture.Capture(x, y + h - 20, w, 1); // 截取一个1pix的长条
                        if (ImageRecognition.IsInRow(onePixHightPic))
                        {
                            rowIn++;
                            rowOut = 0;
                        }
                        else
                        {
                            rowIn = 0;
                            rowOut++;
                        }
                    }
                    catch (Exception ex)
                    {
                        PrintMsg(ex.Message);
                    }

                    YSClick();
                    window.MouseWheelDown();
                    n++;
                }
                Bitmap lastPagePic = capture.Capture(x, y, w, h);
                lastPagePic.Save(Path.Combine(imgPagePath, ++n + ".png"));
                PrintMsg($"滚动截图完成");

                // 4. 分割截图
                PageToSection();
                PrintMsg($"切割成就图片完成");

                // 5. OCR
                List<OcrAchievement> list = LoadImgSection();
                OcrUtils.Ocr(list);
                PrintMsg($"OCR完成");
                Matching(list);
                PrintMsg($"成就匹配完成");
            });
            btnStart.Enabled = true;
        }



        /// <summary>
        /// 读取截图并切片
        /// </summary>
        private void PageToSection()
        {
            IOUtils.CreateFolder(imgSectionPath);
            IOUtils.DeleteFolder(imgSectionPath);

            DirectoryInfo dir = new DirectoryInfo(imgPagePath);
            foreach (FileInfo item in dir.GetFiles())
            {
                Bitmap imgPage = (Bitmap)Image.FromFile(item.FullName);
                List<Bitmap> list = ImageRecognition.Split(imgPage);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Save(Path.Combine(imgSectionPath, item.Name + "_" + i + ".png"));
                }
                //PrintMsg($"{item.Name}切片完成");
            }
        }

        private List<OcrAchievement> LoadImgSection()
        {
            List<OcrAchievement> list = new List<OcrAchievement>();
            DirectoryInfo dir = new DirectoryInfo(imgSectionPath);
            FileInfo[] fileInfo = dir.GetFiles();
            foreach (FileInfo item in fileInfo)
            {
                OcrAchievement achievement = new OcrAchievement();
                achievement.Image = (Bitmap)Image.FromFile(item.FullName);
                achievement.ImagePath = item.FullName;
                list.Add(achievement);
            }
            return list;
        }

        private void Matching(List<OcrAchievement> achievementList)
        {
            foreach (OcrAchievement a in achievementList)
            {
                paimonMoeJson.Matching(cboEdition.Text, a);
            }
        }

        private void YSClick()
        {
            window.MouseMove(x, y + h / 2);
            window.MouseLeftDown();
            window.MouseLeftUp();
        }

        private void PrintMsg(string msg)
        {
            msg = DateTime.Now + " " + msg;
            Console.WriteLine(msg);
            rtbConsole.Text += msg + Environment.NewLine;
            this.rtbConsole.SelectionStart = rtbConsole.TextLength;
            this.rtbConsole.ScrollToCaret();
        }

        private void btnExport1_Click(object sender, EventArgs e)
        {
            FormText form = new FormText(TextUtils.GeneratePaimonMoeJS(cboEdition.Text, paimonMoeJson));
            form.ShowDialog();
        }

        private void btnExport2_Click(object sender, EventArgs e)
        {

        }

    }
}
