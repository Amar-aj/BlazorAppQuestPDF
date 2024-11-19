using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace BlazorAppQuestPDF.Service;

public class PdfGenerator
{
    public void GenerateMarriageCertificatePdf(MarriageRegistrationCertificate certificate)
    {
        QuestPDF.Settings.License = LicenseType.Community;
        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12));

                page.Header().Text("Marriage Registration Certificate").SemiBold().FontSize(24).FontColor(Colors.Blue.Medium);

                page.Content().Column(column =>
                {
                    column.Item().Text($"Registration No: {certificate.RegdNo}");
                    column.Item().Text($"Date of Application: {certificate.DateOfApplication.ToShortDateString()}");
                    column.Item().Text($"Husband: {certificate.Husband.Name}, Address: {certificate.Husband.Address}");
                    column.Item().Text($"Wife: {certificate.Wife.Name}, Address: {certificate.Wife.Address}");
                    column.Item().Text($"Date of Marriage: {certificate.DateOfMarriage.ToShortDateString()}");
                    column.Item().Text($"Place of Marriage: {certificate.PlaceOfMarriage}");
                    column.Item().Text($"Husband Date of Birth: {certificate.HusbandDateOfBirth.ToShortDateString()}");
                    column.Item().Text($"Wife Date of Birth: {certificate.WifeDateOfBirth.ToShortDateString()}");
                    column.Item().Text($"Husband Guardian: {certificate.HusbandGuardian.Name}, Address: {certificate.HusbandGuardian.Address}");
                    column.Item().Text($"Wife Guardian: {certificate.WifeGuardian.Name}, Address: {certificate.WifeGuardian.Address}");

                    column.Item().Text("Witnesses:");
                    foreach (var witness in certificate.Witnesses)
                    {
                        column.Item().Text($"- {witness.Name}, Address: {witness.Address}");
                    }

                    column.Item().Text($"Remark: {certificate.Remark}");
                    column.Item().Text($"Memo No: {certificate.MemoNo}");
                    column.Item().Text($"Registrar: {certificate.Registrar}");
                    column.Item().Text($"Block: {certificate.Block}");
                    column.Item().Text($"District: {certificate.District}");
                });

                page.Footer().AlignCenter().Text(x =>
                {
                    x.Span("Page ");
                    x.CurrentPageNumber();
                });
            });
        }).GeneratePdf("MarriageRegistrationCertificate.pdf");
    }
}