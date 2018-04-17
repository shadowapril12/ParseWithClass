using System;
using System.Collections.Generic;
using System.Text;

namespace Add_Class_To_Parse
{
    class ParserExt : Parser
    {
        //begin - время начала вычисления выражения
        private DateTime begin;
        //end - время окончания вычисления выражения
        private DateTime end;
        //duration - продолжительночть вычисления выражения
        public TimeSpan duration;
        int operCount;

        /// <summary>
        /// Данный метод переопределен для установки начального времени вычисления выражения
        /// </summary>
        public override void GetString()
        {
            S = Console.ReadLine();
            begin = DateTime.Now;
        }
        /// <summary>
        /// Данный метод переопределен для установки конечного времения вычисления выражения,
        /// а также расчета продолжительности выполнения программы
        /// </summary>
        public override void ShowResult()
        {
            end = DateTime.Now;
            duration = end - begin;

            Console.WriteLine("Результат выражения {0}", CalcExpression(S) + "\n");
            
            Console.WriteLine("Продолжительность выполнения программы: {0}," +
                " количество операций {1}\n", duration.TotalMilliseconds + "\n", operCount);
        }

        protected override int Num()
        {

            string buff = "0";

            for (; index < res.Length && char.IsDigit(res[index]); index++)
            {
                buff += res[index];
            }
            ///Проверка на количество операций в выражении, если символ строки не числовой,
            ///то это математический оператор
            if(index < res.Length)
            {
                if (!char.IsDigit(res[index]))
                {
                    operCount++;
                }
            }
            return Convert.ToInt32(buff);
        }
        /// <summary>
        /// Переопределен метод MulDivFac, для отслеживания знака факториала в выражении и увеличения счетчика операций
        /// </summary>
        /// <returns>Возвращает результат результат умножения, деления или вычисления факториала</returns>
        protected override int MulDivFac()
        {
            //Парсированная строка, до первого знака математических операций
            int num = Num();
            //Проверка наличия знака восклицания, в конце числа
            if (index < res.Length && res[index] == '!')
            {
                ///Увеличиваем счетчик количества операций
                operCount++;
                num = FindFac();
            }

            while (index < res.Length)
            {
                if (res[index] == '*')
                {
                    index++;
                    //Num - метод для считывания числа из строки
                    int b = Num();

                    if (index < res.Length && res[index] == '!')
                    {
                        operCount++;
                        num = FindFac();
                    }

                    num *= b;
                }
                else if (res[index] == '/')
                {
                    index++;
                    int b = Num();

                    if (index < res.Length && res[index] == '!')
                    {
                        operCount++;
                        num = FindFac();
                    }

                    num /= b;
                }
                else
                {
                    return num;
                }
            }

            return num;
        }
    }
}
