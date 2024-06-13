namespace ClaseHilos
{
    internal class _5Mutex
    {
        static Mutex mutex = new Mutex();
        static void HolaUAP()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} en Fila");
            mutex.WaitOne();
            Console.WriteLine($"{Thread.CurrentThread.Name} en atención");
            Thread.Sleep(3000);
            Console.WriteLine($"{Thread.CurrentThread.Name} Saliendo");
            mutex.ReleaseMutex();
        }
        internal static void Execute()
        {
            Thread task1 = new Thread(HolaUAP);
            task1.Name = "Hilo 1";
            Thread task2 = new Thread(HolaUAP);
            task2.Name = "Hilo 2";
            Thread task3 = new Thread(HolaUAP);
            task3.Name = "Hilo 3";
            Thread task4 = new Thread(HolaUAP);
            task4.Name = "Hilo 4";

            //Execute all tasks
            task1.Start();
            task2.Start();
            task3.Start();
            task4.Start();

            Console.ReadLine();
        }
    }
}
