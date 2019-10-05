using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;


namespace WindowsFormsApp3
{
    public partial class Form2 : Form


    {
        String s;
        WordCard tmp;
        Label l1, l2;
        TextBox t1;
        Button bt1, bt2, bt3, bt4;
        RadioButton r1, r2, r3, r4;
        bool rb = true;
        static int right = 0, wrong = 0;

        void setInvisible(bool choice)
        {
            rb = choice;
            r1.Visible = rb;
            r2.Visible = rb;
            r3.Visible = rb;
            r4.Visible = rb;

        }

        public ArrayList words = new ArrayList();
        public Queue<String> words2 = new Queue<String>();

        public Form2()
        {

            Random r = new Random();

            this.Size = new Size(330, 330);
            this.Text = "Словарь";

            InitializeComponent();

            #region UI elements
            l1 = new Label()
            {
                Text = "Выберите режим теста: ",
                Location = new Point(10, 10),
                AutoSize = true,
                Size = new Size(150, 30)

            };
            Controls.Add(l1);


            r1 = new RadioButton()
            {
                Text = "Перевод с русского на английский",
                Left = l1.Left,
                Top = l1.Top + 25,
            };
            r1.AutoSize = true;
            r1.Checked = true;
            this.Controls.Add(r1);

            r2 = new RadioButton()
            {
                Text = "Перевод с английского на русский",
                Left = l1.Left,
                Top = r1.Top + 25,
            };

            r2.AutoSize = true;
            this.Controls.Add(r2);

            r3 = new RadioButton()
            {
                Text = "Тестовый: случайные слова",
                Left = l1.Left,
                Top = r2.Top + 25,
            };

            r3.AutoSize = true;
            this.Controls.Add(r3);

            r4 = new RadioButton()
            {
                Text = "Тестовый: слова, с наибольшим количеством ошибок",
                Left = l1.Left,
                Top = r3.Top + 25,
            };

            r4.AutoSize = true;
            this.Controls.Add(r4);

            t1 = new TextBox()
            {
                Left = l1.Left,
                Top = l1.Bottom + 60,
                Size = new Size(100, 60)
            };
            t1.Visible = false;
            Controls.Add(t1);


            bt1 = new Button()
            {
                Text = "Принять",
                Left = t1.Left + 120,
                Top = t1.Top,
                Size = new Size(60, 25)
            };

            bt1.Visible = false;
            this.Controls.Add(bt1);
            bt1.Click += ButtonClick1;

            setInvisible(true);



            bt2 = new Button()
            {
                Text = "Начать",
                Left = t1.Left + 100,
                Top = t1.Top + 70,
                Size = new Size(60, 25)
            };

            this.Controls.Add(bt2);
            bt2.Click += ButtonClick2;

            l2 = new Label()
            {
                Top = l1.Bottom + 100,
                Left = t1.Left,
                AutoSize = true,
                Size = new Size(150, 30)

            };
            Controls.Add(l2);

            bt3 = new Button()
            {
                Text = "Завершить",
                Left = 220,
                Top = 250,
                Size = new Size(80, 25)
            };

            bt3.Visible = false;
            this.Controls.Add(bt3);
            bt3.Click += ButtonClick3;

            bt4 = new Button()
            {
                Text = "Назад",
                Left = 220,
                Top = 250,
                Size = new Size(80, 25)
            };

            bt4.Visible = false;
            this.Controls.Add(bt4);
            bt4.Click += ButtonClick4;

#endregion

            void ButtonClick1(object sender, EventArgs e)
            {
                
                if (r1.Checked == true)
                {
                    l1.Text = @"


Введите руссое слово: ";
                    if (rusTranslation(t1.Text) != null)
                    {
                        l2.Text = "Английский перевод: " + rusTranslation(t1.Text) + " (" + langType(rusTranslation(t1.Text)) + ")";
                    }
                    else l2.Text = "Слово не найдено"; 
                }
                else if (r2.Checked == true)
                {
                    l1.Text = @"


Введите английское  слово: ";
                    if (engTranslation(t1.Text) != null)
                    {
                        l2.Text = "Русский перевод: " + engTranslation(t1.Text) + " (" + langType(t1.Text) + ")";
                    }
                    else l2.Text = "Слово не найдено";
                }
                else
                {
                    if (tmp.translationCheck(t1.Text)) { l2.Text = "Правильно!";
                        right++;
                    }
                    else { l2.Text = "Не правильно!";
                        wrong++;
                    }
                    if (r4.Checked == true)
                    {
                        if (words2.Count!= 0)
                        {
                            s = words2.Dequeue();
                        }
                         else
                    {
                      
                        t1.Visible = false;
                        bt1.Visible = false;
                         s = "";
                    }

                    }
                   
                    else {
                        randomList();
                        int i = r.Next(0, words.Count - 1);
                        s = (String)words[i];
                    }
               if( (this.Owner as Form1).dic.TryGetValue(s, out tmp))
                    l1.Text = @"Английское слово: " + tmp.EnglishWord + @" ("+langType(tmp.EnglishWord) +@")


Введите русский перевод: ";
                    else l1.Text = "Слова закончились";
                }
            }
            void ButtonClick2(object sender, EventArgs e)
            {
                wrong = 0;
                right = 0;
                l2.Text = "";
                if (r1.Checked == true) { l1.Text = @"


Введите руссое слово: ";
                    bt4.Visible = true;
                }
                else if (r2.Checked == true) { l1.Text = @"


Введите английское  слово: ";
                    bt4.Visible = true;
                }
                else
                {
                    if (r4.Checked == true)
                    {
                        mostErrorList();
                        s = words2.Dequeue();
                    }
                    else
                    {
                        randomList();
                        int i = r.Next(0, words.Count - 1);
                        s = (String)words[i];
                    }
                   (this.Owner as Form1).dic.TryGetValue(s, out tmp);
                    l1.Text = @"Английское слово: " + tmp.EnglishWord+ @" (" + langType(tmp.EnglishWord) + @")


Введите русский перевод: ";
                    bt3.Visible = true;
                }


                setInvisible(false);

                t1.Visible = true;
                bt1.Visible = true;
                bt2.Visible = false;
               

                t1.Visible = true;
            }
            void ButtonClick3(object sender, EventArgs e) {
                setInvisible(true);
                bt1.Visible = false;
                bt2.Visible = true;
                t1.Visible = false;
                bt3.Visible = false;
                l1.Text = "Выберите режим:";
                l2.Text = @"

       

              
СТАТИСТИКА

Число правильных ответов: "+right+@"

Число ошибок: "+wrong;


            }
            void ButtonClick4(object sender, EventArgs e)
            {
                setInvisible(true);
                bt1.Visible = false;
                t1.Visible = false;
                bt2.Visible = true;
                l1.Text = "Выберите режим:";
                l2.Text = "";
                bt4.Visible = false;
            }


                #region functions
            void mostErrorList()
            {
               double erAvg = (from n2 in (this.Owner as Form1).dic.Values select n2.Error).Average();
                foreach (WordCard s in (this.Owner as Form1).dic.Values)
                {
                    if (s.Error >= erAvg) words2.Enqueue(s.EnglishWord);
                }

            }
            void randomList()
            {
                foreach (string s in (this.Owner as Form1).dic.Keys)
                {
                    words.Add(s);
                }

            }

            String rusTranslation(String rus)
            {
                foreach (WordCard wc in (this.Owner as Form1).dic.Values)
                {
                    bool flag=false;
                    foreach (String s in wc.RussianWord)
                    {
                        if (s == rus) flag = true;
                    }
                    if (flag) return wc.EnglishWord;
                }
               
                return null;
            }
            String langType(String eng) {
                LanguagePart tmp= LanguagePart.Прочие;
                foreach(WordCard wc in (this.Owner as Form1).dic.Values)
                {
                    if (wc.EnglishWord == eng) tmp = wc.langPart;
                }

                switch (tmp) {
                    case LanguagePart.Глагол: return "Глагол"; break;
                    case LanguagePart.Существительное: return "Существительное"; break;
                    case LanguagePart.Прилагательное: return "Прилагательное"; break;
                    case LanguagePart.Наречие: return "Наречие"; break;
                    case LanguagePart.Прочие: return "Прочие"; break;
                }
                return null;
            }
            String engTranslation(String eng)
            {

                foreach (string s in (this.Owner as Form1).dic.Keys)
                {
                    if (eng == s)
                    {
                        if (!(this.Owner as Form1).dic.TryGetValue(s, out tmp)) l2.Text = "Слово не найдено";

                        else
                        {

                            String russian = "";

                            foreach (String pair in tmp.RussianWord)
                            {
                                russian = russian + pair + ", ";
                            }
                            return russian;
                        }
                    }

                }
                
                return null;

            }
#endregion
        }

    }
}