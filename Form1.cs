using System.Drawing;
using System.Windows.Forms;
using System;
using System.Linq;

namespace calc {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }
        bool isNum1;
        string num1;
        string num2;
        char op;
        double res;
        void init(object sender, EventArgs e) {
            isNum1 = true;
            num1 = "";
            num2 = "";
            op = ' ';
            res = 0;
            textBox_eq.Text = "";
        }

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

        void onClickDot(object sender, EventArgs e) {
            if (textBox_eq.Text.Length == 0 || textBox_eq.Text.Contains(",")) return;
            onClickNumber(sender, e);
        }

        void MouseEnter(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.BackColor = Color.Black;
            button.ForeColor = Color.White;
        }

        void MouseLeave(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.BackColor = Color.Orange;
            button.ForeColor = Color.Black;
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
            if (textBox_eq.Text.Length == 0 || textBox_eq.Text.Last() == ',') return;
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
            if (num2 == "" || textBox_eq.Text.Last() == ',') return;
            calc(ref res, num1, op, num2);
            updateAllAfterCalc();
        }

        private void Form1_Load(object sender, EventArgs e) {
            init(null, null);
            int startX = textBox_eq.Left;
            int x = startX, y = textBox_eq.Bottom;
            for (int i = 0; i < 10; i++) {
                Button button = new Button();
                Controls.Add(button);
                button.Text = i.ToString();
                button.Size = new Size(50, 50);
                button.Location = new Point(x, y);
                button.Click += onClickNumber;
                if (i == 0) {
                    button.Location = new Point(button.Width + button.Left, button.Height * 3 + button.Top);
                }
                else if (i % 3 == 0) {
                    y += button.Height;
                    x = startX;
                }
                else {
                    x += button.Width;
                }
            }

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
            Button_equal.Location = new Point(x + 70, 105);
            Button_equal.Click += onClickEqual;

            Button Button_dot = new Button();
            Controls.Add(Button_dot);
            Button_dot.Text = ",";
            Button_dot.Size = new Size(50, 50);
            Button_dot.Location = new Point(Button_equal.Left, y - Button_dot.Height);
            Button_dot.Click += onClickDot;

            Button Button_C = new Button();
            Controls.Add(Button_C);
            Button_C.Text = "C";
            Button_C.Size = new Size(50, 50);
            Button_C.Location = new Point(Button_equal.Left, textBox_eq.Bottom);
            Button_C.Click += init;

            Button[] buttons = Controls.OfType<Button>().ToArray();
            for (int i = 0; i < buttons.Length; i++) {
                buttons[i].MouseLeave += MouseLeave;
                buttons[i].MouseEnter += MouseEnter;
                buttons[i].BackColor = Color.Orange;
                buttons[i].Font = new Font("Microsoft Sans Serif", 14);
            }
        }
    }
}
