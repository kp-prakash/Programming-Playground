namespace TaskManager.Common
{
    public static class Constants
    {
        public const string DefaultLegacyNamespace = "http://tempuri.org/";

        public static class CommonLinkRelValues
        {
            public const string All = "all";
            public const string CurrentPage = "currentPage";
            public const string NextPage = "nextPage";
            public const string PreviousPage = "previousPage";
            public const string Self = "self";
        }

        public static class CommonParameterNames
        {
            public const string PageNumber = "pageNumber";
            public const string PageSize = "pageSize";
        }

        public static class CommonRoutingDefinitions
        {
            public const string ApiSegmentName = "api";
            public const string ApiVersionSegmentName = "apiVersion";
            public const string CurrentApiVersion = "v1";
        }

        public static class MediaTypeNames
        {
            public const string ApplicationJson = "application/json";
            public const string ApplicationXml = "application/xml";
            public const string TextJson = "text/json";
            public const string TextXml = "text/xml";
        }

        public static class Paging
        {
            public const int DefaultPageNumber = 1;
            public const int MinPageNumber = 1;
            public const int MinPageSize = 1;
        }

        public static class RoleNames
        {
            public const string JuniorWorker = "JuniorWorker";
            public const string Manager = "Manager";
            public const string SeniorWorker = "SeniorWorker";
        }

        public static class SchemeTypes
        {
            public const string Basic = "basic";
        }
    }
}