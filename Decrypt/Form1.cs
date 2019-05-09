using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Numerics;

namespace Decrypt
{
    public partial class Form1 : Form
    {
        public string fileName;
        public string saveName;
        public Form1()
        {
            InitializeComponent();
        }

        private void OpenFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "(*.txt) | *.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;
            }
        }

        private void Decrypt_Click(object sender, EventArgs e)
        {
            if ((textBox_d.Text.Length > 0) && (textBox_n.Text.Length > 0))
            {
                long d;
                long n;
                try
                {
                    d = Convert.ToInt64(textBox_d.Text);
                    n = Convert.ToInt64(textBox_n.Text);
                   
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid input format ");
                    return;
                }
                List<string> input = new List<string>();

                StreamReader reader = new StreamReader(fileName, Encoding.Default);
                while (!reader.EndOfStream)
                {
                    input.Add(reader.ReadLine());
                }
                reader.Close();

                string result = RSA_Dedoce(input, d, n);


                saveFileDialog1.Filter = "(*.txt) | *.txt";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    // получаем выбранный файл
                    saveName = saveFileDialog1.FileName;

                }


                StreamWriter writer = new StreamWriter(saveName);
                writer.WriteLine(result);
                writer.Close();
                MessageBox.Show("Your data was decrypted");
            }
            else
                MessageBox.Show("Enter d or n!");
        }
        private string RSA_Dedoce(List<string> input, long d, long n)
        {
            string result = "";

            BigInteger bi= new BigInteger();

            foreach (string item in input)
            {
                bi = BigInteger.Pow(Convert.ToInt32(item), (int)d);
                BigInteger degree = new BigInteger((int)n);
                bi = bi % degree;
                result += (char)Convert.ToInt32(bi.ToString());
            }

            return result;
        }
        private void OpenDecrypted_Click(object sender, EventArgs e)
        {
            Process.Start(saveName);
        }

        private void Instruction_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Аби розшифрувати ваші дані необіхдно :" +
                "\n 1. Оберіть шлях до файлу(у форматі txt) з зашифрованими даними" +
                "\n 2. Введіть відкритий ключ(d і n відповідно) " +
                "\n 3. Натисність кнопку Decrypt" +
                "\n 4. Аби побачити отримані дані натисніть кнопку Open decrypted file або зробіть це вручну ");
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
