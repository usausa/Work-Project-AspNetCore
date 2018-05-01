using System.Threading.Tasks;
using SoapExample.Models;

namespace SoapExample.WebServices
{
    using System.ServiceModel;

    [ServiceContract]
    public class SampleService
    {
        [OperationContract]
        public int Add(int x, int y)
        {
            return x + y;
        }

        // XmlSerializer only ?
        [OperationContract]
        public SampleResponse Complex(SampleRequest request)
        {
            return new SampleResponse { IntValue = request.IntValue, StringValue = request.StringValue };
        }

        [OperationContract]
        public async Task<string> Async()
        {
            await Task.Delay(1000);

            return "Hello";
        }
    }
}
