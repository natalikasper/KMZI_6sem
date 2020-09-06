using Lab4.Cezar;
using System;
using System.Windows;
using Syncfusion.XlsIO;
using System.Collections.Generic;
using System.Linq;

namespace Lab4
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        Caesar caesar = new Caesar();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            long sec = DateTime.Now.Ticks;
            textBox2.Text = caesar.CaesarChipher(textBox1.Text, Convert.ToInt32(numericUpDown1.Text));
            long sec2 = (DateTime.Now.Ticks - sec) / 1000 + 10;
            zash.Text = (sec2.ToString() + "мс");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            long sec = DateTime.Now.Ticks;
            textBox3.Text = caesar.CaesarChipher(textBox2.Text, -Convert.ToInt32(numericUpDown1.Text));
            long sec2 = (DateTime.Now.Ticks - sec) / 1000;
            rash.Text = (sec2.ToString() + "мс");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var str1 = textBox3.Text;
            Dictionary<char, double> counts = str1.GroupBy(ch => ch).ToDictionary(g => g.Key, g => (g.Count()) / (double)str1.Length);
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                excelEngine.Excel.DefaultVersion = ExcelVersion.Excel2013;
                IWorkbook workbook = excelEngine.Excel.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];
                int i = 1;
                foreach (char c in counts.Keys)
                {
                    double value = 0;
                    counts.TryGetValue(c, out value);
                    worksheet.Range["A" + i].Text = c.ToString();
                    worksheet.Range["B" + i].Number = value;
                    i++;
                }
                IChartShape chart = worksheet.Charts.Add();
                chart.ChartType = ExcelChartType.Column_Clustered;
                chart.DataRange = worksheet.Range["A1:B" + counts.Count];
                workbook.SaveAs("F:/XlsIO_Output2.xlsx");
                workbook.Close();
                excelEngine.Dispose();
            }
        }
    }
}
