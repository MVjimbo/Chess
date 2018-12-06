using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CheksLibruary;
using System.IO;

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        private delegate void NoElemsFunc();
        private delegate void OneElemFunc(int s);
        private event OneElemFunc GiveRezult;
        private event NoElemsFunc ChangeColourWhite;
        private event NoElemsFunc ReturnColourWhite;
        private event NoElemsFunc ChangeColourBlack;
        private event NoElemsFunc ReturnColourBlack;
        private event NoElemsFunc ChangeButtonImmage;
        Dictionary<int, Button> buttons;
        Dictionary<Button, int> number;
        bool _isCliced;
        List<int> variants;
        ChessArr chessArr;
        int _pozold;
        int _isColour;//0-белые,1-черные

        public Form1()
        {
            _isCliced = false;
            variants = new List<int>();
            buttons = new Dictionary<int, Button>();
            number = new Dictionary<Button, int>();
            ChangeColourWhite += ChangeColourW;
            ReturnColourWhite += ReturnColourW;
            ChangeColourBlack += ChangeColourB;
            ReturnColourBlack += ReturnColourB;
            ChangeButtonImmage += ChangeImmage;
            GiveRezult += TryKnowWinner;
            string s;
            FileInfo finfo = new FileInfo("Save.txt");
            if (!finfo.Exists)
            {
                InitializeComponent();
                InitializeButNum();
                chessArr = new ChessArr();
                _isColour = 0;
            }
            else
            {
                InitializeComponent1();
                InitializeButNum();
                chessArr = new ChessArr(1);
                FileStream fstream = new FileStream("Save.txt", FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(fstream);
                s = reader.ReadLine();
                _isColour = Convert.ToInt32(s);
                s = reader.ReadLine();
                if (_isColour == 0)
                {
                    while (s != "q")
                    {
                        char c = s[0];
                        int _type = Convert.ToInt32(c) - 48;
                        int stpoint = 2;
                        int pozition = (Convert.ToInt32(s[stpoint]) - 48) * 10 + Convert.ToInt32(s[stpoint + 1]) - 48;
                        switch (_type)
                        {
                            case 1:
                                buttons[pozition].BackgroundImage = global::WindowsFormsApp.Properties.Resources._1_1;
                                break;
                            case 2:
                                buttons[pozition].BackgroundImage = global::WindowsFormsApp.Properties.Resources._1_2;
                                break;
                            case 3:
                                buttons[pozition].BackgroundImage = global::WindowsFormsApp.Properties.Resources._1_3_1;
                                break;
                            case 4:
                                buttons[pozition].BackgroundImage = global::WindowsFormsApp.Properties.Resources._1_4;
                                break;
                            case 5:
                                buttons[pozition].BackgroundImage = global::WindowsFormsApp.Properties.Resources._1_5;
                                break;
                            case 6:
                                buttons[pozition].BackgroundImage = global::WindowsFormsApp.Properties.Resources._1_6;
                                break;
                        }
                        s = reader.ReadLine();
                    }
                    s = reader.ReadLine();
                    while (s != "q")
                    {
                        char c = s[0];
                        int _type = Convert.ToInt32(c) - 48;
                        int stpoint = 2;
                        int pozition = (Convert.ToInt32(s[stpoint]) - 48) * 10 + Convert.ToInt32(s[stpoint + 1]) - 48;
                        switch (_type)
                        {
                            case 1:
                                buttons[pozition].BackgroundImage = global::WindowsFormsApp.Properties.Resources._2_1;
                                break;
                            case 2:
                                buttons[pozition].BackgroundImage = global::WindowsFormsApp.Properties.Resources._2_2;
                                break;
                            case 3:
                                buttons[pozition].BackgroundImage = global::WindowsFormsApp.Properties.Resources._2_3;
                                break;
                            case 4:
                                buttons[pozition].BackgroundImage = global::WindowsFormsApp.Properties.Resources._2_4;
                                break;
                            case 5:
                                buttons[pozition].BackgroundImage = global::WindowsFormsApp.Properties.Resources._2_5;
                                break;
                            case 6:
                                buttons[pozition].BackgroundImage = global::WindowsFormsApp.Properties.Resources._2_6;
                                break;
                        }
                        s = reader.ReadLine();
                    }
                }
                else
                {
                    while (s != "q")
                    {
                        char c = s[0];
                        int _type = Convert.ToInt32(c) - 48;
                        int stpoint = 2;
                        int pozition = (9-(Convert.ToInt32(s[stpoint]) - 48)) * 10 + 9-(Convert.ToInt32(s[stpoint + 1]) - 48);
                        switch (_type)
                        {
                            case 1:
                                buttons[pozition].BackgroundImage = global::WindowsFormsApp.Properties.Resources._1_1;
                                break;
                            case 2:
                                buttons[pozition].BackgroundImage = global::WindowsFormsApp.Properties.Resources._1_2;
                                break;
                            case 3:
                                buttons[pozition].BackgroundImage = global::WindowsFormsApp.Properties.Resources._1_3_1;
                                break;
                            case 4:
                                buttons[pozition].BackgroundImage = global::WindowsFormsApp.Properties.Resources._1_4;
                                break;
                            case 5:
                                buttons[pozition].BackgroundImage = global::WindowsFormsApp.Properties.Resources._1_5;
                                break;
                            case 6:
                                buttons[pozition].BackgroundImage = global::WindowsFormsApp.Properties.Resources._1_6;
                                break;
                        }
                        s = reader.ReadLine();
                    }
                    s = reader.ReadLine();
                    while (s != "q")
                    {
                        char c = s[0];
                        int _type = Convert.ToInt32(c) - 48;
                        int stpoint = 2;
                        int pozition = (9 - (Convert.ToInt32(s[stpoint]) - 48)) * 10 + 9 - (Convert.ToInt32(s[stpoint + 1]) - 48);
                        switch (_type)
                        {
                            case 1:
                                buttons[pozition].BackgroundImage = global::WindowsFormsApp.Properties.Resources._2_1;
                                break;
                            case 2:
                                buttons[pozition].BackgroundImage = global::WindowsFormsApp.Properties.Resources._2_2;
                                break;
                            case 3:
                                buttons[pozition].BackgroundImage = global::WindowsFormsApp.Properties.Resources._2_3;
                                break;
                            case 4:
                                buttons[pozition].BackgroundImage = global::WindowsFormsApp.Properties.Resources._2_4;
                                break;
                            case 5:
                                buttons[pozition].BackgroundImage = global::WindowsFormsApp.Properties.Resources._2_5;
                                break;
                            case 6:
                                buttons[pozition].BackgroundImage = global::WindowsFormsApp.Properties.Resources._2_6;
                                break;
                        }
                        s = reader.ReadLine();
                    }
                }
                reader.Close();
                fstream.Close();
            }
        }

        private void InitializeButNum()
        {
            buttons.Add(11, button11);
            buttons.Add(12, button12);
            buttons.Add(13, button13);
            buttons.Add(14, button14);
            buttons.Add(15, button15);
            buttons.Add(16, button16);
            buttons.Add(17, button17);
            buttons.Add(18, button18);

            buttons.Add(21, button21);
            buttons.Add(22, button22);
            buttons.Add(23, button23);
            buttons.Add(24, button24);
            buttons.Add(25, button25);
            buttons.Add(26, button26);
            buttons.Add(27, button27);
            buttons.Add(28, button28);

            buttons.Add(31, button31);
            buttons.Add(32, button32);
            buttons.Add(33, button33);
            buttons.Add(34, button34);
            buttons.Add(35, button35);
            buttons.Add(36, button36);
            buttons.Add(37, button37);
            buttons.Add(38, button38);

            buttons.Add(41, button41);
            buttons.Add(42, button42);
            buttons.Add(43, button43);
            buttons.Add(44, button44);
            buttons.Add(45, button45);
            buttons.Add(46, button46);
            buttons.Add(47, button47);
            buttons.Add(48, button48);

            buttons.Add(51, button51);
            buttons.Add(52, button52);
            buttons.Add(53, button53);
            buttons.Add(54, button54);
            buttons.Add(55, button55);
            buttons.Add(56, button56);
            buttons.Add(57, button57);
            buttons.Add(58, button58);

            buttons.Add(61, button61);
            buttons.Add(62, button62);
            buttons.Add(63, button63);
            buttons.Add(64, button64);
            buttons.Add(65, button65);
            buttons.Add(66, button66);
            buttons.Add(67, button67);
            buttons.Add(68, button68);

            buttons.Add(71, button71);
            buttons.Add(72, button72);
            buttons.Add(73, button73);
            buttons.Add(74, button74);
            buttons.Add(75, button75);
            buttons.Add(76, button76);
            buttons.Add(77, button77);
            buttons.Add(78, button78);

            buttons.Add(81, button81);
            buttons.Add(82, button82);
            buttons.Add(83, button83);
            buttons.Add(84, button84);
            buttons.Add(85, button85);
            buttons.Add(86, button86);
            buttons.Add(87, button87);
            buttons.Add(88, button88);

            number.Add(button11, 11);
            number.Add(button12, 12);
            number.Add(button13, 13);
            number.Add(button14, 14);
            number.Add(button15, 15);
            number.Add(button16, 16);
            number.Add(button17, 17);
            number.Add(button18, 18);

            number.Add(button21, 21);
            number.Add(button22, 22);
            number.Add(button23, 23);
            number.Add(button24, 24);
            number.Add(button25, 25);
            number.Add(button26, 26);
            number.Add(button27, 27);
            number.Add(button28, 28);

            number.Add(button31, 31);
            number.Add(button32, 32);
            number.Add(button33, 33);
            number.Add(button34, 34);
            number.Add(button35, 35);
            number.Add(button36, 36);
            number.Add(button37, 37);
            number.Add(button38, 38);

            number.Add(button41, 41);
            number.Add(button42, 42);
            number.Add(button43, 43);
            number.Add(button44, 44);
            number.Add(button45, 45);
            number.Add(button46, 46);
            number.Add(button47, 47);
            number.Add(button48, 48);

            number.Add(button51, 51);
            number.Add(button52, 52);
            number.Add(button53, 53);
            number.Add(button54, 54);
            number.Add(button55, 55);
            number.Add(button56, 56);
            number.Add(button57, 57);
            number.Add(button58, 58);

            number.Add(button61, 61);
            number.Add(button62, 62);
            number.Add(button63, 63);
            number.Add(button64, 64);
            number.Add(button65, 65);
            number.Add(button66, 66);
            number.Add(button67, 67);
            number.Add(button68, 68);

            number.Add(button71, 71);
            number.Add(button72, 72);
            number.Add(button73, 73);
            number.Add(button74, 74);
            number.Add(button75, 75);
            number.Add(button76, 76);
            number.Add(button77, 77);
            number.Add(button78, 78);

            number.Add(button81, 81);
            number.Add(button82, 82);
            number.Add(button83, 83);
            number.Add(button84, 84);
            number.Add(button85, 85);
            number.Add(button86, 86);
            number.Add(button87, 87);
            number.Add(button88, 88);
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (_isColour == 0)
            {
                int _poz = number[((Button)sender)];
                int a = _poz / 10;
                int b = _poz % 10;
                if (_isCliced == false)
                {
                    var list = new List<int>();
                    _pozold = _poz;
                    list = chessArr.GetInfo(a, b);
                    if (list != null)
                    {
                        variants = list;
                        _isCliced = true;
                        ChangeColourWhite();
                    }
                    else
                    {
                        variants.Clear();
                    }
                }
                else if ((_isCliced == true) && (!variants.Contains(_poz)))
                {
                    var list = new List<int>();
                    list = chessArr.GetInfo(a, b);
                    ReturnColourWhite();
                    variants.Clear();
                    _pozold = _poz;
                    if (list != null)
                    {
                        variants = list;
                        _isCliced = true;
                        ChangeColourWhite();
                    }
                    else
                    {
                        _isCliced = false;
                        variants.Clear();
                    }
                }
                else
                {
                    _isCliced = false;
                    ReturnColourWhite();
                    int sost=0;
                    chessArr.ChangePozition(_pozold / 10, _pozold % 10, a, b, ref sost);
                    buttons[_poz].BackgroundImage = buttons[_pozold].BackgroundImage;
                    buttons[_pozold].BackgroundImage = null;
                    variants.Clear();
                    GiveRezult(sost);
                    _isColour = 1;
                    label1.Text = "Ход черных";
                    ChangeButtonImmage();
                }
            }
            else
            {
                int _poz = number[((Button)sender)];
                int a = 9-_poz / 10;
                int b = 9-_poz % 10;
                _poz = a * 10 + b;
                if (_isCliced == false)
                {
                    var list = new List<int>();
                    _pozold = _poz;
                    list = chessArr.GetInfo(a, b);
                    if (list != null)
                    {
                        variants = list;
                        _isCliced = true;
                        ChangeColourBlack();
                    }
                    else
                    {
                        variants.Clear();
                    }
                }
                else if ((_isCliced == true) && (!variants.Contains(_poz)))
                {
                    var list = new List<int>();
                    list = chessArr.GetInfo(a, b);
                    ReturnColourBlack();
                    variants.Clear();
                    _pozold = _poz;
                    if (list != null)
                    {
                        variants = list;
                        _isCliced = true;
                        ChangeColourBlack();
                    }
                    else
                     {
                         _isCliced = false;
                         variants.Clear();
                     }
                }
                else
                {
                    _isCliced = false;
                    ReturnColourBlack();
                    int sost=0;
                    chessArr.ChangePozition(_pozold / 10, _pozold % 10, a, b,ref sost);
                    int m = 9 - _poz/10;
                    int n = 9 - _poz % 10;
                    int f = 9 - _pozold / 10;
                    int g = 9 - _pozold % 10;
                    buttons[m*10+n].BackgroundImage = buttons[f*10+g].BackgroundImage;
                    buttons[f * 10 + g].BackgroundImage = null;
                    variants.Clear();
                    GiveRezult(sost);
                    _isColour = 0;
                    label1.Text = "Ход белых";
                    ChangeButtonImmage();
                }
            }
        }

        private void TryKnowWinner(int s)
        {
            if (s == 3)
            {
                DialogResult result = MessageBox.Show("Черные выйграли. Сыграть заного?", "", MessageBoxButtons.RetryCancel);
                if (result == DialogResult.Retry)
                {
                    Application.Restart();
                }
                else
                    Application.Exit();
            }
            else if (s == 4)
            {
                DialogResult result = MessageBox.Show("Белые выйграли. Сыграть заного?", "", MessageBoxButtons.RetryCancel);
                if (result == DialogResult.Retry)
                {
                    Application.Restart();
                }
                else
                    Application.Exit();
            }
            _isColour = 0;
        }

        private void ChangeColourW ()
        {
            for (int i = 0; i < variants.Count; i++)
            {
                if (buttons[variants[i]].BackColor == Color.SaddleBrown)
                {
                    buttons[variants[i]].BackColor = Color.Chocolate;
                }
                else if (buttons[variants[i]].BackColor == Color.Peru)
                {
                    buttons[variants[i]].BackColor = Color.PeachPuff;
                }
            }
        }

        private void ReturnColourW ()
        {
            for (int i = 0; i < variants.Count; i++)
            {
                if (buttons[variants[i]].BackColor == Color.Chocolate)
                {
                    buttons[variants[i]].BackColor = Color.SaddleBrown;
                }
                else if (buttons[variants[i]].BackColor == Color.PeachPuff)
                {
                    buttons[variants[i]].BackColor = Color.Peru;
                }
            }
        }
        private void ChangeColourB()
        {
            for (int i = 0; i < variants.Count; i++)
            {
                int c = 9 - variants[i] / 10;
                int d = 9 - variants[i] % 10;
                if (buttons[c * 10 + d].BackColor == Color.SaddleBrown)
                {
                    buttons[c * 10 + d].BackColor = Color.Chocolate;
                }
                else if (buttons[c * 10 + d].BackColor == Color.Peru)
                {
                    buttons[c * 10 + d].BackColor = Color.PeachPuff;
                }
            }
        }
        private void ReturnColourB()
        {
            for (int i = 0; i < variants.Count; i++)
            {
                int c = 9 - variants[i] / 10;
                int d = 9 - variants[i] % 10;
                if (buttons[c * 10 + d].BackColor == Color.Chocolate)
                {
                    buttons[c * 10 + d].BackColor = Color.SaddleBrown;
                }
                else if (buttons[c * 10 + d].BackColor == Color.PeachPuff)
                {
                    buttons[c * 10 + d].BackColor = Color.Peru;
                }
            }
        }
        private void ChangeImmage()
        {
            for (int i = 1; i < 5; i++)
                for (int j = 1; j < 9; j++)
                {
                    Button button = new Button();
                    button.BackgroundImage = buttons[i * 10 + j].BackgroundImage;
                    buttons[i * 10 + j].BackgroundImage = buttons[(9 - i) * 10 + 9 - j].BackgroundImage;
                    buttons[(9 - i) * 10 + 9 - j].BackgroundImage = button.BackgroundImage;
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chessArr.Save();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chessArr.NewGame();
            Application.Restart();
        }
    }
}
