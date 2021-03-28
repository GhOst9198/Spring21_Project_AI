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
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {

        bool turn = true;  //if ture it's "X" turns else "O" turn
        int turnCount = 0;  //no space on board, end condition


        bool AAI = false;   //if false game is 2 players  else with AI
        bool match_over = false;    //telling the AI the game is over

        public Form1()
        {
            InitializeComponent();
        }

        private void NewGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            o_win_count.Text = "0";
            x_win_count.Text = "0";
            draw_count.Text = "0";

            turn = true;
            turnCount = 0;
            match_over = false;
            foreach (Control c in Controls)
            {
                try
                {
                    Button b = (Button)c;
                    b.Enabled = true;
                    b.Text = "";
                }//end try
                catch
                { }
            }

            //check if AI is active
            if (turn == false && AAI)
            {
                AI_move();
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void PlayWithComputerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lblP1.Text = "Player";
            lblP2.Text = "AI";
        }

        private void TwoPlayersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            TwoPlayer twoPlayer = new TwoPlayer();
            twoPlayer.Show();
        }

        private void AboutTicTacToeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Developed by:\n   Zia Ur Rehman \n   Bilal Hussain", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Removing border from Buttons
            A1.FlatAppearance.BorderSize = 0;
            A2.FlatAppearance.BorderSize = 0;
            A3.FlatAppearance.BorderSize = 0;
            B1.FlatAppearance.BorderSize = 0;
            B2.FlatAppearance.BorderSize = 0;
            B3.FlatAppearance.BorderSize = 0;
            C1.FlatAppearance.BorderSize = 0;
            C2.FlatAppearance.BorderSize = 0;
            C3.FlatAppearance.BorderSize = 0;

            playWithComputerToolStripMenuItem.PerformClick();

            //check if AI is active
            if (turn == false && AAI)
            {
                AI_move();
            }
        }

        private void button_click(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            if (turn)
            {
                b.Text = "X";
            }
            else
            {
                b.Text = "O";
            }

            b.Enabled = false;
            turnCount++;
            turn = !turn;
            winner();

            //check if AI is active
            if (turn == false && AAI)
            {
                AI_move();
            }
        }

        private void LblP2_TextChanged(object sender, EventArgs e)
        {
            if (lblP2.Text.ToUpper() == "AI")
            { AAI = true; }
            else
            { AAI = false; }
        }

        private void mouse_hover(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (turn)
            {
                b.Text = "X";
            }
            else
            {
                b.Text = "O";
            }
        }

        private void mouse_leave(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Enabled)
            {
                b.Text = "";
            }
        }

        private void disableButton()
        {
            try
            {
                foreach (Control c in Controls)
                {
                    Button b = (Button)c;
                    b.Enabled = false;
                }
            }
            catch
            { }
        }

        //A.I logic
        private void AI_move()
        {
            /*
             * AI rules!
             * rule 1 : get 3 in a row
             * rule 2: stop 3 in a row
             * rule 3: go for center tile
             * rule 4: go for corner tile
             * rule 5: just pick what eve is left
             */

            Button move = null;

            move = win_Or_Block("O");//look for the  win
            if (move == null)
            {
                move = win_Or_Block("X");//look for the block
                if (move == null)
                {
                    move = center_tile();//always get center tile
                    if (move == null)
                    {
                        move = corner_tile();
                        if (move == null)
                        {
                            move = open_space();
                        }
                    }
                }
            }
            if (!match_over)
            { move.PerformClick(); }
        }

        #region AI logic
        private Button center_tile()
        {
            if (B2.Text == "")
            { return B2; }
            return null;
        }

        private Button open_space()
        {
            Console.WriteLine("Looking for open space");
            Button b = null;
            foreach (Control c in Controls)
            {
                b = c as Button;
                if (b != null)
                {
                    if (b.Text == "")
                        return b;
                }
            }

            return null;
        }

        private Button corner_tile()
        {
            Console.WriteLine("Looking for corner");
            if (A1.Text == "O")
            {
                if (A3.Text == "")
                    return A3;
                if (C3.Text == "")
                    return C3;
                if (C1.Text == "")
                    return C1;
            }

            if (A3.Text == "O")
            {
                if (A1.Text == "")
                    return A1;
                if (C3.Text == "")
                    return C3;
                if (C1.Text == "")
                    return C1;
            }

            if (C3.Text == "O")
            {
                if (A1.Text == "")
                    return A3;
                if (A3.Text == "")
                    return A3;
                if (C1.Text == "")
                    return C1;
            }

            if (C1.Text == "O")
            {
                if (A1.Text == "")
                    return A3;
                if (A3.Text == "")
                    return A3;
                if (C3.Text == "")
                    return C3;
            }

            if (A1.Text == "")
                return A1;
            if (A3.Text == "")
                return A3;
            if (C1.Text == "")
                return C1;
            if (C3.Text == "")
                return C3;

            return null;
        }

        private Button win_Or_Block(string mark)
        {
            Console.WriteLine("Looking for win or block:  " + mark);
            //HORIZONTAL TESTS
            if ((A1.Text == mark) && (A2.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A2.Text == mark) && (A3.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (A3.Text == mark) && (A2.Text == ""))
                return A2;

            if ((B1.Text == mark) && (B2.Text == mark) && (B3.Text == ""))
                return B3;
            if ((B2.Text == mark) && (B3.Text == mark) && (B1.Text == ""))
                return B1;
            if ((B1.Text == mark) && (B3.Text == mark) && (B2.Text == ""))
                return B2;

            if ((C1.Text == mark) && (C2.Text == mark) && (C3.Text == ""))
                return C3;
            if ((C2.Text == mark) && (C3.Text == mark) && (C1.Text == ""))
                return C1;
            if ((C1.Text == mark) && (C3.Text == mark) && (C2.Text == ""))
                return C2;

            //VERTICAL TESTS
            if ((A1.Text == mark) && (B1.Text == mark) && (C1.Text == ""))
                return C1;
            if ((B1.Text == mark) && (C1.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (C1.Text == mark) && (B1.Text == ""))
                return B1;

            if ((A2.Text == mark) && (B2.Text == mark) && (C2.Text == ""))
                return C2;
            if ((B2.Text == mark) && (C2.Text == mark) && (A2.Text == ""))
                return A2;
            if ((A2.Text == mark) && (C2.Text == mark) && (B2.Text == ""))
                return B2;

            if ((A3.Text == mark) && (B3.Text == mark) && (C3.Text == ""))
                return C3;
            if ((B3.Text == mark) && (C3.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A3.Text == mark) && (C3.Text == mark) && (B3.Text == ""))
                return B3;

            //DIAGONAL TESTS
            if ((A1.Text == mark) && (B2.Text == mark) && (C3.Text == ""))
                return C3;
            if ((B2.Text == mark) && (C3.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (C3.Text == mark) && (B2.Text == ""))
                return B2;

            if ((A3.Text == mark) && (B2.Text == mark) && (C1.Text == ""))
                return C1;
            if ((B2.Text == mark) && (C1.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A3.Text == mark) && (C1.Text == mark) && (B2.Text == ""))
                return B2;

            return null;
        }
        #endregion

        private void winner()
        {
            bool winnerWinnerChickenDinner = false;

            //horizontal win check
            if (A1.Text == A2.Text && A2.Text == A3.Text && A1.Enabled == false)
            { winnerWinnerChickenDinner = true; }
            else if (B1.Text == B2.Text && B2.Text == B3.Text && B1.Enabled == false)
            { winnerWinnerChickenDinner = true; }
            else if (C1.Text == C2.Text && C2.Text == C3.Text && C1.Enabled == false)
            { winnerWinnerChickenDinner = true; }
            //end horizontal win check

            //vettical win check
            else if (A1.Text == B1.Text && B1.Text == C1.Text && A1.Enabled == false)
            { winnerWinnerChickenDinner = true; }
            else if (A2.Text == B2.Text && B2.Text == C2.Text && A2.Enabled == false)
            { winnerWinnerChickenDinner = true; }
            else if (A3.Text == B3.Text && B3.Text == C3.Text && A3.Enabled == false)
            { winnerWinnerChickenDinner = true; }
            //end vertical win check

            //diagonal win check
            else if (A1.Text == B2.Text && B2.Text == C3.Text && A1.Enabled == false)
            { winnerWinnerChickenDinner = true; }
            else if (A3.Text == B2.Text && B2.Text == C1.Text && C1.Enabled == false)
            { winnerWinnerChickenDinner = true; }
            //end diagonal win check


            if (winnerWinnerChickenDinner == true)
            {
                disableButton();

                if (turn)
                {
                   o_win_count.Text = (int.Parse(o_win_count.Text) + 1).ToString();
                   match_over = true;
                }
                else
                {
                    x_win_count.Text = (int.Parse(x_win_count.Text) + 1).ToString();
                    match_over = true;
                }
                MessageBox.Show("AI Wins!", "Alert",MessageBoxButtons.OK,MessageBoxIcon.Information);
               
            }
            else
            {
                if (turnCount == 9)
                {
                    MessageBox.Show("Match Draw!!!", "Alert", MessageBoxButtons.OK,MessageBoxIcon.Information);
                    draw_count.Text = (int.Parse(draw_count.Text) + 1).ToString();
                    match_over = true;
                }
            }

        }

        private void Label1_Click(object sender, EventArgs e)
        {
            o_win_count.Text = "0";
            x_win_count.Text = "0";
            draw_count.Text = "0";

            turn = false;
            turnCount = 0;
            match_over = false;
            foreach (Control c in Controls)
            {
                try
                {
                    Button b = (Button)c;
                    b.Enabled = true;
                    b.Text = "";
                }
                catch
                { }
            }

            //check if AI is active
            if (turn == true && AAI)
            {
                AI_move();
            }
        }

        private void BtnPlayagain_Click(object sender, EventArgs e)
        {
            turn = true;
            turnCount = 0;
            match_over = false;
            foreach (Control c in Controls)
            {
                try
                {
                    Button b = (Button)c;
                    b.Enabled = true;
                    b.Text = "";
                }//end try
                catch
                { }
            }

            //check if AI is active
            if (turn == false && AAI)
            {
                AI_move();
            }
        }
    }
}
