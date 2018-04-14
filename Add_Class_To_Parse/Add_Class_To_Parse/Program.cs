using System;

namespace Add_Class_To_Parse
{
    /// <summary>
    /// Parser - класс, содержащий методы, поля и свойства, предназначенные для
    /// парсирования строки, передаваемой через консоль в программу
    /// </summary>
    class Parser
    {   
        private string s;

        //Свойство s - служит для возврата и передачи значения приватному полю s, 
        public string S
        {
            get
            {
                return s;
            }
            set
            {
                s = value;
            }
        }
        /// <summary>
        /// CalcExpression - основной метод, обнаруживает позиции скобок, для сохранения
        /// приоритетности выполнения математических операций.
        /// </summary>
        /// <param name="s">Выражение передаваемое в виде строки</param>
        /// <returns>Возвращает результат вычисления</returns>
        public string CalcExpression(string s)
        {
            //Цикл, выполняющий до тех пор, пока в строке присутствую открывающиеся скобки
            while (s.Contains("("))
            {
                ///lvl - счетчик, увеличивающийся при обнаружении открывающейся скобки,
                ///и уменьшающийся при обнаружении закрывающейся
                int lvl = 1;

                //idx - индекс открывающейся скобки
                int idx = s.IndexOf("(");
                int i;
                for (i = idx + 1; lvl > 0 && i < s.Length; i++)
                {
                    if (s[i] == ')')
                    {
                        lvl--;
                    }
                    if (s[i] == '(')
                    {
                        lvl++;
                    }
                }

                ///localRes - строка, содержащая внутри внешних скобок, передаваемая в функцию повторно,
                ///для обнаружения внутренних скобок
                string localRes = CalcExpression(s.Substring(idx + 1, i - idx - 2));

                s = s.Substring(0, idx) + localRes +
                    (i < s.Length ? s.Substring(i, s.Length - i) : "");
            }

            return CalcPlusMinus(s) + "";
        }

        /// <summary>
        /// Приватный метод CalcFactorial - служит для нахождения факториала, передаваемого в функцию числа
        /// </summary>
        /// <param name="n">Передаваемое число</param>
        /// <returns>Возаращает значение факториала</returns>
        private int CalcFactorial(int n)
        {
            //переменная, а которую будет передаваться значение факториала
            int factorial;

            if (n == 1)
            {
                return n;
            }
            else
            {
                factorial = CalcFactorial(n - 1) * n;
            }

            return factorial;
        }

        /// <summary>
        /// Приватный метод MulDivFac - служит для выполнения операция умножения с делением, а также для
        /// нахождения факториала
        /// </summary>
        /// <param name="s">Строка передаваемая в функцию</param>
        /// <param name="i">Индекс символа</param>
        /// <returns>Возвращает результат умножения/деления либо фактрориала</returns>
        private int MulDivFac(string s, ref int i)
        {
            //Парсированная строка, до первого знака математических операций
            int num = Num(s, ref i);

            while (i < s.Length)
            {
                ///Если индекс содержит знак восклицания, то выполняется функция,
                ///возвращающая значение факториала
                if (s[i] == '!')
                {
                    int facIdx = i - 1;
                    num = CalcFactorial(int.Parse(s[facIdx] + ""));
                    i++;
                }
                else if (s[i] == '*')
                {
                    i++;
                    //Num - метод для считывания числа из строки
                    int b = Num(s, ref i);
                    num *= b;
                }
                else if (s[i] == '/')
                {
                    i++;
                    int b = Num(s, ref i);
                    num /= b;
                }
                else
                {
                    return num;
                }
            }

            return num;
        }

        /// <summary>
        /// Приватный метод CalcPlusMinus - служит для выполнения
        /// математических операция сложения и вычитания
        /// </summary>
        /// <param name="s">Передаваемая в функцию строка</param>
        /// <returns>Возвращает сумму или разность, в зависимости от знака</returns>
        private int CalcPlusMinus(string s)
        {
            //Индекс первого символа строки
            int index = 0;

            int num = MulDivFac(s, ref index);

            while (index < s.Length)
            {
                if (s[index] == '+')
                {
                    index++;
                    int b = MulDivFac(s, ref index);
                    num += b;
                }
                else if (s[index] == '-')
                {
                    index++;
                    int b = MulDivFac(s, ref index);
                    num -= b;
                }
                else
                {
                    Console.WriteLine("Error");
                    return 0;
                }
            }

            return num;
        }
        /// <summary>
        /// Метод Num - служит для считывания числа из строки, останавливается
        /// на первом не числовом символе
        /// </summary>
        /// <param name="s">Передаваемая в функцию строка</param>
        /// <param name="i">Индекс символа строки</param>
        /// <returns>Возвращает парсированную строку</returns>
        private int Num(string s, ref int i)
        {
            string buff = "0";

            for (; i < s.Length && char.IsDigit(s[i]); i++)
            {
                buff += s[i];
            }

            return Convert.ToInt32(buff);
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                //Создание экземпляра класса Parser
                Parser obj = new Parser();
                obj.S = Console.ReadLine();
                Console.WriteLine("Результат выражения {0}", obj.CalcExpression(obj.S) + "\n");
            }
        }
    }
}
