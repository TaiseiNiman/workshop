
mongoDBにおけるドキュメント

simulation[数字]

user
ユーザー名　：氏名
ハッシュID　：ユニークなハッシュ値
グループ名　：任意の文字列
帰宅選択状況　：^1{1,n+1}[2-9]{0,n}$の正規表現にマッチする文字列　ただしnは0から帰宅手段の選択できる最大数である。例えば11時から23時まで帰宅手段を選択できるなら、(23-11+1) = 13となる.


ユーザー名：氏名
グループ名：任意の文字列
ハッシュID：ユニークなハッシュ値

サーバー

用いるエンドポイント：localhost:8080/simulation

OnMessage

・ドキュメントにユーザーが存在するか調べる
引数："ハッシュIDの元となるパスワード=ユニークなランダム文字列:メッセージ認識ID＝existingNow", send引数: "ハッシュID:メッセージ認識ID:bool=ハッシュIDが存在するならtrueそうでないならfalse"
・ドキュメントに以下の正規表現を満たすユーザーがいるかどうか調べ、いればそのハッシュ値の配列を返す
引数："[^1{1,n+1}[2-9]{0,n}$]=この正規表現を満たすすべての正規表現のどれかを指定する。具体的には,^1{1,a}[2-9]{0,b}$として,aかつbならば1<=a<=n+1かつ0<=b<=nを満足するa,bを選ぶ: メッセージ認識ID=Rxp:",
send引数: "引数:[ハッシュID,..]"
・ハッシュIDからuserコレクションを取得あるいは全userコレクションを取得
・

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using WebSocketSharp;
using WebSocketSharp.Server;
using System.Timers;
using System.Collections.Generic;
using System.Diagnostics;

public class UserLog : WebSocketBehavior
{
    private static List<UserLog> clients = new List<UserLog>();
    private static string serverIp = GetLocalIPAddress();

    protected override void OnOpen()
    {
        lock (clients)
        {
            clients.Add(this);
        }

        var clientIp = Context.UserEndPoint.Address.ToString();
        Console.WriteLine($"New client connected: {clientIp}");

        Broadcast($"New client connected: {clientIp}");
    }

    protected override void OnClose(CloseEventArgs e)
    {
        lock (clients)
        {
            clients.Remove(this);
        }
        var clientIp = Context.UserEndPoint.Address.ToString();
        Console.WriteLine($"client removed: {clientIp}");

        Broadcast($"client removed: {clientIp}");
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        var clientIp = Context.UserEndPoint.Address.ToString();
        if (clientIp == serverIp)
        {
            // サーバーと同一デバイスからの接続
            if(e.Data == "StartWorkshopSimulation")
            {
                //ワークショップのシミュレーションの開始を全クライアントに伝える
                Broadcast("StartWorkshopSimulation");
            }
            
        }
        else
        {
            // 通常の処理
            
        }
    }

    private void Broadcast(string message)
    {
        lock (clients)
        {
            foreach (var client in clients)
            {
                client.Send(message);
            }
        }
    }

    private static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new Exception("No network adapters with an IPv4 address in the system!");
    }
}

public class Workshop : WebSocketBehavior
{
    protected override void OnMessage(MessageEventArgs e)
    {
        Send("Workshop: " + e.Data);
    }
}

public class WebsocketTimer : WebSocketBehavior
{
    private static System.Timers.Timer broadcastTimer;
    //
    private Stopwatch stopwatch;
    private DateTime startTime;
    public TimeSpan elapsed;
    public DateTime currentTime;

    protected override void OnOpen()
    {
        if (broadcastTimer == null)
        {
            // タイマーの設定
            broadcastTimer = new System.Timers.Timer(500); // 1秒間隔
            broadcastTimer.Elapsed += OnTimedEvent;
            broadcastTimer.AutoReset = true;
            broadcastTimer.Enabled = true;
            //ストップウォッチの設定
            　// ストップウォッチを開始
            stopwatch = new Stopwatch();
            stopwatch.Start();

            // 特定の時刻を設定（11時）
            startTime = new DateTime(1997, 7, 1, 11, 0, 0);
        }
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        // クライアントからのメッセージを受信した場合の処理
        Console.WriteLine("Message received: " + e.Data);
    }

    private void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        // 定期的に送信するメッセージ

        //
        // 経過時間を取得
        elapsed = stopwatch.Elapsed;
        TimeSpan multipliedElapsed2 = TimeSpan.FromTicks(elapsed.Ticks * 20 - new TimeSpan(9, 0, 0).Ticks*2) + new TimeSpan(9, 0, 0);
        //
        TimeSpan multipliedElapsed = TimeSpan.FromTicks(elapsed.Ticks * 20);

        // 特定の時刻に経過時間を加算
        if (new DateTime(1997, 7, 1, 20, 0, 0) <= startTime.Add(multipliedElapsed))
        {
            currentTime = startTime.Add(multipliedElapsed2);
        }
        else
        {
            currentTime = startTime.Add(multipliedElapsed);
        }
        Sessions.Broadcast(currentTime.ToString());
        Console.WriteLine($"現在の時刻: {currentTime:HH時mm分ss秒}");
    }
}

public class Program
{
    private static System.Timers.Timer broadcastTimer; // 明示的に指定

    public static void Main(string[] args)
    {
        var wssv = new WebSocketServer("ws://0.0.0.0:8080");
        wssv.AddWebSocketService<UserLog>("/UserLog");
        wssv.AddWebSocketService<Workshop>("/Workshop");
        wssv.AddWebSocketService<WebsocketTimer>("/Timer");
        wssv.Start();
        Console.WriteLine("WebSocket server started at ws://localhost:8080/UserLaaog"+ IPAddress.Broadcast.ToString());
        Console.WriteLine("WebSocket server started at ws://localhost:8080/Workshop");
        Console.WriteLine("WebSocket server started at ws://localhost:8080/Timer");
        // ブロードキャストメッセージを定期的に送信
        broadcastTimer = new System.Timers.Timer(5000); // 明示的に指定
        broadcastTimer.Elapsed += BroadcastMessage;
        broadcastTimer.AutoReset = true;
        broadcastTimer.Enabled = true;

        Console.ReadKey(true);
        wssv.Stop();
    }

    private static void BroadcastMessage(Object source, ElapsedEventArgs e)
    {
        var udpClient = new UdpClient();
        udpClient.EnableBroadcast = true;
        var broadcastMessage = Encoding.UTF8.GetBytes("WebSocketServerWorkshop:8080");
        udpClient.Send(broadcastMessage, broadcastMessage.Length, new IPEndPoint(IPAddress.Broadcast, 8888));
        //Console.WriteLine("Broadcast message sent");
    }
}
