namespace CoreBox.Repositories.Cache
{
    public class CacheModel
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public CacheModel(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}