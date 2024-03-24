using System;
using System.Collections.Generic;
using System.Text;  

// I'm not sure if there's a "Yes, this is Unity" define symbol
// (#if UNITY doesn't seem to work). If you happen to know one - please create
// an issue here https://github.com/Razenpok/FastBigDouble.cs/issues. 
namespace LD
{ 
    public partial struct BigDouble : IFormattable, IComparable, IComparable<BigDouble>, IEquatable<BigDouble>
    {
        private static Dictionary<long, string> alphabetCache = new Dictionary<long, string>();
        private static StringBuilder unitSb = new StringBuilder();  
        /// <summary>
        /// 몇 단위로 자를건지
        /// </summary>
        public const int EXPONENT_UNIT = 3; 
        
        public static (long rangeA, long rangeB) GetExponentFromAlphabetUnit(string unit)
        {
            if (string.IsNullOrEmpty(unit))
            {
                throw new ArgumentException("알파벳을 올바르게 입력했나요.. 값이 비어있습니다.");
            }

            long exponent = 0;
            int length = unit.Length;
            for (int i = 0; i < length; i++)
            {
                int letterValue = unit[i] - 'A' + 1;
                exponent += letterValue * (int)Math.Pow(26, length - i - 1);
            }

            return (exponent * 3, exponent * 3 + 2);
        }
    
    
        public static string GetAlphabetUnit(long exponent)
        {
            if (alphabetCache.ContainsKey(exponent) == false)
            {
                Span<char> unit = stackalloc char[8];
                int index = unit.Length - 1;
                if (exponent < 3)
                {
                    return null;
                }

                long adjustedExponent = (exponent - 3) / 3;
                do
                {
                    long remainder = adjustedExponent % 26;
                    char letter = (char)('A' + remainder);
                    unit[index--] = letter;
                    adjustedExponent = (adjustedExponent / 26) - 1;
                } while (adjustedExponent >= 0);

                var str = new string(unit.Slice(index + 1, unit.Length - index - 1));
                alphabetCache.Add(exponent, str);
            }

            return alphabetCache[exponent];
        }
         
        /// <summary>
        /// 컬럼내임을 카운트로 바꾼다.
        /// 이값에 EXPONENT UNIT 상수를 곱하면 대응되는 지수가 나온다.
        /// </summary> 
        public static long GetNumberFromUnitName(string unit)
        {
            var range = GetExponentFromAlphabetUnit(unit);
            return range.rangeB / 3;  
        } 

         
        /// <summary>
        /// 컬럼내임을 카운트로 바꾼다.
        /// 이값에 EXPONENT UNIT 상수를 곱하면 대응되는 지수가 나온다.
        /// </summary> 
        public static long GetExponentFromUnitName(string unit)
        { 
            return GetNumberFromUnitName(unit) * EXPONENT_UNIT;
        } 

 
        static double MantissaNormalize(double mantissa, long exponentRemain)
        {
            double mul = Math.Pow(10, exponentRemain);
            return mantissa * mul; 
        } 
         
        
         
        /// <summary>
        /// 값을 넣으면 알파벳이 붙은 수치로 변환해줌
        /// </summary> 
        public string GetUnit()
        {
            unitSb.Clear();
            long remain = exponent % 3; 
            var normalized = MantissaNormalize(mantissa, remain);  
            // 최적화 된 ToString코드. 내부 주석 확인
            var mantissaToString = normalized.OptimizeToString(1); 
            unitSb.Append(mantissaToString);
            unitSb.Append(GetAlphabetUnit(exponent));
            return unitSb.ToString(); 
        }  

        public string ToString(string format, IFormatProvider formatProvider)
        {`
        }
    }
}