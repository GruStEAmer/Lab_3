using System.Numerics;

namespace Lab_3
{
    public partial class Form1 : Form
    {
        private int
            N = 10,
            point = 0,
            endless_point = 0;

        private bool
            check_N = false,
            final_game = false;
        private 
            Dictionary<int, Tuple<int,int,int,int>> d = new Dictionary<int, Tuple<int, int, int,int>>();

        public Form1()
        {
            InitializeComponent();
        }


        private async void Form1_Load(object sender, EventArgs e)
        {
            Random rnd = new Random();
            while (point < 2 * N)
            {
                Button btn = new Button();

                //Свойства кнопки
                btn.BringToFront();
                btn.Location = new Point(rnd.Next(0, 600), rnd.Next(0, 400));
                btn.Name = "But" + endless_point.ToString();
                btn.AutoSize = true;
                btn.TabIndex = endless_point;
                btn.BackColor = Color.FromArgb(rnd.Next(0,255), rnd.Next(0, 255), rnd.Next(0, 255));
                btn.Padding = new Padding(6);
                btn.Font = new Font("French Script MT", 18);
                btn.Click += But_Click;


                //Рандом вертикально или горизонтально
                if (rnd.Next(0, 2) == 0)
                {
                    btn.Size = new Size(50, 200);
                }
                else btn.Size = new Size(200, 50);

                //Добавляем в наш словарь координаты кнопок
                d[endless_point] = Tuple.Create(btn.Location.X,btn.Location.Y,btn.Location.X + btn.Size.Width, btn.Location.Y + btn.Size.Height);


                //Показываем
                Controls.Add(btn);
                    
                //Проверка
                point++;
                endless_point++;
                await Task.Delay(2000);

                if (check_N && point == 0)
                {
                    final_game = true;
                    break;
                }
            }

            if (final_game)
            {
                MessageBox.Show("You won!");
            }
            else
            {
                MessageBox.Show("You lose!");
            }

        }
        //Функция нажатия
        private void But_Click(object sender, EventArgs e)
        {     
            if (point >= N)
            {
                check_N = true;
            }
            if (check_N)
            {
                Button b = (Button)sender;
                int b_point = b.TabIndex;
                for (int i = b_point; i > 0; i--)
                {
                    if (((d[i].Item1 < d[b_point].Item1 && d[b_point].Item1 < d[i].Item3) ||
                        (d[i].Item1 < d[b_point].Item3 && d[b_point].Item3 < d[i].Item3)) &&
                       ((d[i].Item2 < d[b_point].Item2 && d[b_point].Item2 < d[i].Item4) ||
                        (d[i].Item2 < d[b_point].Item4 && d[b_point].Item4 < d[i].Item4))
                    )
                    {
                        return;
                    }
                }
                point--;
                d[b.TabIndex] = Tuple.Create(0, 0, 0, 0);
                b.Dispose();
            }  
        }
            
        }

}
