using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace HelperFramework.Http.UrlShortner
{

	/// <summary>
	/// Google Url Shortner API
	/// </summary>
	public class GoogleApi
	{

		#region Private Properties;

		private const String OAUTH_URL = @"https://www.googleapis.com/urlshortener/v1/url";

		#endregion Private Properties;

		#region Methods;

		/// <summary>
		/// Shorten Url
		/// </summary>
		/// <param name="url">Url to shorten</param>
		/// <param name="key">API Key</param>
		/// <returns>Short Url</returns>
		public static String Shorten(String url, String key = "")
		{
			String post = "{\"longUrl\": \"" + url + "\"}";
			Byte[] postBuffer = Encoding.UTF8.GetBytes(post);

			String oauthUrl = OAUTH_URL;
			if (!String.IsNullOrWhiteSpace(key))
			{
				oauthUrl += "?key=" + key;
			}

			try
			{
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(oauthUrl);
				request.ServicePoint.Expect100Continue = false;
				request.Method = "POST";
				request.ContentLength = postBuffer.Length;
				request.ContentType = "application/json";
				request.Headers.Add("Cache-Control", "no-cache");

				using (Stream requestStream = request.GetRequestStream())
				{
					requestStream.Write(postBuffer, 0, postBuffer.Length);
				}

				using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
				{
					using (Stream responseStream = response.GetResponseStream())
					{
						if (responseStream != null)
							using (StreamReader responseReader = new StreamReader(responseStream))
							{
								String json = responseReader.ReadToEnd();
								url = Regex.Match(json, @"""id"": ?""(?<id>.+)""").Groups["id"].Value;
							}
					}
				}
			}
			catch (Exception ex)
			{
				// if Google's URL Shortner is down...
				System.Diagnostics.Debug.WriteLine(ex.Message);
				System.Diagnostics.Debug.WriteLine(ex.StackTrace);
			}

			return url;
		}

		#endregion Methods;
		
	}
}
