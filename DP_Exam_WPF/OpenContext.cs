using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DP_Exam_WPF
{
    public class OpenContext
    {
        private TabControl _tabControl;

        public OpenContext(TabControl tabControl)
        {
            _tabControl = tabControl;
        }

        public void Open()
        {
            //канвас текущей вкладки
            Canvas canvas = (Canvas)_tabControl.SelectedContent;

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = "*.txt";
            dlg.Filter = "All files (*.*)|*.*|TXT|*.txt|RTF|*.rtf";
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                //берем расширение файла
                string ext = filename.Split(".".ToCharArray())[1];

                //выбираем стратегии
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(filename, UriKind.Relative));
                canvas.Background = brush;
            }
        }
    }
}
