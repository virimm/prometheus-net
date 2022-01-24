namespace Prometheus.HttpClientMetrics
{
	/// <summary>
	/// Label names reserved for the use by the HttpClient metrics.
	/// </summary>
	public static class HttpClientRequestLabelNames
	{
		public const string Method = "method";
		public const string Uri = "uri";
		public const string Status = "status";

		public static readonly string[] All =
		{
			Method,
			Uri,
			Status
		};

		// The labels known before receiving the response.
		// Everything except the response status code, basically.
		public static readonly string[] KnownInAdvance =
		{
			Method,
			Uri
		};
	}
}