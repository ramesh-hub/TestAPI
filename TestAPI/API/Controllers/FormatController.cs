using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;

namespace TestAPI.API.Controllers
{
    public class FormatController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>        
        public string Post()
        {
            List<string> lines = new List<string>();
            var response = new ResponseMessage();
            //Source file is added for local project directory, but can come from anywhere.
            var outFile = @"D:\\TestAPI\\outFile.txt";

            try
            {
                var httpReq = HttpContext.Current.Request;

                if(httpReq.Files != null && httpReq.Files.Count > 0)
                {
                    HttpPostedFile postedFile = httpReq.Files[0];
                    Stream stream = postedFile.InputStream;

                    using (StreamReader file = new StreamReader(stream))
                    {
                        string line = string.Empty;

                        while(!string.IsNullOrEmpty(line = file.ReadLine()))
                        {
                            lines.Add(line);
                        }
                    }

                    if (lines.Count == 0)
                    {
                        response.AddMessage("No entries found in the file.");
                        return JsonConvert.SerializeObject(response);
                    }

                    if (File.Exists(outFile))
                        File.Delete(outFile);

                    using(StreamWriter writer = File.CreateText(outFile))
                    {
                        lines.ForEach(line => writer.WriteLine(string.Format("{0}{1}{2}", "[", line.Replace("\",\"", "][").Replace("\",", "][").Replace(",\"", "][").Replace("\"", ""), "]")));

                        response.AddMessage(@"Formatted file is outFile.txt");
                    }
                }
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.AddMessage("Failed to format the file content: " + ex.Message);
            }

            return JsonConvert.SerializeObject(response);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}