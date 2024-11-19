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
                page.Header().Column(column =>
                {
                    column.Item().PaddingBottom(10).Column(headerColumn =>
                    {
                        headerColumn.Item().Text("MARRIAGE REGISTRATION CERTIFICATE")
                            .FontSize(18)
                            .Bold()
                            .AlignCenter();
                        headerColumn.Item().Text($"Store Name: {storeName}")
                            .FontSize(14)
                            .AlignCenter();
                        headerColumn.Item().Text($"Phone: {storePhone}")
                            .FontSize(14)
                            .AlignCenter();
                        headerColumn.Item().Text($"Address: {storeAddress}")
                            .FontSize(14)
                            .AlignCenter();
                    });
                });

                // Content
                page.Content().Column(column =>
                {
                    // Registration Details
                    column.Item().PaddingTop(10).Column(detailsColumn =>
                    {
                        detailsColumn.Item().Text("Registration Details")
                            .FontSize(16)
                            .Bold();
                        detailsColumn.Item().Row(row =>
                        {
                            row.RelativeItem().Text("Registration No: ");
                            row.RelativeItem(0.5f).Text(" : "); // Middle column with smaller size
                            row.RelativeItem().Text(certificate.RegdNo);
                        });
                        detailsColumn.Item().Row(row =>
                        {
                            row.RelativeItem().Text("Date of Application: ");
                            row.RelativeItem(0.5f).Text(" : "); // Middle column with smaller size
                            row.RelativeItem().Text(certificate.DateOfApplication.ToShortDateString());
                        });
                    });

                    // Husband Details
                    column.Item().PaddingTop(10).Column(husbandColumn =>
                    {
                        husbandColumn.Item().Text("Husband Details")
                            .FontSize(16)
                            .Bold();
                        husbandColumn.Item().Row(row =>
                        {
                            row.RelativeItem().Text("Name: ");
                            row.RelativeItem(0.5f).Text(" : ");
                            row.RelativeItem().Text(certificate.Husband.Name);
                        });
                        husbandColumn.Item().Row(row =>
                        {
                            row.RelativeItem().Text("Address: ");
                            row.RelativeItem(0.5f).Text(" : ");
                            row.RelativeItem().Text(certificate.Husband.Address);
                        });
                        husbandColumn.Item().Row(row =>
                        {
                            row.RelativeItem().Text("Date of Birth: ");
                            row.RelativeItem(0.5f).Text(" : ");
                            row.RelativeItem().Text(certificate.HusbandDateOfBirth.ToShortDateString());
                        });
                    });

                    // Wife Details
                    column.Item().PaddingTop(10).Column(wifeColumn =>
                    {
                        wifeColumn.Item().Text("Wife Details")
                            .FontSize(16)
                            .Bold();
                        wifeColumn.Item().Row(row =>
                        {
                            row.RelativeItem().Text("Name: ");
                            row.RelativeItem(0.5f).Text(" : ");
                            row.RelativeItem().Text(certificate.Wife.Name);
                        });
                        wifeColumn.Item().Row(row =>
                        {
                            row.RelativeItem().Text("Address: ");
                            row.RelativeItem(0.5f).Text(" : ");
                            row.RelativeItem().Text(certificate.Wife.Address);
                        });
                        wifeColumn.Item().Row(row =>
                        {
                            row.RelativeItem().Text("Date of Birth: ");
                            row.RelativeItem(0.5f).Text(" : ");
                            row.RelativeItem().Text(certificate.WifeDateOfBirth.ToShortDateString());
                        });
                    });

                    // Marriage Details
                    column.Item().PaddingTop(10).Column(marriageColumn =>
                    {
                        marriageColumn.Item().Text("Marriage Details")
                            .FontSize(16)
                            .Bold();
                        marriageColumn.Item().Row(row =>
                        {
                            row.RelativeItem().Text("Date of Marriage: ");
                            row.RelativeItem(0.5f).Text(" : ");
                            row.RelativeItem().Text(certificate.DateOfMarriage.ToShortDateString());
                        });
                        marriageColumn.Item().Row(row =>
                        {
                            row.RelativeItem().Text("Place of Marriage: ");
                            row.RelativeItem(0.5f).Text(" : ");
                            row.RelativeItem().Text(certificate.PlaceOfMarriage);
                        });
                    });

                    // Guardian Details
                    column.Item().PaddingTop(10).Column(guardianColumn =>
                    {
                        guardianColumn.Item().Text("Guardian Details")
                            .FontSize(16)
                            .Bold();
                        guardianColumn.Item().Row(row =>
                        {
                            row.RelativeItem().Text("Husband's Guardian: ");
                            row.RelativeItem(0.5f).Text(" : ");
                            row.RelativeItem().Text($"{certificate.HusbandGuardian.Name}, Address: {certificate.HusbandGuardian.Address}");
                        });
                        guardianColumn.Item().Row(row =>
                        {
                            row.RelativeItem().Text("Wife's Guardian: ");
                            row.RelativeItem(0.5f).Text(" : ");
                            row.RelativeItem().Text($"{certificate.WifeGuardian.Name}, Address: {certificate.WifeGuardian.Address}");
                        });
                    });

                    // Witnesses
                    column.Item().PaddingTop(10).Column(witnessColumn =>
                    {
                        witnessColumn.Item().Text("Witnesses")
                            .FontSize(16)
                            .Bold();
                        foreach (var witness in certificate.Witnesses)
                        {
                            witnessColumn.Item().Row(row =>
                            {
                                row.RelativeItem().Text("- ");
                                row.RelativeItem().Text($"{witness.Name}, Address: {witness.Address}");
                            });
                        }
                    });

                    // Remarks
                    column.Item().PaddingTop(10).Text($"Remark: {certificate.Remark}");
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