using System;
using System.Collections.Generic;
using UnityEngine;

namespace LD
{ 
    public partial struct BigDouble : IFormattable, IComparable, IComparable<BigDouble>, IEquatable<BigDouble>
    {
    
        /// <summary>
        /// 스트링을 계속 생성하지 않고 재사용한다.
        /// </summary> 
        private static Dictionary<int, string> CachedAlphabet = new Dictionary<int, string>(); 
        public enum eFormat
        {
            Number,
            NumberWithExponent, 
            NumberWithAlphabet 
        }
        
        /// <summary>
        /// 알파벳을 포함하는가?
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static bool HasAlphabet(string input)
        {
            return IsAlphabet(input[^1]);
            return false;
        }
        
        static bool IsAlphabet(char input)
        {
            if (input>= 'A' && input <= 'Z')
                return true; 
            return false;
        }
        public static (double numericPart, string alphaPart) SplitAlphabetValue(string input)
        { 
            int MAX_ALPHABET = 8;
            Span<char> buffer = stackalloc char[MAX_ALPHABET]; 
            
            int charToValue = 0;
            int charToValueCount = 0;
            double numericPart = 0; 
            bool isNumeric = true;
            bool isDecimal = false;
            bool isNegative = false;
            double decimalFactor = 1.0; 
            
            for (var index = 0; index < input.Length; index++)
            {
                var c = input[index];
                if (isNumeric)
                {
                    if (c >= '0' && c <= '9')
                    {
                        numericPart = numericPart * 10 + (c - '0');
                        if (isDecimal) decimalFactor *= 10;
                    }
                    else if (c == '.')
                    {
                        isDecimal = true;
                    }
                    else if (c == '-' && numericPart == 0)
                    {
                        isNegative = true;
                    }
                    else
                    {
                        isNumeric = false; 
                        buffer[charToValueCount++] = c; 
                        charToValue += c;
                    }
                }
                else
                {
                    buffer[charToValueCount++] = c; 
                    charToValue += c;
                }
            } 
            if (isNegative)
            {
                numericPart = -numericPart;
            }

            if (isDecimal)
            {
                numericPart /= decimalFactor;
            }

            
            if (!CachedAlphabet.ContainsKey(charToValue))
            {
                CachedAlphabet[charToValue] = new string(buffer.Slice(0, charToValueCount)); 
            }
 
            return (numericPart, CachedAlphabet[charToValue] );
        }
        

 

 
        public BigDouble(string value, eFormat format)
        {
            switch (format)
            {
                case eFormat.Number:
                    this = BigDouble.Parse(value);
                    break;
                case eFormat.NumberWithAlphabet:
                    var (mantissa, exponent) = SplitAlphabetValue(value);
                  
                    var exponentIndex = GetExponentFromUnitName(exponent); 
                    if (mantissa >= 10)
                    {
                        mantissa *= .1d;
                        exponentIndex++;
                    }
                    if (mantissa >= 1000d) 
                        throw new Exception("생성자로 알파벳 넘버를 받을 시 가수부는 1000을 초과할 수 없습니다.");
                    else 
                        this = new BigDouble(mantissa, exponentIndex); 
                    break;
                case eFormat.NumberWithExponent:
                    var split = FastDouble.GetBigValueInfo(value);
                    this.exponent = split.Exponent;
                    this.mantissa = split.Mantissa;
                    break;
                default:
                    this.exponent = 0;
                    this.mantissa = 0;
                    break;
            } 
        }
        public BigDouble(string value)
        { 
            if (value.IndexOf('e') != -1)
            {
                this = new BigDouble(value, eFormat.NumberWithExponent);
            }
            else if (HasAlphabet(value))
            {
                this = new BigDouble(value, eFormat.NumberWithAlphabet);
            } 
            else
            {
                this = BigDouble.Parse(value);
            }
        }
         
    }
}