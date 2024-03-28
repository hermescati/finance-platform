namespace Expensier.API.Models
{
    public class APIKey
    {
        public string Key { get; }

        public APIKey( string key )
        {
            Key = key;
        }
    }
}
