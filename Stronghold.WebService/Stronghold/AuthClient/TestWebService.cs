namespace Stronghold.AuthClient
{
    using CookComputing.XmlRpc;
    using System;

    public class TestWebService
    {
        public string GetTestString()
        {
            return XmlRpcProxyGen.Create<ITestProxy>().ping();
        }

        public static XmlRpcCardsResponse GiveCardExample(string userguid, int kingdomsworldID, string commaSeparatedCardStrings)
        {
            XmlRpcCardsRequest req = new XmlRpcCardsRequest {
                UserGUID = userguid,
                WorldID = kingdomsworldID.ToString(),
                CardString = commaSeparatedCardStrings
            };
            return XmlRpcCardsProvider.CreateForEndpoint("http", "174.129.182.8", "8080", "secureservices/cards.php").giveCards(req, null, null, 0x7530);
        }
    }
}

