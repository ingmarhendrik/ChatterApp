using ChatClient.MVVM.ViewModel;

namespace ConsoleChatApp
{
    class Program
    {
        static void Main(string[] args)
        {

            var viewModel = new MainViewModel();

            Console.Write("Enter your username: ");
            var username = Console.ReadLine();
            viewModel.ConnectToServer(username);

            while (true)
            {
                var message = Console.ReadLine();
                if (message.ToLower() == "/exit")
                {
                    break;
                }
                viewModel.SendMessage(message);
            }
        }
    }
}