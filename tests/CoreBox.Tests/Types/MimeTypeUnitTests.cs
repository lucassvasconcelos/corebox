using CoreBox.Types;
using FluentAssertions;
using Xunit;

namespace CoreBox.Tests.Types
{
    public class MimeTypeUnitTests
    {
        [Fact]
        public void Deve_Testar_Valores_Dos_MimeTypes()
        {
            MimeType.aac.Should().Be("audio/aac");
            MimeType.abw.Should().Be("application/x-abiword");
            MimeType.arc.Should().Be("application/x-freearc");
            MimeType.avi.Should().Be("video/x-msvideo");
            MimeType.azw.Should().Be("application/vnd.amazon.ebook");
            MimeType.bin.Should().Be("application/octet-stream");
            MimeType.bmp.Should().Be("image/bmp");
            MimeType.bz.Should().Be("application/x-bzip");
            MimeType.bz2.Should().Be("application/x-bzip2");
            MimeType.csh.Should().Be("application/x-csh");
            MimeType.css.Should().Be("text/css");
            MimeType.csv.Should().Be("text/csv");
            MimeType.doc.Should().Be("application/msword");
            MimeType.docx.Should().Be("application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            MimeType.eot.Should().Be("application/vnd.ms-fontobject");
            MimeType.epub.Should().Be("application/epub+zip");
            MimeType.gz.Should().Be("application/gzip");
            MimeType.gif.Should().Be("image/gif");
            MimeType.htm.Should().Be("text/html");
            MimeType.html.Should().Be("text/html");
            MimeType.ico.Should().Be("image/vnd.microsoft.icon");
            MimeType.ics.Should().Be("text/calendar");
            MimeType.jar.Should().Be("application/java-archive");
            MimeType.jpeg.Should().Be("image/jpeg");
            MimeType.jpg.Should().Be("image/jpeg");
            MimeType.js.Should().Be("text/javascript");
            MimeType.json.Should().Be("application/json");
            MimeType.jsonld.Should().Be("application/ld+json");
            MimeType.mid.Should().Be("audio/midi audio/x-midi");
            MimeType.midi.Should().Be("audio/midi audio/x-midi");
            MimeType.mjs.Should().Be("text/javascript");
            MimeType.mp3.Should().Be("audio/mpeg");
            MimeType.mpeg.Should().Be("video/mpeg");
            MimeType.mpkg.Should().Be("application/vnd.apple.installer+xml");
            MimeType.odp.Should().Be("application/vnd.oasis.opendocument.presentation");
            MimeType.ods.Should().Be("application/vnd.oasis.opendocument.spreadsheet");
            MimeType.odt.Should().Be("application/vnd.oasis.opendocument.text");
            MimeType.oga.Should().Be("audio/ogg");
            MimeType.ogv.Should().Be("video/ogg");
            MimeType.ogx.Should().Be("application/ogg");
            MimeType.opus.Should().Be("audio/opus");
            MimeType.otf.Should().Be("font/otf");
            MimeType.png.Should().Be("image/png");
            MimeType.pdf.Should().Be("application/pdf");
            MimeType.php.Should().Be("application/x-httpd-php");
            MimeType.ppt.Should().Be("application/vnd.ms-powerpoint");
            MimeType.pptx.Should().Be("application/vnd.openxmlformats-officedocument.presentationml.presentation");
            MimeType.rar.Should().Be("application/vnd.rar");
            MimeType.rtf.Should().Be("application/rtf");
            MimeType.sh.Should().Be("application/x-sh");
            MimeType.svg.Should().Be("image/svg+xml");
            MimeType.swf.Should().Be("application/x-shockwave-flash");
            MimeType.tar.Should().Be("application/x-tar");
            MimeType.tif.Should().Be("image/tiff");
            MimeType.tiff.Should().Be("image/tiff");
            MimeType.ts.Should().Be("video/mp2t");
            MimeType.ttf.Should().Be("font/ttf");
            MimeType.txt.Should().Be("text/plain");
            MimeType.vsd.Should().Be("application/vnd.visio");
            MimeType.wav.Should().Be("audio/wav");
            MimeType.weba.Should().Be("audio/webm");
            MimeType.webm.Should().Be("video/webm");
            MimeType.webp.Should().Be("image/webp");
            MimeType.woff.Should().Be("font/woff");
            MimeType.woff2.Should().Be("font/woff2");
            MimeType.xhtml.Should().Be("application/xhtml+xml");
            MimeType.xls.Should().Be("application/vnd.ms-excel");
            MimeType.xlsx.Should().Be("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            MimeType.xml.Should().Be("application/xml");
            MimeType.xul.Should().Be("application/vnd.mozilla.xul+xml");
            MimeType.zip.Should().Be("application/zip");
            MimeType.audio3gp.Should().Be("audio/3gpp");
            MimeType.video3gp.Should().Be("video/3gpp");
            MimeType.audio3g2.Should().Be("audio/3gpp2");
            MimeType.video3g2.Should().Be("video/3gpp2");
            MimeType.archive7z.Should().Be("application/x-7z-compressed");
        }
    }
}