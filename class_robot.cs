using System;

namespace Robot_csharp
{
    class Robot : Plansza
    {
        protected int x_pos;
        protected int y_pos;

        private bool first00;
        public bool czy_komunikat;

        private int x_first;
        private int y_first;
        public Robot(int x, int y, int pozycja_x, int pozycja_y)
        : base(x, y)
        {
            x_pos = x_first = pozycja_x;
            y_pos = y_first = pozycja_y;
            if ((pozycja_x == 0) && (pozycja_y == 0))
                first00 = true;
            else
                first00 = false;
        }
        public override void Rysuj()
        {
            base.Rysuj();
            for (int i = y_max - 1; i >= 0; i--)
            {
                if (i<10)
                {
                    Console.Write("0"+i+"   ");                   
                }
                else
                    Console.Write(i + "   ");
                for (int j = 0; j < x_max; j++)
                {
                    if (x_pos == j && y_pos == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("TU  ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        if (punkty[j, i] == true)
                        {                            
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("X   ");
                                Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                                Console.Write("O   ");
                        }
                            
                    }
                }
                Console.WriteLine();
            }
            Console.Write("\nXY   ");
            for (int i = 0; i < x_max; i++)
            {
                if (i<10)
                {
                    Console.Write(i + "   ");
                }
                else
                    Console.Write(i + "  ");
            }
            Console.WriteLine("");
            if (czy_komunikat == true)
            {
                Komunikat();
            }
        }
        
        public void Ruch_w_Lewo(int vector)
        {
            if (Obszar(-vector, 0))
            {
                for (int i = vector; i > 0; i--)
                {
                    Zapisz_pozycje();
                    x_pos--;
                }
            }
            else
                czy_komunikat = true;
        }
        public void Ruch_w_Prawo(int vector)
        {
            if (Obszar(vector, 0))
            {
                for (int i = vector; i > 0; i--)
                {
                    Zapisz_pozycje();
                    x_pos++;
                }
            }
            else
                czy_komunikat = true;
        }
        public void Ruch_w_Gore(int vector)
        {
            if (Obszar(0, vector))
            {
                for (int i = vector; i > 0; i--)
                {
                    Zapisz_pozycje();
                    y_pos++;
                }
            }
            else
                czy_komunikat = true;
        }
        public void Ruch_w_Dol(int vector)
        {
            if (Obszar(0, -vector))
            {
                for (int i = vector; i > 0; i--)
                {
                    Zapisz_pozycje();
                    y_pos--;
                }
            }
            else
                czy_komunikat = true;
        }
        public void Reset()
        {
            string quest;
            if (first00 == true)
                { x_pos = 0; y_pos = 0; }
            else
            {               
                do
                {
                    Console.WriteLine("Czy chcesz zacząć od punktu [0;0] ? Y/N");
                    quest = Console.ReadLine().ToUpper();
                } while (quest != "Y" && quest != "N");
                if (quest == "Y")
                    { x_pos = 0; y_pos = 0; }
                else
                    { x_pos = x_first; y_pos = y_first; }
            }
            Console.WriteLine("Ustawiono punkt początkowy [" + x_pos + ";" + y_pos + "]");
            for (int i = 0; i < x_max; i++)
            {
                for (int j = 0; j < y_max; j++)
                {
                    punkty[i, j] = false;
                }
            }
        }
        private void Zapisz_pozycje()
        {
            punkty[x_pos, y_pos] = true;
        }
        private bool Obszar(int do_x, int do_y)
        {
            if ((x_max > (x_pos + do_x) && (x_pos + do_x) >= 0) && (y_max > (y_pos + do_y) && (y_pos + do_y) >= 0))
            {
                return true;
            }
            else
                return false;
        }
        private void Komunikat()
        {
            Console.WriteLine("Próba wykonania ruchu po za planszę.");
        }
    }
}
