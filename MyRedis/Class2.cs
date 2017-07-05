using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.Redis;
using System.Threading;

namespace MyRedis
{
    public class Class2
    {
        public void Test()
        {
            RedisClient client = new RedisClient("127.0.0.1", 6379);
            client.FlushAll(); //清除所有缓存
            client.FlushDb();  //删除当前数据库里面的所有数据
            //client.Add<string>("mykey", "我已设置过期时间噢3秒后会消失", DateTime.Now.AddMilliseconds(3000));
            #region 显示所以Key
            foreach (var key in client.Keys("*"))
            {
                Console.WriteLine(key); //若 key 存在返回 1 ，否则返回 0 
            }
            #endregion
            #region 新增删除
            client.Add<string>("mykey", "数据abcd");
            Console.WriteLine(client.Exists("mykey")); //若 key 存在返回 1 ，否则返回 0 
            Console.WriteLine(client.Get<string>("mykey"));
            client.Del("mykey"); //client.Remove("key");
            Console.WriteLine(client.Exists("mykey"));
            if (!client.ContainsKey("mykey"))
                Console.WriteLine("已删除");
            #endregion
            #region 过期
            Console.WriteLine("过期测试"); //若 key 存在返回 1 ，否则返回 0 
            client.Add<string>("mykey", "数据abcd", DateTime.Now.AddMilliseconds(900));
            Console.WriteLine(client.Exists("mykey")); //若 key 存在返回 1 ，否则返回 0 
            Thread.Sleep(1000);
            Console.WriteLine(client.Exists("mykey")); //若 key 存在返回 1 ，否则返回 0 

            client.Add<string>("mykey", "数据abcd");
            client.Expire("mykey", 1);
            Console.WriteLine(client.Exists("mykey")); //若 key 存在返回 1 ，否则返回 0 
            Thread.Sleep(1200);
            Console.WriteLine(client.Exists("mykey")); //若 key 存在返回 1 ，否则返回 0 
            #endregion
        }
    }
}
