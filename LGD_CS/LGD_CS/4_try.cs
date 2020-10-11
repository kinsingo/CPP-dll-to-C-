using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGD_CS
{
    class _4_try
    {
        // 전역 변수 사용 (큰 의미는 없음)
        static int count = 0;
        public static void Main()
        {
            while (true)
            {
                try
                {
                    if (++count == 1) throw new Exception();    // 익셉션 발생 (이순간에 catch문으로 진입)
                    if (count == 3) break;  // 3번째 루프에서 while문 종요 이 다음 호출될 구문을 생각하며 확인해주세요
                    Console.WriteLine(count + ") No exception");
                }
                catch (Exception)
                {
                    Console.WriteLine(count + ") Exception thrown");
                }
                finally
                {
                    // 언제 호출되는지 보세요~ 중요합니다~
                    // finally 구문의 활용성을 생각할 수 있습니다.
                    Console.WriteLine(count + ") in finally clause");
                }
            } // end while 
            Console.WriteLine("Main program ends");
        }
    }
}
