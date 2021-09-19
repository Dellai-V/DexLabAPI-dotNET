using System;
using RestSharp;
using System.Collections.Generic;
namespace DexLab
{

    public class DexLabAPI
    {
        public static string url = "https://api.dexlab.space/";
        public static string key = "";

        public static AllMarket GetAllMarket()
        {
            var client = new RestClient(url);
            var request = new RestRequest("v1/pairs");
            return client.Get<AllMarket>(request).Data;
        }
        public static OrderBook GetOrderBook(string MarketAddress)
        {
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest("v1/orderbooks/" + MarketAddress);
            return client.Get<OrderBook>(request).Data;
        }
        public static AllPrices GetAllPrice()
        {
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest("v1/prices");
            return client.Get<AllPrices>(request).Data;
        }
        public static AllPrices24h GetAllPrice24h()
        {
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest("v1/prices/recent");
            return client.Get<AllPrices24h>(request).Data;
        }
        public static Prices GetPrice(string address)
        {
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest("v1/prices/" + address + "/last");
            return client.Get<Prices>(request).Data;
        }
        public static Prices GetPrice24hAgo(string address)
        {
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest("v1/prices/" + address + "/closing-price");
            return client.Get<Prices>(request).Data;
        }
        public static AllVolume GetAllVolume()
        {
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest("v1/volumes");
            return client.Get<AllVolume>(request).Data;
        }
        public static Volume GetVolume(string address)
        {
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest("v1/volumes/" + address);
            return client.Get<Volume>(request).Data;
        }
        public static AllTrade GetAllLastTrade()
        {
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest("v1/trades");
            return client.Get<AllTrade>(request).Data;
        }
        public static AllTrade GetTrade24h(string address)
        {
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest("v1/trades/" + address + "/24h");
            return client.Get<AllTrade>(request).Data;
        }
        public static Trade GetLastTrade24h(string address)
        {
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest("v1/trades/" + address + "/last");
            return client.Get<Trade>(request).Data;
        }
        public static Balance GetAllBalance()
        {
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest("v1/wallet/balances");
            request.AddHeader("x-wallet-private-key", key);
            return client.Get<Balance>(request).Data;
        }
        public static Transfer TransferToken(string From, string To, string TokenAddress, string Amount)
        {
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest("v1/wallet/transfer");
            request.AddHeader("x-wallet-private-key", key);
            request.AddJsonBody(new { from = From, to = To, tokenAddress = TokenAddress, amount = Amount }); ;
            return client.Post<Transfer>(request).Data;
        }
        public static Orders PlaceOrder(string Side, string Coin, string PriceCurrency, double Quantity, double Price, string OrderType)
        {
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest("v1/orders");
            request.AddHeader("x-wallet-private-key", key);
            request.AddJsonBody(new { side = Side, coin = Coin, priceCurrency = PriceCurrency, quantity = Quantity, price = Price, orderType = OrderType }); ;
            return client.Post<Orders>(request).Data;
        }
        public static Cancel CancelOrder(string orderId)
        {
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest("v1/orders/" + orderId + "/cancel");
            request.AddHeader("x-wallet-private-key", key);
            return client.Put<Cancel>(request).Data;
        }
        public static Settles Settlement(string Coin, string PriceCurrency)
        {
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest("v1/orders/settles");
            request.AddHeader("x-wallet-private-key", key);
            request.AddJsonBody(new { coin = Coin, priceCurrency = PriceCurrency });
            return client.Post<Settles>(request).Data;
        }
        public static OpenOrders GetOpenOrders(string Coin, string PriceCurrency)
        {
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest("v1/orders/open-orders");
            request.AddHeader("x-wallet-private-key", key);
            request.AddQueryParameter("coin", Coin);
            request.AddQueryParameter("priceCurrency", PriceCurrency);
            return client.Get<OpenOrders>(request).Data;
        }
        public static Unsettles GetUnsettles(string Coin, string PriceCurrency)
        {
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest("v1/orders/settles");
            request.AddHeader("x-wallet-private-key", key);
            request.AddQueryParameter("coin", Coin);
            request.AddQueryParameter("priceCurrency", PriceCurrency);
            return client.Get<Unsettles>(request).Data;
        }
    }

    public class DexLabTV
    {
        // ! \\ The chart API requires authentication. Contact Dexlab ( dev@dexlab.space )

        public static string url = "https://tv-api.dexlab.space";
        public static TVconfig GetBaseConfig()
        {
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest("v1/trade-history/tv/config");
            return client.Get<TVconfig>(request).Data;
        }
        public static TVhistory GetHistory(string Symbol,string From,string To,string Resolution)
        {
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest("v1/trade-history/tv/history");
            request.AddQueryParameter("symbol", Symbol);
            request.AddQueryParameter("from", From);
            request.AddQueryParameter("to", To);
            request.AddQueryParameter("resolution", Resolution);
            return client.Get<TVhistory>(request).Data;
        }
        public static TVsymbols GetSymbolInfo(string Symbol)
        {
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest("v1/trade-history/tv/history");
            request.AddQueryParameter("symbol", Symbol);
            return client.Get<TVsymbols>(request).Data;
        }
    }


    public class AllMarket
    {
        public bool success { get; set; }
        public List<ListData> data { get; set; }
        public class ListData
        {
            public string market { get; set; }
            public string coin { get; set; }
            public string priceCurrency { get; set; }
            public string address { get; set; }
            public string baseMint { get; set; }
            public string quoteMint;
            public string programId { get; set; }
        }
    }
    public class OrderBook
    {
        public bool success { get; set; }
        public List<ListData> data { get; set; }
        public class ListData
        {
            public double lastOrderPrice { get; set; }
            public List<Book> bids { get; set; }
            public List<Book> asks { get; set; }
            public class Book
            {
                public double price { get; set; }
                public double size { get; set; }
                public string side { get; set; }
            }
        }
    }
    public class AllPrices
    {
        public bool success { get; set; }
        public List<ListData> data { get; set; }
        public class ListData
        {
            public double price { get; set; }
            public string market { get; set; }
            public DateTime time { get; set; }
            public string market_address { get; set; }
        }
    }
    public class AllPrices24h
    {
        public bool success { get; set; }
        public List<ListData> data { get; set; }
        public class ListData
        {
            public string marketAddress { get; set; }
            public double closePrice { get; set; }
            public double price { get; set; }
            public double changePrice { get; set; }
            public double percent { get; set; }
        }
    }
    public class Prices
    {
        public bool success { get; set; }
        public ListData data { get; set; }
        public class ListData
        {
            public double price { get; set; }
            public string market { get; set; }
            public DateTime time { get; set; }
            public string marketAddress { get; set; }
        }
    }
    public class AllVolume
    {
        public bool success { get; set; }
        public List<ListData> data { get; set; }
        public class ListData
        {
            public string name { get; set; }
            public string address { get; set; }
            public string programId { get; set; }
            public double totalVolume { get; set; }
            public double sellVolume { get; set; }
            public double buyVolume { get; set; }
            public double highPrice { get; set; }
            public double lowPrice { get; set; }
        }
    }
    public class Volume
    {
        public bool success { get; set; }
        public ListData data { get; set; }
        public class ListData
        {
            public string market_name { get; set; }
            public string address { get; set; }
            public string programId { get; set; }
            public double price { get; set; }
            public ListSummary summary { get; set; }
            public ListLast24hOrder last24hOrder { get; set; }
            public class ListSummary
            {
                public double totalVolume { get; set; }
                public double sellVolume { get; set; }
                public double buyVolume { get; set; }
                public double highPrice { get; set; }
                public double lowPrice { get; set; }
            }
        }
        public class ListLast24hOrder
        {
            public bool exist { get; set; }
            public DateTime time { get; set; }
            public double price { get; set; }
            public double percent { get; set; }
        }
    }
    public class AllTrade
    {
        public bool success { get; set; }
        public List<ListData> data { get; set; }
        public class ListData
        {
            public int id { get; set; }
            public double price { get; set; }
            public double size { get; set; }
            public string market { get; set; }
            public string side { get; set; }
            public DateTime time { get; set; }
            public string orderId { get; set; }
            public double feeCost { get; set; }
            public string marketAddress { get; set; }
            public DateTime createdAt { get; set; }
        }
    }
    public class Trade
    {
        public bool success { get; set; }
        public List<ListData> data { get; set; }
        public class ListData
        {
            public int id { get; set; }
            public double price { get; set; }
            public double size { get; set; }
            public string market { get; set; }
            public string side { get; set; }
            public DateTime time { get; set; }
            public string orderId { get; set; }
            public double feeCost { get; set; }
            public string marketAddress { get; set; }
            public DateTime createdAt { get; set; }
        }
    }
    public class Balance
    {
        public bool success { get; set; }
        public List<ListData> data { get; set; }
        public class ListData
        {
            public string coin { get; set; }
            public string mintKey { get; set; }
            public int decimals { get; set; }
            public double total { get; set; }
        }
    }
    public class Orders
    {
        public bool success { get; set; }
        public List<ListData> data { get; set; }
        public class ListData
        {
            public string orderId { get; set; }
            public string txId { get; set; }
            public string side { get; set; }
            public string orderType { get; set; }
        }
    }
    public class Cancel
    {
        public bool success { get; set; }
        public List<ListData> data { get; set; }
        public class ListData
        {
            public string orderId { get; set; }
            public string coin { get; set; }
            public string priceCurrency { get; set; }
        }
    }
    public class OpenOrders
    {
        public bool success { get; set; }
        public List<ListData> data { get; set; }
        public class ListData
        {
            public string coin { get; set; }
            public string priceCurrency { get; set; }
            public List<ListOrders> orders { get; set; }
            public class ListOrders
            {
                public string orderId { get; set; }
                public double baseTokenFree { get; set; }
                public double baseTokenTotal { get; set; }
                public double quoteTokenFree { get; set; }
                public double quoteTokenTotal { get; set; }
            }
        }
    }
    public class Transfer
    {
        public bool success { get; set; }
        public List<ListData> data { get; set; }
        public class ListData
        {
            public string txid { get; set; }
        }
    }
    public class Settles
    {
        public bool success { get; set; }
        public List<ListData> data { get; set; }
        public class ListData
        {
            public string coin { get; set; }
            public string priceCurrency { get; set; }
            public string[] txIds { get; set; }
            public bool isSettlement { get; set; }
            public string message { get; set; }
        }
    }
    public class Unsettles
    {
        public bool success { get; set; }
        public List<ListData> data { get; set; }
        public class ListData
        {
            public string coin { get; set; }
            public string priceCurrency { get; set; }
            public List<ListSettles> settles { get; set; }
            public class ListSettles
            {
                public double baseTokenFree { get; set; }
                public double quoteTokenFree { get; set; }
            }
        }
    }
    public class TVconfig
    {
        public string[] supported_resolutions { get; set; }
        public bool supports_group_request { get; set; }
        public bool supports_marks { get; set; }
        public bool supports_search { get; set; }
        public bool supports_timescale_marks { get; set; }
    }
    public class TVsymbols
    {
        public string name { get; set; }
        public string ticker { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public string session { get; set; }
        public string exchange { get; set; }
        public string listed_exchange { get; set; }
        public string timezone { get; set; }
        public bool has_intraday { get; set; }
        public string[] supported_resolutions { get; set; }
        public double minmov { get; set; }
        public double pricescale { get; set; }
    }
    public class TVhistory
    {
        public string s { get; set; }
        public Int64[] t { get; set; }
        public double[] c { get; set; }
        public double[] o { get; set; }
        public double[] h { get; set; }
        public double[] l { get; set; }
        public double[] v { get; set; }
    }
}
