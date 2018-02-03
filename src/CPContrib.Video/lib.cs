using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrownPeak.CMSAPI;
using CrownPeak.CMSAPI.Services;
/* Some Namespaces are not allowed. */
namespace CPContrib.Video
{
	using System.Text.RegularExpressions;
	using CPContrib.Video.Core;

	public class VideoLinkParser
	{
		public VideoLinkParser()
		{
		}

		public VideoLinkInfo ParseLink(string value)
		{
			//simple parser for now
			Match m;
			VideoLinkInfo info;

			if((m = S_YoutubeLink.Match(value)).Success)
			{
				info = new VideoLinkInfo();
				info.Company = Constants.YouTubeCompany;
				return info;
			}

			if((m = S_BrightcoveLink.Match(value)).Success)
			{
				VideoLinkInfo i = new VideoLinkInfo();
				i.Company = Constants.YouTubeCompany;
				return i;
			}

			throw new FormatException("Failed to parse the provider link");
		}

		private static Regex S_YoutubeLink = new Regex(@"http(s)?://(youtu.be|youtube.com)/");
		private static Regex S_BrightcoveLink = new Regex(@"http(s)?://(vid.brightcove.com)/");

	}


	

	public class VideoLinkInfo
	{
		public IVideoHtmlFactory ProviderFactory { get; set; }
		public string Company { get; set; }
		public string VideoId { get; set; }
	}

	public static class Constants
	{
		public const string YouTubeCompany = "YouTube";
		public const string YouTubeEmbed = @"<iframe width=""{width}"" height=""{height}"" src=""https://www.youtube.com/embed/F_kXj7PSCac?rel=0"" frameborder=""0"" allow=""autoplay; encrypted-media"" allowfullscreen></iframe>";
		public const string BrightcoveCompany = "Brightcove";

	}

	public class BrightcoveHtml : IVideoHtml
	{

		public string GetEmbed(VideoLinkInfo videoInfo, Size size, IDictionary<string,string> embedoptions = null)
		{
			throw new NotImplementedException();
		}

	}

	public class YoutubeHtml : IVideoHtml
	{
		public string GetEmbed(VideoLinkInfo videoInfo, Size size, IDictionary<string,string> embedoptions = null)
		{
			StringBuilder sb = new StringBuilder(Constants.YouTubeEmbed);

			return sb.ToString();
		}
	}
}
namespace CPContrib.Video.Core
{
	public interface IVideoHtmlFactory
	{
		IVideoHtml CreateProvider();
	}
	public class SimpleVideoHtmlProviderFactory
	{
		public SimpleVideoHtmlProviderFactory(string company)
		{
			this._Company = company;
		}
		private string _Company;

		public IVideoHtml CreateProvider()
		{
			return CreateProvider(this._Company);
		}

		public IVideoHtml CreateProvider(string company)
		{
			switch(company)
			{
				case "Brightcove":
					return new BrightcoveHtml();
				case "YouTube":
					return new YoutubeHtml();

			}
			throw new NotImplementedException(string.Format("Company '{0}' not implemented yet."));
		}
	}

	public interface IVideoHtml
	{
		string GetEmbed(VideoLinkInfo videoInfo, Size size, IDictionary<string,string> embedoptions = null);

	}


}
