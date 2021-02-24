namespace HslCommunication.BasicFramework
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;

    public class SoftMsgQueue<T> : SoftFileSaveBase
    {
        private Queue<T> all_items;
        private object lock_queue;
        private int m_Max_Cache;

        public SoftMsgQueue()
        {
            this.all_items = new Queue<T>();
            this.m_Max_Cache = 200;
            this.lock_queue = new object();
            base.LogHeaderText = "SoftMsgQueue<" + typeof(T).ToString() + ">";
        }

        public void AddNewItem(T item)
        {
            object obj2 = this.lock_queue;
            lock (obj2)
            {
                while (this.all_items.Count >= this.m_Max_Cache)
                {
                    this.all_items.Dequeue();
                }
                this.all_items.Enqueue(item);
            }
        }

        public override void LoadByString(string content)
        {
            JArray array = JArray.Parse(content);
            this.all_items = (Queue<T>) array.ToObject(typeof(Queue<T>));
        }

        public override string ToSaveString()
        {
            return JArray.FromObject(this.all_items).ToString();
        }

        public T CurrentItem
        {
            get
            {
                if (this.all_items.Count > 0)
                {
                    return this.all_items.Peek();
                }
                return default(T);
            }
        }

        public int MaxCache
        {
            get
            {
                return this.m_Max_Cache;
            }
            set
            {
                if (value > 10)
                {
                    this.m_Max_Cache = value;
                }
            }
        }
    }
}

