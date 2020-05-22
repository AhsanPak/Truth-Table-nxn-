using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TT
{
    class Program
    {
        

        static void Main(string[] args)
        {
            Console.WriteLine("XOR == ^ ; OR == | ; AND == & ; NOT == ~ ; Implication == - ; Biconditional== =");
            Console.WriteLine("Plz enter the Equation");

            string equation = Console.ReadLine();
            try
            {
                string[] variable = getvariable(equation);
                bool[] a = new bool[variable.Length];
                bool[,] combi = combination(equation);
                for (int i = 0; i < variable.Length; i++)
                {
                    Console.Write(" " + variable[i] + "\t|");
                }
                Console.Write("Result");
                Console.WriteLine("");


                for (int i = 0; i < combi.GetLength(1); i++)
                {
                    for (int j = 0; j < combi.GetLength(0); j++)
                    {
                        Console.Write(combi[j, i] + "\t|");

                    }
                    for (int l = 0; l < variable.Length; l++)
                    {
                        a[l] = combi[l, i];
                    }
                    generatedAns(equation, a);
                    Console.WriteLine();

                }
            }
            catch
            {
                Console.WriteLine("Error");
            }

           
            
           

        }
        /*Generate answer*/
        public static void generatedAns(string infixEquation,bool[] values)
        {
            string InfixEqu = infixEquation;
            bool[] val =values;

            string[] ab = PFEinTrueandFalse(InfixEqu, val);

            stack st = new stack(ab.Length);
            stack temp = new stack(ab.Length);
            for (int i = ab.Length - 1; i >= 0; i--)
            {
                st.push(ab[i]);
            }

            bool tempAns;
            while (!st.isEmpty())
            {
                string abc = st.pop();
                while (abc == "True" || abc == "False")
                {

                    temp.push(abc);
                    abc = st.pop();
                }
                bool a, b;
               
                if(abc=="~")
                {
                    if (temp.pop() == "True")
                    {
                        a = true;
                    }
                    else
                    {
                        a = false;
                    }
                    tempAns=Not(a);
                }
                else
                {
                    if (temp.pop() == "True")
                    {
                        a = true;
                    }
                    else
                    {
                        a = false;
                    }

                    if (temp.pop() == "True")
                    {
                        b = true;
                    }
                    else
                    {
                        b = false;
                    }
                tempAns = SolveTwoVariable(abc, a, b);
                
                }
                temp.push(tempAns.ToString());
            }

            Console.Write(temp.pop());
        }

        /*get equation in tru and fal value and take postfixequation in true and false value*/
        public static string[] PFEinTrueandFalse(string equationinfix,bool[] truthValue)
        {
            string postfixEqu1 = infixTopostfix(equationinfix);
            stack st = new stack(postfixEqu1.Length);
            stack tempst = new stack(postfixEqu1.Length);
            string[] postfixEqu = new string[postfixEqu1.Length];

            string[] getvari = getvariable(equationinfix);

            for (int i = 0; i < postfixEqu.Length; i++)
            {
                postfixEqu[i] = postfixEqu1[i].ToString();
            }


            bool[] value;
            value = truthValue;


            for (int i = postfixEqu.Length - 1; i >= 0; i--)
            {

                for (int j = 0; j < getvari.Length; j++)
                {
                    if (postfixEqu[i] == getvari[j])
                    {
                        st.push(value[j].ToString());
                    }

                }
                if (!check(postfixEqu[i]))
                {
                    st.push(postfixEqu[i]);
                }

            }


            string[] result = new string[postfixEqu.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = st.pop();
            }

            return result;
        }
        /*Infix to postfix*/
        public static string infixTopostfix(string infixequation)
        {
            string abc = "";
            string equation1 = infixequation;
            string[] equation = new string[equation1.Length];
            string expression = "";
            stack st = new stack(equation.Length);
            for (int i = 0; i < equation1.Length; i++)
            {
                equation[i] = equation1[i].ToString();
            }

            for (int i = 0; i < equation.Length; i++)
            {

                if (check(equation[i]))
                {

                    expression = expression + equation[i];
                }
                else
                {
                    if (st.isEmpty() || equation[i] == "(")
                    {
                        st.push(equation[i]);
                    }
                    else
                    {
                        abc = st.pop();
                        if (precedence(abc) > precedence(equation[i]) || equation[i] == ")")
                        {
                            while (precedence(abc) > precedence(equation[i]) && abc != "(")
                            {

                                if (abc != "(")
                                {

                                    expression = expression + abc;

                                }
                                abc = st.pop();
                            }
                            if (equation[i] != ")")
                                st.push(equation[i]);

                        }
                        else
                        {

                            st.push(abc);
                            st.push(equation[i]);

                        }




                    }
                }
                
            }

            while (st.isEmpty() != true)
            {
                expression = expression + st.pop();
            }
            return expression;
                
        }
        /*check variable*/
        public static bool check(string item)
        {
            string a = "A";
            char b = Convert.ToChar(a);

            for (int i = 65; i <= 122; i++)
            {
                if (i != 91 && i != 92 && i != 93 && i != 94 && i != 95 && i != 96)
                {


                    if (b.ToString() == item)
                    {
                        return true;
                        
                    }
                }
                b++;
            }
            
            
                return false;
        }
        /*precedence*/
        public static int precedence(string _operator)
        { 
        string[] ope={"^","=","-","|","&","~"};
            int result=0;
        for (int i = 0; i < ope.Length; i++)
        {
            
            if(ope[i]==_operator)
            {
                result = i;
                break;
            }
            
        }
            return result+1;
            //(p-q)&~q
            //(~p&(p|q))-q
            //((p-q)&(q-r))-(p-q)

        }
        /*Get variable*/
        public static string[] getvariable(string exp)
        {
            string a = "A";
            string expression = exp;
            string var1 = "";
            string[] var2;
            string[] result;

            for (int j = 0; j < expression.Length; j++)
            {
                char b = Convert.ToChar(a);

                for (int i = 65; i <= 122; i++)
                {
                    if (i != 91 && i != 92 && i != 93 && i != 94 && i != 95 && i != 96)
                    {
                        if (expression[j] == b)
                        {
                            var1 = var1 + expression[j];

                        }


                    }
                    b++;
                }





            }
            var2 = new string[var1.Length];
            for (int i = 0; i < var2.Length; i++)
            {
                var2[i] = Convert.ToString(var1[i]);
            }
            for (int i = 0; i < var2.Length; i++)
            {
                for (int j = 0; j < var2.Length - 1 - i; j++)
                {
                    int temp2 = var2[j].CompareTo(var2[j + 1]);
                    string temp3;
                    if (temp2 > 0)
                    {
                        temp3 = var2[j];
                        var2[j] = var2[j + 1];
                        var2[j + 1] = temp3;
                    }

                }
            }
            for (int i = 0; i < var2.Length; i++)
            {
                for (int j = 0; j < var2.Length; j++)
                {
                    if (var2[i] == var2[j] && i != j)
                    {

                        var2[j] = null;
                    }
                }
            }
            for (int j = 0; j < var2.Length; j++)
            {
                for (int i = 0; i < var2.Length - 1; i++)
                {
                    if (var2[i] == null)
                    {
                        var2[i] = var2[i + 1];
                        var2[i + 1] = null;
                    }
                }
            }
            int resultlenght  = 0;
            
            for (int i = 0; i < var2.Length; i++)
            {
                if (var2[i] != null)
                    resultlenght++;
            }
            result = new string[resultlenght];
            for (int i = 0; i < resultlenght; i++)
            {
                result[i]=var2[i];
            }
            return result;


        }
        /*Operant check*/
        public static bool OpeCheck(string _operator)
        {
            string[] ope = { "=","-", "|", "^", "&", "~" };
            bool result = false;
            for (int i = 0; i < ope.Length; i++)
            {

                if (ope[i] == _operator)
                {
                    result = true;
                    break;
                }

            }
            return result;


        }
        /*solve two variable*/
        public static bool SolveTwoVariable(string _Ope,bool A,bool B)
        {
            bool temp=false;
        if(_Ope=="&")
        {
            temp = B & A;
        }
        else if (_Ope == "|")
        {
            temp = B | A;
        }
        else if (_Ope == "^")
        {
            temp = B ^ A;
        }
        else if (_Ope == "-")
        {
            temp = !B | A;
        }
        else if (_Ope == "=")
        {
            temp = !(B ^ A);
        }
        return temp;
        }
        /*Commination of table*/
        public static bool[,] combination(string equ)
        {
            string[] TableLsenght = getvariable(equ);
            double len = Math.Pow(2, TableLsenght.Length);
            int abc = Convert.ToInt32(len) / 2;
            bool[,] table = new bool[TableLsenght.Length, Convert.ToInt32(len)];


            for (int i = 0; i < TableLsenght.Length; i++)
            {
                int count = 0;
                for (int j = 0; j < Convert.ToInt32(len); j++)
                {
                    if (abc > count)
                        table[i, j] = true;
                    else
                        table[i, j] = false;
                    count++;
                    if (count == abc * 2)
                    {
                        count = 0;
                    }
                }

                abc = abc / 2;
            }
            return table;
        }
        /*Only for NOT gate*/
        public static bool Not(bool a)
        { 
        if(a==false)
        {
            return true;
        }else
        {
            return false;
        }
        }






       
        


        
    }
}
