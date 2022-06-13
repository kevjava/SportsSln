namespace SportsStore.Infrastructure {

    public static class UrlEtensions {

        public static string PathAndQuery(this HttpRequest request) =>
            request.QueryString.HasValue ? 
                $"{request.Path}{request.QueryString}" : 
                request.Path.ToString();

    }
}