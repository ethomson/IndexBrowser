using System;
using System.Collections.Generic;
using System.Text;

namespace IndexBrowser
{
    public class HexDisplay
    {
        public static string CreateHexDisplay(ReadOnlySpan<byte> contents)
        {
            const string lineEnding = "\n";
            const int bytesPerLine = 16;
            const int chunkLength = (bytesPerLine / 2);
            int linelen = bytesPerLine;
            int position;

            if (contents.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder hex = new StringBuilder();

            for (position = 0; position < contents.Length; position += linelen)
            {
                linelen = Math.Min(contents.Length - position, bytesPerLine);

                hex.Append(String.Format("{0,8:x8}", position));

                for (var i = 0; i < bytesPerLine; i++)
                {
                    if ((i % chunkLength) == 0)
                    {
                        hex.Append("  ");
                    }
                    else if (i > 0)
                    {
                        hex.Append(" ");
                    }

                    string b = (i < linelen) ? FormatHex(contents[position + i]) : "  ";
                    hex.Append(b);
                }

                hex.Append("  |");

                for (int i = 0; i < linelen; i++)
                {
                    String c = FormatChar(contents[position + i]);
                    hex.Append(c);
                }

                hex.Append("|");
                hex.Append(lineEnding);
            }

            hex.Append(String.Format("{0,8:x8}{1}", position, lineEnding));

            return hex.ToString();
        }

        private static string FormatHex(byte b)
        {
            return String.Format("{0,2:x2}", b);
        }

        private static string FormatChar(byte b)
        {
            if (b >= 32 && b < 127)
            {
                return Encoding.ASCII.GetString(new byte[] { b });
            }

            return ".";
        }
    }
}
