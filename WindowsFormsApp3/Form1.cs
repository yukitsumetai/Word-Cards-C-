using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Linq;


namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {

       

        float erAvg = 0;
        int mEr = 0;

        bool rb = false, bin=true;
        ListBox lb;
        RadioButton r1, r2, r3, r4, r5;

      public  Dictionary<string, WordCard> dic = new Dictionary<string, WordCard>();
       
        void setInvisible(bool choice)
        {
            rb = choice;
            l2.Visible = rb;
            l3.Visible = rb;
            txt2.Visible = rb;
            r1.Visible = rb;
            r2.Visible = rb;
            r3.Visible = rb;
            r4.Visible = rb;
            r5.Visible = rb;
        }



        Label l1, l2, l3, l4, l5;
        Button bt1, bt2, bt3, bt4, bt5, bt6, bt7, bt8, bt9, bt10;
        TextBox txt1, txt2;

      Timer t = new Timer();

        public Form1()
        {


            this.Size = new Size(550, 330);
            this.Text = "Англо-Русский Словарь";
            InitializeComponent();

            #region КНОПКИ
            bt1 = new Button()
            {
                Text = "Добавить слово",
                Size = new Size(90, 60),
                Location = new Point(10, 10),

            };

            this.Controls.Add(bt1);
            bt1.Click += ButtonClick;

            bt2 = new Button()
            {
                Text = "Удалить слово",
                Left = bt1.Left+110,
                Top = bt1.Top,
                Size = bt1.Size
            };

            this.Controls.Add(bt2);
            bt2.Click += ButtonClick2;

            bt3 = new Button()
            {
                Text = "Сохранить в файл",
                Left = bt1.Left,
                Top = bt1.Top + 80,
                Size = bt1.Size
            };

            this.Controls.Add(bt3);
            bt3.Click += ButtonClick3;

            bt4= new Button()
            {
                Text = "Загрузить из файла",
                Left = bt2.Left,
                Top = bt1.Top + 80,
                Size = bt1.Size
            };

            this.Controls.Add(bt4);
            bt4.Click += ButtonClick4;


            bt5 = new Button()
            {
                Text = "Статистика словаря",
                Left = bt1.Left,
                Top = bt3.Top + 80,
                Size = bt1.Size
            };

            this.Controls.Add(bt5);
            bt5.Click += ButtonClick5;

            bt6 = new Button()
            {
                Text = "Открыть словарь",
                Left = bt2.Left,
                Top = bt4.Top + 80,
                Size = bt1.Size
            };


            this.Controls.Add(bt6);
            bt6.Click += ButtonClick6;


            l1 = new Label()
            {
                Left = bt2.Left+120,
                Top = bt2.Top+10,
                BackColor = Color.Empty
               

            };
            l1.AutoSize = true;
            Controls.Add(l1);


            txt1 = new TextBox()
            {
                Left = l1.Left+150,
                Top = l1.Top-3,
                Size = new Size(100, 60)
            };

            txt1.Visible = false;
            Controls.Add(txt1);

            bt7 = new Button()
            {
                Text = "Сохранить",
                Location = new Point(435, 230),
                Size = new Size(80, 40)
            };

           bt7.Visible = false;
            this.Controls.Add(bt7);
            bt7.Click += ButtonClick7;

            bt8 = new Button()
            {
                Text = "Загрузить",
                Location=bt7.Location,
                Size = bt7.Size
            };

            bt8.Visible = false;
            this.Controls.Add(bt8);
            bt8.Click += ButtonClick8;

            l5 = new Label()
            { 
                Left = l1.Left,
                Top = l1.Top,
                
            };
            l5.AutoSize = true;
            Controls.Add(l5);

            #region add/delete


            l3 = new Label()
            {
                Text = "Введите русский перевод (через зяпятую): ",
                Left = l1.Left,
                Top = l1.Top + 30,
                BackColor = Color.Empty
            };
            l3.AutoSize = true; 
            Controls.Add(l3);


            txt2 = new TextBox()
            {
                Left = l3.Left,
                Top = l3.Top + 20,
                Size = new Size(200, 80)
            };

           
            Controls.Add(txt2);

            bt9 = new Button()
            {
                Text = "Добавить",
                Location = bt7.Location,
                Size = bt7.Size
            };

            bt9.Visible = false;
            this.Controls.Add(bt9);
            bt9.Click += ButtonClick9;

            l4 = new Label()
            {   
                Left =txt2.Left,
                Top = 245,
                BackColor = Color.Empty
            };
            l4.AutoSize = true;
            Controls.Add(l4);


            bt10 = new Button()
            {
                Text = "Удалить",
                Location = bt7.Location,
                Size = bt7.Size
            };

            bt10.Visible = false;
            this.Controls.Add(bt10);
            bt10.Click += ButtonClick10;

            l2 = new Label()
            {
                Text = "Выберите часть речи: ",
                Left = l3.Left,
                Top = txt2.Top + 30,
                BackColor = Color.Empty
            };
            l2.AutoSize = true;
            
            Controls.Add(l2);
           
            r1 = new RadioButton()
            {
                Text = "Глагол",
                Left = l2.Left,
                Top = l2.Top+15,
            };
            r1.Visible = rb;
            this.Controls.Add(r1);
          
            r2 = new RadioButton()
            {
                Text = "Существительное",
                Left = l2.Left,
                Top = r1.Top + 20,
            };
            r2.Visible = rb;
            r2.AutoSize = true;
            this.Controls.Add(r2);
            

            r3 = new RadioButton()
            {
                Text = "Прилагательное",
                Left = l2.Left,
                Top = r2.Top + 20,
            };
            r3.Visible = rb;
            r3.AutoSize = true;
            this.Controls.Add(r3);


            r4 = new RadioButton()
            {
                Text = "Наречие",
                Left = l2.Left,
                Top = r3.Top + 20,
            };
           

            this.Controls.Add(r4);
            

            r5 = new RadioButton()
            {
                Text = "Другое",
                Left = l2.Left,
                Top = r4.Top + 20,
            };
            
            this.Controls.Add(r5);
            r5.Checked = true;
            setInvisible(false);
            #endregion

            lb = new ListBox()
            {
                Text = "...",
                Left = txt1.Left + 105,
                Top = txt1.Top,
                Size = new Size(30, 30)

            };
            lb.Items.AddRange(new string[] { ".bin", ".xml"});
            lb.SelectedItem = ".bin";
            lb.Visible = false;
            this.Controls.Add(lb);
            

            #endregion

        }


        #region Buttons functions

        public void ButtonClick(object sender, EventArgs e)
        {
            bt9.Visible = true;
            txt1.Visible = true;  
            l1.Text= "Введите английское слово: ";
            setInvisible(true);
            bt8.Visible = false;
            bt7.Visible = false;
            bt10.Visible = false;
            l5.Text = "";
            lb.Visible = false;

        }
        public void ButtonClick2(object sender, EventArgs e)
        {
            bt10.Visible = true;
            txt1.Visible = true;
            l1.Text = "Введите английское слово: ";
            

            setInvisible(false);
            bt8.Visible = false;
            bt7.Visible = false;
            bt9.Visible = false;
            l5.Text = "";
            lb.Visible = false;

        }   
        public void ButtonClick3(object sender, EventArgs e)
        {
            l1.Text = "Введите имя файла: ";
            txt1.Visible = true;          
            bt7.Visible = true;
            lb.Visible = true;

            setInvisible(false);
            bt8.Visible = false;
            bt9.Visible = false;
            bt10.Visible = false;
        }
        public void ButtonClick4(object sender, EventArgs e)
        {
            l1.Text = "Введите имя файла: ";
            txt1.Visible = true;
            bt8.Visible = true;
            lb.Visible = true;

            setInvisible(false);
            bt7.Visible = false;
            bt9.Visible = false;
            bt10.Visible = false;
        }
        public void ButtonClick5(object sender, EventArgs e)
        {
            
            bt10.Visible = false;
            txt1.Visible = false;
            setInvisible(false);
            bt8.Visible = false;
            bt7.Visible = false;
            bt9.Visible = false;
            bt10.Visible = false;
            lb.Visible = false;

            int n = dic.Count;
            int  x=0, y = 0;
            double engAvg = 0, erAvg = 0, rusAvg=0;
            

            int su = (from n2 in dic.Values where n2.langPart.Equals(LanguagePart.Существительное) select n2).Count();
            int gl = (from n2 in dic.Values where n2.langPart.Equals(LanguagePart.Глагол) select n2).Count();
            int pr = (from n2 in dic.Values where n2.langPart.Equals(LanguagePart.Прилагательное) select n2).Count();
            int nar = (from n2 in dic.Values where n2.langPart.Equals(LanguagePart.Наречие) select n2).Count();
            int dr = (from n2 in dic.Values where n2.langPart.Equals(LanguagePart.Прочие) select n2).Count();
              
            
            if (n > 0) {
            engAvg = (from n2 in dic.Values  select n2.EnglishWord.Length).Average();
            erAvg = (from n2 in dic.Values select n2.Error).Average();
            }

            foreach (WordCard wc in dic.Values)
            {
                foreach (String s in wc.RussianWord)
                {
                    x++;
                    y += s.Length;
                } 
            }

            if (x > 0) rusAvg = (float)y / x;
         

      l1.Text=      @"              СТАТИСТИКА СЛОВАРЯ        
                       
Число английских слов: " + dic.Count+@"

- существительных: "+su+@"

- глаголов: " +gl+ @"

- прилагательных: " + pr + @"

- наречий: " + nar + @"

- других частей речи: " + dr + @"

Среднее количество ошибок: " + erAvg  + @"

Средняя длинна русского слова: " + rusAvg + @"

Средная длинна английского слова: " + engAvg;

        }
        public void ButtonClick6(object sender, EventArgs e)
        {
            l1.Text= "";
            txt1.Visible = false;
            bt7.Visible = false;
            setInvisible(false);
            bt8.Visible = false;
            bt9.Visible = false;
            bt10.Visible = false;
            lb.Visible = false;

            if (dic.Count != 0)
            {
                

                Form2 ff = new Form2();
                ff.Owner = this;
                ff.Show();
            }
            else l4.Text = "Cначала добавьте слова!";
        }


        public void ButtonClick7(object sender, EventArgs e)
        {

            if (lb.SelectedItem.ToString() == ".xml") bin = false;
            else bin = true;

            if (bin)
            {
                string fileName = txt1.Text + ".bin";
               
                if (File.Exists(fileName)) l4.Text = "Файл уже существует";
                else
                {
                    if (binSerealize(fileName)) l4.Text = "Слова загружены в файл "+fileName;
                    else l4.Text = "Ошибка сохранения";
                }
            }

            else {
                string fileName = txt1.Text + ".xml";
                
                if (File.Exists(fileName)) l4.Text = "Файл уже существует";
                else
                {
                    if (xmlSerealize(fileName)) l4.Text = "Слова загружены в файл " + fileName;
                    else l4.Text = "Ошибка сохранения";
                }
            }
            txt1.Text = "";
        }
        public void ButtonClick8(object sender, EventArgs e)
        {
            if (lb.SelectedItem.ToString() == ".xml") bin = false;
            else bin = true;

            if (bin) {
                string fileName = txt1.Text + ".bin";
                if (File.Exists(fileName))
                {
                    if (binDeserealize(fileName)) l4.Text = "Слова загружены";
                    else l4.Text = "Ошибка загрузки";
                }
                else l4.Text = "Файл не найден";
            }
            else {
                string fileName = txt1.Text + ".xml";
                if (File.Exists(fileName))
                {
                    if (xmlDeserealize(fileName)) l4.Text = "Слова загружены";
                    else l4.Text = "Ошибка загрузки";
                }
                else l4.Text = "Файл не найден";

            }

            txt1.Text = "";
        }
        public void ButtonClick9(object sender, EventArgs e)
        { if (txt2.Text != "" & txt1.Text != "") {
                LanguagePart lP;
                if (r1.Checked == true) lP = LanguagePart.Глагол;
               else if (r2.Checked == true) lP = LanguagePart.Существительное;
               else if (r3.Checked == true) lP = LanguagePart.Прилагательное;
               else if (r4.Checked == true) lP = LanguagePart.Наречие;
               else lP = LanguagePart.Прочие;

            List<String> russian = new List<String>(Regex.Split(txt2.Text, @"\,+\s?")); 
            String english = txt1.Text;
            WordCard wc = new WordCard(english, russian, lP);
                if (dic.ContainsKey(english)) l4.Text = "Уже существует";
                else
                {
                    dic.Add(english, wc);
                    l4.Text = "Карточка добавлена";
                    txt1.Text = "";
                    txt2.Text = "";
                }
        }
          else  l4.Text = "Поля обязательны для заполнения";
        }
        public void ButtonClick10(object sender, EventArgs e)
        {           
            String english = txt1.Text;
           if (dic.Remove(english))
                l4.Text = "Карточка удалена";
           else l4.Text = "Карточка не найдена";

        }

        #endregion

        bool binDeserealize(String file) {
            Stream stream = null;
            try
            {
                stream = new FileStream(file, FileMode.Open);
                IFormatter fmt = new BinaryFormatter();
                Dictionary<String, WordCard> tmp = (Dictionary<String, WordCard>)fmt.Deserialize(stream);
                foreach (WordCard wc2 in tmp.Values)
                {
                    dic.Add(wc2.EnglishWord, wc2);
                }
                return true;
            }
            catch (Exception err)
            {
               return false;
            }
            finally
            {
                stream.Close();
            }

        }
        bool xmlDeserealize(String file) {
            Stream stream = null;
            try
            {
                stream = new FileStream(file, FileMode.Open);

                XmlSerializer xmlSer = new XmlSerializer(typeof(List<WordCard>));
                List<WordCard> tmp = (List<WordCard>)xmlSer.Deserialize(stream);
                foreach (WordCard wc2 in tmp)
                {
                    dic.Add(wc2.EnglishWord, wc2);
                }
                return true;
            }
            catch (Exception err)
            {
                return false;
            }
            finally
            {
                stream.Close();
            }
        }

        bool binSerealize (String file){
            Stream stream = null;
            try
            {
                stream = new FileStream(file, FileMode.Create);

                IFormatter fmt = new BinaryFormatter();
                fmt.Serialize(stream, dic);
                return true;
            }
            catch (Exception err)
            {
                return false;
            }
            finally
            {
                stream.Close();
            }
        }
        bool xmlSerealize(String file) {
            Stream stream = null;
            XmlSerializer xmlSer = null;
            try
            {
                stream = new FileStream(file, FileMode.Create);
                var tmp = dic.Values.ToList();
                xmlSer = new XmlSerializer(typeof(List<WordCard>));

                xmlSer.Serialize(stream, tmp);
                return true;
            }
            catch (Exception err)
            {
                return false;
            }
            finally
            {
                stream.Close();
                txt1.Text = "";
            }
        }
    }
}
