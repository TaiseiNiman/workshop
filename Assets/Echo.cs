using System;
using WebSocketSharp;
using WebSocketSharp.Server;
using UnityEngine;
using UnityEngine.Events;
using Unity.Netcode;

namespace MyProject
{
    public class Echo : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            Send("Echo: " + e.Data);
        }
    }

    public class WebsocketMain
    {
        public static void websocketMain()
        {
            var wssv = new WebSocketServer("ws://localhost:8081");

            // エンドポイントを /Test に変更
            wssv.AddWebSocketService<Echo>("/Test");
            wssv.Start();
            Debug.Log("websocketServerCreate!");
            //Console.WriteLine("WebSocket server started at ws://localhost:8081/Test");
            
            //Console.ReadKey(true);
            //wssv.Stop();
        }
    }

}