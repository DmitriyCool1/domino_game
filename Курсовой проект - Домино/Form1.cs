using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Timers;

namespace Курсовой_проект___Домино
{
    public partial class Form1 : Form
    {
        public static Game game; 
        public Form1()
        {
            InitializeComponent();

        }
        
                    
        private void button1_Click(object sender, EventArgs e)
        {
            
            button1.Text = "БАЗАР (" + game.Bazar() + ")"; //Взятие домино с базара
          
            //System.Media.SoundPlayer a = new System.Media.SoundPlayer("C:\\Users\\валентина\\Desktop\\Курсовой проект - Домино\\Курсовой проект - Домино\\Sound\\Базар-Sound.wav");
            //a.Play();
       }

        private void button2_Click(object sender, EventArgs e)
        {
            game.CompMove(); //Пропускаем ход
            game.SortCompK();
            //System.Media.SoundPlayer a = new System.Media.SoundPlayer("C:\\Users\\валентина\\Desktop\\Курсовой проект - Домино\\Курсовой проект - Домино\\Sound\\Пропустить ход Sound.wav");
            //a.Play();
                       
        }
       
         
        private void Form1_Shown_1(object sender, EventArgs e)
        {
            
                    
            game = new Game(); 
        }

        private void новаяИграToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Visible = false;
            DialogResult result;
            result = MessageBox.Show("Посмотреть результаты прошлых игр ?  ", "Результаты игр", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {

                string fileName = String.Empty;
                               OpenFileDialog OF = new OpenFileDialog();
                OF.InitialDirectory = "c:\\";
                OF.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
                OF.FilterIndex = 2;
                OF.RestoreDirectory = true;
            
                if (OF.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                
                    label3.Visible = true;
                    richTextBox1.Visible = true;
                    richTextBox1.Text = File.ReadAllText(OF.FileName, Encoding.UTF8);
                  richTextBox1.Show();
                   
                }
            }
            else {
                if (result == System.Windows.Forms.DialogResult.No) {
                    label3.Visible = false;
                    richTextBox1.Visible = false;
                    game = new Game(); 
                    

                }


            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.ShowDialog();
        }
       // int time = 60;
       // private void timer1_Tick(object sender, EventArgs e)
        //{
        //    time--; if (time == 0) Application.ExitThread();
            //label1.Text = " Время " + time  + "сек";
       // }

      
       
        

        
        
    }
}
