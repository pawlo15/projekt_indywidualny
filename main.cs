using System;

namespace Robot_csharp
{
    class Program
    {
        static void Main()
        {
            string input_stream_cmd, command;
            int end_possition;
            int what_number = 0;
            bool is_number, is_command;

            Console.WriteLine("Programistyczny Projekt Indywidualny\n");
            Robot robocik = Wprowadzanie();            
            Console.Clear();
            robocik.Rysuj();
            while(true)            
            {
                Console.Write(">> ");
                input_stream_cmd = Console.ReadLine()+";";      // wprowadzenie znaków oraz usunięcie odstępów
                input_stream_cmd = Usuwanie(input_stream_cmd.ToUpper(),' ');
                is_command = false;
                robocik.czy_komunikat = false;
                do
                {
                    end_possition = input_stream_cmd.IndexOf(';');
                    if (end_possition > -1)
                    {
                        command = input_stream_cmd.Remove(end_possition);     // wycięcie polecenia do ';'
                        is_number = Wystepuje_liczba(command);                       
                        if (is_number)                                          // sprawdzenie czy w poleceniu występuje liczba
                        {
                            what_number = Wytnij_liczbe(command);             // wycięcie liczby z polecenia
                            command = Wytnij_Polecenie(command);
                        }
                        switch (command) // wykonanie polecenia
                        {
                            case "UP":
                                is_command = true;
                                if (is_number)                                    
                                    robocik.Ruch_w_Gore(what_number);
                                else                                   
                                    robocik.Ruch_w_Gore(1);                                                                      
                                break;

                            case "DOWN":
                                is_command = true;
                                if (is_number)
                                    robocik.Ruch_w_Dol(what_number);
                                else
                                    robocik.Ruch_w_Dol(1);
                                break;

                            case "LEFT":
                                is_command = true;
                                if (is_number)
                                    robocik.Ruch_w_Lewo(what_number);
                                else
                                    robocik.Ruch_w_Lewo(1);
                                break;

                            case "RIGHT":
                                is_command = true;
                                if (is_number)
                                    robocik.Ruch_w_Prawo(what_number);
                                else
                                    robocik.Ruch_w_Prawo(1);
                                break;

                            case "RESET":
                                is_command = true;
                                robocik.Reset();
                                break;

                            case "SKIP":
                                is_command = true;
                                break;

                            case "HELP":
                                Help();
                                break;

                            case "EXIT":
                                goto koniec;

                            default:
                                Console.WriteLine("BAD_COMMAND - USE 'HELP' TO SHOW MORE COMMAND");
                                break;
                        }                       
                    }
                    input_stream_cmd = input_stream_cmd.Substring(end_possition + 1);
                } while (end_possition != -1);
                if (is_command)
                {
                    Console.Clear();
                    robocik.Rysuj();
                }               
            }
                koniec:
            Console.WriteLine("Wyłączono program");
            Console.ReadKey();
        }
        public static string Usuwanie(string input_stream_cmd_to_change, char user_char)
        {
            string without_space = "";
            for (int i = 0; i < input_stream_cmd_to_change.Length; i++)
            {
                if (input_stream_cmd_to_change[i] != user_char)
                    without_space += input_stream_cmd_to_change[i];
            }
            return without_space;
        }
        public static bool Wystepuje_liczba(string user_polecenie)
        {
            for (int i = 0; i < user_polecenie.Length; i++)
            {
                if (CzyCyfra(user_polecenie[i]))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool CzyCyfra(char user_c)
        {
            if (('0'<=user_c)&&(user_c<='9'))
            {
                return true;
            }
            return false;
        }
        public static int Wytnij_liczbe(string user_polecenie)
        {
            int number_to_send = 0;
            string nowy = "";
            for (int i = 0; i < user_polecenie.Length; i++)
            {
                if (CzyCyfra(user_polecenie[i]))
                {
                    nowy = user_polecenie.Substring(i);
                    break;
                }
            }
            for (int i = 0; i < nowy.Length; i++)
            {
                if (!CzyCyfra(nowy[i]))
                {
                    nowy = nowy.Remove(i);
                    break;
                }
            }        
            for (int i = 0; i < nowy.Length; i++)            
                number_to_send += (int)(Ascii_zmiana(nowy[i]) * Math.Pow(10, nowy.Length - i - 1));
            return number_to_send;
        }
        public static int Ascii_zmiana(int code)
        {
            int number = 0;
            int[] numbers1 = { 48, 49, 50, 51, 52, 53, 54, 55, 56, 57 };
            int[] numbers2 = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            for (int i = 0; i < numbers1.Length; i++)
            {
                if (code == numbers1[i])
                {
                    number = numbers2[i];
                    break;
                }
            }
            return number;
        }
        public static string Wytnij_Polecenie(string user_polecenie)
        {
            string polecenie_zwrot = "";
            for (int i = 0; i < user_polecenie.Length; i++)
            {
                if (CzyCyfra(user_polecenie[i]))
                {
                    polecenie_zwrot += user_polecenie.Remove(i);
                    break;
                }
            }
            return polecenie_zwrot;
        }
        public static void Help()
        {
            Console.WriteLine("----------------------------");
            Console.WriteLine(" Dostępne komendy:");
            Console.WriteLine("----------------------------");
            Console.WriteLine(" up\t\tup n\n down\t\tdown n\n left\t\tleft n\n right\t\tright n\n reset\t\tskip\n exit");
            Console.WriteLine("----------------------------");
            Console.WriteLine(" n - dowolna liczba mieszcząca się w tablicy");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\tUWAGA!\tUWAGA!\tUWAGA!");            
            Console.WriteLine(" Jeżeli chcesz wpisać kilka komend w jednym wierszu, oddziel je ';'");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static Robot Wprowadzanie() 
        {
            int rozmiar_x, rozmiar_y, start_x, start_y;           
            string answer, input_string;
            bool check;

            rozmiar_x = rozmiar_y = start_x = start_y = 0;
            // sprawdzenie poprawności danych
            // sprawdzenie czy pozycja jest w planszy
            Console.WriteLine("Menu tworzenia nowej planszy i wyboru pozycji robota.");
            do
            {
                Console.WriteLine("Czy chcesz, aby stworzyć standardową planszę [10;10]? Y/N");
                answer = Console.ReadLine().ToUpper();
            } while (answer != "Y" && answer != "N");
            if (answer=="Y")
            {
                rozmiar_x = 10;
                rozmiar_y = 10;
            }
            else
            {
                do
                {
                    Console.WriteLine("Obliczana jest wartość |Absolute| liczby. Rozmiar planszy musi być z przedziału [1;50]");
                    Console.WriteLine("Podaj szerokość planszy: ");
                    input_string = Console.ReadLine();
                    check = Wystepuje_liczba(input_string);
                    if (check)
                    {
                        rozmiar_x = Wytnij_liczbe(input_string);
                    }
                } while ((check != true) || (check == true && rozmiar_x > 50));
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Szerokość planszy wynosi " + rozmiar_x);
                Console.ForegroundColor = ConsoleColor.White;
                do
                {
                    Console.WriteLine("Obliczana jest wartość |Absolute| liczby. Rozmiar planszy musi być z przedziału [1;50]");
                    Console.WriteLine("Podaj wysokość planszy: ");
                    input_string = Console.ReadLine();
                    check = Wystepuje_liczba(input_string);
                    if (check)
                    {
                        rozmiar_y = Wytnij_liczbe(input_string);
                    }
                } while ((check != true) || (check == true && rozmiar_y > 50));
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Wysokość planszy wynosi " + rozmiar_y);
                Console.ForegroundColor = ConsoleColor.White;
            }
            do
            {
                Console.WriteLine("Czy chcesz ustawić standardową pozycję początkową robota [0;0] ? Y/N");
                answer = Console.ReadLine().ToUpper();
            } while (answer != "Y" && answer != "N");
            if (answer == "Y")
            {
                start_x = 0;
                start_y = 0;
            }
            else 
            {
                do
                {
                    Console.WriteLine("Obliczana jest wartość |Absolute| liczby. Współrzędne muszą być z zakresu [0;"+(rozmiar_x-1)+"]");
                    Console.WriteLine("Podaj wpółrzędną x: ");
                    input_string = Console.ReadLine();
                    check = Wystepuje_liczba(input_string);
                    if (check)
                    {
                        start_x = Wytnij_liczbe(input_string);
                    }
                } while ((check != true) || (check == true && start_x > (rozmiar_x-1)));
                do
                {
                    Console.WriteLine("Obliczana jest wartość |Absolute| liczby. Współrzędne muszą być z zakresu [0;" + (rozmiar_y - 1) + "]");
                    Console.WriteLine("Podaj wpółrzędną y: ");
                    input_string = Console.ReadLine();
                    check = Wystepuje_liczba(input_string);
                    if (check)
                    {
                        start_y = Wytnij_liczbe(input_string);
                    }
                } while ((check != true) || (check == true && start_y > (rozmiar_y - 1)));
            }
            Robot nowy_obiekt = new Robot(rozmiar_x, rozmiar_y, start_x, start_y);
            return nowy_obiekt;
        }
    }
}
