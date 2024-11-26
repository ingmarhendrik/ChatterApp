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
                var message = Console.ReadLine() ?? string.Empty;
                if (message.Trim().Length == 0) continue;

                if (message.ToLower() == "/exit")
                {
                    break;
                }
                viewModel.SendMessage(message);
            }
        }
    }
}