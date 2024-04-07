using QuestPDF.Infrastructure;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using RareNFTs.Application.Services.Interfaces;
using RareNFTs.Infraestructure.Repository.Interfaces;




namespace RareNFTs.Application.Services.Implementations;

public class ServiceReport : IServiceReport
{
    private readonly IRepositoryNft _repositoryNft;
    private readonly IRepositoryClient _repositoryClient;
    private readonly IRepositoryCountry _repositoryCountry;


    public ServiceReport(IRepositoryNft repositoryNft, IRepositoryClient repositoryClient, IRepositoryCountry repositoryCountry)
    {
        _repositoryNft = repositoryNft;
        _repositoryClient = repositoryClient;
        _repositoryCountry = repositoryCountry;
    }

    public async Task<byte[]> ProductReport()
    {
        // Get Data
        var collection = await _repositoryNft.ListAsync();

        // License config ******  IMPORTANT ******
        QuestPDF.Settings.License = LicenseType.Community;

        // return ByteArrays
        var pdfByteArray = QuestPDF.Fluent.Document.Create(document =>
        {
            document.Page(page =>
            {

                page.Size(PageSizes.Letter);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.Margin(30);

                page.Header().Row(row =>
                {
                    row.RelativeItem().Column(col =>
                    {
                        col.Item().AlignLeft().Text("RareNFTs ").Bold().FontSize(14).Bold();
                        col.Item().AlignLeft().Text($"Date: {DateTime.Now} ").FontSize(9);
                        col.Item().LineHorizontal(1f);
                    });

                });


                page.Content().PaddingVertical(10).Column(col1 =>
                {
                    col1.Item().AlignCenter().Text("NFT Report").FontSize(14).Bold();
                    col1.Item().Text("");
                    col1.Item().LineHorizontal(0.5f);

                    col1.Item().Table(tabla =>
                    {
                        tabla.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();

                        });

                        tabla.Header(header =>
                        {
                            header.Cell().Background("#4666FF")
                            .Padding(2).AlignCenter().Text("ID").FontColor("#fff");

                            header.Cell().Background("#4666FF")
                            .Padding(2).AlignCenter().Text("NFT").FontColor("#fff");

                            header.Cell().Background("#4666FF")
                           .Padding(2).AlignCenter().Text("Image").FontColor("#fff");

                            header.Cell().Background("#4666FF")
                           .Padding(2).AlignCenter().Text("Date").FontColor("#fff");

                            header.Cell().Background("#4666FF")
                           .Padding(2).AlignCenter().Text("QTY").FontColor("#fff");

                            header.Cell().Background("#4666FF")
                           .Padding(2).AlignCenter().Text("Author").FontColor("#fff");

                            header.Cell().Background("#4666FF")
                           .Padding(2).AlignCenter().Text("Price").FontColor("#fff");
                        });

                        foreach (var item in collection)
                        {
                            // Column 1
                            tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                            .Padding(2).Text(item.Id.ToString());
                            // Column 2
                            tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                            .Padding(2).Text(item.Description?.ToString());
                            // Column 3
                            tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                            .Padding(2).Image(item.Image).UseOriginalImage();

                            // Column 4
                            tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                            .Padding(2).Text(item.Date?.ToString());

                            // Column 5
                            tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                                .Padding(2).AlignRight().Text(item.Quantity.ToString()).FontSize(10);
                            // Column 6
                            tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                                .Padding(2).AlignRight().Text(item.Author?.ToString()).FontSize(10);

                            // Column 7
                            tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                                .Padding(2).AlignRight().Text(item.Price.ToString()).FontSize(10);
                        }

                    });

                    var granTotal = collection.Sum(p => p.Quantity * p.Price);

                    col1.Item().AlignRight().Text("Total " + granTotal.ToString()).FontSize(12).Bold();

                });

                page.Footer()
                .AlignRight()
                .Text(txt =>
                {
                    txt.Span("Page ").FontSize(10);
                    txt.CurrentPageNumber().FontSize(10);
                    txt.Span(" of ").FontSize(10);
                    txt.TotalPages().FontSize(10);
                });
            });
        }).GeneratePdf();

        File.WriteAllBytes(@"C:\temp\ProductReport.pdf", pdfByteArray);
        return pdfByteArray;

    }



    public async Task<byte[]> ClientReport()
    {
        // Get Data
        var collection = await _repositoryClient.ListAsync();
        
        // License config ******  IMPORTANT ******
        QuestPDF.Settings.License = LicenseType.Community;

        // return ByteArrays
        var pdfByteArray = QuestPDF.Fluent.Document.Create(document =>
        {
            document.Page(page =>
            {

                page.Size(PageSizes.Letter);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.Margin(30);

                page.Header().Row(row =>
                {
                    row.RelativeItem().Column(col =>
                    {
                        col.Item().AlignLeft().Text("RareNFTs ").Bold().FontSize(14).Bold();
                        col.Item().AlignLeft().Text($"Date: {DateTime.Now} ").FontSize(9);
                        col.Item().LineHorizontal(1f);
                    });

                });


                page.Content().PaddingVertical(10).Column(col1 =>
                {
                    col1.Item().AlignCenter().Text("Client Report").FontSize(14).Bold();
                    col1.Item().Text("");
                    col1.Item().LineHorizontal(0.5f);

                    col1.Item().Table(tabla =>
                    {
                        tabla.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();

                        });

                        tabla.Header(header =>
                        {
                            header.Cell().Background("#4666FF")
                            .Padding(2).AlignCenter().Text("ID").FontColor("#fff");

                            header.Cell().Background("#4666FF")
                            .Padding(2).AlignCenter().Text("Name").FontColor("#fff");

                            header.Cell().Background("#4666FF")
                           .Padding(2).AlignCenter().Text("Surname").FontColor("#fff");

                            header.Cell().Background("#4666FF")
                           .Padding(2).AlignCenter().Text("Genre").FontColor("#fff");

                            header.Cell().Background("#4666FF")
                           .Padding(2).AlignCenter().Text("Country").FontColor("#fff");

                            header.Cell().Background("#4666FF")
                           .Padding(2).AlignCenter().Text("Email").FontColor("#fff");

                           
                        });

                        foreach (var item in collection)
                        {
                            // Column 1
                            tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                            .Padding(2).Text(item.Id.ToString());
                            // Column 2
                            tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                            .Padding(2).Text(item.Name?.ToString());
                            // Column 3
                            tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                            .Padding(2).Text(item.Surname?.ToString());

                            // Column 4
                            tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                            .Padding(2).Text(item.Genre?.ToString());

                            // Column 5
                            tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                            .Padding(2).Text((_repositoryCountry.FindByIdAsync(item.IdCountry).Result.Name).ToString());

                            // Column 5
                             tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                 .Padding(2).Text(item.Email?.ToString());


                        }

                    });

                    

                });

                page.Footer()
                .AlignRight()
                .Text(txt =>
                {
                    txt.Span("Page ").FontSize(10);
                    txt.CurrentPageNumber().FontSize(10);
                    txt.Span(" of ").FontSize(10);
                    txt.TotalPages().FontSize(10);
                });
            });
        }).GeneratePdf();

        File.WriteAllBytes(@"C:\temp\ClientReport.pdf", pdfByteArray);
        return pdfByteArray;

    }
}

