// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System.IO;
using System.Runtime.CompilerServices;
using ImageMagick;
using Perspex.Controls;
using Perspex.Media.Imaging;
using Xunit;

#if PERSPEX_CAIRO
using Perspex.Cairo;
#else
    using Perspex.Direct2D1;
#endif

#if PERSPEX_CAIRO
namespace Perspex.Cairo.RenderTests
#else
namespace Perspex.Direct2D1.RenderTests
#endif
{
    public class TestBase
    {
        static TestBase()
        {
#if PERSPEX_CAIRO
            CairoPlatform.Initialize();
#else
            Direct2D1Platform.Initialize();
#endif
        }

        public TestBase(string outputPath)
        {
#if PERSPEX_CAIRO
            string testFiles = Path.GetFullPath(@"..\..\..\TestFiles\Cairo");
#else
            string testFiles = Path.GetFullPath(@"..\..\..\TestFiles\Direct2D1");
#endif
            OutputPath = Path.Combine(testFiles, outputPath);
        }

        public string OutputPath
        {
            get; }

        protected void RenderToFile(Control target, [CallerMemberName] string testName = "")
        {
            string path = Path.Combine(OutputPath, testName + ".out.png");

            RenderTargetBitmap bitmap = new RenderTargetBitmap(
                (int)target.Width,
                (int)target.Height);

            Size size = new Size(target.Width, target.Height);
            target.Measure(size);
            target.Arrange(new Rect(size));
            bitmap.Render(target);
            bitmap.Save(path);
        }

        protected void CompareImages([CallerMemberName] string testName = "")
        {
            string expectedPath = Path.Combine(OutputPath, testName + ".expected.png");
            string actualPath = Path.Combine(OutputPath, testName + ".out.png");
            MagickImage expected = new MagickImage(expectedPath);
            MagickImage actual = new MagickImage(actualPath);
            double error = expected.Compare(actual, ErrorMetric.RootMeanSquared);

            if (error > 0.02)
            {
                Assert.True(false, actualPath + ": Error = " + error);
            }
        }
    }
}
