namespace MicroSample.Api.Configuration.ServiceBusQueue
{
  internal class ServiceBusQueueConfiguration
  {
    public const string Section = "ServiceBusQueue";

    public bool IsEnabled { get; set; }
    public string ConnectionString { get; set; }
    public string EntityPath { get; set; }
  }
}
