using System.Drawing;
using System.Windows.Forms;
using System;

namespace calc {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }
        bool isNum1 = true;
        string num1 = "";
        string num2 = "";
        char op = ' ';
        double res = 0;
        void onClickNumber(object sender, EventArgs e) {
            Button button = (Button)sender;
            textBox_eq.Text += button.Text;
            if (isNum1) {
                num1 += button.Text;
            }
            else {
                num2 += button.Text;
            }
        }
        
        void calc(ref double res, string num1, char op, string num2) {
            //MessageBox.Show("num1 = " + num1 + " num2 = " + num2);
            double a = Convert.ToDouble(num1), b = Convert.ToDouble(num2);
            switch (op) {
                case '+': res = a + b; break;
                case '-': res = a - b; break;
                case '*': res = a * b; break;
                case '/': res = a / b; break;
            }
        }

        void updateAllAfterCalc() {
            textBox_eq.Text = res.ToString();
            num1 = res.ToString();
            num2 = "";
            isNum1 = false;
        }

        void onClickOp(object sender, EventArgs e) {
            Button button = (Button)sender;
            op = Convert.ToChar(button.Text);
            isNum1 = false;
            if (double.TryParse(num1, out _) && double.TryParse(num2, out _)) {
                calc(ref res, num1, op, num2);
                updateAllAfterCalc();
            }
            textBox_eq.Text = "";
        }

        void onClickEqual(object sender, EventArgs e) {
            calc(ref res, num1, op, num2);
            updateAllAfterCalc();
        }

        private void Form1_Load(object sender, EventArgs e) {
            int startX = textBox_eq.Left;
            int x = startX, y = textBox_eq.Bottom;
            for (int i = 1; i < 10; i++) {
                Button button = new Button();
                Controls.Add(button);
                button.Text = i.ToString();
                button.Size = new Size(50, 50);
                button.Location = new Point(x, y);
                button.Click += onClickNumber;
                if (i % 3 == 0) {
                    y += button.Height;
                    x = startX;
                }
                else {
                    x += button.Width;
                }
            }
            Button button0 = new Button();
            Controls.Add(button0);
            button0.Text = "0";
            button0.Size = new Size(50, 50);
            button0.Location = new Point(textBox_eq.Width / 3, y);
            button0.Click += onClickNumber;

            char[] opSymbols = {'+', '-', '*', '/'};
            x = startX + 175;
            y = textBox_eq.Bottom;
            for (int i = 0; i < opSymbols.Length; i++) {
                Button button = new Button();
                Controls.Add(button);
                button.Text = opSymbols[i].ToString();
                button.Size = new Size(50, 50);
                button.Location = new Point(x, y);
                button.Click += onClickOp;
                y += button.Height;
            }
            Button Button_equal = new Button();
            Controls.Add(Button_equal);
            Button_equal.Text = "=";
            Button_equal.Size = new Size(50, 70);
            Button_equal.Location = new Point(x + 70, y / 3);
            Button_equal.Click += onClickEqual;
        }
    }
}
