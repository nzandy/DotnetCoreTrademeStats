using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using DotnetCoreTrademeStats.ClassLib.Models;
using DotnetCoreTrademeStats.ClassLib.Attributes;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace DotnetCoreTrademeStats.ClassLib.Connectors {
	public abstract class TrademeApiConnector<TEntity> where TEntity : TrademeListing {

		const string ConsumerKey = "10E46E6F1019249C17FDF2DE6F6787EA";
		const string ConsumerSecret = "7560BA2CAB4AF54FF2300F5D4C327E74";
		const string SignatureMethod = "PLAINTEXT";

		protected JsonSerializerSettings Settings;

		public HttpClient Client { get; set; }
		public JsonSerializerSettings JsonSettings { get; set; }
		public string RelativeUri { get; set; }
		public IEnumerable<TEntity> Listings { get; set; }
		protected readonly ILogger _logger;

		protected TrademeApiConnector(ILogger logger) {
			
			RelativeUri = GetApiUri();
			_logger = logger;
			Client = new HttpClient();
			Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("OAuth", GetAuthorizationHeader());
			Client.BaseAddress = new Uri("https://api.tmsandbox.co.nz/");
			Client.DefaultRequestHeaders.Accept.Clear();
			Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			Settings = new JsonSerializerSettings() {
				Error = delegate (object sender, ErrorEventArgs errorArgs) {
					Console.WriteLine(errorArgs.ErrorContext.Error);
				}
			};
		}

		private string GetApiUri(){
			var attr = typeof(TEntity).GetTypeInfo().GetCustomAttribute<TrademeListingAttribute>();
			if (attr == null){
				throw new Exception("The specified type does not have a TrademeListingAttribute");
			}
			return attr.ApiPath;
		}

		public abstract IEnumerable<TEntity> GetListings(); 

		public static string GetAuthorizationHeader() {
			StringBuilder str = new StringBuilder();
			str.AppendFormat(@"oauth_consumer_key=""{0}"", ", ConsumerKey);
			str.AppendFormat(@"oauth_signature_method=""{0}"", ", SignatureMethod);
			str.AppendFormat(@"oauth_signature=""{0}&""", ConsumerSecret);
			return str.ToString();
		}
	}
}
