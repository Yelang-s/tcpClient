using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace tcpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // 以下方式在初始化的时候就会自动连接
            //TcpClient tcpClient = new TcpClient("localhost", 7788);
            //TcpClient tcpClient1 = new TcpClient(new System.Net.IPEndPoint(IPAddress.Parse("127.0.0.1"), 7788));

            TcpClient tcpClient3 = new TcpClient();
            tcpClient3.ReceiveTimeout = 3000;
            tcpClient3.SendTimeout = 3000;
            tcpClient3.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7788));

            NetworkStream stream = tcpClient3.GetStream();
            while (true)
            {
                string msg = Console.ReadLine();
                byte[] writeD = Encoding.UTF8.GetBytes(msg);
                // 写入数据  writeD：需要写入的数据 offset：从writeD中的那个位置开始 size：写入数据的大小
                stream.Write(writeD, 0, writeD.Length);
                byte[] datas = new byte[1024];
                // datas 读取的数据存储的位置 offset:从哪个地方开始存储 size:每次读取的数据大小 res:实际读取的大小
                int res = stream.Read(datas, 0, datas.Length);
                string readD = Encoding.UTF8.GetString(datas, 0, res);
                Console.WriteLine($"{DateTime.Now}-->{readD}");
            }
           
        }
    }
}
