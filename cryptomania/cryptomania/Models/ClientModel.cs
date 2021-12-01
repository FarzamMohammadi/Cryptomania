using Amazon;
using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cryptomania.Models
{
    public class ClientModel
    {
        private static string AccessKeyID = "AKIAW6YHDHANOHPKQLD2";
        private static string SecretKey = "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";
        public static RegionEndpoint Region = RegionEndpoint.GetBySystemName("ca-central-1");
        public static string RDSConnStr = GetConnStr("/Client/RDSConnStr").Result;

        public static async Task<string> GetConnStr(string parameter)
        {
            //Gets Parameter store value of DB connection string 
            var ssmClient = new AmazonSimpleSystemsManagementClient(AccessKeyID, SecretKey, Region);

            var response = await ssmClient.GetParameterAsync(new GetParameterRequest
            {
                Name = parameter,
                WithDecryption = true
            });
            return response.Parameter.Value;
        }
    }
}
