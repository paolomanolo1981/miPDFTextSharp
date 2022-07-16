using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppPDF
{
    public class BackgroundColorHandler : IEventHandler
    {

        Color SolidColor;
        public BackgroundColorHandler()
        {
            SolidColor = new DeviceRgb(0.545f, 0.909f, 0.745f);

        }

        public void HandleEvent(Event @event)
        {
            PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
            PdfDocument pdf= docEvent.GetDocument();
            PdfPage page = docEvent.GetPage();

            Rectangle pageSize = page.GetPageSize();
            PdfCanvas pdfCanvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdf);

            pdfCanvas.SaveState()
                .SetFillColor(SolidColor)
                .Rectangle(pageSize.GetLeft(), pageSize.GetBottom(), pageSize.GetWidth(), pageSize.GetHeight())
                .Fill().RestoreState();

            pdfCanvas.Release();

        }
    }
}