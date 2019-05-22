using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tChappell_proj03
{
    class Program
    {
        static void Main(string[] args)
        {
            Records r = new Records();
            r.GetRecords();
            string choice;
            int num;
            do
            {
                Console.WriteLine("Would you like to do (1-4):");
                Console.WriteLine("1. Add");
                Console.WriteLine("2. View");
                Console.WriteLine("3. Delete");
                Console.WriteLine("4. Exit");
                choice = Console.ReadLine();
                num = CheckChoice(choice);
                switch (num)
                {
                    case 1:
                        r.Add();
                        break;
                    case 2:
                        r.View();
                        break;
                    case 3:
                        r.Delete();
                        break;
                }
                Console.WriteLine();
            } while (num != 4);
            r.WriteRecords();
        }

        static int CheckChoice(string input)
        {
            int num = -1;
            while (!int.TryParse(input, out num) || num < 1 || num > 4)
            {
                Console.WriteLine("Please enter a valid input (1-4)");
                input = Console.ReadLine();
            }
            return num;
        }
    }

    class Records
    {
        List<string> records = new List<string>();
        int size = 0;

        /**
         * Retrieves records from a file named "Records.txt." If the file does not exist it is then created
         */
        public void GetRecords()
        {
            
            if (!File.Exists(@"Records.txt"))
            {
                File.Create(@"Records.txt").Close();
                return;
            }
            using (StreamReader file = new StreamReader(@"Records.txt"))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    records.Add(line);
                    size++;
                }
            }
        }

        /**
         * Adds a record
         */
        public void Add()
        {
            Console.WriteLine("Enter what you would like to add:");
            records.Add(Console.ReadLine());
            Console.WriteLine("Record added.");
        }

        /**
         * Views a record at some index
         */
        public void View()
        {
            if (size == 0)
            {
                Console.WriteLine("No files to view");
            }
            else
            {
                Console.WriteLine("Enter which record you would like to view:");
                Console.WriteLine(records[CheckIndex(Console.ReadLine()) - 1]);
            }
        }

        /**
         * Deletes a record at some index
         */
        public void Delete()
        {
            if(size == 0)
            {
                Console.WriteLine("No files to delete");
            }
            else
            {
                Console.WriteLine("Enter what record you would like to delete:");
                records.RemoveAt(CheckIndex(Console.ReadLine()) - 1);
                size--;
                Console.WriteLine("Record deleted.");
            }           
        }

        /**
         * Checks if a user input is a valid index
         * @return int A valid index
         */
        public int CheckIndex(string index)
        {
            int num = -1;
            while(!int.TryParse(index, out num) || num < 1 || num > size)
            {
                Console.WriteLine("Please enter a valid index (1-" + size + "):");
                index = Console.ReadLine();
            }
            return num;
        }

        /**
         * Writes all records to a file called "Records.txt"
         */
        public void WriteRecords()
        {
            using (StreamWriter file = new StreamWriter(@"Records.txt"))
            {
                foreach (string record in records)
                {
                    file.WriteLine(record);
                }
            }       
        }
    }
}
