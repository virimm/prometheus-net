using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Prometheus.HttpMetrics
{
	internal sealed class HttpRequestDurationMiddleware : HttpRequestMiddlewareBase<ICollector<IHistogram>, IHistogram>
	{
		private readonly RequestDelegate _next;

		public HttpRequestDurationMiddleware( RequestDelegate next, HttpRequestDurationOptions? options )
			: base( options, options?.Histogram ) {
			_next = next ?? throw new ArgumentNullException( nameof( next ) );
		}

		public async Task Invoke( HttpContext context ) {
			var stopWatch = ValueStopwatch.StartNew();

			// We need to write this out in long form instead of using a timer because routing data in
			// ASP.NET Core 2 is only available *after* executing the next request delegate.
			// So we would not have the right labels if we tried to create the child early on.
			try {
				await _next( context );
			} finally {
				CreateChild( context ).Observe( stopWatch.GetElapsedTime().TotalSeconds );
			}
		}

		protected override string[] DefaultLabels => HttpRequestLabelNames.All;

		protected override ICollector<IHistogram> CreateMetricInstance( string[] labelNames ) => MetricFactory.CreateHistogram(
			"http_in_requests_duration",
			"The duration in seconds of HTTP requests processed by an ASP.NET Core application.",
			new HistogramConfiguration {
				LabelNames = labelNames
			} );
	}
}