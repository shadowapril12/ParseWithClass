using System;

namespace Add_Class_To_Parse
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                //Создание экземпляра класса Parser
                Parser obj = new ParserExt();
                obj.GetString();
                obj.ShowResult();
            }
        }
    }
}
