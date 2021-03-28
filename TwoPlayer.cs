using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe_AIproject
{
    public partial class TwoPlayer : MetroFramework.Forms.MetroForm
    {
        bool _turn = true;       //true = X turn and falsa = ✓ turn
        int count_turn = 0;

        public TwoPlayer()
        {
            InitializeComponent();
        }

        private void NewGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lblCrossplayer.Text = "0";
            lblTickplayer.Text = "0";
            lblDraw.Text = "0";

            _turn = true;
            count_turn = 0;

            try
            {
                foreach (Control c in Controls)
                {
                    try
                    {
                        Button b = (Button)c;
                        b.Enabled = true;
                        b.Text = "";
                    }
                    catch { }
                }
            }
            catch { }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void PlayWithComputerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.Show();
        }

        private void AboutTicTacToeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Developed by:\n   Zia Ur Rehman \n   Bilal Hussain", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void TwoPlayer_Load(object sender, EventArgs e)
        {
            //Removing border from Buttons
            A11.FlatAppearance.BorderSize = 0;
            A22.FlatAppearance.BorderSize = 0;
            A33.FlatAppearance.BorderSize = 0;
            B11.FlatAppearance.BorderSize = 0;
            B22.FlatAppearance.BorderSize = 0;
            B33.FlatAppearance.BorderSize = 0;
            C11.FlatAppearance.BorderSize = 0;
            C22.FlatAppearance.BorderSize = 0;
            C33.FlatAppearance.BorderSize = 0;
        }

        private void button_click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (_turn)
            {
                b.Text = "✗";
            }
            else
            {
                b.Text = "✓";
            }
            _turn = !_turn;
            b.Enabled = false;
            count_turn++;

            checkWinner();
        }

        private void checkWinner()
        {
            bool there_is_a_winner = false;

            //horizontal checks
            if ((A11.Text == A22.Text) && (A22.Text == A33.Text) && (!A11.Enabled))
                there_is_a_winner = true;
            else if ((B11.Text == B22.Text) && (B22.Text == B33.Text) && (!B11.Enabled))
                there_is_a_winner = true;
            else if ((C11.Text == C22.Text) && (C22.Text == C33.Text) && (!C11.Enabled))
                there_is_a_winner = true;

            //Vetical checks
            else if ((A11.Text == B11.Text) && (B11.Text == C11.Text) && (!A11.Enabled))
            {
                there_is_a_winner = true;
            }
            else if ((A22.Text == B22.Text) && (B22.Text == C22.Text) && (!A22.Enabled))
            {
                there_is_a_winner = true;
            }
            else if ((A33.Text == B33.Text) && (B33.Text == C33.Text) && (!A33.Enabled))
            {
                there_is_a_winner = true;
            }
            //Diagonal checks
            else if ((A11.Text == B22.Text) && (B22.Text == C33.Text) && (!A11.Enabled))
            {
                there_is_a_winner = true;
            }
            else if ((A33.Text == B22.Text) && (B22.Text == C11.Text) && (!C11.Enabled))
            {
                there_is_a_winner = true;
            }


            if (there_is_a_winner)
            {
                disableButtons();

                string winner = "";
                if (_turn)
                {
                    winner = "✓";
                    lblTickplayer.Text = (int.Parse(lblTickplayer.Text) + 1).ToString();
                }
                else
                {
                    winner = "✗";
                    lblCrossplayer.Text = (int.Parse(lblCrossplayer.Text) + 1).ToString();

                }
                MessageBox.Show(winner + " Player Wins", "Alert");
            }
            else
            {
                if (count_turn == 9)
                {
                    MessageBox.Show("Game Draw", "Alert");
                    lblDraw.Text = (int.Parse(lblDraw.Text) + 1).ToString();
                }
            }
        }

        private void disableButtons()
        {
            try
            {
                foreach (Control c in Controls)
                {
                    try
                    {
                        Button b = (Button)c;
                        b.Enabled = false;
                    }
                    catch { }
                }
            }
            catch { }
        }

        private void BtnPlayagain_Click(object sender, EventArgs e)
        {
            _turn = true;
            count_turn = 0;

            try
            {
                foreach (Control c in Controls)
                {
                    try
                    {
                        Button b = (Button)c;
                        b.Enabled = true;
                        b.Text = "";
                    }
                    catch { }
                }
            }
            catch { }
        }
    }
}
