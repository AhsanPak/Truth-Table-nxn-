using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TT
{
    class stack
    {
        public int Count;
        public string[] Temp;

        public stack(int arrlenght)
        {
            Count = -1;
            Temp=new string[arrlenght];

        }
        public bool isEmpty()
        {
            if (Count == -1)
            {
                return true;
            }
            return false;
        }

        public void push(String p)
        {

            Count++;
            Temp[Count] = p;




        }

        public string pop()
        {
            if (Count == -1)
            {

                return "-1";
            }
            else
            {
                string abc;
                abc = Temp[Count];
                Temp[Count] = null;
                Count--;

                return abc;
            }
        }
    }
}