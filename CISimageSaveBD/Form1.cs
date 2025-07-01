using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CISimageSaveBD
{
    public partial class Form1 : Form
    {
        string configPath = "config.txt";

        public Form1()
        {
            InitializeComponent();
            LoadConfig();
        }

        private void LoadConfig()
        {
            if (File.Exists(configPath))
            {
                var lines = File.ReadAllLines(configPath);
                if (lines.Length > 0) txtSrc.Text = lines[0];
                if (lines.Length > 1) txtDst.Text = lines[1];
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveConfig();
        }

        private void SaveConfig()
        {
            File.WriteAllLines(configPath, new[] { txtSrc.Text, txtDst.Text });
        }
        private void btnSelectSrc_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "원본 이미지 폴더 선택";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtSrc.Text = dlg.SelectedPath;
                }
            }
        }

        private void btnSelectDst_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "저장할 폴더 선택";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtDst.Text = dlg.SelectedPath;
                }
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            string srcDir = txtSrc.Text;
            string dstDir = txtDst.Text;

            if (string.IsNullOrWhiteSpace(srcDir) || string.IsNullOrWhiteSpace(dstDir))
            {
                MessageBox.Show("원본 폴더와 저장 폴더를 선택하세요.");
                return;
            }

            lblStatus.Text = "상태: 처리중...";
            var imgExts = new[] { ".jpg", ".jpeg", ".png", ".bmp", ".tif", ".tiff" };
            var files = Directory.GetFiles(srcDir)
                        .Where(f => imgExts.Contains(Path.GetExtension(f).ToLower()))
                        .ToList();
            int count = 0;

            string format = cmbFormat.SelectedItem?.ToString() ?? "BMP";
            string ext = ".bmp";
            var param = new System.Collections.Generic.List<int>();

            switch (format)
            {
                case "JPG":
                    ext = ".jpg";
                    param.Add((int)OpenCvSharp.ImwriteFlags.JpegQuality);
                    param.Add(95); // JPEG 품질(0~100), 필요시 조정
                    break;
                case "PNG":
                    ext = ".png";
                    param.Add((int)OpenCvSharp.ImwriteFlags.PngCompression);
                    param.Add(3); // PNG 압축(0~9), 필요시 조정
                    break;
                case "BMP":
                default:
                    ext = ".bmp";
                    break;
            }

            foreach (var file in files)
            {
                using (var mat = Cv2.ImRead(file))
                {
                    if (mat.Empty())
                        continue;

                    var even = new Mat(mat.Rows / 2 + mat.Rows % 2, mat.Cols, mat.Type()); // 0,2,4...
                    var odd = new Mat(mat.Rows / 2, mat.Cols, mat.Type()); // 1,3,5...

                    int evenIdx = 0, oddIdx = 0;
                    for (int r = 0; r < mat.Rows; r++)
                    {
                        if (r % 2 == 0)
                            mat.Row(r).CopyTo(even.Row(evenIdx++));
                        else
                            mat.Row(r).CopyTo(odd.Row(oddIdx++));
                    }

                    var fileName = Path.GetFileNameWithoutExtension(file);
                    //var ext = ".bmp";// Path.GetExtension(file);

                    // 파일명 길이 제한 (예: 100자, 필요시 조정)
                    //if (fileName.Length >= 255)
                    //{
                    //    fileName = fileName.Substring(0, fileName.Length - 3);
                    //}

                    int centerX = mat.Width / 2;

                    // 첫 번째 줄(Y=0)
                    var colorTop = mat.At<Vec3b>(0, centerX);
                    int brightnessTop = (colorTop.Item0 + colorTop.Item1 + colorTop.Item2) / 3;
                    var colorBottom = mat.At<Vec3b>(1, centerX);
                    int brightnessBottom = (colorBottom.Item0 + colorBottom.Item1 + colorBottom.Item2) / 3;

                    if (brightnessTop > brightnessBottom)
                    {
                        if (chkSaveEven.Checked)
                            Cv2.ImWrite(Path.Combine(dstDir, $"{fileName}_BR{ext}"), even, param.ToArray()); // 짝수행
                        if (chkSaveOdd.Checked)
                            Cv2.ImWrite(Path.Combine(dstDir, $"{fileName}_DK{ext}"), odd, param.ToArray());  // 홀수행
                    }
                    else
                    {
                        if (chkSaveOdd.Checked)
                            Cv2.ImWrite(Path.Combine(dstDir, $"{fileName}_DK{ext}"), even, param.ToArray()); // 짝수행
                        
                        if (chkSaveEven.Checked)
                             Cv2.ImWrite(Path.Combine(dstDir, $"{fileName}_BR{ext}"), odd, param.ToArray());  // 홀수행
                    }


                    mat.Dispose();
                    even.Dispose();
                    odd.Dispose();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    count++;
                }
            }
            lblStatus.Text = $"상태: 완료! {count}개 파일 처리";
            MessageBox.Show("처리 완료!");
        }
    }
}
