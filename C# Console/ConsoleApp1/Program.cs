
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            string jsonText = GetJson("http://agl-developer-test.azurewebsites.net/people.json");
            JavaScriptSerializer json = new JavaScriptSerializer();
            var people = json.Deserialize<person[]>(jsonText);
            List<string> gender = people.Select(t => t.gender).Distinct().ToList();
            foreach (var item in gender)
            {
                Console.WriteLine(item);
                List<pet> myPets = people.Where(x => x.gender == item && x.pets != null).SelectMany(x => x.pets).OrderBy(x => x.name).ToList();
                foreach (var item1 in myPets)
                {
                    if (item1.type == "Cat")
                    {
                        Console.WriteLine("。" + item1.name);
                    }

                }
            }

            Console.ReadLine();
        }

        public static string GetJson(string Url)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Proxy = null;
            request.KeepAlive = false;
            request.Method = "GET";
            request.ContentType = "application/json; charset=UTF-8";
            request.AutomaticDecompression = DecompressionMethods.GZip;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            if (response != null)
            {
                response.Close();
            }
            if (request != null)
            {
                request.Abort();
            }
            return retString;
        }
    }
    public class person
    {
        public string name { get; set; }
        public string gender { get; set; }
        public int age { get; set; }
        public List<pet> pets { set; get; }
    }
    public class pet
    {
        public string name { get; set; }
        public string type { get; set; }
    }
}

