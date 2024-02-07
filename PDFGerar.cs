using System;
using System.IO;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace carimbos
{
    public class PDFGerar
    {
        public void Gerar()
        {
            string inputPdf = @"C:\Users\Sabin\Downloads\Manifesto-2910.pdf";
            string outputPdf = @"C:\Users\Sabin\Downloads\Manifesto-2910-testetarde.pdf";
            string textoMarcaDagua = "APROVADO";

            using (PdfReader reader = new PdfReader(inputPdf))
            using (PdfWriter writer = new PdfWriter(outputPdf))
            {
                PdfDocument pdfDoc = new PdfDocument(reader, writer);
                Document doc = new Document(pdfDoc);

                // Configurar a marca d'água
                PdfFont font = PdfFontFactory.CreateFont();
                float fontSize = 40;
                Color color = Color.ConvertRgbToCmyk(new DeviceRgb(100, 100, 100));
                float angle = (float)(Math.PI / 4);
                float margin = 50; // Defina a margem
                float x = margin;
                float y = margin;

                // Adicionar a marca d'água em todas as páginas
                for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
                {
                    PdfPage page = pdfDoc.GetPage(i);
                    var pageSize = page.GetPageSizeWithRotation();

                    // Define a posição central na página, considerando a margem
                    float centerX = pageSize.GetWidth() / 2;
                    float centerY = pageSize.GetHeight() / 2;

                    // Rotacionar a marca d'água
                    AffineTransform transform = AffineTransform.GetRotateInstance(angle, centerX, centerY);
                    PdfCanvas canvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdfDoc);
                    canvas.SaveState().ConcatMatrix(transform);

                    // Adicionar o texto da marca d'água
                    Paragraph p = new Paragraph(textoMarcaDagua)
                        .SetFont(font)
                        .SetFontSize(fontSize)
                        .SetFontColor(color)
                        .SetFixedPosition(200,pageSize.GetHeight(), pageSize.GetWidth() - 2 * margin); // Ajusta para a margem

                  Paragraph Z = new Paragraph("tesdadfasdfs")
                        .SetFont(font)
                        .SetFontSize(fontSize)
                        .SetFontColor(color)
                        .SetFixedPosition(200,pageSize.GetHeight(), pageSize.GetWidth() - 2 * margin); // Ajusta para a margem


                    doc.ShowTextAligned(p, x, y, i, TextAlignment.CENTER, VerticalAlignment.TOP, angle);
                    doc.ShowTextAligned(Z, x, y, i, TextAlignment.LEFT, VerticalAlignment.TOP, angle);

                    canvas.RestoreState();
                }

                doc.Close();
            }

            Console.WriteLine("Marca d'água adicionada com sucesso!");
        }
    }
}
