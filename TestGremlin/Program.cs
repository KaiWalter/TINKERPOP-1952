using Gremlin.Net.Driver;
using Gremlin.Net.Structure.IO.GraphSON;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGremlin
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length == 4)
            {
                Execute(args[0], args[1], args[2], args[3]).GetAwaiter().GetResult();
            }
        }

        static async Task<int> Execute(string hostName, string userName, string password, string graphStatement)
        {
            string output = null;

            var gremlinServer = new GremlinServer(
                hostName,
                443,
                enableSsl: true,
                username: userName,
                password: password);

            using (var gremlinClient = new GremlinClient(gremlinServer, new GraphSON2Reader(), new GraphSON2Writer(), GremlinClient.GraphSON2MimeType))
            {
                var resultSet = await gremlinClient.SubmitAsync<dynamic>(graphStatement);

                foreach (var result in resultSet)
                {
                    output = JsonConvert.SerializeObject(result);
                }
            }

            return 0;
        }
    }
}
