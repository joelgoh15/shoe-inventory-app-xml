using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace test_project4
{
    public static class Class1
    {
        public static void SortElementsXMLDocumentWithElementValue()
        {
            // path to \bin\Debug folder
            string startupPath = Environment.CurrentDirectory;
            string folderName = "xml_data";
            string fileName = "XMLFileStudents.xml";
            string filePath = startupPath + "\\" + folderName + "\\" + fileName;

            XDocument fileXDocument = XDocument.Load(filePath);

            Console.WriteLine("original xml file:");

            Console.WriteLine(fileXDocument);


            // Sort students by Age element in ascending order
            var sortedStudents = fileXDocument.Descendants("Student")
                                    .OrderBy(s => int.Parse(s.Element("Age").Value));

            // Create a new XDocument with sorted elements
            XDocument sortedDoc = new XDocument(
                new XElement("Students", sortedStudents)
            );

            // Save the sorted XML document
            sortedDoc.Save(filePath);

            Console.WriteLine("after sort by age value (xml document):");


            // Display the sorted XML
            Console.WriteLine(sortedDoc);


        }


        public static void FnCheckElementExist()
        {
            // path to \bin\Debug folder
            string startupPath = Environment.CurrentDirectory;
            string folderName = "xml_data";
            string fileName = "XMLFileStudents.xml";
            string filePath = startupPath + "\\" + folderName + "\\" + fileName;

            // Load an XML file into XDocument
            XDocument fileXDocument = XDocument.Load(filePath);

            // Check if a "Name" element exists anywhere in the XML
            bool nameExists = fileXDocument.Descendants("Name").Any();

            if (nameExists)
            {
                Console.WriteLine("The 'Name' element exists!");
            }
            else
            {
                Console.WriteLine("The 'Name' element does not exist.");
            }


        }


        public static void MergeOperations()
        {
            //// Load both XML documents
            //XDocument doc1 = XDocument.Load("students1.xml");
            //XDocument doc2 = XDocument.Load("students2.xml");

            //// Merge Student elements while removing duplicates by ID
            //var mergedStudents = doc1.Root.Elements("Student")
            //                    .Concat(doc2.Root.Elements("Student"))
            //                    .GroupBy(s => (string)s.Attribute("ID") && (string)s.Attribute(""))
            //                    .Select(g => g.First()); // Pick the first unique instance

            //// Create a new XML document with filtered students
            //XDocument mergedDoc = new XDocument(
            //    new XElement("Students", mergedStudents)
            //);

            //// Save the cleaned merged XML document
            //mergedDoc.Save("merged_students_clean.xml");

            //// Display the cleaned XML
            //Console.WriteLine(mergedDoc);


        }







    }//end-class
}//end-namespace
