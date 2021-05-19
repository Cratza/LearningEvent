using System;

namespace 理解事件2
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

            // 父母的处理方法订阅到宝贝哭的事件
            child.BabyCry += father.DoSomething;
            child.BabyCry += mother.DoSomething;

            // 当孩子哭时，自动调用已订阅父母的DoSomething方法
            // 未订阅事件的Miranda无法响应
            child.Cry();
            // Baby Rose is crying...
            // Father Ethan plays with kid.
            // Mother Mia feeds kid.
        }
    }

    // 触发事件的类
    class Child
    {
        // 定义宝贝哭的事件
        public event Action BabyCry;

        public string name { get; set; }

        public Child(string name)
        {
            this.name = name;
        }

        // 孩子哭了之后，调用事件
        public void Cry()
        {
            Console.WriteLine($"Baby {this.name} is crying...");
            // 此处的?为可空修饰符，用于处理BabyCry未被订阅时导致的错误，其等价于
            // if (BabyCry != null) BabyCry.Invoke();
            BabyCry?.Invoke();
        }
    }

    // 关注事件类的基类
    class Parent
    {
        public string name { get; set; }
        public Parent(string name)
        {
            this.name = name;
        }
        public virtual void DoSomething() { }
    }

    // 关注事件类的子类
    class Father : Parent
    {
        public Father(string name) : base(name) { }
        public override void DoSomething()
        {
            Console.WriteLine($"Father {this.name} plays with kid.");
        }
    }

    // 关注事件类的子类
    class Mother : Parent
    {
        public Mother(string name) : base(name) { }
        public override void DoSomething()
        {
            Console.WriteLine($"Mother {this.name} feeds kid.");
        }
    }
}
