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

namespace CallCenter.Pages
{
    /// <summary>
    /// Логика взаимодействия для SOSW.xaml
    /// </summary>
    public partial class SOSW : Page
    {/// <summary>
    /// переменая для определения пути к файлу
    /// </summary>
        public string File_Name = "";
        public SOSW()
        {
            InitializeComponent();
        }

        private void btnLike_Click(object sender, RoutedEventArgs e)
        {   //определение пути к файлу
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.InitialDirectory = @"\Resourses\Шаблон.txt";
            openFile.DefaultExt = ".txt";
            openFile.Filter = "Текст документа (*.TXT)|*.txt";
            if (openFile.ShowDialog() == true)
            {
                try
                {//запись шаблона в файл
                    StreamReader sr = new StreamReader(openFile.FileName);
                    richBox.Text = sr.ReadToEnd();
                    sr.Close();
                    this.File_Name = openFile.FileName;
                }
                catch//обработка ошибок
                {
                    MessageBox.Show("Невозможно открыть файл!");
                }
            }
            else
            {
                MessageBox.Show("Операция отменена!");
            }
        }
        //кнопка сохранить
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            if (File_Name == "")
            {
                save.FileName = "Безымянный";
            }
            else//сохранение файла по указанному пути
                save.FileName = File_Name;

            save.FileName = @"\DBDocument";
            save.DefaultExt = ".txt";
            save.Filter = "Текстовый документ (.txt) |*.txt";

            if (save.ShowDialog() == true)
            {
                // сохранение имени файла в переменной
                this.File_Name = save.FileName;
                StreamWriter writing = new StreamWriter(save.FileName);
                writing.Write(richBox.Text);
                writing.Close();
            }
        }
    }
}
