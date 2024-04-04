using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Курсовой_проект___Домино
{
    public class Kosti
    {
        public PictureBox pict = new PictureBox();
        public string Zn;
        public int Povorot = 0;

        public Kosti(string p1, int p2, int p3, int p4, bool p5, bool black)
        {
            Zn = p1;


            Bitmap bit = (black == true) ? (global::Курсовой_проект___Домино.Properties.Resources.Кости) : (global::Курсовой_проект___Домино.Properties.Resources.Кости);

            Rectangle rect = new Rectangle((Convert.ToInt32(Zn[2].ToString()) - Convert.ToInt32(Zn[0].ToString())) * 48,
                                            Convert.ToInt32(Zn[0].ToString()) * 25, 48, 25);//вырезаем изображение по 4 точкам (конвертируем в стрирнг . т.к значения костей записаны в стринге)

            bit = bit.Clone(rect, bit.PixelFormat);//создаёем копии вырезаемой картинки
            pict.Location = new System.Drawing.Point(p2 - 24, p3 - 13); //Задание позиции для расположения на форме

            switch (Povorot = p4)//переменная p4 jотвечает за поворот картинки на форме
            {
                case 90:
                    bit.RotateFlip(RotateFlipType.Rotate90FlipNone);//поворот на 90 градусов 
                    pict.Location = new System.Drawing.Point(p2 - 13, p3 - 24);//расположение пикчбокса на форме
                    break;

                case 270:
                    bit.RotateFlip(RotateFlipType.Rotate270FlipNone); //поворот на 270 градусов
                    pict.Location = new System.Drawing.Point(p2 - 13, p3 - 24);//расположение пикчбокса на форме
                    break;
            }

            pict.Image = bit;//изображение пикчбокса записываем в bit
            pict.Visible = p5;
            pict.SizeMode = PictureBoxSizeMode.AutoSize;//автоподстройка пикчбокса под содержимое
            pict.Click += new EventHandler(pict_Click);  //Задание действия при одиночном нажатии на домино/
                                                         //Основное назначение метода — инициировать процесс перерисовки изображения на форме, для чего из него вызывается метод формы, выполняющий рисование. 

            pict.BringToFront();
            pict.BackColor = System.Drawing.Color.Green; //Выбор фонового цвета

            Form1.ActiveForm.Controls.Add(pict); //Добавление домино на форму
        }


        public void pict_Click(object sender, EventArgs e)
        {
            Form1.game.PlayerMove(this); //Перемещение домино при нажатии на нее
            this.pict.BringToFront(); //
        }

    }
}


    
