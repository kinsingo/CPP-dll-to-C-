using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading; // thread 사용

namespace LGD_CS
{
    class _5_thread
    {
        // 쓰레드 함수
        static void ThreadBody()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(DateTime.Now.Second + " : " + i);
                Thread.Sleep(1000);
            }
        }

        // lock을 이용한 동기화
        void ThreadBody2()
        {
            // Thread Lock 사용 예제
            // 쓰레드간의 락을 사용
            Thread myself = Thread.CurrentThread;
            lock (this)
            {
                for (int i = 1; i <= 10; i++)
                {
                    Console.WriteLine("{0} is activated => {1}", myself.Name, i);
                    Thread.Sleep(100);
                }
            }
        }

        void MyThread()
        {
            ////////////////////////////////////////////////
            ThreadStart ts1 = new ThreadStart(ThreadBody2);
            Thread t1 = new Thread(ts1);
            ThreadStart ts2 = new ThreadStart(ThreadBody2);
            Thread t2 = new Thread(ts2);
            t1.Name = "btn1-t1";
            t2.Name = "btn1-t2";
            t1.Start();
            t2.Start();
        }

        // monitor를 이용한 동기화
        public void ThreadBody3()
        {
            Thread myself = Thread.CurrentThread;
            Monitor.Enter(this);
            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine("{0} is activated => {1}", myself.Name, i);
                Thread.Sleep(100);
            }

            Monitor.Exit(this);

            Monitor.Enter(this);
            // try 추가 응용
            try
            {
                for (int i = 1; i <= 10; i++)
                {
                    Console.WriteLine("{0} is activated => {1}", myself.Name, i);
                    Thread.Sleep(100);
                };
            }
            finally { Monitor.Exit(this); }
        }

        //////////////////////// pulse를 이용한 이벤트 전달 동기화
        int g_add = 0;
        public void ThreadBody4()
        {
            Thread myself = Thread.CurrentThread;
            lock (this)
            {
                while (true)
                {
                    Thread.Sleep(100);
                    g_add--;
                    Console.WriteLine("{0} is activated => {1}", myself.Name, g_add);
                    Monitor.Pulse(this);
                    Monitor.Wait(this);
                }
            }
        }
        public void ThreadBody5()
        {
            Thread myself = Thread.CurrentThread;
            lock (this)
            {
                while (true)
                {
                    Thread.Sleep(100);
                    g_add++;
                    Console.WriteLine("{0} is activated => {1}", myself.Name, g_add);
                    Monitor.Pulse(this);
                    Monitor.Wait(this);
                }
            }
        }

        public static void Main()
        {
            // 쓰레드 생성 및 쓰레드 함수 할당
            ThreadStart ts = new ThreadStart(ThreadBody);
            Thread t = new Thread(ts); // threadbody
            t.Start();

            t.IsBackground = true;
            // 중요
            //*foreground thread
            //하나의 프로세스에서 전경스레드가 남아있다면 프로그램은 종료되지 않는다.       
            //모든 전경스레드의 종료를 기다리기 때문에, 메인함수에서 return을 하더라도 프로그램은 종료되지 않는다.

            //*background thread
            //이에 비해 배경스레드는 프로세스의 종료에 별다른 영향을 주지 않는다.
            //프로세스가 종료될 때 배경스레드가 남아 있더라도 프로그램은 종료된다.
            // C++ 시 했던 detach, join을 보시고 다시 위 글을 보시면 더 이해해 도움이 될 수 있습니다.

            //////////////////////////////////////////////
            _5_thread cls = new _5_thread();
            // lock 동기화 사용 예제 (출력 탭에 로그가 찍힙니다)
            cls.MyThread();
            // monitor 동기화 예제
            ThreadStart ts1 = new ThreadStart(cls.ThreadBody3);
            Thread t1 = new Thread(ts1);
            ThreadStart ts2 = new ThreadStart(cls.ThreadBody3);
            Thread t2 = new Thread(ts2);
            t1.Name = "btn2-t1";
            t2.Name = "btn2-t2";
            t1.Start();
            t2.Start();
            // pulse 예제, 서로 핑퐁하듯이 실행
            cls.g_add = 40;
            ThreadStart ts4 = new ThreadStart(cls.ThreadBody4);
            Thread t4 = new Thread(ts4);
            ThreadStart ts5 = new ThreadStart(cls.ThreadBody5);
            Thread t5 = new Thread(ts5);
            t4.Name = "btn3-t4";
            t5.Name = "btn3-t5";
            t4.Start();
            t5.Start();
        }
    }
}


// 비동기 코드는 기본 코드가 익숙해지신 후 보는게 좋습니다.
// 동기코드 자체가 코딩방식을 혼란스럽게 만들기 때문에
// 성능상 이점이 있다고 하여도
// 동기코드를 thread를 이용하여 멀티쓰레딩으로 만들 수 있다면
// 반드시 비동기를 사용할 필요는 없습니다.
// 코드가 짧아지고 관리할 지점이 낮아지긴하지만
// (개발자가)관리할 부분이 감소하고, .net framework가 도와주는 부분이 늘어난 것입니다.
// .net framework가 잘 만들어져 있지만 우리들에 입장에선 블랙박스이기 때문에, 반드시 비동기를 써야하는 것 아닌 이상 
// 코드(클래스 및 함수)설계 자체를 우선적으로 잘하는 것이 좋습니다.
// 소프트웨어 기술 트렌드를 따라가는 것과 안정성은 별개에 이야기 입니다.
// 안정성은 개발사가 보장해야하는 것입니다.
// 업데이트가 수시로 가능한 시스템은 아래와 같은 최신기술 도입이 보다 적극적일 수 있지만
// 그것이 아니라면 시스템 스트레스 테스트, .net framework에서 권하는 개발방식을 적용하시길 바랍니다.

/******* 비동기 처리, 병렬처리 코드
 * 
        // 1 비동기 코드
        private async void asyncfunc()
        {
            Console.WriteLine("asyncfunc " + Thread.CurrentThread.ManagedThreadId);
            label1.Text = "start";
            Thread.Sleep(1000);
            // 비동기로 Worker Thread에서 도는 task1
            // Task.Run(): .NET Framework 4.5+
            var task1 = Task.Run(() => CalcAsync(10));

            // task1이 끝나길 기다렸다가 끝나면 결과치를 sum에 할당
            int sum = await task1;
            Thread.Sleep(10000);
            // UI Thread 에서 실행
            // Control.Invoke 혹은 Control.BeginInvok 필요없음
            //label1.Text = "Sum = " + sum;
        }

        // 비동기로 실행할 함수
        private int CalcAsync(int times)
        {
            Console.WriteLine("CalcAsync " + Thread.CurrentThread.ManagedThreadId);
            int result = 0;
            for (int i = 0; i < times; i++)
            {
                result += i;
                Thread.Sleep(100);
            }
            return result;
        }

        // 비동기 추가예제
        private async void awaitfunc()
        {
            Console.WriteLine("awaitfunc " + Thread.CurrentThread.ManagedThreadId);
            label2.Text = "Start";
            Thread.Sleep(1000);
            int sum = await Calc2(10);
            //label2.Text = "Sum = " + sum;
        }

        // 비동기로 실행할 함수
        private async Task<int> Calc2(int times)
        {
            //UI Thread에서 실행
            Console.WriteLine("Calc2 " + Thread.CurrentThread.ManagedThreadId);
            int result = 0;
            for (int i = 0; i < times; i++)
            {
                result += i;
                await Task.Delay(100);
                //Thread.Sleep(100);
            }
            label2.Text = "UI Sum = " + result;
            return result;
        }

        // 병렬처리 코드
        private void parallelfunc()
        {
            // 1. 순차적 실행
            //
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("{0}: {1}",
                    Thread.CurrentThread.ManagedThreadId, i);
            }
            Console.WriteLine("--------------------------------");

            // 2. 병렬 처리
            // 다중쓰레드가 병렬로 출력
            //
            Parallel.For(0, 100, (i) => {
                Console.WriteLine("{0}: {1}",
                    Thread.CurrentThread.ManagedThreadId, i);
            });
        }


/// 사용할 때
/// 
        private void button4_Click(object sender, EventArgs e)
        {
            asyncfunc();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            awaitfunc();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            parallelfunc();
        }
 * *****/
