using System.ServiceModel;

namespace DataEncodingApi.Interfaces.IServices
{
  [ServiceContract]
  public interface IEncodingService
  {
      [OperationContract]
      string EncodeToUtf8(string input);

      // [OperationContract]
      // string EncodeToTis620(string input);
  }
}
