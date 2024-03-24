using System; 

namespace LD
{ 
    public static class FastDouble
    {
        public static FastBigValueInfo GetBigValueInfo(string input)
        { 
            double mantissa = 0;
            long exponent = 0;
            double decimalFactor = 1;
            bool isExponent = false;
            bool isExponentNegative = false;
            bool isMantissaNegative = false;
            bool isDecimal = false;

            for (var index = 0; index < input.Length; index++)
            {
                var c = input[index];
                if (c == 'e' || c == 'E')
                {
                    isExponent = true;
                    continue;
                }

                if (!isExponent)
                {
                    if (c == '.')
                    {
                        isDecimal = true;
                        continue;
                    }

                    if (c == '-')
                    {
                        isMantissaNegative = true;
                        continue;
                    }

                    if (c == '+')
                    {
                        continue; // Ignore the '+' sign
                    }

                    int digit = c - '0';
                    if (isDecimal)
                    {
                        decimalFactor *= 0.1;
                        mantissa += digit * decimalFactor;
                    }
                    else
                    {
                        mantissa = mantissa * 10 + digit;
                    }
                }
                else
                {
                    if (c == '-')
                    {
                        isExponentNegative = true;
                        continue;
                    }

                    if (c == '+')
                    {
                        continue; // Ignore the '+' sign
                    }

                    exponent = exponent * 10 + (c - '0');
                }
            }

            if (isMantissaNegative)
            {
                mantissa = -mantissa;
            }

            if (isExponentNegative)
            {
                exponent = -exponent;
            }

            return new FastBigValueInfo()
            {
                Exponent = exponent,
                Mantissa = mantissa
            };
        }
        public static double ParseDouble(string s, int maxDecimalPlaces = 4)
        {
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            } 
            if (maxDecimalPlaces < 0) 
                maxDecimalPlaces = 0;
            bool isNegative = s[0] == '-';
            int startIndex = isNegative || s[0] == '+' ? 1 : 0;

            double mantissa = 0.0;
            int exponent = 0;
            bool negativeExponent = false;
            bool hasExponent = false;
            bool hasDecimal = false;
            int decimalPlaces = 0;

            for (int i = startIndex; i < s.Length; i++)
            {
                char c = s[i];
                if (c >= '0' && c <= '9')
                {
                    if (hasExponent)
                    {
                        exponent = exponent * 10 + (c - '0');
                    }
                    else if (decimalPlaces < maxDecimalPlaces || !hasDecimal)
                    {
                        mantissa = mantissa * 10.0 + (c - '0');
                        if (hasDecimal) decimalPlaces++;
                    }
                    else if (decimalPlaces == maxDecimalPlaces)
                    {
                        //소수점 이후에 지수부가 나올 예정이기 때문에 break해도 됨~~
                        break;
                    }
                }
                else if (c == '.' && !hasDecimal && !hasExponent)
                {
                    hasDecimal = true;
                }
                else if ((c == 'e' || c == 'E') && !hasExponent)
                {
                    hasExponent = true;
                    if (i + 1 < s.Length && (s[i + 1] == '-' || s[i + 1] == '+'))
                    {
                        if(s[i+1] == '-') 
                            negativeExponent = true; 
                        exponent = s[i + 1] == '-' ? -exponent : exponent; 
                        i++;
                    }
                }
                else
                {
                    throw new FormatException("입력값이 잘못 됨 => " + s);
                }
            }

            if (hasDecimal)
            {
                mantissa /= Math.Pow(10.0, decimalPlaces);
            }

            if (hasExponent)
            {
                mantissa *= Math.Pow(10.0, negativeExponent ? -exponent : exponent);
            } 
            return isNegative ? -mantissa : mantissa;
        }
        
        /// <summary>
        /// 이 함수는 -999~999의 값일때만 사용한다.
        /// </summary>
        /// <param name="numberStr"></param>
        /// <param name="decimalPlaces"></param>
        /// <returns></returns>
        public static double FastParse(string numberStr, int decimalPlaces = 4)
        {
            
            if (string.IsNullOrEmpty(numberStr))
            {
                return 0;
            } 
            if (decimalPlaces < 0) 
                decimalPlaces = 0;

            double result = 0;
            bool isNegative = numberStr[0] == '-';
            int startIndex = isNegative ? 1 : 0; // 음수 기호가 있으면 시작 인덱스 조정
            int decimalIndex = numberStr.IndexOf('.'); // 소수점 위치 찾기
            int endIndex = decimalIndex == -1 ? numberStr.Length : decimalIndex; // 소수점이 없으면 끝까지, 있으면 소수점 직전까지

            // 정수 부분 파싱
            for (int i = startIndex; i < endIndex; i++)
            {
                result = result * 10 + (numberStr[i] - '0');
            }

            // 소수점 아래 부분 파싱
            if (decimalIndex != -1 && decimalPlaces > 0)
            {
                double fraction = 0;
                double divisor = 1;
                int fractionLength = Math.Min(decimalPlaces, numberStr.Length - decimalIndex - 1); // 소수점 아래 길이 계산
                for (int i = 0; i < fractionLength; i++)
                {
                    fraction = fraction * 10 + (numberStr[decimalIndex + 1 + i] - '0');
                    divisor *= 10;
                }
                result += fraction / divisor; // 소수 부분을 더함
            }

            return isNegative ? -result : result; // 음수면 결과에 -1을 곱함
        }
        /// <summary>
        /// -999.9~999.9 범위의 ToString을 지원하는 함수
        /// 기존 ToString은 너무 느려서 이렇게 사용해야 할 듯.
        /// </summary>  
        public static string OptimizeToString(this double value, int decimalPlaces)
        {
            if (decimalPlaces < 0)
                throw new ArgumentOutOfRangeException(nameof(decimalPlaces), "소수점 자릿수는 반드시 0 이상의 숫자여야합니다.");
            if (value <= -1000 || value >= 1000)
                throw new ArgumentOutOfRangeException(nameof(value), "최적화를 위해서 제작된 함수입니다. value(정수가 될 값)은 반드시 -1000보다크고 1000보다 작아야합니다.");

            const int maxIntegerDigits = 3; // 정수 부분 최대 자릿수
            Span<char> buffer = stackalloc char[maxIntegerDigits + 2 + decimalPlaces]; // 정수부 + 소수점 + 소수부
            int index = 0;

            // 음수 처리
            if (value < 0)
            {
                buffer[index++] = '-';
                value = -value;
            }

            // 정수 부분 처리
            int integerPart = (int)value;
            index += IntegerToString(integerPart, buffer.Slice(index), maxIntegerDigits);

            // 소수점 처리
            if (decimalPlaces > 0)
            {
                buffer[index++] = '.';
                // 이렇게 하면 정수부를 모두 지우고 소수부만 획득할 수 있음. 
                double fractionalPart = value - integerPart;  
                // 소수부분을 순회하면서 처리 (여기서 반올림/반내림을 결정해도 되긴 함.)
                for (int i = 0; i < decimalPlaces; i++)
                {
                    fractionalPart *= 10;
                    int digit = (int)fractionalPart;
                    buffer[index++] = (char)('0' + digit);
                    fractionalPart -= digit;
                }
            }

            return new string(buffer.Slice(0, index));
        }


        
        private static int IntegerToString(int value, Span<char> buffer, int maxIntegerDigits)
        {
            // 0 제외 변환
            int index = maxIntegerDigits;
            do
            {
                buffer[--index] = (char)('0' + value % 10);
                value /= 10;
            } while (value > 0);

            // 올바른 출력을 위해 0을 제외한 만큼 슬라이스 해야 핢. 
            int length = maxIntegerDigits - index;
            buffer.Slice(index, length).CopyTo(buffer);
            return length;
        }
        
    }
}