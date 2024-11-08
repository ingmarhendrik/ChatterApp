using ChatClient.MVVM.Core;
using ChatClient.MVVM.Model;
using ChatClient.Net;
using System.Collections.ObjectModel;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Interop;


namespace ChatClient.MVVM.ViewModel
{
    public class MainViewModel
    {
        public List<UserModel> Users { get; set; } = new List<UserModel>();
        public List<string> Messages { get; set; } = new List<string>();

        private Server _server;

        public MainViewModel()
        {
            _server = new Server();
            _server.connectedEvent += UserConnected;
            _server.msgReceivedEvent += MessageReceived;
            _server.userDisconnectEvent += RemoveUser;

        }

        public void ConnectToServer(string username)
        {
            _server.ConnectToServer(username);
        }

        public void SendMessage(string message)
        {
            _server.SendMessageToServer(message);
        }

        private void RemoveUser()
        {
            var uid = _server.PacketReader.ReadMessage();
            var user = Users.FirstOrDefault(x => x.UID == uid);
            if (user != null)
            {
                Users.Remove(user);
                Console.WriteLine($"{user.Username} has disconnected.");
            }
        }

        private void MessageReceived()
        {
            var msg = _server.PacketReader.ReadMessage();
            Messages.Add(msg);
            Console.WriteLine(msg);
        }

        private void UserConnected()
        {
            var user = new UserModel()
            {
                Username = _server.PacketReader.ReadMessage(),
                UID = _server.PacketReader.ReadMessage(),
            };
            if (!Users.Exists(x => x.UID == user.UID))
            {
                Users.Add(user);
                Console.WriteLine($"{user.Username} has connected.");
            }
        }
    }
}
