using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RabbitEventHubTest.Consumers
{
    // public interface IngestFileReceived
    // {
    //     //string Url { get; }
    //      object data { get; }
    // }
    //
    public record IngestFileReceived
    {
        public data data { get; set; }
    }
    
    public record data
    {
        public string api { get; set; }
        public string clientRequestId { get; set; }
        public string requestId { get; set; }
        public string eTag { get; set; }
        public string contentType { get; set; }
        public string contentLength { get; set; }
        public string blobType { get; set; }
        public string url { get; set; }
        public string sequencer { get; set; }
        public storageDiagnostics storageDiagnostics { get; set; }
    }

    public record storageDiagnostics
    {
        public string batchId { get; set; } 
    }
}