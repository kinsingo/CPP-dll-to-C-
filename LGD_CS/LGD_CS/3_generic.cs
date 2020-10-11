using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGD_CS
{
    // 잘 사용하면 엄청난 효과를 얻을 수 있는 제네릭 방법
    // 가급적 이방법을 고려해서 프로그래밍 하면 좋음

    // 제네릭 예시
    // 제네릭 코드를 확인하면 데이터타입보다 기능에 초점이 맞춰진것을 이해한다면 제네릭이 왜 잘 사용하면 큰 무기인지 알 수 있는 예제입니다.
    class SimpleGeneric<T>
    {
        private T[] values;
        private int index;
        public SimpleGeneric(int len)
        {            // Constructor
            values = new T[len];
            index = 0;
        }
        public void Add(params T[] args)
        {
            foreach (T e in args)
                values[index++] = e;
        }
        public void Print()
        {
            foreach (T e in values)
                Console.Write(e + " ");
            Console.WriteLine();
        }
    }


    class _3_generic
    {
        // 제네릭 활용에 또다른 예시
        // SWAP 기능 ref는 C++에서 포인터 참조로 넘겨준다고 생각하면 좋습니다.
        // 위에 T를 좀더 이해하기 쉽게 DataType이라는 단어를 써서 표현한 것임
        // 즉 T는 이름을 지어 준 것입니다.
        static void Swap<DataType>(ref DataType x, ref DataType y)
        {
            DataType temp;
            temp = x; x = y; y = temp;
        }

        // where 조건 예시
        static void SwapWhere<DataType>(ref DataType x, ref DataType y) where DataType : struct // 값형만 사용해야함
        {
            DataType temp;
            temp = x; x = y; y = temp;
        }

        public static void Main()
        {
            // 한 소스코드로 자료형 구분없이 코딩을 한 예제
            // 제네릭의 활용성 예시입니다.
            SimpleGeneric<Int32> gInteger = new SimpleGeneric<Int32>(10);
            SimpleGeneric<double> gdouble = new SimpleGeneric<double>(10);
            SimpleGeneric<float> gfloat = new SimpleGeneric<float>(10);

            // 또다른 활용 예시
            int a = 1, b = 2; double c = 1.5, d = 2.5;
            Console.WriteLine("Before: a = {0}, b = {1}", a, b);
            Swap<int>(ref a, ref b);           // 정수형 변수로 호출 
            Console.WriteLine(" After: a = {0}, b = {1}", a, b);
            Console.WriteLine("Before: c = {0}, d = {1}", c, d);
            Swap<double>(ref c, ref d);    // 실수형 변수로 호출 
            Console.WriteLine(" After: c = {0}, d = {1}", c, d);

            // where 조건
            int swap1 = 1, swap2 = 2;
            SwapWhere<int>(ref swap1, ref swap2);           // 정수형 변수로 호출 
        }
    }
}
