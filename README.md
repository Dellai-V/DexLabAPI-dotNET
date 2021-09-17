# DexLabAPI-dotNET
Class for API control on DexLab

Exchange : https://www.dexlab.space/

Docs : https://docs.dexlab.space/

# Installation
For build the program you need to use .NET 2.1 SDK : <a href="https://dotnet.microsoft.com/download">Download</a>

The following packages are required :
- <a href="https://restsharp.dev/">RestSharp</a>

<pre>
dotnet add package RestSharp
</pre>
# Available Functions
- GetAllMarket
- GetOrderBook
- GetAllPrice
- GetAllPrice24h
- GetPrice
- GetPrice24hAgo
- GetAllVolume
- GetVolume
- GetAllLastTrade
- GetTrade24h
- GetLastTrade24h
- GetAllBalance
- TransferToken
- PlaceOrder
- CancelOrder
- Settlement
- GetOpenOrders
- GetUnsettles

# Example
how to insert the list of all markets in a combo box:
<pre>
using DexLab;
    class TestClass
    {
    AllMarket Market;
    public void UploadMarket()
    {
        Market = DexLabAPI.GetAllMarket();
        if (Market.success == true)
        {
            for (int i = 0; i < Market.data.Count; i++)
            {
                combobox1.InsertText(i, Market.data[i].coin + "/" + Market.data[i].priceCurrency);
            }
        }
    }
    }
</pre>

How to view our wallet data in a nodeview:
<pre>
    public void UploadWallet()
    {
        Wallet = DexLabAPI.GetAllBalance();
        Gtk.NodeStore WalletStore = new Gtk.NodeStore(typeof(MyTreeNode));

        if (Wallet.success == true)
        {
            for (int i = 0; i < Wallet.data.Count; i++)
            {
                WalletStore.AddNode(new MyTreeNode(Wallet.data[i].coin, Wallet.data[i].total, Wallet.data[i].mintKey));
            }
        }

        nodeview1.AppendColumn("Asset", new Gtk.CellRendererText(), "text", 0);
        nodeview1.AppendColumn("Balance", new Gtk.CellRendererText(), "text", 1);
        nodeview1.AppendColumn("Mint Key", new Gtk.CellRendererText(), "text", 2);

        nodeview1.NodeStore = WalletStore;
        nodeview1.SetSizeRequest(800, 200);
    }

    [Gtk.TreeNode(ListOnly = true)]
    public class MyTreeNode : Gtk.TreeNode
    {
        public MyTreeNode(string coin, double total, string mintkey)
        {
            Coin = coin;
            Total = total;
            mintKey = mintkey;
        }

        [Gtk.TreeNodeValue(Column = 0)]
        public string Coin;

        [Gtk.TreeNodeValue(Column = 1)]
        public double Total;

        [Gtk.TreeNodeValue(Column = 2)]
        public string mintKey;
    }
</pre>
