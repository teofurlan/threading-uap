namespace ClaseHilos
{
   internal class Producto
   {
      public string Nombre { get; set; }
      public decimal PrecioUnitarioDolares { get; set; }
      public int CantidadEnStock { get; set; }

      public Producto(string nombre, decimal precioUnitario, int cantidadEnStock)
      {
         Nombre = nombre;
         PrecioUnitarioDolares = precioUnitario;
         CantidadEnStock = cantidadEnStock;
      }
   }
   internal class Solution //reference: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/lock
   {

      static List<Producto> productos = new List<Producto>
        {
            new Producto("Camisa", 10, 50),
            new Producto("Pantalón", 8, 30),
            new Producto("Zapatilla/Champión", 7, 20),
            new Producto("Campera", 25, 100),
            new Producto("Gorra", 16, 10)
        };

      static int precio_dolar = 500;
      static Mutex mutex = new Mutex();

      static void Tarea1()
      {
         {
            Console.WriteLine("Iniciando Tarea 1: Actualización de stock");
            foreach (var producto in productos)
            {
               producto.CantidadEnStock += 10;
            }
            Console.WriteLine("Finalización de la Tarea 1");
         }

      }
      static void Tarea2()
      {
         lock (mutex)
         {
            Console.WriteLine("Iniciando Tarea 2: Actualización precio dolar");
            precio_dolar += 745;
            Console.WriteLine("Finalización de la Tarea 2");
         }

      }

      static void Tarea3()
      {
         lock (mutex)
         {
            Console.WriteLine("Iniciando Tarea 3: Actualizando la lista de productos por según la inflación");
            foreach (var producto in productos)
            {
               producto.PrecioUnitarioDolares *= 1.1m;
            }
            Console.WriteLine("Finalización de la Tarea 3");
         }
      }
      static void Tarea4()
      {
         lock (mutex)
         {
            Console.WriteLine("Iniciando Tarea 4: Generar Informe");
            foreach (var producto in productos)
            {
               Console.WriteLine($"Producto: {producto.Nombre}, Cantidad en Stock: {producto.CantidadEnStock}, Precio Total: {producto.PrecioUnitarioDolares * producto.CantidadEnStock * precio_dolar}");
            }
            Console.WriteLine("Finalización de la Tarea 4");
         }
      }

      internal static void Execute()
      {
         Thread task1 = new Thread(Tarea1);
         Thread task2 = new Thread(Tarea2);
         Thread task3 = new Thread(Tarea3);
         Thread task4 = new Thread(Tarea4);

         // Ejecutar las tareas 1 y 2 concurrentemente
         task1.Start();
         task2.Start();

         // Esperamos a que las tareas 1 y 2 finalicen
         task1.Join();
         task2.Join();

         // Luego se ejecuta la tarea 3
         task3.Start();
         task3.Join();

         // Por último se genera el informe
         task4.Start();

         Console.ReadLine();
      }

   }
}