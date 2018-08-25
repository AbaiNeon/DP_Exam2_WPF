using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

namespace DP_Exam_WPF
{
    public partial class MainWindow : Window
    {
        public int tabsCount = 0;
        Point startPosition;
        Point endPosition;
        
        public List<RichTextBox> richTextList = new List<RichTextBox>();

        public MainWindow()
        {
            InitializeComponent();
        }

        //Events-----------------------------------------------------------------------------
        private void CanvasMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                startPosition = e.GetPosition(this);
            }
        }

        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            endPosition = e.GetPosition(this);

            Line line = new Line();
            line.Stroke = SystemColors.WindowFrameBrush;
            line.X1 = startPosition.X-140;
            line.Y1 = startPosition.Y-50;
            line.X2 = endPosition.X-140;
            line.Y2 = endPosition.Y-50;

            Canvas c = (Canvas)products.SelectedContent;
            c.Children.Add(line);
        }
        //Events

        //Новая вкладка-----------------------------------------------------------------------------
        private void NewTabItem_Click(object sender, RoutedEventArgs e)
        {
            Canvas canvas = new Canvas();
            canvas.Height = 300;
            canvas.Width = 500;
            canvas.Background = new SolidColorBrush(Colors.AliceBlue);

            // добавление вкладки
            products.Items.Add(new TabItem
            {
                Header = new TextBlock { Text = "New tab" }, // установка заголовка вкладки
                Content = canvas,

            });

            canvas.MouseDown += new MouseButtonEventHandler(CanvasMouseDown);
            canvas.MouseLeftButtonUp += new MouseButtonEventHandler(Canvas_MouseLeftButtonUp);
        }

        //Открыть изображение-----------------------------------------------------------------------------
        private void OpenItem_Click(object sender, RoutedEventArgs e)
        {
            //открыть png, jpg
            OpenContext openContext = new OpenContext(products);
            openContext.Open();

        }

        //Сохранить в файл-----------------------------------------------------------------------------
        private void SaveItem_Click(object sender, RoutedEventArgs e)
        {
            //сохраняет изображение немного криво конечно
            SaveContext saveContext = new SaveContext(products);
            saveContext.Save();

        }

        //закрыть вкладку-----------------------------------------------------------------------------
        private void CloseItem_Click(object sender, RoutedEventArgs e)
        {
            products.Items.RemoveAt(products.SelectedIndex);
        }
    }
}
