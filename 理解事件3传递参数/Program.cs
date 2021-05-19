using System;

namespace 理解事件3传递参数
{
    class Program
    {
        static void Main(string[] args)
        {
            // 初始化对象
            Child child = new Child("Rose");
            Father father = new Father("Ethan");
            Mother mother = new Mother("Mia");
            // 这里为了对比订阅+=的作用，多声明一个Mother实例
            Mother miranda = new Mother("Miranda");

            // z父母的处理方法订阅到宝贝哭的事件
            //child.CryEvent += new Action<object, CryEventArgs>(father.DoSomething);
            child.CryEvent += new Action<object, CryEventArgs>(father.DoSomething);
            child.CryEvent += new Action<object, CryEventArgs>(mother.DoSomething);

            // 当孩子哭时，自动调用已订阅父母的DoSomething方法
            // 未订阅事件的Miranda无法响应
            child.Cry();
            // Baby Rose is crying...
            // Father Ethan plays with kid.
            // Mother Mia feeds kid.
        }
    }

    
    // 用于传递宝宝哭泣时间的事件参数
    public class CryEventArgs : EventArgs
    {
        public string currTime { get; }

        public CryEventArgs()
        {
            this.currTime = DateTime.Now.ToString();
        }
    }
    
    class Child
    {
        public string name { get; set; }

        public Child(string name)
        {
            this.name = name;
        }

        // 定义孩子哭的事件
        public event Action<object, CryEventArgs> CryEvent;
        // 孩子哭了之后，调用事件
        public void Cry()
        {
            Console.WriteLine($"Baby {this.name} is crying...");
            CryEventArgs e = new CryEventArgs();
            CryEvent?.Invoke(this, e);
        }
    }

    class Parent
    {
        public string name { get; set; }
        public Parent(string name)
        {
            this.name = name;
        }
        public virtual void DoSomething(object sender, CryEventArgs e) { }
    }

    class Father : Parent
    {
        public Father(string name) : base(name) { }
        public override void DoSomething(object sender, CryEventArgs e)
        {
            Console.WriteLine($"Father-{this.name} (事件的响应者)\t plays with Child-{sender.ToString()}(发送者)\t at {e.currTime}(传递的参数).");
        }
    }

    class Mother : Parent
    {
        public Mother(string name) : base(name) { }
        public override void DoSomething(object sender, CryEventArgs e)
        {
            Console.WriteLine($"Mother-{this.name} (事件的响应者)\t feeds Child-{sender.ToString()}(发送者)\t\t at {e.currTime}(传递的参数).");
        }
    }
}
