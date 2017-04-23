using System;

namespace MapperExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            //SampleMappingTests.Run();
            //ConfigureMappingTests.Run();
            EfMappingTests.Run();

            Console.WriteLine("Tests complete");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}
