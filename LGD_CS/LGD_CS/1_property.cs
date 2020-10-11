using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms; // messagebox 사용하기 위함

namespace LGD_CS
{
    class _1_property
    {
        // enum 선언
        public enum State { idle = 0, ready, running };

        private int m_num; // get, set 에서 사용할 private 변수

        public int number  // get, set
        {
            set
            {
                if (value == (int)State.idle) // (형변환) 및 enum 사용 예시
                {
                    m_num = value + 100;
                }
                else
                {
                    m_num = value; // value의 경우 set에서 사용하는 임시변수
                }
            }

            get
            {
                return m_num; // getter
            }
        }


        // '+'연산자에 경우 좌, 우 두종류가 있기때문에 이와 같이 둘다 재정의(*오버라이딩)해야함
        /// 연산자 + 오버로딩     i + _1_property 일때     
        public static int operator +(int i, _1_property obj)
        {
            return i + obj.m_num;
        }
        /// 연산자 + 오버로딩     _1_property + i 일때
        public static int operator +(_1_property obj, int i)
        {
            return obj.m_num + i;
        }

        /// == != 연산자 재정의, 같은지, 다른지 확인하는 연산자
        public static bool operator ==(_1_property obj1, _1_property obj2)
        {
            return obj1.GetType() == obj1.GetType(); // GetType은 object 클래스 메소드
        }
        public static bool operator !=(_1_property obj1, _1_property obj2)
        {
            return obj1.GetType() != obj1.GetType();
        }

        public static void Main()
        {
            _1_property cls = new _1_property();  // 클래스 정의
            cls.number = 0; // set 사용 예시

            // 메세지박스 사용예시
            MessageBox.Show(cls.number.ToString());

            // 메세지박스 사용예시
            MessageBox.Show(Enum.GetName(typeof(State), 1));   // enum객체를 string으로 사용

            _1_property cls2 = new _1_property()
            {
                m_num = 999   // 생성시 초기화 예시
            };
        }
    }
}
