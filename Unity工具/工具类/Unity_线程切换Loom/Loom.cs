using UnityEngine;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Linq;

public class Loom : MonoBehaviour
{

    #region 初始化Loom
    private static Loom _current;
    private static bool initialized;// 静态成员，随着类的加载而加载，初始是false
    // 方法1：Loom这个类组件已经被手动添加在空的游戏对象上
    void Awake()
    {
        _current = this;
        initialized = true;
    }
    // 方法2：Loom这个类组件未被设置到游戏物体上，这个方法就可以将Loom添加到游戏对象上
    public static Loom Current
    {
        get
        {
            Initialize();
            return _current;
        }
    }
    static void Initialize()
    {
        if (initialized == false)
        {
            if (Application.isPlaying == false)
            { return; }
            initialized = true;
            GameObject g = new GameObject("Loom"); // 创建空的游戏对象
            _current = g.AddComponent<Loom>();     // 在这个游戏对象上添加并获取这个组件
        }

    }
    #endregion


    public static int maxThreads = 8;
    private static int numThreads = 0;   // 静态加载时是0
    private int _count;
    public struct DelayedQueueItem
    {
        public float time;
        public Action action;

    }

    private List<Action> _actions = new List<Action>();                     // 存储立即执行的委托
    private List<DelayedQueueItem> _delayed = new List<DelayedQueueItem>(); // 存储带延时的委托

    private List<Action> _currentActions = new List<Action>();              // 存储临时的委托
    private List<DelayedQueueItem> _currentDelayed = new List<DelayedQueueItem>();   // 存储临时的委托

    // 在主线程中执行委托,一般用于 子线程向主线程传递数据
    public static void QueueOnMainThread(Action action)
    {
        QueueOnMainThread(action, 0f);
    }
    public static void QueueOnMainThread(Action action, float time)
    {
        Initialize();
        if (time != 0)
        {
            lock (Current._delayed)
            {
                Current._delayed.Add(new DelayedQueueItem { time = Time.time + time, action = action });
            }
        }
        else
        {
            lock (Current._actions)
            {
                Current._actions.Add(action);
            }
        }
    }


    // 在子线程中执行委托，一般用于 耗时操作，将主线程的任务在子线程中执行
    public static Thread RunAsync(Action action)
    {
        Initialize();
        while (numThreads >= maxThreads)
        {
            Thread.Sleep(1);    // 当前线程沉睡1ms
            Console.WriteLine(Thread.CurrentThread.Name);
        }
        Interlocked.Increment(ref numThreads);
        ThreadPool.QueueUserWorkItem(RunAction, action); // 将任务放入线程任务队列中
        return null;
    }
    private static void RunAction(object action)
    {
        try
        {
            ((Action)action)();  // 执行传入的委托
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        finally
        {
            Interlocked.Decrement(ref numThreads); // 执行完成 numThreads-1
        }

    }


    void Update()
    {
        lock (_actions)
        {
            _currentActions.Clear();
            _currentActions.AddRange(_actions);
            _actions.Clear();
        }
        foreach (Action action in _currentActions)
        {
            action();
        }
        lock (_delayed)
        {
            _currentDelayed.Clear();
            _currentDelayed.AddRange(_delayed.Where(d => d.time <= Time.time));
            foreach (var item in _currentDelayed)
            { _delayed.Remove(item); }
        }
        foreach (var delayed in _currentDelayed)
        {
            delayed.action();
        }
    }

    void OnDisable()
    {
        if (_current == this)
        {
            _current = null;
        }
    }
}

