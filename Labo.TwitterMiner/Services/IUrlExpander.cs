namespace Labo.TwitterMiner.Services
{
    internal interface IUrlExpander
    {
        string ExpandUrl(string url, bool throwException = false);
    }
}