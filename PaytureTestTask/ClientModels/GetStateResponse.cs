using System.Xml.Serialization;

namespace PaytureTestTask.ClientModels;

[XmlRoot("GetState")]
public class GetStateResponse
{
    [XmlAttribute("Success")]
    public string Success { get; set; }
    
    [XmlAttribute("OrderId")]
    public string OrderId { get; set; }
    
    [XmlAttribute("Forwarded")]
    public string Forwarded { get; set; }

    [XmlAttribute("State")]
    public string State { get; set; }

    [XmlAttribute("ErrCode")]
    public string  ErrCode { get; set; }
}