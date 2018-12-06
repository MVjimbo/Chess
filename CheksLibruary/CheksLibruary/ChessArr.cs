using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CheksLibruary
{
    public class ChessArr
    {
        Dictionary<int, Chess> black;
        Dictionary<int, Chess> white;
        Dictionary<int, Dictionary<int, Chess>> Pole;
        private  delegate void ThreeElemEvent(ref List<int> list, int x, int y);
        Dictionary<int, ThreeElemEvent> AllEvents;
        int colornow;

        public ChessArr(int a)
        {
            Chess chess;
            black = new Dictionary<int, Chess>();
            white = new Dictionary<int, Chess>();
            Pole = new Dictionary<int, Dictionary<int, Chess>>();
            FileStream fstream = new FileStream("Save.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(fstream);
            string s = reader.ReadLine();
            colornow= Convert.ToInt32(s);
            s = reader.ReadLine();
            while (s!="q")
            {
                char c = s[0];
                int _type = Convert.ToInt32(c)-48;
                int stpoint= 2;
                int pozitionX = Convert.ToInt32(s[stpoint++])-48;
                int pozitionY = Convert.ToInt32(s[stpoint])-48;
                switch (_type)
                {
                    case 1:
                        chess = new Pawn(pozitionX, pozitionY);
                        white.Add(chess.ReturnPozition(), chess);
                        break;
                    case 2:
                        chess = new Rook(pozitionX, pozitionY);
                        white.Add(chess.ReturnPozition(), chess);
                        break;
                    case 3:
                        chess = new Horse(pozitionX, pozitionY);
                        white.Add(chess.ReturnPozition(), chess);
                        break;
                    case 4:
                        chess = new Elephant(pozitionX, pozitionY);
                        white.Add(chess.ReturnPozition(), chess);
                        break;
                    case 5:
                        chess = new Queen(pozitionX, pozitionY);
                        white.Add(chess.ReturnPozition(), chess);
                        break;
                    case 6:
                        chess = new King(pozitionX, pozitionY);
                        white.Add(chess.ReturnPozition(), chess);
                        break;
                }
                s = reader.ReadLine();

            }
            s = reader.ReadLine();
            while (s != "q")
            {
                char c = s[0];
                int _type = Convert.ToInt32(c)-48;
                int stpoint = 2;
                int pozitionX = Convert.ToInt32(s[stpoint++])-48;
                int pozitionY = Convert.ToInt32(s[stpoint])-48;
                switch (_type)
                {
                    case 1:
                        chess = new Pawn(pozitionX, pozitionY);
                        black.Add(chess.ReturnPozition(), chess);
                        break;
                    case 2:
                        chess = new Rook(pozitionX, pozitionY);
                        black.Add(chess.ReturnPozition(), chess);
                        break;
                    case 3:
                        chess = new Horse(pozitionX, pozitionY);
                        black.Add(chess.ReturnPozition(), chess);
                        break;
                    case 4:
                        chess = new Elephant(pozitionX, pozitionY);
                        black.Add(chess.ReturnPozition(), chess);
                        break;
                    case 5:
                        chess = new Queen(pozitionX, pozitionY);
                        black.Add(chess.ReturnPozition(), chess);
                        break;
                    case 6:
                        chess = new King(pozitionX, pozitionY);
                        black.Add(chess.ReturnPozition(), chess);
                        break;
                }
                s = reader.ReadLine();
            }
            reader.Close();
            fstream.Close();
            Pole.Add(1, black);
            Pole.Add(0, white);
            AllEvents = new Dictionary<int, ThreeElemEvent>
            {
                {1,this.Pawn },
                {2,this.Rook },
                {3,this.Horse  },
                {4,this.Elephant },
                {5,this.Queen },
                {6,this.King },
            };
        }

        public ChessArr()
        {
            colornow = 0;//0-белые,1-черные
            Chess chess;
            black = new Dictionary<int, Chess>();
            white = new Dictionary<int, Chess>();
            Pole = new Dictionary<int, Dictionary<int, Chess>>();
            //Расставляем пешки
            for (int i = 1; i < 9; i++)
            {
                chess = new Pawn(2, i);
                white.Add(chess.ReturnPozition(), chess);
                chess = new Pawn(7, i);
                black.Add(chess.ReturnPozition(), chess);
            }
            //Белые фигуры
            chess = new Rook(1, 1);
            white.Add(chess.ReturnPozition(), chess);
            chess = new Rook(1, 8);
            white.Add(chess.ReturnPozition(), chess);
            chess = new Horse(1, 2);
            white.Add(chess.ReturnPozition(), chess);
            chess = new Horse(1, 7);
            white.Add(chess.ReturnPozition(), chess);
            chess = new Elephant(1, 3);
            white.Add(chess.ReturnPozition(), chess);
            chess = new Elephant(1, 6);
            white.Add(chess.ReturnPozition(), chess);
            chess = new Queen(1, 4);
            white.Add(chess.ReturnPozition(), chess);
            chess = new King(1, 5);
            white.Add(chess.ReturnPozition(), chess);
            //Черные фигуры
            chess = new Rook(8, 1);
            black.Add(chess.ReturnPozition(), chess);
            chess = new Rook(8, 8);
            black.Add(chess.ReturnPozition(), chess);
            chess = new Horse(8, 2);
            black.Add(chess.ReturnPozition(), chess);
            chess = new Horse(8, 7);
            black.Add(chess.ReturnPozition(), chess);
            chess = new Elephant(8, 3);
            black.Add(chess.ReturnPozition(), chess);
            chess = new Elephant(8, 6);
            black.Add(chess.ReturnPozition(), chess);
            chess = new Queen(8, 4);
            black.Add(chess.ReturnPozition(), chess);
            chess = new King(8, 5);
            black.Add(chess.ReturnPozition(), chess);
            Pole.Add(1, black);
            Pole.Add(0, white);
            AllEvents = new Dictionary<int, ThreeElemEvent>
            {
                {1,this.Pawn },
                {2,this.Rook },
                {3,this.Horse  },
                {4,this.Elephant },
                {5,this.Queen },
                {6,this.King },
            };
        }


        //Получение информации о возможных вариантах
        public List<int> GetInfo(int a,int b)
        {
            Chess help = new Help();
            int _poz = help.ReturnPozition(a, b);//Позиция выбранной фигуры
            if (!Pole[colornow].ContainsKey(_poz))//Если выбранное не содержится в том цвете
                {
                    var list = new List<int>();
                    list = null;
                    return list;
                }
            else
                {
                    Chess chess = Pole[colornow][_poz];
                    var list = new List<int>();
                    list = chess.FindPozitions();
                    AllEvents[chess.Type](ref list,a,b);
                    return list;
                }
        
        }
       
        public void ChangePozition(int a,int b,int c,int d,ref int sost)
        {
            Chess help = new Help();
            int _poz1 = help.ReturnPozition(a, b);
            int _poz2 = help.ReturnPozition(c,d);
            if (colornow==1)//ход черных
            {
                Chess chess = black[_poz1];
                chess.ChangeReturnPozition(c, d);
                black.Remove(_poz1);
                black.Add(_poz2, chess);
                colornow = 0;
                if (white.ContainsKey(_poz2))
                {
                    if (white[_poz2].Type==6)
                    {
                        sost = 3;
                        return;
                    }
                    white.Remove(_poz2);
                    if (white.Count == 0)
                        sost = 3;
                }
            }
            else
            {
               
                Chess chess = white[_poz1];
                chess.ChangeReturnPozition(c, d);
                white.Remove(_poz1);
                white.Add(_poz2, chess);
                colornow = 1;
                if (black.ContainsKey(_poz2))
                {
                    if (black[_poz2].Type == 6)
                    {
                        sost = 4;
                        return;
                    }
                    black.Remove(_poz2);
                    if (black.Count == 0)
                        sost = 4;
                }
            }
        }


        private void Pawn(ref List<int> list,int a,int b)
        {
            if ((colornow == 1) && (a == 7)) 
                list.Add((a -2) * 10 + b);
             else if ((colornow == 0) && (a == 2)) 
                list.Add((a + 2) * 10 + b);
            for (int i = 0; i < list.Count; i++)
                {
                    if ((white.ContainsKey(list[i])) || (black.ContainsKey(list[i])))//Для собратьев белых
                    {
                        list.RemoveAt(i--);
                    }
                }
            if (colornow == 1)
            {
                list.Remove((a + 1) * 10 + b);
                if (a == 7)
                    list.Add((a - 2) * 10 + b);
                if (white.ContainsKey((a - 1) * 10 + b + 1))
                    list.Add((a - 1) * 10 + b + 1);
                if (white.ContainsKey((a - 1) * 10 + b - 1))
                    list.Add((a - 1) * 10 + b - 1);
            }
            else
            {
                list.Remove((a - 1) * 10 + b);
                if (a == 2)
                    list.Add((a + 2) * 10 + b);
                if (black.ContainsKey((a + 1) * 10 + b + 1))
                    list.Add((a + 1) * 10 + b + 1);
                if (black.ContainsKey((a + 1) * 10 + b - 1))
                    list.Add((a + 1) * 10 + b - 1);
            }
        }

        private void Rook (ref List<int> list,int a,int b)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (Pole[colornow].ContainsKey(list[i]))
                {
                    int m = list[i] / 10;
                    int n = list[i] % 10;
                    if ((m > a) && (n == b))//Верх
                    {
                        while (m < 9)
                        {
                            list.RemoveAt(i);
                            m++;
                        }
                        i--;
                    }
                    else if ((m < a) && (n == b))
                    {
                        while (m > 0)
                        {
                            list.RemoveAt(i);
                            m--;
                        }
                        i--;
                    }
                    else if ((m == a) && (n > b))
                    {
                        while (n < 9)
                        {
                            list.RemoveAt(i);
                            n++;
                        }
                        i--;
                    }
                    else if ((m == a) && (n < b))
                    {
                        while (n > 0)
                        {
                            list.RemoveAt(i);
                            n--;
                        }
                        i--;
                    }
                }
                else if (Pole[Abs(colornow-1)].ContainsKey(list[i]))
                {
                    int m = list[i] / 10;
                    int n = list[i] % 10;
                    if ((m > a) && (n == b))//Верх
                    {
                        m++;
                        while (m < 9)
                        {
                            list.Remove(m * 10 + n);
                            m++;
                        }
                    }
                    else if ((m < a) && (n == b))
                    {
                        m--;
                        while (m > 0)
                        {
                            list.Remove(m * 10 + n);
                            m--;
                        }
                    }
                    else if ((m == a) && (n > b))
                    {
                        n++;
                        while (n < 9)
                        {
                            list.Remove(m * 10 + n);
                            n++;
                        }
                    }
                    else if ((m == a) && (n < b))
                    {
                        n--;
                        while (n > 0)
                        {
                            list.Remove(m * 10 + n);
                            n--;
                        }
                    }
                }
            }
        }

        private void Horse (ref List<int> list,int a,int b)
        {
                for (int i = 0; i < list.Count; i++)
                {
                    if (Pole[colornow].ContainsKey(list[i]))
                    {
                        list.RemoveAt(i--);
                    }
                }
        }

        private void Elephant(ref List<int> list,int a,int b)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (Pole[colornow].ContainsKey(list[i]))//Для собратьев белых,list[i]
                {
                    int m = list[i] / 10;
                    int n = list[i] % 10;
                    if ((m > a) && (n > b))//Вверхний правый угол
                    {
                        while ((m < 9) && (n < 9))
                        {
                            list.RemoveAt(i);
                            m++;
                            n++;
                        }
                        i--;
                    }
                    else if ((m < a) && (n < b))//Левый нижний угол
                    {
                        while ((m > 0) && (n > 0))
                        {
                            list.RemoveAt(i);
                            m--;
                            n--;
                        }
                        i--;
                    }
                    else if ((m > a) && (n < b))//Левый верхний угол
                    {
                        while ((m < 9) && (n > 0))
                        {
                            list.RemoveAt(i);
                            n--;
                            m++;
                        }
                        i--;
                    }
                    else if ((m < a) && (n > b))//Правый нижний угол
                    {
                        while ((m > 0) && (n < 9))
                        {
                            list.RemoveAt(i);
                            n++;
                            m--;
                        }
                        i--;
                    }
                }
                else if (Pole[Abs(colornow-1)].ContainsKey(list[i]))//Для врагов черных
                {
                    int m = list[i] / 10;
                    int n = list[i] % 10;
                    if ((m > a) && (n > b))//Вверхний правый угол
                    {
                        m++;
                        n++;
                        while ((m < 9) && (n < 9))
                        {
                            list.Remove(m * 10 + n);
                            m++;
                            n++;
                        }
                    }
                    else if ((m < a) && (n < b))//Левый нижний угол
                    {
                        m--;
                        n--;
                        while ((m > 0) && (n > 0))
                        {
                            list.Remove(m * 10 + n);
                            m--;
                            n--;
                        }
                    }
                    else if ((m > a) && (n < b))//Левый верхний угол
                    {
                        n--;
                        m++;
                        while ((m < 9) && (n > 0))
                        {
                            list.Remove(m * 10 + n);
                            n--;
                            m++;
                        }
                    }
                    else if ((m < a) && (n > b))//Правый нижний угол
                    {
                        n++;
                        m--;
                        while ((m > 0) && (n < 9))
                        {
                            list.Remove(m * 10 + n);
                            n++;
                            m--;
                        }
                    }
                }
            }
        }
        private void Queen (ref List<int> list,int a,int b)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (Pole[colornow].ContainsKey(list[i]))
                {
                    int m = list[i] / 10;
                    int n = list[i] % 10;
                    if ((m > a) && (n > b))//Вверхний правый угол
                    {
                        while ((m < 9) && (n < 9))
                        {
                            list.RemoveAt(i);
                            m++;
                            n++;
                        }
                        i--;
                    }
                    else if ((m < a) && (n < b))//Левый нижний угол
                    {
                        while ((m > 0) && (n > 0))
                        {
                            list.RemoveAt(i);
                            m--;
                            n--;
                        }
                        i--;
                    }
                    else if ((m > a) && (n < b))//Левый верхний угол
                    {
                        while ((m < 9) && (n > 0))
                        {
                            list.RemoveAt(i);
                            n--;
                            m++;
                        }
                        i--;
                    }
                    else if ((m < a) && (n > b))//Правый нижний угол
                    {
                        while ((m > 0) && (n < 9))
                        {
                            list.RemoveAt(i);
                            n++;
                            m--;
                        }
                        i--;
                    }
                    else if ((m > a) && (n == b))//Верх
                    {
                        while (m < 9)
                        {
                            list.RemoveAt(i);
                            m++;
                        }
                        i--;
                    }
                    else if ((m < a) && (n == b))
                    {
                        while (m > 0)
                        {
                            list.RemoveAt(i);
                            m--;
                        }
                        i--;
                    }
                    else if ((m == a) && (n > b))
                    {
                        while (n < 9)
                        {
                            list.RemoveAt(i);
                            n++;
                        }
                        i--;
                    }
                    else if ((m == a) && (n < b))
                    {
                        while (n > 0)
                        {
                            list.RemoveAt(i);
                            n--;
                        }
                        i--;
                    }
                }
                else if (Pole[Abs(colornow-1)].ContainsKey(list[i]))
                {
                    int m = list[i] / 10;
                    int n = list[i] % 10;
                    if ((m > a) && (n > b))//Вверхний правый угол
                    {
                        m++;
                        n++;
                        while ((m < 9) && (n < 9))
                        {
                            list.Remove(m * 10 + n);
                            m++;
                            n++;
                        }
                    }
                    else if ((m < a) && (n < b))//Левый нижний угол
                    {
                        m--;
                        n--;
                        while ((m > 0) && (n > 0))
                        {
                            list.Remove(m * 10 + n);
                            m--;
                            n--;
                        }
                    }
                    else if ((m > a) && (n < b))//Левый верхний угол
                    {
                        n--;
                        m++;
                        while ((m < 9) && (n > 0))
                        {
                            list.Remove(m * 10 + n);
                            n--;
                            m++;
                        }
                    }
                    else if ((m < a) && (n > b))//Правый нижний угол
                    {
                        n++;
                        m--;
                        while ((m > 0) && (n < 9))
                        {
                            list.Remove(m * 10 + n);
                            n++;
                            m--;
                        }
                    }
                    else if ((m > a) && (n == b))//Верх
                    {
                        m++;
                        while (m < 9)
                        {
                            list.Remove(m * 10 + n);
                            m++;
                        }
                    }
                    else if ((m < a) && (n == b))
                    {
                        m--;
                        while (m > 0)
                        {
                            list.Remove(m * 10 + n);
                            m--;
                        }
                    }
                    else if ((m == a) && (n > b))
                    {
                        n++;
                        while (n < 9)
                        {
                            list.Remove(m * 10 + n);
                            n++;
                        }
                    }
                    else if ((m == a) && (n < b))
                    {
                        n--;
                        while (n > 0)
                        {
                            list.Remove(m * 10 + n);
                            n--;
                        }
                    }
                }
            }
        }

        private void King(ref List<int> list,int a,int b)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (Pole[colornow].ContainsKey(list[i]))
                {
                    list.RemoveAt(i--);
                }
            }
        }

        private int Abs(int a)
        {
            if (a < 0)
                return -a;
            return a;
        }

        public void Save ()
        {
            FileStream fstream = new FileStream("Save.txt", FileMode.Create, FileAccess.Write); ;
            StreamWriter writer = new StreamWriter(fstream);
            writer.WriteLine(colornow);
            foreach (var v in white)
                writer.WriteLine($"{v.Value.Type} {v.Value.ReturnPozition()}");
            writer.WriteLine("q");
            foreach (var v in black)
                writer.WriteLine($"{v.Value.Type} {v.Value.ReturnPozition()}");
            writer.WriteLine("q");
            writer.Close();
            
             fstream.Close();
        }

        public void NewGame()
        {
            File.Delete("Save.txt");
        }
    }
}
