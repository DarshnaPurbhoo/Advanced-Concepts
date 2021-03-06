using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Diagnostics;
using System.Collections;
using CsvHelper;

namespace WindowsFormsApp_Practises
{

    public abstract class LogBase
    {
        public abstract void Log(string Message);
    }

    public class Logger : LogBase
    {
        private string CurrentDirectory { get; set; }
        private string FileName { get; set; }
        private string FilePath { get; set; }

        public Logger()
        {
            this.CurrentDirectory = Directory.GetCurrentDirectory();
            this.FileName = "Log.txt";
            this.FilePath = this.CurrentDirectory + "/" + this.FileName;
        }

        public override void Log(string Message)
        {
            using (System.IO.StreamWriter w = System.IO.File.AppendText("C:\\Users\\Anita Purbhoo\\Documents\\Children\\Darshna\\Darshna Purbhoo"))
            {

                w.Write("\r\nLog Entry: ");
                w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                w.WriteLine("  :{0}", Message);
                w.WriteLine("---------------------------------------------------------------------------");

            }
        }
    }

    public partial class Form1 : Form
    {
        public class Input
        {
            public String Firstname { get; set; }
            public String Surname { get; set; }
            public double Algebra { get; set; }
            public double Calculus { get; set; }
            public double Programming { get; set; }
            public double Databases { get; set; }
        }

        public class Output
        {
            public String Firstname { get; set; }
            public String Surname { get; set; }
            public double Average { get; set; }
            public String Grade { get; set; }
        }        

        public static double CalculateAverage(double Algebra, double Calculus, double Programming, double Databases)
        {
            double Average = (Algebra + Calculus + Programming + Databases) / 4;
            return Average;
        }

        public Form1()
        {
            InitializeComponent();

            var logger = new Logger();

            List<Input> records;
            using (var reader = new StreamReader(@"C:\Users\Anita Purbhoo\Downloads"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                records = csv.GetRecords<Input>().ToList();
            }

            DataTable table = new DataTable();
            table.Columns.Add("Firstname", typeof(String));
            table.Columns.Add("Surname", typeof(String));
            table.Columns.Add("Average", typeof(Double));
            table.Columns.Add("Grade", typeof(String));

            foreach(var item in records)
            {
                double average = (item.Algebra + item.Calculus + item.Programming + item.Databases) / 4;
                //MessageBox.Show(average.ToString());

                switch (average)
                {
                    case var expression when (average >= 80):
                        table.Rows.Add(item.Firstname, item.Surname, Math.Round(average, 1), "A");
                        break;

                    case var expression when (average >= 70 && average <= 79.9):
                        table.Rows.Add(item.Firstname, item.Surname, Math.Round(average, 1), "B");
                        break;

                    case var expression when (average >= 60 && average <= 69.9):
                        table.Rows.Add(item.Firstname, item.Surname, Math.Round(average, 1), "C");
                        break;

                    case var expression when (average >= 50 && average <= 59.9):
                        table.Rows.Add(item.Firstname, item.Surname, Math.Round(average, 1), "D");
                        break;

                    case var expression when (average >= 40 && average <= 49.9):
                        table.Rows.Add(item.Firstname, item.Surname, Math.Round(average, 1), "E");
                        break;

                    case var expression when (average >= 0 && average <= 39.9):
                        table.Rows.Add(item.Firstname, item.Surname, Math.Round(average, 1), "F");
                        break;

                }
            }

            dataGridView1.DataSource = table;

            StringBuilder sb = new StringBuilder();

            string[] columnNames = table.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName).
                                              ToArray();
            sb.AppendLine(string.Join(",", columnNames));

            foreach (DataRow row in table.Rows)
            {
                string[] fields = row.ItemArray.Select(field => field.ToString()).
                                                ToArray();
                sb.AppendLine(string.Join(",", fields));
            }

            logger.Log(sb.ToString());

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable table = new DataTable();
                StringBuilder sb = new StringBuilder();

                string[] columnNames = table.Columns.Cast<DataColumn>().
                                                  Select(column => column.ColumnName).
                                                  ToArray();
                sb.AppendLine(string.Join(",", columnNames));

                foreach (DataRow row in table.Rows)
                {
                    string[] fields = row.ItemArray.Select(field => field.ToString()).
                                                    ToArray();
                    sb.AppendLine(string.Join(",", fields));
                }

                File.WriteAllText("Output.csv", sb.ToString());
                MessageBox.Show("File Successfully saved");
            }
            catch
            {
                MessageBox.Show("Unable to Save file");
            }
        }
    }
}

