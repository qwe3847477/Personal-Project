using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace 하디_바인베르크_시뮬레이션
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int Male_AA_num = 0;
            int Male_Aa_num = 0;
            int Male_aa_num = 0;
            int Female_AA_num = 0;
            int Female_Aa_num = 0;
            int Female_aa_num = 0;
            int Death_Rate = 50;       //%단위
            int Des_Count = 2;      //낳는 자식의 갯수

            List<string> Male_List = new List<string>();  
            List<string> Female_List = new List<string>();

            Console.WriteLine("Male 개체값을 입력하시오. (AA Aa aa)");
            Console.WriteLine("ex예시) 25 5 10\nㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ");
            Input(ref Male_AA_num, ref Male_Aa_num, ref Male_aa_num);
            Console.Clear();

            Console.WriteLine("Female 개체값을 입력하시오. (AA Aa aa)");
            Console.WriteLine("ex예시) 25 5 10\nㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ");
            Input(ref Female_AA_num, ref Female_Aa_num, ref Female_aa_num);
            Console.Clear();

            //사망률 입력 나중에 구현하자!!!

            First_Set(Male_AA_num, Male_Aa_num, Male_aa_num,
                        Female_AA_num, Female_Aa_num, Female_aa_num,
                       ref Male_List, ref Female_List);

            Console.WriteLine("\nWaiting...");
            //Thread.Sleep(1000); //1초 대기

            int index = 0;

            while (true)
            {
                float AA = 0;
                float Aa = 0;
                float aa = 0;

                index++;
                Mating(ref Male_List, ref Female_List, Des_Count);
                Death(ref Male_List, ref Female_List, Death_Rate);  
                List<string> Output = new List<string>();
                Output.AddRange(Male_List);
                Output.AddRange(Female_List);
                foreach (string Gene in Output)
                {
                    if (Gene == "AA")
                        AA++;
                    else if (Gene == "Aa")
                        Aa++;
                    else
                        aa++;
                }

                Console.WriteLine();
                Console.WriteLine("AA Aa aa");
                Console.WriteLine($"{AA},{Aa},{aa}    {index}번쨰");
                Console.WriteLine("A인자의 갯수 = {0}    ({1}%)", AA * 2 + Aa, (AA * 2 + Aa) / (2 * (AA + Aa + aa)) * 100);
                Console.WriteLine("a인자의 갯수 = {0}    ({1}%)", aa * 2 + Aa, (aa * 2 + Aa) / (2 * (AA + Aa + aa)) * 100);

                Console.WriteLine("AA = {0}    ({1}%)", AA, AA / (AA + Aa + aa) * 100);
                Console.WriteLine("Aa = {0}    ({1}%)", Aa, Aa / (AA + Aa + aa) * 100);
                Console.WriteLine("aa = {0}    ({1}%)", aa, aa / (AA + Aa + aa) * 100);

                Console.Write(Male_List.Count); Console.WriteLine("    " + Female_List.Count);
                Console.WriteLine("ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ");
                Console.WriteLine();

                if (AA + Aa + aa == 0)
                    break;
            }
        }

        static void Input(ref int AA, ref int Aa, ref int aa)
        {
            string input;
            string[] token;
            input = Console.ReadLine();
            token = input.Split(' ');
            if (token.Length != 3)
            {
                Console.WriteLine("\n올바르지 않은 입력입니다.다시 입력해주세요.\nㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ");
                Input(ref AA, ref Aa, ref aa);
                return;
            }
            for (int i = 0; i < token.Length; i++)
                if (int.TryParse(token[i], out int reslut) == false)
                {
                    Console.WriteLine("\n올바르지 않은 입력입니다.다시 입력해주세요.\nㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ");
                    Input(ref AA, ref Aa, ref aa);
                    return;
                }
            AA = Int32.Parse(token[0]);
            Aa = Int32.Parse(token[1]);
            aa = Int32.Parse(token[2]);

        }

        static void First_Set(int Male_AA_num, int Male_Aa_num, int Male_aa_num,
                       int Female_AA_num, int Female_Aa_num, int Female_aa_num,
                       ref List<string> Male_List, ref List<string> Female_List)
        {
            for (int i = 0; i < Male_AA_num; i++)
                Male_List.Add("AA");
            for (int i = 0; i < Male_Aa_num; i++)
                Male_List.Add("Aa");
            for (int i = 0; i < Male_aa_num; i++)
                Male_List.Add("aa");

            for (int i = 0; i < Female_AA_num; i++)
                Female_List.Add("AA");
            for (int i = 0; i < Female_Aa_num; i++)
                Female_List.Add("Aa");
            for (int i = 0; i < Female_aa_num; i++)
                Female_List.Add("aa");

        }

        static void Mating(ref List<string> Male_LIst, ref List<string> Female_List, int Des_Count)
        {
            List<string> tmp_Male_LIst = new List<string>();
            List<string> tmp_Female_LIst = new List<string>();

            List<int> Male_Random_List = Enumerable.Range(0,Male_LIst.Count).ToList();
            List<int> Female_Random_List= Enumerable.Range(0,Female_List.Count).ToList();

            Random random = new Random();
            while (0 < Male_Random_List.Count && 0 < Female_Random_List.Count)
            {
                int Male_Index = random.Next(Male_Random_List.Count); // 꺼낼 번호를 랜덤하게 선택
                int Female_Index = random.Next(Female_Random_List.Count);

                int Male_Random_Num = Male_Random_List[Male_Index]; //숫자 추출
                int Female_Random_Num = Female_Random_List[Female_Index];

                Male_Random_List.RemoveAt(Male_Index);          // 중복되지 않도록 제거 
                Female_Random_List.RemoveAt(Female_Index);           
                

                for (int i = 0; i < Des_Count; i++)
                {
                    int Des_sex = random.Next(2); // 0이면 수컷,    1이면 암컷
                    if (Des_sex == 0)
                        tmp_Male_LIst.Add(Des_Gene(Male_LIst[Male_Random_Num], Female_List[Female_Random_Num]));
                    else if (Des_sex == 1)
                        tmp_Female_LIst.Add(Des_Gene(Male_LIst[Male_Random_Num], Female_List[Female_Random_Num]));
                }



            }
            Male_LIst.AddRange(tmp_Male_LIst);
            Female_List.AddRange(tmp_Female_LIst);

        }

        static string Des_Gene(string Male_Gene, string Female_Gene)
        {
            Random random = new Random();
            string _return = "";

            char[] c = Male_Gene.ToCharArray();
            _return += c[random.Next(2)];

            c = Female_Gene.ToCharArray();
            _return += c[random.Next(2)];

            if (_return == "aA")
                _return = "Aa";

            return _return;
        }

        static void Death(ref List<string> Male_List, ref List<string> Female_LIst, int Death_rate)
        {
            for (int i = 0; i < Male_List.Count;)
            {
                Random random = new Random();
                if (random.Next(100) < Death_rate)
                    Male_List.RemoveAt(i);
                else
                    i++;
            }
            for (int i = 0; i < Female_LIst.Count;)
            {
                Random random = new Random();
                if (random.Next(100) < Death_rate)
                    Female_LIst.RemoveAt(i);
                else
                    i++;
            }
        }
    }
}
