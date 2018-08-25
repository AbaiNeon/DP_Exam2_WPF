using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DP_Exam_WPF
{
    public class SaveContext
    {
        private TabControl _tabControl;

        public SaveContext(TabControl tabControl)
        {
            _tabControl = tabControl;
        }

        public void Save()
        {
            Canvas canvas = (Canvas)_tabControl.SelectedContent;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.FileName = "myImage";
            //saveFileDialog.DefaultExt = "png";
            saveFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            Nullable<bool> result = saveFileDialog.ShowDialog();

            if (result == true)
            {
                string name = saveFileDialog.SafeFileName;

                //расширение
                string ext = name.Split(".".ToCharArray())[1];

                RenderTargetBitmap rtb = new RenderTargetBitmap((int)canvas.RenderSize.Width,
                    (int)canvas.RenderSize.Height, 96d, 96d, System.Windows.Media.PixelFormats.Default);
                rtb.Render(canvas);

                //var crop = new CroppedBitmap(rtb, new Int32Rect(50, 50, 400,200));

                if (ext == "png")
                {
                    BitmapEncoder pngEncoder = new PngBitmapEncoder();
                    pngEncoder.Frames.Add(BitmapFrame.Create(rtb));

                    using (var fs = System.IO.File.OpenWrite(name)) // "name" + ".png"
                    {
                        pngEncoder.Save(fs);
                    }
                }
                else if (ext == "jpg")
                {
                    BitmapEncoder jpgEncoder = new JpegBitmapEncoder();
                    jpgEncoder.Frames.Add(BitmapFrame.Create(rtb));

                    using (var fs = System.IO.File.OpenWrite(name)) // "name" + ".jpg"
                    {
                        jpgEncoder.Save(fs);
                    }
                }
                else
                {
                    //другой формат
                }

            }
        }
    }
}
