using QuestPDF.Infrastructure;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace BlazorAppQuestPDF.Service;

public class PdfService
{
    private readonly IConfiguration _configuration;

    public PdfService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public byte[] GenerateMarriageCertificatePdf(MarriageRegistrationCertificate certificate)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        using var stream = new MemoryStream();

        // Retrieve store information from configuration
        var storeName = _configuration.GetSection("BaseInfo:StoreName").Value;
        var storePhone = _configuration.GetSection("BaseInfo:Phone").Value;
        var storeAddress = _configuration.GetSection("BaseInfo:Address").Value;
        var hotline = _configuration.GetSection("BaseInfo:Hotline").Value;

        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12).FontFamily("Times New Roman"));

                // Header
                page.Header().PaddingBottom(15).Column(column =>
                {
                    column.Item().Text("Marriage Registration Certificate").FontSize(24).Bold().AlignCenter();
                    column.Item().Text($"Store Name: {storeName}").AlignCenter();
                    column.Item().Text($"Phone: {storePhone}").AlignCenter();
                    column.Item().Text($"Address: {storeAddress}").AlignCenter();
                });

                // Content
                page.Content().Column(column =>
                {
                    // Marriage Information
                    column.Item().Text($"Registration No: {certificate.RegdNo}").Bold();
                    column.Item().Text($"Date of Application: {certificate.DateOfApplication.ToShortDateString()}");
                    column.Item().Text($"Husband: {certificate.Husband.Name}, Address: {certificate.Husband.Address}");
                    column.Item().Text($"Wife: {certificate.Wife.Name}, Address: {certificate.Wife.Address}");
                    column.Item().Text($"Date of Marriage: {certificate.DateOfMarriage.ToShortDateString()}");
                    column.Item().Text($"Place of Marriage: {certificate.PlaceOfMarriage}");
                    column.Item().Text($"Husband Date of Birth: {certificate.HusbandDateOfBirth.ToShortDateString()}");
                    column.Item().Text($"Wife Date of Birth: {certificate.WifeDateOfBirth.ToShortDateString()}");
                    column.Item().Text($"Husband Guardian: {certificate.HusbandGuardian.Name}, Address: {certificate.HusbandGuardian.Address}");
                    column.Item().Text($"Wife Guardian: {certificate.WifeGuardian.Name}, Address: {certificate.WifeGuardian.Address}");

                    // Witnesses
                    column.Item().Text("Witnesses:").Bold();
                    foreach (var witness in certificate.Witnesses)
                    {
                        column.Item().Text($"- {witness.Name}, Address: {witness.Address}");
                    }

                    // Remarks
                    column.Item().Text($"Remark: {certificate.Remark}");
                    column.Item().Text($"Memo No: {certificate.MemoNo}");
                    column.Item().Text($"Registrar: {certificate.Registrar}");
                    column.Item().Text($"Block: {certificate.Block}");
                    column.Item().Text($"District: {certificate.District}");
                });

                // Footer
                page.Footer().AlignCenter().Row(row =>
                {
                    row.Spacing(5);
                    row.ConstantItem(180).AlignMiddle().Text($"HOTLINE: {hotline}").Bold();
                });
            });
        }).GeneratePdf(stream);

        return stream.ToArray();
    }
}