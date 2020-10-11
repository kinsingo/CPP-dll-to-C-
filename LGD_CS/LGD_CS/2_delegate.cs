using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms; // messagebox 사용하기 위함
using System.Collections; // ArrayList를 위함

namespace LGD_CS
{
    // 델리게이트를 사용한다는것은 내가 원하는 함수들을 별칭을 붙여서 호출할 수 있다는 이야기
    // 다시이야기하면, 필요한 함수를 골라서 다시 이름을 지어 사용할 수 있게 한다고 생각하면 좀 더 쉽게 이해가 가능함


    delegate void dele1(string text);   // 델리게이트 선언 --- 사용하려는 함수 형식이 함수명(string) 이어야함
    delegate T dele2<T>(T a, T b);      // 제네릭을 이용하여 좀 더 일반화 시켜 사용하기 위함 T는 자료형(int, double 등)과 같다고 생각
                                        // 즉, 다양한 자료형을 델리게이트 한개로 일반화 시켜 사용할 수 있음을 알수 있음 (코드의 활용성 극대화)

    class _2_delegate    // 델리게이트 예시를 보여주기 위한 클래스
    {
        void text_1(string text)      // 위에서 이야기한 함수명(string) 형식의 함수
        {                             // 이함수는 델리게이트로 호출될 것임
            MessageBox.Show(text); 
        }


        public static void print(int a, int b, dele1 callback)   // 이함수는 델리게이트를 인자로 받아서 호출하고 있음, 위에 이야기를 극대화시켜 활용하는 예제임
                                                                 // print 이함수 입장에서는 자신이 하려는 기능만 잘하고, 호출자가 원한 함수만 호출해 주면 됨. 즉, 코드분리, 임무분리가 가능한 것임
        {
            int c = 0;
            c = a + b;
            callback("complete" + c.ToString());                 // 호출자가 원한 함수 호출 (여기서는 콜백용으로 사용함, 
                                                                 // 호출자의 의도는 이함수가 일이 끝나면 호출자가 전달한 함수를 호출해서 호출자가 원하는 다음 처리를 하도록함)
        }

        public void non_print(int a, int b, dele1 callback)      // static을 이용하지 않는 예, main에서 차이를 확인해보세요
        {
            int c = 0;
            c = a + b;
            callback("complete" + c.ToString());
        }

        public static void Main()
        {
            _2_delegate cls = new _2_delegate();                // 클래스 인스턴스화 (중요한 지점)

            dele1 mydelegate1 = new dele1(cls.text_1);          // 델리게이트1 생성과 함수를 할당함 new 델리게이트(누구의.함수) <= static과 non static의 차이를 확인하여야함
            mydelegate1 += new dele1(cls.text_1);               // 델리게이트1에 함수를 추가로 할당함
                                                                // 체인화. 하나를 호출했는데 여러개 함수가 호출된 효과를 얻음. 점점 더 개발이 편리해짐

            //mydelegate1 += new dele1(cls.print(1,2,mydelegate1);  // <-- 에러나는 이유를 보세요~
            
            mydelegate1("test delegate");                       // 델리게이트 사용 - 델리게이트지만 사용할 때는 함수콜 처럼 이용함 (연상기억, 함수의 별칭을 붙여서 사용한다는 의미)

            _2_delegate.print(1, 2, mydelegate1);               // 스태틱함수 사용

            
            // 자료형 변환 예제 슬라이드 588p **********************************************************
            // float자료형의 오차를 기억해야함
            // 오차가 발생하는것을 반드시 기억해야합니다.
            int big = 1234567890;
            float approx;
            approx = (float)big;
            Console.WriteLine("difference = " + (big - (int)approx));    // (타입)변수 -> 형변환방법


            // 자료형 변환 예제 슬라이드 589p **********************************************************
            // 박싱, 언박싱 예제
            int box_int = 0;    /// 스택에 저장된 지역변수 그리고 값
            object o = box_int;  // 스택에 있던 값을 힙영역에 할당시켜 o변수를 이용해 참조함 (생성, 메모리 이동이 발생함)
            int unbox_int = (int)o; // 언박싱 
            // 값형을 -> 참조형으로 변환 = 박싱
            // 참조형을 -> 값형으로 변환 = 언박싱

            // 어디에 사용 할수 있을까요?
            ArrayList box_example = new ArrayList();
            // object형을 받음 어떤 것이든 배열리스트로 활용할 수 있음
            // 하지만 비용이 많이 발생하는 방법이므로
            // 가급적 제네릭을 사용하여 해야함
            // 하지만 때에 따라 활용 가능하므로 값형이 참조형으로 바뀐다는 것만 인지하고 사용하면 충분함
        }
    }
}
