using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Masons4Gewinnt
{
    public partial class Form1 : Form
    {
        Label[,] Feld = new Label[7, 6];
        bool red = true;
        int[] hoehe = { 5, 5, 5, 5, 5, 5, 5 };
        int moves = 42;
        public Form1()
        {
            InitializeComponent();
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, 100, 100);
            int index = 2;
            this.BackColor = Color.SkyBlue;
            for (int X = 0; X < 7; X++)
            {
                for (int Y = 0; Y < 6; Y++)
                {
                    Feld[X, Y] = new System.Windows.Forms.Label();
                    Feld[X, Y].BackColor = Color.White;
                    Feld[X, Y].AutoSize = false;
                    Feld[X, Y].Location = new System.Drawing.Point(X * 100, Y * 100);
                    Feld[X, Y].Name = "label" + (index++);
                    Feld[X, Y].Size = new System.Drawing.Size(100, 100);
                    Feld[X, Y].TabIndex = index;
                    Feld[X, Y].Region = new Region(path);
                    Feld[X, Y].Text = "";
                    Feld[X, Y].AccessibleDescription = "";
                    Feld[X, Y].Click += new System.EventHandler(this.label1_Click);
                    this.Controls.Add(Feld[X, Y]);
                }
            }
        }

        private bool inReihe(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
        {
            if (Feld[x1, y1].AccessibleDescription == Feld[x2, y2].AccessibleDescription &&
                Feld[x2, y2].AccessibleDescription == Feld[x3, y3].AccessibleDescription &&
                Feld[x3, y3].AccessibleDescription == Feld[x4, y4].AccessibleDescription &&
                Feld[x1, y1].AccessibleDescription != "")
                return true;
            return false;
        }
        private bool Diagonale1() //von oben links nach unten rechts
        {
            for (int x = 0; x < 4; x++)
            {
                for (int y = 5; y >= 3; y--)
                {
                    if (inReihe(x, y, x + 1, y - 1, x + 2, y - 2, x + 3, y - 3)) return true;
                }
            }
            return false;
        }
        private bool Diagonale2() //von unten links nach oben rechts
        {
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y <= 2; y++)
                {
                    if (inReihe(x, y, x + 1, y + 1, x + 2, y + 2, x + 3, y + 3)) return true;
                }
            }
            return false;
        }
        private bool gewonnen(int X, int Y)
        {
            for (int i = 0; i < 4; i++)
                if (inReihe(i, Y, i + 1, Y, i + 2, Y, i + 3, Y)) return true;
            for (int i = 0; i < 3; i++)
                if (inReihe(X, i, X, i + 1, X, i + 2, X, i + 3)) return true;
            if (Diagonale1()) return true;
            if (Diagonale2()) return true;
            return false;
        }
        private void reset(string s)
        {
            MessageBox.Show(s);
            red = true;
            for (int i = 0; i < 7; i++)
                hoehe[i] = 5;
            foreach (Label L in Feld)
            {
                L.BackColor = Color.White;
                L.AccessibleDescription = "";
            }
            moves = 42;
        }
        private void label1_Click(object sender, EventArgs e)
        {
            int X = (sender as Label).Location.X / 100;
            //(sender as Label).BackColor = (red ? Color.Red : Color.Yellow);

            int Y = hoehe[X]--;
            if (Y >= 0)
            {
                Feld[X, Y].BackColor = (red ? Color.Red : Color.Yellow);
                Feld[X, Y].AccessibleDescription = (red ? "red" : "yellow");
                moves--;
                if (gewonnen(X, Y)) reset((red ? "Rot" : "Gelb") + " hat gewonnen.");
                else if (moves <= 0) reset("Unentschieden");
                else red = red ? false : true;

            }

           
        }
        int RotPunktzahl = 0;
        int GelbPunktzahl = 0;

        
    }
} 
