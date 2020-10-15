using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingGame
{
    public partial class Form1 : Form
    {
        private Label firstClicked = null;
        private Label secondClicked = null;
        private Random random = new Random();

        private List<string> icons = new List<string>()
        {
             "!", "!", "N", "N", ",", ",", "k", "k",
        "b", "b", "v", "v", "w", "w", "z", "z"
        };

        private void AssignIconsToSquares()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                // iconLabel의 값이 있으면 실행
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    // 랜덤으로 icons배열의 값을 iconLabel에 넣는다
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;

                    icons.RemoveAt(randomNumber);
                }
            }
        }

        public Form1()

        {
            InitializeComponent();
            AssignIconsToSquares();
        }

        private void label_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                if (clickedLabel.ForeColor == Color.Black)
                {
                    return;
                }

                // 첫번째 클릭 값이 null 이면 실행
                // 첫클릭 하고나면 null값이 아니기때문에 실행안된다
                if (firstClicked == null)
                {
                    // 내가 클릭한 레이블을 firstClicked에 할당
                    firstClicked = clickedLabel;
                    // firstClicked 색 검정색으로
                    firstClicked.ForeColor = Color.Black;
                    // 빠져나가기
                    return;
                }

                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                CheckForWinner();

                // 그림이 일치하면
                if (firstClicked.Text == secondClicked.Text)
                {
                    // 모든 클릭 초기화
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            firstClicked = null;
            secondClicked = null;
        }

        private void CheckForWinner()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconlabel = control as Label;

                if (iconlabel != null)
                {
                    if (iconlabel.ForeColor == iconlabel.BackColor)
                        return;
                }
            }

            MessageBox.Show("모든 그림을 다 맞췄습니다", "축하합니다");
            Close();
        }
    }
}