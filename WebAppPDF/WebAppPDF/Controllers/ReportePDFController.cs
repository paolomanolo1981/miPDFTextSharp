
using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppPDF.Controllers
{
    public class ReportePDFController : Controller
    {
        // GET: ReportePDF
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult pdf()
        {
            string nameFont = Server.MapPath("~/fonts/megan_june.otf");
            string pathLogo = Server.MapPath("~/Content/Images/logo_market.png");
            Image img=new Image(ImageDataFactory.Create(pathLogo));

            MemoryStream ms = new MemoryStream();

            PdfWriter pw = new PdfWriter(ms);
            PdfDocument pdfDocument = new PdfDocument(pw);
            Document doc = new Document(pdfDocument, PageSize.LETTER);
            doc.SetMargins(75, 25, 70, 35);
            //evento
            pdfDocument.AddEventHandler(PdfDocumentEvent.START_PAGE, new HeaderEventHandler(img));
            pdfDocument.AddEventHandler(PdfDocumentEvent.END_PAGE, new FooterEventHandler());


            PdfFont font = PdfFontFactory.CreateFont(nameFont);// StandardFonts.HELVETICA);

            Style styles = new Style()
                .SetFontSize(24)
                .SetFont(font)
                .SetFontColor(ColorConstants.BLUE)
                .SetBackgroundColor(ColorConstants.RED)
                ;


            doc.Add(new Paragraph("Hello iText7!!!")
               .AddStyle(styles)
                );


            doc.Close();

            byte[] bytesStream=ms.ToArray();
            ms = new MemoryStream();
            ms.Write(bytesStream,0,bytesStream.Length);
            ms.Position = 0;


            return new FileStreamResult(ms,"application/pdf");
        }

        public ActionResult PdfClase2()
        {
            string pathLogo = Server.MapPath("~/Content/Images/logo_market.png");
            Image img = new Image(ImageDataFactory.Create(pathLogo));

            List<Productos> oProductos = new List<Productos>();
            for(int i = 0; i < 1000; i++)
            {
                Productos productos = new Productos();


                productos.NombreProducto = "Nombre Producto: " + i.ToString();
                productos.PrecioUnidad = i;
                productos.UnidadesEnExistencia = (2 * 5) * i;



                oProductos.Add(productos);




            }

            string nameFont = Server.MapPath("~/fonts/megan_june.otf");
            MemoryStream ms = new MemoryStream();

            PdfWriter pw = new PdfWriter(ms);
            PdfDocument pdfDocument = new PdfDocument(pw);
            Document doc = new Document(pdfDocument, PageSize.A4);
            doc.SetMargins(75, 25, 70, 35);

            //evento
            pdfDocument.AddEventHandler(PdfDocumentEvent.START_PAGE, new HeaderEventHandler(img));
            pdfDocument.AddEventHandler(PdfDocumentEvent.END_PAGE, new FooterEventHandler());

            //tabla 1 para la cabecera
            Table table = new Table(1).UseAllAvailableWidth();
            Cell cell = new Cell().Add(new Paragraph("Reporte de Productos").SetFontSize(14))
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER)
               ;
            table.AddCell(cell);

            cell = new Cell().Add(new Paragraph("Productos en existencia"))
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER)
                ;
            table.AddCell(cell);

           
            doc.Add(table);


            Style styleCell = new Style()
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               ;

            //para las 4 columnas de la tabla detalle
            Table _tabla = new Table(4).UseAllAvailableWidth();
            Cell _cell = new Cell(2, 1).Add(new Paragraph("#"));
            _tabla.AddHeaderCell(_cell.AddStyle(styleCell));

            _cell = new Cell(1, 2).Add(new Paragraph("Producto"));
            _tabla.AddHeaderCell(_cell.AddStyle(styleCell));

            _cell = new Cell(2, 1).Add(new Paragraph("Unidades en Existencia"));
            _tabla.AddHeaderCell(_cell.AddStyle(styleCell));

            _cell = new Cell().Add(new Paragraph("Nombre"));
            _tabla.AddHeaderCell(_cell.AddStyle(styleCell));

            _cell = new Cell().Add(new Paragraph("Precio Unitario"));
            _tabla.AddHeaderCell(_cell.AddStyle(styleCell));

            int x = 0;
            foreach(Productos oProducto in oProductos)
            {
                x++;
                _cell = new Cell().Add(new Paragraph(x.ToString()));
                _tabla.AddCell(_cell.SetBackgroundColor(ColorConstants.GREEN));

                _cell = new Cell().Add(new Paragraph(oProducto.NombreProducto));
                _tabla.AddCell(_cell.SetBackgroundColor(ColorConstants.ORANGE));

                _cell = new Cell().Add(new Paragraph(oProducto.PrecioUnidad.ToString()));
                _tabla.AddCell(_cell.SetBackgroundColor(ColorConstants.YELLOW));

                if(oProducto.UnidadesEnExistencia<10)
                {
                    _cell = new Cell().Add(new Paragraph(oProducto.UnidadesEnExistencia.ToString()));
                    _tabla.AddCell(_cell.SetBackgroundColor(ColorConstants.RED));
                }
                else
                {
                    _cell = new Cell().Add(new Paragraph(oProducto.UnidadesEnExistencia.ToString()));
                    _tabla.AddCell(_cell);
                } 


               
                

            }


            doc.Add(_tabla);
            doc.Close();

            byte[] bytesStream = ms.ToArray();
            ms = new MemoryStream();
            ms.Write(bytesStream, 0, bytesStream.Length);
            ms.Position = 0;


            return new FileStreamResult(ms, "application/pdf");
        }
    }
}