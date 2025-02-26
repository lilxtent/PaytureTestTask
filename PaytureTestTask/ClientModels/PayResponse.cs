using System.Xml.Serialization;

namespace PaytureTestTask.ClientModels;

[XmlRoot("Pay")]
public class PayResponse
{
    [XmlAttribute("OrderId")]
    public string OrderId { get; set; }

    [XmlAttribute("Key")]
    public string Key { get; set; }

    [XmlAttribute("Success")]
    public string Success { get; set; }

    [XmlAttribute("ErrCode")]
    public string ErrCode { get; set; }
}