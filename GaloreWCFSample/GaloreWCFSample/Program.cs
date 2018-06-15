using System;

namespace Kognifai.Galore.Sample
{
    class Program
    {
        private static string _nodeLevel0 = "Level1";
        private static string _nodeLevel1 = "Level2";
        private static string _nodeLevel2 = "Level3";

        static void Main(string[] args)
        {
            Console.WriteLine("Please choose your option as bellow:");
            Console.WriteLine("1 - for creating simple asset model Galore.");
            Console.WriteLine("2 - for write to Galore.");
            Console.WriteLine("3 - for read from Galore.");
            Console.Write("Your input : ");

            var input = Console.ReadLine();
            if (int.TryParse(input, out int option))
            {
                try
                {
                    switch (option)
                    {
                        case 1:
                            {
                                //Create an asset model in Galore
                                GaloreAssetCreator creator = new GaloreAssetCreator();
                                var task = creator.CreateNode(_nodeLevel0, _nodeLevel1, _nodeLevel2);
                                task.Wait();
                                break;
                            }
                        case 2:
                            {
                                //Write data to the asset model created from above step
                                GaloreWriter writter = new GaloreWriter();
                                var task = writter.WriteValue($"{_nodeLevel0}/{_nodeLevel1}/{_nodeLevel2}", 1.23);
                                task.Wait();
                                break;
                            }
                        case 3:
                            {
                                //Read data from the asset model from above step
                                GaloreReader reader = new GaloreReader();
                                var task = reader.ReadValue($"{_nodeLevel0}/{_nodeLevel1}/{_nodeLevel2}");
                                task.Wait();
                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Unable to parse user option.");
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }
    }
}
