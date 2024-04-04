 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
namespace Курсовой_проект___Домино
{
    public class Game
    {
        static string[] AllK = { "0|0","0|1","0|2","0|3","0|4","0|5","0|6","1|1","1|2","1|3","1|4","1|5","1|6","2|2", 
                                 "2|3","2|4","2|5","2|6","3|3","3|4","3|5","3|6","4|4","4|5","4|6","5|5","5|6","6|6"}; //значения на костях домино

        List<string> K = new List<string>(AllK); //список костпей на базаре(все кости)
        List<Kosti> PlayerK = new List<Kosti>(); // список костей  игрока
        List<Kosti> CompK = new List<Kosti>();  //список костей компьютера
        List<Kosti> TableK = new List<Kosti>(); //список домино на игровом столе
        Random rnd = new Random();  //рандомное заполнение


        public Game()
        { 
            for (int i = 0; i < 7; i++)//просматриваем в цикле только 7 элементов, которые будут добавлять игрок и компьютер в свои "колоды"
            {
                int bit = rnd.Next(0, K.Count);//определяем bit - объект игры(выбирается случайно из диапазона)
                int p2 = 150 + i*30;// координаты и интервал между костями игрока
                int p3 = Form.ActiveForm.Height - 66;
                int p4 = 90;

                Kosti p = new Kosti(K.ElementAt(bit), p2, p3, p4, true,true);//метод ElementAt - возвращаем элемент с базара
                PlayerK.Add(p);


                K.RemoveAt(bit);// удаления взятого элемента из базара 

              bit = rnd.Next(0, K.Count);// bit - объект игры(выбирается случайно из диапазона)

                int c2 = 150 + i * 30;//координаты и интервал между костями компьютера
                int c3 = Form.ActiveForm.Height - 130;//делаем форму активной чтобы кости появились + координаты
               int c4 = 90;//угол поворота

               Kosti c = new Kosti(K.ElementAt(bit), c2, c3, c4, true, false);//1-видимость домино 2- видимость значений
                CompK.Add(c);

                K.RemoveAt(bit); // удаляем с базара после добавления 
            }
        } 


        public int Bazar()
        { //взятия домино с базара

            int bazar = K.Count;// 
            if (bazar == 0)
            {// если нет домино на базаре
                if (TableK.Count == 0)//если на игровом столе нет домино
                {
                    MessageBox.Show("В базаре больше нет костяшек!");//текстовое сообщение
                    return 0; }


                for (int i = 0; i < PlayerK.Count; i++)
                {// в цикле просматриваем элементы игрока
                  

                    if (i == PlayerK.Count - 1)//Если у игрока нет подходящих домино
                    {
                        MessageBox.Show(Kol());//показываем функцию с результатами
                       
                        return 0;
                    }
                }
                return 0;
            }
            int bit = rnd.Next(0, bazar);// определяем объект домино bit
            if (PlayerK.  Count == 0)//если у игрока нет  костей домино
            {
                int p2 = 150;//отступ
                int p3 = Form.ActiveForm.Height - 66;//форма активна  + высота  элемента
                int p4 = 90;//угол расположения

                Kosti m = new Kosti(K.ElementAt(bit), p2, p3, p4, true,true);
                PlayerK.Add(m);

            }
            else//если у игрока есть кости домино
            {

                int p2 = PlayerK.Last().pict.Location.X + 43;//добавляем а конец списка по координатами оси х
                int p3 = Form.ActiveForm.Height - 66;
                int p4 = 90;
                Kosti m = new Kosti(K.ElementAt(bit), p2, p3, p4, true, true);
                PlayerK.Add(m);//игрок добавляет кости
                                

            }

            K.RemoveAt(bit);//удаляем  кости с базара после взятия на руки
            return bazar - 1;
        }

        public string Kol()// cчитаем сумму на домино 
        {
            int Playerkol = 0;
            int Compkol = 0;

            DialogResult result;
            {// диалог
                result = DialogResult.No;

                SaveFileDialog SF = new SaveFileDialog();// запрос для сохранения файла
                StreamWriter sw = new StreamWriter(Application.StartupPath + "/result.txt");

                for (int i = 0; i <= PlayerK.Count - 1; i++)
                { 

                    Playerkol += Convert.ToInt32(PlayerK.ElementAt(i).Zn[0].ToString()) +
                                 Convert.ToInt32(PlayerK.ElementAt(i).Zn[2].ToString());
                }// конвертируем чтобы значения можно было суммировать для результатов

                for (int i = 0; i <= CompK.Count - 1; i++)//просматриваем костяшки компьютера  и также конвертируем для результата
                {
                    Compkol += Convert.ToInt32(CompK.ElementAt(i).Zn[0].ToString()) +
                               Convert.ToInt32(CompK.ElementAt(i).Zn[2].ToString());
                }


                if (Playerkol > Compkol)//если у игрока больше сумма очков на домино чем у компа
                {
                    result = MessageBox.Show("Ваши очки: " + Playerkol + ", очки компьютера: " + Compkol + "\n\r Вы проиграли! Cохранить результаты игры?", "Результаты и их сохранение", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    sw.WriteLine(result);//вывод результата игры
                }
                if (Playerkol < Compkol)//если сумма очков у компа больше чем у игрока
                {
                    result = MessageBox.Show("Ваши очки:" + Playerkol + ",очки компьютера:" + Compkol + "\n\r Вы выиграли! Cохранить результаты игры?", "Предложение продолжить", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    sw.WriteLine(result);//вывод результата игры

                }
                if (Playerkol == Compkol)
                {
                    result = MessageBox.Show("Ваши очки: " + Playerkol + ", очки компьютера: " + Compkol + "\n\r Ничья!Cохранить результаты игры?", "Предложение продолжить", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    sw.WriteLine(result);//вывод результата игры


                }
                if (result == System.Windows.Forms.DialogResult.Yes)//сохранение файла с результатом игры
                {
                    SF.FileName = "result.txt";
                    SF.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
                    SF.DefaultExt = "*.txt";
                    if (SF.ShowDialog() == DialogResult.OK)
                    {
                        sw.WriteLine("Ваши очки: " + Playerkol + ", очки компьютера: " + Compkol + "\n\r");
                        sw.Close();
                        result = MessageBox.Show("Файл Успешно Сохранён!!! Продолжить игру ?", "Предложение продолжить", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == System.Windows.Forms.DialogResult.Yes) { Application.Restart(); }
                        if (result == System.Windows.Forms.DialogResult.No) { Application.Exit(); }
                    }
                    else
                    {
                        if (SF.ShowDialog() != DialogResult.OK)
                        {
                            result = MessageBox.Show("Файл Не Сохранён!!! Продолжить игру ?", "Предложение продолжить", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (result == System.Windows.Forms.DialogResult.Yes) { Application.Restart(); }
                            if (result == System.Windows.Forms.DialogResult.No) { Application.Exit(); }

                        }
                    }
                }
                else
                {


                    if (result == System.Windows.Forms.DialogResult.No)
                    {

                        result = MessageBox.Show("Файл Не Сохранён!!! Продолжить игру ?", "Предложение продолжить", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == System.Windows.Forms.DialogResult.Yes) { Application.Restart(); }
                        if (result == System.Windows.Forms.DialogResult.No) { Application.Exit(); }
                    }
                }


                sw.Close();

                return " ";
            }
        }

        public void CompMove()
        { // Ход компьютера

            Kosti p1;
            int bit = rnd.Next(0, K.Count);

            if (K.Count == 0)
            {
                if (CompK.Count == 0)
                {//Если на базаре пусто - игрок выиграл
                    MessageBox.Show(Kol());
                    return;
                }
            }

            if (CompK.Count == 0)
            {// Если у компа нечем  ходить

                CompK.Add(new Kosti(K.ElementAt(bit), 150, Form.ActiveForm.Height - 130, 90, true, true));
                K.RemoveAt(bit);
                Form.ActiveForm.Controls["button1"].Text = "БАЗАР (" + K.Count + ")";
            } //кликаем базар 
            for (int i = 0; i < CompK.Count; i++)
            { //Перебор доминошек компа, выбор нужной и перемещение на стол
                p1 = CompK.ElementAt(i);
                if (TableK.Count == 0)
                {//если на столе ничего

                    Movingmoment(p1);
                    p1.pict.BringToFront();
                    TableK.Add(CompK.ElementAt(i));
                    CompK.RemoveAt(i);
                    return;
                }

                if (TableK.First().Zn[0] == p1.Zn[0] ||
                    TableK.First().Zn[0] == p1.Zn[2] ||
                    TableK.Last().Zn[2] == p1.Zn[0] ||
                    TableK.Last().Zn[2] == p1.Zn[2])
                {
                    CompK.ElementAt(i).pict.Visible = true;
                    Movingmoment(p1);
                    p1.pict.BringToFront();
                    if (TableK.First().Zn[0] == p1.Zn[2])
                    {
                        TableK.Insert(0, CompK.ElementAt(i));
                    }
                    else { TableK.Add(CompK.ElementAt(i)); }
                    CompK.RemoveAt(i);
                    return;
                }

                int bazar = K.Count;
                bit = rnd.Next(0, K.Count);
                if (i == CompK.Count - 1)
                {//Если у компьютера нет подходящих домино
                    if (bazar == 0)
                    { //Если на базаре пусто - игрок выиграл
                        MessageBox.Show(Kol());
                        return;
                    }
                    CompK.Add(new Kosti(K.ElementAt(bit), CompK.Last().pict.Location.X + 43, Form.ActiveForm.Height - 130, 90, true, false));// компьютер добавляет кости в конец списка
                    K.RemoveAt(bit);
                    Form.ActiveForm.Controls["button1"].Text = "БАЗАР (" + K.Count + ")";
                }
            }
        }
        public void PlayerMove(Kosti p1)//ход игрока
        {
            int bit = PlayerK.IndexOf(p1); 
            if (bit == -1)
                return;
            if (TableK.Count != 0 &&
                TableK.First().Zn[0] != p1.Zn[0] &&
                TableK.First().Zn[0] != p1.Zn[2] &&
                TableK.Last().Zn[2] != p1.Zn[0] &&
                TableK.Last().Zn[2] != p1.Zn[2])
            { //Если кость нельзя положить на стол - выход
                return;
            }

            Movingmoment(p1); //Переместить кость

            if (TableK.Count == 0)
            {
                TableK.Add(PlayerK.ElementAt(bit));
            } //Добавление первой кости на стол
            else
            {
                if (TableK.First().Zn[0] == p1.Zn[2])
                {
                    TableK.Insert(0, PlayerK.ElementAt(bit));
                } //Добавление домино в начало списка костей на столе
                else
                {
                    TableK.Add(PlayerK.ElementAt(bit));
                }
            } //Добавление костей в конец списка костей на столе
            PlayerK.RemoveAt(bit); //Удаление костей от игрока
            SortPlayerK(); //Сортировка костей игрока
            CompMove(); //Ход компьютера
            SortCompK();
        } //Сортировка костей компьютера


        


        public void SortCompK()
        { //Сортируем домино компа
            for (int i = 0; i < CompK.Count; i++)
            {
                int x = 150 + i * 30 - 13;
                int y = Form.ActiveForm.Height-130-24;
                CompK.ElementAt(i).pict.Location = new System.Drawing.Point(x, y);   
            }
        }

        public void SortPlayerK()
        { //Сортируем своё домино
            for (int i = 0; i < PlayerK.Count; i++)
            {
                int x = 150 + i * 30 - 13;
                int y = Form.ActiveForm.Height - 66 - 24;
                PlayerK.ElementAt(i).pict.Location = new System.Drawing.Point(x, y);
            }
        }



        public void Movingmoment(Kosti p1)  //Перемещение домино (эта также функция выполняет попиксельный подбор положений костей домино на игровом столе, выполяет стыковку костей домино)
        {
            if (p1.Zn[0] != p1.Zn[2])
            {
                p1.pict.Image.RotateFlip(System.Drawing.RotateFlipType.Rotate270FlipNone);
                p1.pict.Refresh();//перерисовка
            }
            if (TableK.Count == 0)
            {//если на столе ничего
                
                p1.pict.Location = new System.Drawing.Point(Form.ActiveForm.Width/2 - p1.pict.Width/2,Form.ActiveForm.Height/ 8-p1.pict.Height / 2);
            }// задаём расположение первой домино на столе
            else
            {
                int a = p1.pict.Image.Width,//установка ширины картинки (кости)
                    b = p1.pict.Image.Height,//установка высоты картинки(кости)

                XF = TableK.First().pict.Location.X,
                YF = TableK.First().pict.Location.Y,
                AF = TableK.First().pict.Image.Width,
                BF = TableK.First().pict.Image.Height,

                XL = TableK.Last().pict.Location.X,
                YL = TableK.Last().pict.Location.Y,
                AL = TableK.Last().pict.Image.Width,
                BL = TableK.Last().pict.Image.Height;

                TableK.First().pict.Width = AF;// ширина первой домино на столе
                TableK.First().pict.Height = BF;//высота
                TableK.Last().pict.Width = AL;// последней
                TableK.Last().pict.Height = BL;//высота последней кости

                if (p1.Zn[0] == p1.Zn[2])
                {
                    if (TableK.First().Zn[0] == p1.Zn[0])//если значение домино на столе = значению выставляемого домино
                    {
                        if (XF < 100)
                        {
                            p1.pict.Image.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipNone);//поворачиваем на 90градусов
                            p1.pict.Refresh();//перерисовка
                            p1.pict.Location = new System.Drawing.Point(XF - a / 4, YF + BF);
                            return;
                        }
                        p1.pict.Location = new System.Drawing.Point(XF - a, YF - b / 4);
                    }
                    else
                    {
                        if (XL > 1000)
                        {
                            p1.pict.Image.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipNone);
                            p1.pict.Refresh();
                            p1.pict.Location = new System.Drawing.Point(XL - a / 4, YL + BL);
                            return;
                        }
                        p1.pict.Location = new System.Drawing.Point(XL + AL, YL - b / 4);
                    }
                }
                else




                {
                    if (TableK.First().Zn[0] == p1.Zn[2])
                    {
                        if (XF < 100)
                        {
                            p1.pict.Image.RotateFlip(System.Drawing.RotateFlipType.Rotate270FlipNone);//поворачиваем на 270 градусов
                            p1.pict.Refresh();
                            p1.pict.Location = new System.Drawing.Point((TableK.First().Zn[0] == TableK.First().Zn[2]) ? XF + AF / 4 : XF, YF + BF);
                            return;
                        }
                        p1.pict.Location = new System.Drawing.Point(XF - a, (TableK.First().Zn[0] == TableK.First().Zn[2]) ? YF + BF / 4 : YF);
                    }



                    else
                        if (TableK.First().Zn[0] == p1.Zn[0])
                        {
                            if (XF < 100)
                            {



                                p1.pict.Image.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipNone);
                                p1.pict.Refresh();

                                p1.Zn = p1.Zn[2] + "|" + p1.Zn[0];
                                p1.pict.Location = new System.Drawing.Point((TableK.First().Zn[0] == TableK.First().Zn[2]) ? XF + AF / 4 : XF, YF + BF);
                                return;


                            }
                            p1.pict.Image.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
                            p1.pict.Refresh();
                            p1.Zn = p1.Zn[2] + "|" + p1.Zn[0];
                            p1.pict.Location = new System.Drawing.Point(XF - a, (TableK.First().Zn[0] == TableK.First().Zn[2]) ? YF + BF / 4 : YF);
                        }
                        else
                            if (TableK.Last().Zn[2] == p1.Zn[0])
                            {
                                if (XL > 1000)
                                {
                                    p1.pict.Image.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipNone);
                                    p1.pict.Refresh();

                                    p1.pict.Location = new System.Drawing.Point((TableK.Last().Zn[0] == TableK.Last().Zn[2]) ? XL + AL / 4 : XL + ((AL > 30) ? AL / 2 : 0), YL + BL);
                                    return;
                                }



                                p1.pict.Location = new System.Drawing.Point(XL + AL, (TableK.Last().Zn[0] == TableK.Last().Zn[2]) ? YL + BL / 4 : YL);
                            }
                            else
                                if (TableK.Last().Zn[2] == p1.Zn[2])
                                {
                                    if (XL > 1000)
                                    {
                                        p1.pict.Image.RotateFlip(System.Drawing.RotateFlipType.Rotate270FlipNone);
                                        p1.pict.Refresh();
                                        p1.Zn = p1.Zn[2] + "|" + p1.Zn[0];
                                        p1.pict.Location = new System.Drawing.Point((TableK.Last().Zn[0] == TableK.Last().Zn[2]) ? XL + AL / 4 : XL + ((AL > 30) ? AL / 2 : 0), YL + BL);
                                        return;
                                    }
                                    p1.pict.Image.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
                                    p1.pict.Refresh();
                                    p1.Zn = p1.Zn[2] + "|" + p1.Zn[0];
                                    p1.pict.Location = new System.Drawing.Point(XL + AL, (TableK.Last().Zn[0] == TableK.Last().Zn[2]) ? YL + BL / 4 : YL);
                                }
                }
            }
        }






    };
}
