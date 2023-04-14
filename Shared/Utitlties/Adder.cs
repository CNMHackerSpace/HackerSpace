using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utitlties
{
    public class Adder
    {
        private float num1;

        public float Num1
        {
            get { return num1; }
            set { num1 = value; Calc(); }
        }

        private float num2;

        public float Num2
        {
            get { return num2; }
            set { num2 = value; Calc(); }
        }

        public float Sum { get; private set; }
        private void Calc()
        {
            Sum = num1 + num2;
        }
    }
}
