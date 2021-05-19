using System;

namespace 理解事件
{
    class Program
    {
        static void Main(string[] args)
        {
            // 实例化一个事件源对象
            Me cratza = new Me("Cratza");

            // 实例化关注事件的对象
            Friend1 obj1 = new Friend1();
            Friend2 obj2 = new Friend2();

            // 使用委托把对象及其方法注册到事件中

            cratza.BirthDayEvent += new BirthDayEventHandle(obj1.SendGift);
            cratza.BirthDayEvent += new BirthDayEventHandle(obj2.Buycake);

            // 事件到了触发生日事件，事件的调用
            cratza.TimeUp();
            Console.Read();


            //var anonymousClass = new
            //{
            //    ID = 10010,
            //    Name = "EdisonChou",
            //    Age = 25,
            //    Address = new { City = "Hyderabad", Country = "INDIA" }
            //};

            //var anonymousClassArr = new[]
            //{
            //    new {ID = 1,Name = "EdisonChou"},
            //    new {ID = 2,Name = "JackCheng"}
            //};

            //Action<string> action = (a) => Console.WriteLine(a + " works hard without any achievement.");
            //action.Invoke("Cratza");
            //Console.WriteLine(anonymousClass.ToString());
        }
    }

    // 第一步：定义一个类型用来保存所有需要发送给事件接收者的附加信息
    public class BirthdayEventArgs : EventArgs
    {
        // 表示过生日人的姓名
        private string name;
        public string Name => name;

        public BirthdayEventArgs(string name)
        {
            this.name = name;
        }
    }

    // 第二步：定义一个生日事件，首先需要定义一个委托类型，用于指定事件触发时被调用的方法类型
    public delegate void BirthDayEventHandle(object sender, BirthdayEventArgs e);

    // 定义事件成员
    public class Subject
    {
        // 定义生日事件
        public event BirthDayEventHandle BirthDayEvent;

        // 第三步：定义一个负责引发事件的方法，它通知已关注的对象（通知我的好友）
        protected virtual void Notify(BirthdayEventArgs e)
        {
            // 触发事件，与方法的使用方式相同
            // 事件通知委托对象，委托对象调用封装的方法
            BirthDayEvent?.Invoke(this, e);
            //// 出于线程安全的考虑，现在将对委托字段的引用复制到一个临时字段中
            // BirthDayEventHandle temp = Interlocked.CompareExchange(ref BirthDayEvent, null, null);
            // temp?.Invoke(this, e);
        }
    }

    // 定义触发事件的对象，事件源
    public class Me : Subject
    {
        private string name;
        public Me(string name)
        {
            this.name = name;
        }
        public void TimeUp()
        {
            BirthdayEventArgs eventarg = new BirthdayEventArgs(name);
            // 生日到了，通知朋友们
            this.Notify(eventarg);
        }
    }

    // 好友对象
    public class Friend1
    {
        public void SendGift(object sender, BirthdayEventArgs e)
        {
            Console.WriteLine(e.Name + " 生日到了，我要送礼物");
        }
    }
    public class Friend2
    {
        public void Buycake(object sender, BirthdayEventArgs e)
        {
            Console.WriteLine(e.Name + " 生日到了,我要准备买蛋糕");
        }
    }
}
