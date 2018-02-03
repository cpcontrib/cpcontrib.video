﻿using CPContrib.Video;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPContrib.Video_Tests
{
	[TestFixture]
	public class VideoLinkParser_Tests
	{
		[Test]
		public void FormatException_when_not_recognized()
		{
			
			Assert.Throws<FormatException>(() => {
				var parser = new VideoLinkParser();

				parser.ParseLink("");

			});
			
		}

		[Test]
		[TestCaseSource("GetYouTubeVideoLinks")]
		public void YouTube_recognized(string value)
		{
			//arrange
			var parser = new VideoLinkParser();

			//act
			var actual = parser.ParseLink(value);

			//assert
			Assert.That(actual.Company, Is.EqualTo(Constants.YouTubeCompany));
		}


		public static string[] GetYouTubeVideoLinks()
		{
			return new string[] {
			//"http://youtube.com/VIDEO_IDENTIFIER",
			"http://youtu.be/VIDEO_IDENTIFIER",
			"https://youtu.be/VIDEO_IDENTIFIER",
			"https://www.youtube.com/embed/VIDEO_IDENTIFIER?rel=0",
		
			//from https://developer.apple.com/library/content/featuredarticles/iPhoneURLScheme_Reference/YouTubeLinks/YouTubeLinks.html
			"http://www.youtube.com/watch?v=VIDEO_IDENTIFIER",
			"http://www.youtube.com/v/VIDEO_IDENTIFIER",
			};
		}

	}
}
