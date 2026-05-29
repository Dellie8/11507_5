using System;

namespace Levels
{
    class User
    {
        public string Name { get; set; }
    }

    class UserNotifier
    {
        public event Action<User> OnNotify;

        public void SafeNotify(User user)
        {
            if (OnNotify == null)
                return;

            foreach (Delegate handler in OnNotify.GetInvocationList())
            {
                try
                {
                    handler.DynamicInvoke(user);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }
    }
}