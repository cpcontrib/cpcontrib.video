using CPContrib.Video;
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
		[TestCase("http://youtube.com/randomid")]
		[TestCase("http://youtu.be/randomid")]
		[TestCase("https://youtu.be/randomid")]
		[TestCase("https://youtu.be/randomid")]
		public void YouTube_recognized(string value)
		{
			//arrange
			var parser = new VideoLinkParser();

			//act
			var actual = parser.ParseLink(value);

			//assert
			Assert.That(actual.Company, Is.EqualTo(Constants.YouTubeCompany));
		}

	}
}
