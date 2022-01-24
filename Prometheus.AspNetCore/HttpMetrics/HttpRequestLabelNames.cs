namespace Prometheus.HttpMetrics
{
	/// <summary>
	/// Label names reserved for the use by the HTTP request metrics.
	/// </summary>
	public static class HttpRequestLabelNames
	{
		public const string Code = "code";
		public const string Method = "method";
		public const string Uri = "uri";

		public static readonly string[] All =
		{
			Code,
			Method,
			Uri
		};

		internal static readonly string[] PotentiallyAvailableBeforeExecutingFinalHandler =
		{
            // Always available, part of request.
            Method,
            // These two are available only in ASP.NET Core 3.
            Uri
		};

		// Labels that do not need routing information to be collected.
		internal static readonly string[] NonRouteSpecific =
		{
			Code,
			Method
		};
	}
}