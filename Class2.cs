using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Xml;

namespace test_project4
{
    public static class Class2
    {

        public static void ProgramIntro()
        {
            System.Console.WriteLine("shoe list application. linq to xml.");
        }

        public static void ProgramSelectOptions()
        {
            int userOption;
            do {
                Console.WriteLine("program option:");
                Console.WriteLine("1 - display all shoe items" + "\n" +
                    "2 - get shoe item by id" + "\n" +
                    "3 - add new shoe item" + "\n" +
                    "4 - edit shoe item" + "\n" +
                    "5 - delete shoe item" + "\n" +
                    "0 - exit application");
                Console.Write("select option: ");
                
                string strUserOption = Console.ReadLine().ToString().Trim();
                while (!int.TryParse(strUserOption, out userOption))
                {
                    System.Console.WriteLine("value entered not valid. try again.");
                    System.Console.Write("select option: ");
                    strUserOption = Console.ReadLine().ToString().Trim();
                }
                switch (userOption)
                {
                    case 1:
                        XmlDisplayAllShoeList();
                        break;
                    case 2:
                        XmlDisplayShoeItemById();
                        break;
                    case 3:
                        XmlAddNewShoeList();
                        break;
                    case 4:
                        XmlEditShoeList();
                        break;
                    case 5:
                        XmldeleteShoeList();
                        break;
                }
            } while (userOption != 0);

        }

        public static void XmlDisplayShoeItemById()
        {
            int shoeId;
            //prompt user for shoe id for edit
            System.Console.Write("enter shoe id:");
            string strShoeId = System.Console.ReadLine().ToString().Trim();
            while (!int.TryParse(strShoeId, out shoeId))
            {
                System.Console.WriteLine("shoe id value entered not valid. try again.");
                System.Console.Write("shoe id: ");
                strShoeId = Console.ReadLine().ToString().Trim();
            }
            string data_folder = Environment.CurrentDirectory;
            string folder_name = "xml_data";
            string data_file_name = "ShoeInventory.xml";
            string log_file_name = "ShoeInventoryLog.xml";
            string data_file_path = data_folder + "\\" + folder_name + "\\" + data_file_name;
            string log_file_path = data_folder + "\\" + folder_name + "\\" + log_file_name;
            XDocument xDocumentDataFileName = XDocument.Load(data_file_path);
            // Find the shoe list item with Id = idEdit
            XElement xElementShoeItem = xDocumentDataFileName.Descendants("ShoeList")
                          .FirstOrDefault(s => s.Element("Id").Value.ToString() == shoeId.ToString());
            if (xElementShoeItem != null)
            {
                string shoeName = xElementShoeItem.Element("ShoeName")?.Value;
                string shoeDescription = xElementShoeItem.Element("ShoeDescription")?.Value;
                string shoePrice = xElementShoeItem.Element("ShoePrice")?.Value;
                string shoeSize = xElementShoeItem.Element("ShoeSize")?.Value;
                string shoeQuantity = xElementShoeItem.Element("Quantity").Value;
                System.Console.WriteLine("shoe id: " + shoeId.ToString());
                System.Console.WriteLine("shoe name: " + shoeName);
                System.Console.WriteLine("shoe description: " + shoeDescription);
                System.Console.WriteLine("shoe price: " + shoePrice);
                System.Console.WriteLine("shoe size: " + shoeSize);
                System.Console.WriteLine("quantity: " + shoeQuantity);
                System.Console.WriteLine();

                //open xml log file
                XDocument xDocumentLogFileName = XDocument.Load(log_file_path);
                //write to xml log file
                int newLogId = xDocumentLogFileName.Descendants("LogMessage")
                               .Select(s => (int?)s.Attribute("id")) // Extract ID
                               .Max() ?? 0;
                newLogId++;
                string newLogMessage = DateTime.Now.ToString() + " execute command to view shoe item by id.";
                XElement xElementLogMessage = new XElement("LogMessage", newLogMessage, new XAttribute("id", newLogId.ToString()));
                xDocumentLogFileName.Root.Add(xElementLogMessage);
                xDocumentLogFileName.Save(log_file_path);

            } else {
                System.Console.WriteLine("shoe  with id "+shoeId.ToString() + " not found.");
                System.Console.WriteLine();
            }
        }

        public static void XmlDisplayAllShoeList()
        {
            string data_folder = Environment.CurrentDirectory;
            string folder_name = "xml_data";
            string data_file_name = "ShoeInventory.xml";
            string log_file_name = "ShoeInventoryLog.xml";
            string data_file_path = data_folder + "\\" + folder_name + "\\" + data_file_name;
            string log_file_path = data_folder + "\\" + folder_name + "\\" + log_file_name;
            XDocument xDocumentDataFileName = XDocument.Load(data_file_path);
            var varShoeList = xDocumentDataFileName.Descendants("ShoeList").Select(s => new
            {
                id = s.Element("Id")?.Value ?? "n/a",
                shoeName = s.Element("ShoeName")?.Value ?? "n/a",
                shoeDescription = s.Element("ShoeDescription")?.Value ?? "n/a",
                shoePrice = s.Element("ShoePrice")?.Value ?? "n/a",
                shoeSize = s.Element("ShoeSize")?.Value ?? "n/a",
                quantity = s.Element("Quantity")?.Value ?? "n/a"
            });

            foreach (var item in varShoeList)
            {
                System.Console.WriteLine("shoe id: " + item.id);
                System.Console.WriteLine("shoe name: " + item.shoeName);
                System.Console.WriteLine("shoe description: " + item.shoeDescription);
                System.Console.WriteLine("shoe price: " + item.shoePrice);
                System.Console.WriteLine("quantity: " + item.quantity);
                System.Console.WriteLine(); 
            }
            //open xml log file
            XDocument xDocumentLogFileName = XDocument.Load(log_file_path);
            //write to xml log file
            int newLogId = xDocumentLogFileName.Descendants("LogMessage")
                           .Select(s => (int?)s.Attribute("id")) // Extract ID
                           .Max() ?? 0;
            newLogId++;
            string newLogMessage = DateTime.Now.ToString() + " execute command to view all shoe list.";
            XElement xElementLogMessage = new XElement("LogMessage", newLogMessage, new XAttribute("id", newLogId.ToString()));
            xDocumentLogFileName.Root.Add(xElementLogMessage);
            xDocumentLogFileName.Save(log_file_path);
        }

        public static void XmlAddNewShoeList()
        {
            string shoeName;
            string shoeDescription;
            double shoePrice;
            int shoeSize;
            int shoeQuantity;
            //prompt user for new shoe details
            System.Console.WriteLine("Please provide details for new show item.");
            Console.Write("shoe name:");
            shoeName = Console.ReadLine().ToString().Trim();
            Console.Write("shoe description:");
            shoeDescription = Console.ReadLine().ToString().Trim();
            Console.Write("shoe price:");
            string strShoePrice = Console.ReadLine().ToString().Trim();
            while (!double.TryParse(strShoePrice, out shoePrice))
            {
                System.Console.WriteLine("shoe price value entered not valid. try again.");
                System.Console.Write("shoe price: ");
                strShoePrice = Console.ReadLine().ToString().Trim();
            }
            Console.Write("shoe size:");
            string strShoeSize = Console.ReadLine().ToString().Trim();
            while (!int.TryParse(strShoeSize, out shoeSize))
            {
                System.Console.WriteLine("shoe size value entered not valid. try again.");
                System.Console.Write("shoe size: ");
                strShoeSize = Console.ReadLine().ToString().Trim();
            }
            Console.Write("shoe quantity:");
            string strShoeQuantity = Console.ReadLine().ToString().Trim();
            while (!int.TryParse(strShoeQuantity, out shoeQuantity))
            {
                System.Console.WriteLine("shoe quantity value entered not valid. try again.");
                System.Console.Write("shoe quantity: ");
                strShoeQuantity = Console.ReadLine().ToString().Trim();
            }
            string data_folder = Environment.CurrentDirectory;
            string folder_name = "xml_data";
            string data_file_name = "ShoeInventory.xml";
            string log_file_name = "ShoeInventoryLog.xml";
            string data_file_path = data_folder + "\\" + folder_name + "\\" + data_file_name;
            string log_file_path = data_folder + "\\" + folder_name + "\\" + log_file_name;
            XDocument xDocumentDataFileName = XDocument.Load(data_file_path);
            //find the highest ID value and increment it
            int newId = xDocumentDataFileName.Descendants("ShoeList")
                           .Select(s => (int?)s.Element("Id")) 
                           .Max() ?? 0;
            //increment for uniqueness
            newId++; 
            //description attribute for element id 
            string newIdDescriptionAttrbute = DateTime.Now.ToString() + ": new shoe item added.";
            //create a new shoe list element item
            XElement xElementShoeList = new XElement("ShoeList",
                new XElement("Id", newId.ToString(), new XAttribute("description", newIdDescriptionAttrbute)),
                new XElement("ShoeName", shoeName, new XAttribute("description", "shoe name description")),
                new XElement("ShoeDescription", shoeDescription, new XAttribute("description", "shoe description description")),
                new XElement("ShoePrice", shoePrice.ToString("F2"), new XAttribute("description", "shoe price dscription")),
                new XElement("ShoeSize", shoeSize, new XAttribute("description", "shoe size description")),
                new XElement("Quantity", shoeQuantity, new XAttribute("description", "shoe quantity description"))
            );
            //add the new shoe item to the document
            xDocumentDataFileName.Root.Add(xElementShoeList);
            //save the updated xml
            xDocumentDataFileName.Save(data_file_path);
            //open and edit xml log file
            XDocument xDocumentLogFileName = XDocument.Load(log_file_path);
            int newLogId = xDocumentLogFileName.Descendants("LogMessage")
                           .Select(s => (int?)s.Attribute("id")) // Extract ID
                           .Max() ?? 0;
            newLogId++;
            string newLogMessage = DateTime.Now.ToString() + " new shoe item added with id " + newId.ToString();
            XElement xElementLogMessage = new XElement("LogMessage", newLogMessage, new XAttribute("id", newLogId.ToString()));
            xDocumentLogFileName.Root.Add(xElementLogMessage);
            xDocumentLogFileName.Save(log_file_path);
        }

        public static void XmlEditShoeList()
        {
            int shoeId;
            string shoeName;
            string shoeDescription;
            double shoePrice;
            int shoeSize;
            int shoeQuantity;
            //prompt user for shoe id for edit
            System.Console.Write("enter id of shoe name for edit:");
            string strShoeId = System.Console.ReadLine().ToString().Trim();
            while (!int.TryParse(strShoeId, out shoeId))
            {
                System.Console.WriteLine("shoe id value entered not valid. try again.");
                System.Console.Write("shoe id: ");
                strShoeId = Console.ReadLine().ToString().Trim();
            }
            System.Console.Write("shoe name: ");
            shoeName = System.Console.ReadLine().ToString().Trim();
            System.Console.Write("shoe description: ");
            shoeDescription = System.Console.ReadLine().ToString().Trim();
            Console.Write("shoe price:");
            string strShoePrice = Console.ReadLine().ToString().Trim();
            while (!double.TryParse(strShoePrice, out shoePrice))
            {
                System.Console.WriteLine("shoe price value entered not valid. try again.");
                System.Console.WriteLine("shoe price: ");
                strShoePrice = Console.ReadLine().ToString().Trim();
            }
            Console.Write("shoe size:");
            string strShoeSize = Console.ReadLine().ToString().Trim();
            while (!int.TryParse(strShoeSize, out shoeSize))
            {
                System.Console.WriteLine("shoe size value entered not valid. try again.");
                System.Console.WriteLine("shoe size: ");
                strShoeSize = Console.ReadLine().ToString().Trim();
            }
            Console.Write("shoe quantity:");
            string strShoeQuantity = Console.ReadLine().ToString().Trim();
            while (!int.TryParse(strShoeQuantity, out shoeQuantity))
            {
                System.Console.WriteLine("shoe quantity value entered not valid. try again.");
                System.Console.WriteLine("shoe quantity: ");
                strShoeQuantity = Console.ReadLine().ToString().Trim();
            }
            string data_folder = Environment.CurrentDirectory;
            string folder_name = "xml_data";
            string data_file_name = "ShoeInventory.xml";
            string log_file_name = "ShoeInventoryLog.xml";
            string data_file_path = data_folder + "\\" + folder_name + "\\" + data_file_name;
            string log_file_path = data_folder + "\\" + folder_name + "\\" + log_file_name;
            XDocument xDocumentDataFileName = XDocument.Load(data_file_path);
            // Find the shoe list item with Id = idEdit
            XElement xElementShoeItem = xDocumentDataFileName.Descendants("ShoeList")
                                  .FirstOrDefault(s => s.Element("Id").Value.ToString() == shoeId.ToString());
            // Update the name if the shoe item exists
            if (xElementShoeItem != null)
            {
                if (shoeName != string.Empty)
                {
                    xElementShoeItem.Element("ShoeName").Value = shoeName;
                    xElementShoeItem.Element("ShoeName").Attribute("description").Value = DateTime.Now.ToString() + " shoe name update";
                }
                if (shoeDescription != string.Empty)
                {
                    xElementShoeItem.Element("ShoeDescription").Value = shoeDescription;
                    xElementShoeItem.Element("ShoeDescription").Attribute("description").Value = DateTime.Now.ToString() + " shoe description update";
                }
                if (shoePrice.ToString() != string.Empty)
                {
                    xElementShoeItem.Element("ShoePrice").Value = shoePrice.ToString("F2");
                    xElementShoeItem.Element("ShoePrice").Attribute("description").Value = DateTime.Now.ToString() + " shoe update update";
                }
                if (shoeSize.ToString() != string.Empty)
                {
                    xElementShoeItem.Element("ShoeSize").Value = shoeSize.ToString();
                    xElementShoeItem.Element("ShoeSize").Attribute("description").Value = DateTime.Now.ToString() + " shoe size update";
                }
                if (shoeQuantity.ToString() != string.Empty)
                {
                    xElementShoeItem.Element("Quantity").Value = shoeQuantity.ToString();
                    xElementShoeItem.Element("Quantity").Attribute("description").Value = DateTime.Now.ToString() + " quantity update";
                }
                xDocumentDataFileName.Save(data_file_path);
                //open and update xml log file
                XDocument xDocumentLogFileName = XDocument.Load(log_file_path);
                int newLogId = xDocumentLogFileName.Descendants("LogMessage")
                               .Select(s => (int?)s.Attribute("id")) // Extract ID
                               .Max() ?? 0;
                newLogId++;
                string newLogMessage = DateTime.Now.ToString() + " edit shoe item with id " + shoeId;
                XElement xElementLogMessage = new XElement("LogMessage", newLogMessage, new XAttribute("id", newLogId.ToString()));
                xDocumentLogFileName.Root.Add(xElementLogMessage);
                xDocumentLogFileName.Save(log_file_path);
            }
            else
            {
                Console.WriteLine("shoe list with id not found.");
            }
        }

        public static void XmldeleteShoeList()
        {
            int shoeId;
            System.Console.Write("enter id of shoe name for edit:"); 
            string strShoeId = System.Console.ReadLine().ToString().Trim();
            while (!int.TryParse(strShoeId, out shoeId))
            {
                System.Console.WriteLine("shoe id value entered not valid. try again.");
                System.Console.Write("shoe id: ");
                strShoeId = Console.ReadLine().ToString().Trim();
            }
            string data_folder = Environment.CurrentDirectory;
            string folder_name = "xml_data";
            string data_file_name = "ShoeInventory.xml";
            string log_file_name = "ShoeInventoryLog.xml";
            string data_file_path = data_folder + "\\" + folder_name + "\\" + data_file_name;
            string log_file_path = data_folder + "\\" + folder_name + "\\" + log_file_name;
            XDocument xDocumentDataFileName = XDocument.Load(data_file_path);
            // find the shoe list that has Id = idDelete
            XElement xElementShoeListDelete = xDocumentDataFileName.Descendants("ShoeList")
                                      .FirstOrDefault(s => (string)s.Element("Id") == shoeId.ToString());
            // remove shoe item if found
            if (xElementShoeListDelete != null)
            {
                //delete shoe item
                xElementShoeListDelete.Remove();
                //save changes
                xDocumentDataFileName.Save(data_file_path);
                Console.WriteLine("selected shoe list with id deleted.");
                //open and update xml log file
                XDocument xDocumentLogFileName = XDocument.Load(log_file_path);
                int newLogId = xDocumentLogFileName.Descendants("LogMessage")
                               .Select(s => (int?)s.Attribute("id")) // Extract ID
                               .Max() ?? 0;
                newLogId++;
                string newLogMessage = DateTime.Now.ToString() + " deleted shoe item with id " + shoeId.ToString();
                XElement xElementLogMessage = new XElement("LogMessage", newLogMessage, new XAttribute("id", newLogId.ToString()));
                xDocumentLogFileName.Root.Add(xElementLogMessage);
                xDocumentLogFileName.Save(log_file_path);
            }
            else
            {
                Console.WriteLine("shoe list with id not found.");
            }
        }

    }//end-class
}//end-namespace

