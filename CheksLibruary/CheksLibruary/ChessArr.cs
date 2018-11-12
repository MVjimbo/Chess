using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheksLibruary
{
    public class ChessArr
    {
        Dictionary<int, Chess> black;
        Dictionary<int, Chess> white;
        bool colornow;


        public ChessArr()
        {
            colornow = false;//0-белые,1-черные
            Chess chess;
            black = new Dictionary<int, Chess>();
            white = new Dictionary<int, Chess>();
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
        }



        //Получение информации о возможных вариантах
        public List<int> GetInfo(int a,int b)
        {
            Chess help = new Help();
            int _poz = help.ReturnPozition(a, b);//Позиция выбранной фигуры
            if (colornow==true)//Ход черных
            {
                if (!black.ContainsKey(_poz))//Если выбранное не содержится в черном
                {
                    var list = new List<int>();
                    list = null;
                    return list;
                }
                else
                {
                    Chess chess = black[_poz];
                    var list = new List<int>();
                    list = chess.FindPozitions();
                    int _type = chess.Type;
                    switch (_type)
                    {
                        case 1://Выбрали пешку
                            list.Remove((a + 1) * 10 + b);
                            for (int i = 0; i < list.Count; i++)
                            {
                                if ((white.ContainsKey(list[i])) || (black.ContainsKey(list[i])))//Для собратьев белых
                                {
                                    list.RemoveAt(i--);
                                }
                            }
                            if (a == 7)
                                list.Add((a -2) * 10 + b);
                            if (white.ContainsKey((a-1)*10+b+1))
                                list.Add((a - 1) * 10 + b + 1);
                            if (white.ContainsKey((a - 1) * 10 + b -1))
                                list.Add((a - 1) * 10 + b -1);
                            break;
                        case 2://Выбрали ладью
                            for (int i = 0; i < list.Count; i++)
                            {
                                if (black.ContainsKey(list[i]))//Для собратьев черных,list[i]
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
                                else if (white.ContainsKey(list[i]))//Для врагов белых
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
                            break;
                        case 3://Конь
                            for (int i = 0; i < list.Count; i++)
                            {
                                if (black.ContainsKey(list[i]))//Для собратьев черных,list[i]
                                {
                                    list.RemoveAt(i--);
                                }
                            }
                            break;
                        case 4://Cлон
                            for (int i = 0; i < list.Count; i++)
                            {
                                if (black.ContainsKey(list[i]))//Для собратьев черных,list[i]
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
                                else if (white.ContainsKey(list[i]))//Для врагов белых
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
                            break;
                        case 5://Ферзь
                            for (int i = 0; i < list.Count; i++)
                            {
                                if (black.ContainsKey(list[i]))//Для собратьев черных,list[i]
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
                                else if (white.ContainsKey(list[i]))//Для врагов белых
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
                            break;
                        case 6://Король
                            for (int i = 0; i < list.Count; i++)
                            {
                                if (black.ContainsKey(list[i]))//Для собратьев черных,list[i]
                                {
                                    list.RemoveAt(i--);
                                }
                            }
                            break;


                    }
                    return list;
                }
            }
            else // Белые ходят
            {
                if (!white.ContainsKey(_poz))//Если выбранное не содержится в белом
                {
                    var list = new List<int>();
                    list = null;
                    return list;
                }
                else
                {
                    Chess chess = white[_poz];
                    var list = new List<int>();
                    list = chess.FindPozitions();
                    int _type = chess.Type;
                    switch (_type)
                    {
                        case 1://Выбрали пешку
                            list.Remove((a - 1) * 10 + b);
                            for (int i = 0; i < list.Count; i++)
                            {
                                if ((white.ContainsKey(list[i])) || (black.ContainsKey(list[i])))//Для собратьев белых
                                {
                                    list.RemoveAt(i--);
                                }
                            }
                            if (a == 2)
                                list.Add((a + 2) * 10 + b);
                            if (black.ContainsKey((a + 1) * 10 + b + 1))
                                list.Add((a + 1) * 10 + b + 1);
                            if (black.ContainsKey((a + 1) * 10 + b - 1))
                                list.Add((a + 1) * 10 + b - 1);
                            break;
                        case 2://Выбрали ладью
                            for (int i = 0; i < list.Count; i++)
                            {
                                if (white.ContainsKey(list[i]))//Для собратьев белых,list[i]
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
                                else if (black.ContainsKey(list[i]))//Для врагов черных
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
                            break;
                        case 3://Конь
                            for (int i = 0; i < list.Count; i++)
                            {
                                if (white.ContainsKey(list[i]))//Для собратьев белых,list[i]
                                {
                                    list.RemoveAt(i--);
                                }
                            }
                            break;
                        case 4://Cлон
                            for (int i = 0; i < list.Count; i++)
                            {
                                if (white.ContainsKey(list[i]))//Для собратьев белых,list[i]
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
                                else if (black.ContainsKey(list[i]))//Для врагов черных
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
                            break;
                        case 5://Ферзь
                            for (int i = 0; i < list.Count; i++)
                            {
                                if (white.ContainsKey(list[i]))//Для собратьев белых,list[i]
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
                                else if (black.ContainsKey(list[i]))//Для врагов черных
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
                            break;
                        case 6://Король
                            for (int i = 0; i < list.Count; i++)
                            {
                                if (white.ContainsKey(list[i]))//Для собратьев белых,list[i]
                                {
                                    list.RemoveAt(i--);
                                }
                            }
                            break;
                    }
                    return list;
                }
            }
        }
       
        public void ChangePozition(int a,int b,int c,int d,out int sost)
        {
            Chess help = new Help();
            int _poz1 = help.ReturnPozition(a, b);
            int _poz2 = help.ReturnPozition(c,d);
            if (colornow==true)//ход черных
            {
                Chess chess = black[_poz1];
                chess.ChangeReturnPozition(c, d);
                black.Remove(_poz1);
                black.Add(_poz2, chess);
                colornow = false;
                sost = 0;
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
                colornow = true;
                sost = 1;
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
    }
}
