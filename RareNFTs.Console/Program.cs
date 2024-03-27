using RareNFTs.Infraestructure.Repository.Implementation;
using RareNFTs.Infraestructure.Repository.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Logging;
using RareNFTs.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using RareNFTs.Infraestructure.Models;

/*
 Developed By AMV 10-03-2024 10:48 pm
  
*/
namespace RareNFTs.Console
{

    public class Principal
    {

        /// <summary>
        /// Taken from https://www.c-sharpcorner.com/article/using-dependency-injection-in-net-console-apps/
        /// How to D.I. using Console
        /// </summary>
        /// <returns></returns>
        private static ServiceProvider CreateServices()
        {

            var serviceProvider = new ServiceCollection()
                   .AddLogging(options =>
                   {
                       options.ClearProviders();
                       options.AddConsole();
                   })
                   // Add D.I.
                   .AddTransient<IRepositoryCard, RepositoryCard>()
                   .AddTransient<IRepositoryClient, RepositoryClient>()
                   .AddTransient<IRepositoryNft, RepositoryNft>()
                   .AddTransient<IRepositoryCountry, RepositoryCountry>()
                   // Add SQLServer Connection
                   .AddDbContext<RareNFTsContext>(options =>
                   {
                       options.UseSqlServer("Server=localhost;Database=RareNFTs_db;Integrated Security=false;user id=sa;password=123456;Encrypt=false;");
                       options.EnableSensitiveDataLogging();
                   })
                   .AddTransient<MyApplication>()
                   .BuildServiceProvider();

            return serviceProvider;

        }


        /// <summary>
        /// Main 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            // Config Services (D.I., DataBases, etc)
            var services = CreateServices();
            MyApplication app = services.GetRequiredService<MyApplication>();
            // Call Reports
            app.ProductReport();

        }

        // Class resposible to create reports
        public class MyApplication
        {
            private readonly ILogger<MyApplication> _logger;
            private readonly IRepositoryNft _repositoryProducto;

            public MyApplication(ILogger<MyApplication> logger, IRepositoryNft repositoryProducto)
            {
                _logger = logger;
                _repositoryProducto = repositoryProducto;
            }


            public void ProductReport()
            {
                // Not async calling. 
                var collection = _repositoryProducto.ListAsync().GetAwaiter();

                // License config ******  IMPORTANT ******
                QuestPDF.Settings.License = LicenseType.Community;

                Document.Create(document =>
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
                                col.Item().AlignLeft().Text("RareNFTs").Bold().FontSize(14).Bold();
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

                                });

                                tabla.Header(header =>
                                {
                                    header.Cell().Background("#4666FF")
                                    .Padding(2).AlignCenter().Text("ID").FontColor("#fff");

                                    header.Cell().Background("#4666FF")
                                    .Padding(2).AlignCenter().Text("Description").FontColor("#fff");

                                    header.Cell().Background("#4666FF")
                                   .Padding(2).AlignCenter().Text("Image").FontColor("#fff");

                                    header.Cell().Background("#4666FF")
                                   .Padding(2).AlignCenter().Text("Author").FontColor("#fff");

                                    header.Cell().Background("#4666FF")
                                   .Padding(2).AlignCenter().Text("Date").FontColor("#fff");

                                    header.Cell().Background("#4666FF")
                                   .Padding(2).AlignCenter().Text("Price").FontColor("#fff");
                                });

                                foreach (var item in collection.GetResult())
                                {

                                    // Column 1
                                    tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                    .Padding(2).Text(item.Id.ToString()).FontSize(10);

                                    // Column 2
                                    tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                    .Padding(2).Text(item.Description.ToString()).FontSize(10);

                                    // Column 3
                                    tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                    .Padding(2).Image(imageData: item.Image).UseOriginalImage();

                                    // Column 4
                                    tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                                           .Padding(2).AlignRight().Text(item.Author.ToString()).FontSize(10);
                                    // Column 5
                                    tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                                         .Padding(2).AlignRight().Text(item.Date.ToString()).FontSize(10);

                                    // Column 6
                                    tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                                         .Padding(2).AlignRight().Text(item.Price.ToString()).FontSize(10);
                                }

                            });

                            var total = collection.GetResult().Sum(p => p.Price);

                            col1.Item().AlignRight().Text("Total " + total.ToString()).FontSize(12).Bold();

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
                }).ShowInPreviewer();
            }
        }
    }
}
