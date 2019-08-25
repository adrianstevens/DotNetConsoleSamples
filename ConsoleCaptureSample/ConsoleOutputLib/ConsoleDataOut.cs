using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleOutputLib
{
    public static class ConsoleDataOut
    {
        public static void Start ()
        {
          Task.Run(() =>
          {
              int count = 0;
              while (count < 20)
              {
                  Console.WriteLine($"The count is {count++}");
                  Thread.Sleep(500);
              }
          });
        }
    }
}
