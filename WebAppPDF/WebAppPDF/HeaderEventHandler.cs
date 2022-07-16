using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
 
using System.Linq;
using System.Web;

namespace WebAppPDF
{
    public class HeaderEventHandler : IEventHandler
    {
        Image Img;
        protected Document doc;

        public HeaderEventHandler(Document _doc)
        {
            this.doc = _doc;
        }

        public HeaderEventHandler(Image img)
        {
            Img = img;
        }



        public void HandleEvent(Event @event)
        {
            PdfDocumentEvent docEvent=(PdfDocumentEvent)@event;
            PdfDocument pdfDoc = docEvent.GetDocument();
            PdfPage page = docEvent.GetPage();

            PdfCanvas canvas1 = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdfDoc);
            Rectangle rootArea = new Rectangle(35, page.GetPageSize().GetTop() - 75, page.GetPageSize().GetRight() - 70, 55);
            new Canvas(canvas1, pdfDoc, rootArea)
                .Add(getTable(docEvent));




/*
            Rectangle rootArea = new Rectangle(35, page.GetPageSize().GetTop() - 70, page.GetPageSize().GetRight() - 70, 50);
            Canvas canvas = new Canvas(docEvent.GetPage(), rootArea);
            canvas
                .Add(getTable(docEvent))
                .ShowTextAligned("Este es el encabezado de página", 10, 0, TextAlignment.CENTER)
                .ShowTextAligned("Este es el pie de página", 10, 10, TextAlignment.CENTER)
                .ShowTextAligned("texto Agregado",612,0,TextAlignment.RIGHT)
                .Close();
*/
        }

        public Table getTable(PdfDocumentEvent docEvent)
        {
            float[] cellWidth = { 20f, 80f };
            Table tableEvent = new Table(UnitValue.CreatePercentArray(cellWidth)).UseAllAvailableWidth();

            Style styleCell = new Style()
                .SetBorder(Border.NO_BORDER);

            Style styleText =new Style()
               .SetTextAlignment(TextAlignment.RIGHT).SetFontSize(10f);

            Cell cell = new Cell().Add(Img.SetAutoScale(true));
                //.SetBorder(new SolidBorder(ColorConstants.BLACK, 0));

            tableEvent.AddCell(cell
                .AddStyle(styleCell)
                .SetTextAlignment(TextAlignment.LEFT)

                );

            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);
            cell = new Cell()
                .Add(new Paragraph("Reporte diario").SetFont(bold))
                .Add(new Paragraph("Departamento de Recursos Materiales").SetFont(bold))
                .Add(new Paragraph("Fecha de Emisión: " + DateTime.Now.ToShortDateString()))
                .AddStyle(styleText).AddStyle(styleCell)
              //  .SetBorder(new SolidBorder(ColorConstants.BLACK,0))
                ;

            tableEvent.AddCell(cell);
            return tableEvent;
        }

        public void HandleEventOK(Event @event)
        {
            PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
            Rectangle pageSize = docEvent.GetPage().GetPageSize();

            float a = pageSize.GetLeft();
            float b = pageSize.GetRight();
            float c = doc.GetLeftMargin();
            float d = doc.GetRightMargin();

            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_OBLIQUE);

            float coordenadaX = ((a + c) + (b + d)) / 2;
            float headerY = pageSize.GetTop() - doc.GetTopMargin() + 10;
            float footerY = doc.GetBottomMargin();
            Canvas canvas = new Canvas(docEvent.GetPage(), pageSize);
            canvas
                .SetFont(font)
                .SetFontSize(12)
                .ShowTextAligned("Este es el encabezado de pagina", coordenadaX, headerY, TextAlignment.CENTER)
                .ShowTextAligned("Esto es el pie de pagina",coordenadaX,footerY,TextAlignment.CENTER)
                .ShowTextAligned("texto agregado",612,0,TextAlignment.RIGHT)
                .Close()
                ;



        }
    }
}