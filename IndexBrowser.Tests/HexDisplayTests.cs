using System;
using Xunit;

using IndexBrowser;

namespace IndexBrowser.Tests
{
    public class HexDisplayTests
    {
        [Fact]
        public void WriteByteArray()
        {
            byte[] contents = new byte[] { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x2c, 0x20, 0x77, 0x6f, 0x72, 0x6c, 0x64, 0x2e, 0x2e, 0x2e, 0x0a };
            string output = HexDisplay.CreateHexDisplay(contents);

            Assert.Equal("00000000  48 65 6c 6c 6f 2c 20 77  6f 72 6c 64 2e 2e 2e 0a  |Hello, world....|\n00000010\n", HexDisplay.CreateHexDisplay(contents));
        }

        [Fact]
        public void WriteSpanOfBytes()
        {
            byte[] contents = new byte[] { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x2c, 0x20, 0x77, 0x6f, 0x72, 0x6c, 0x64, 0x2e, 0x2e, 0x2e, 0x0a };
            string output = HexDisplay.CreateHexDisplay(new ReadOnlySpan<byte>(contents));

            Assert.Equal("00000000  48 65 6c 6c 6f 2c 20 77  6f 72 6c 64 2e 2e 2e 0a  |Hello, world....|\n00000010\n", HexDisplay.CreateHexDisplay(contents));
        }

        [Fact]
        public void WriteShortLine()
        {
            byte[] contents = new byte[] { 0x48, 0x69, 0x2e, 0x0a };
            string output = HexDisplay.CreateHexDisplay(contents);

            Assert.Equal("00000000  48 69 2e 0a                                       |Hi..|\n00000004\n", output);
        }

        [Fact]
        public void WriteMultipleLines()
        {
            byte[] contents = new byte[] { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x2c, 0x20, 0x77, 0x6f, 0x72, 0x6c, 0x64, 0x2e, 0x2e, 0x2e, 0x21, 0x0a };
            string output = HexDisplay.CreateHexDisplay(contents);

            Assert.Equal("00000000  48 65 6c 6c 6f 2c 20 77  6f 72 6c 64 2e 2e 2e 21  |Hello, world...!|\n00000010  0a                                                |.|\n00000011\n", output);
        }

        [Fact]
        public void EmptyByteArrayHasNoOutput()
        {
            byte[] contents = new byte[0];
            string output = HexDisplay.CreateHexDisplay(contents);

            Assert.Equal("", output);
        }
    }
}
