﻿using System;

namespace 事件练习1_输入值监听
{
    class Program
    {
        static void Main(string[] args)
        {
            // 声明监听者，用于监听用户输入
            Listener listener = new Listener();
            // 接收者1、2，对监听者输入做出不同的反应
            Reciever1 reciever1 = new Reciever1();
            Reciever2 reciever2 = new Reciever2();
            // 将监听者的ListenEvent事件与接受者的方法关联（建立订阅关系）
            listener.ListenEvent += new Action<object, ListenEventArgs>(reciever1.OutPutInf);
            listener.ListenEvent += new Action<object, ListenEventArgs>(reciever2.OutPutInf);
            // 调用ListenInut方法时，由于该方法调用了ListenEvent委托，而该委托被多个具体方法订阅（多播委托）
            // 因此会调用reciever1、reciever2的OutPutInf()方法
            listener.ListenInut();
            // 输出结果：
            // x
            // 用户输入的值为：x
            // 下个字符为：y
        }
    }

    // 传递参数类：用于传递用户输入字符
    public class ListenEventArgs : EventArgs
    {
        public char inputChar { get; set; }
        public ListenEventArgs(char inputChar)
        {
            this.inputChar = inputChar;
        }
    }

    // 监听类，其方法ListenInut()不断地监听用户的输入值，并将参数传入委托调用
    public class Listener
    {
        // 声明一个委托，用于订阅事件
        public event Action<object, ListenEventArgs> ListenEvent;
        public void ListenInut()
        {
            while (true)
            {
                // 保存输入参数
                char res = Console.ReadKey().KeyChar;
                ListenEventArgs e = new ListenEventArgs(res);
                // 将输入的值作为参数传递到事件中
                ListenEvent?.Invoke(this, e);
            }
        }
    }

    // 接收者类1
    public class Reciever1
    {
        public void OutPutInf(object sender,ListenEventArgs e)
        {
            Console.WriteLine($"\n用户输入的值为：{e.inputChar}");
        }
    }

    // 接收者类2
    public class Reciever2
    {
        public void OutPutInf(object sender, ListenEventArgs e)
        {
            Console.WriteLine($"下个字符为：{(char)(e.inputChar+1)}");
        }
    }
}
