USE [master]
GO
/****** Object:  Database [RareNFTs_db]    Script Date: 14/3/2024 21:36:34 ******/
CREATE DATABASE [RareNFTs_db]

GO
USE [RareNFTs_db]
GO
/****** Object:  Table [dbo].[Card]    Script Date: 14/3/2024 21:36:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Card](
	[Id] [uniqueidentifier] NOT NULL DEFAULT NEWID(),
	[Description] [varchar](50) NULL,
 CONSTRAINT [PK_Card] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 14/3/2024 21:36:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[Id] [uniqueidentifier] NOT NULL DEFAULT NEWID(),
	[Name] [varchar](50) NULL,
	[Surname] [varchar](50) NULL,
	[Genre] [varchar](50) NULL,
	[IdCountry] [varchar](3) NULL,
	[IdWallet] [uniqueidentifier] NOT NULL,
	[IdUser] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClientNFT]    Script Date: 14/3/2024 21:36:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientNFT](
	[IdClient] [uniqueidentifier] NOT NULL,
	[IdNFT] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NULL,
 CONSTRAINT [PK_ClientNFT] PRIMARY KEY CLUSTERED 
(
	[IdClient] ASC,
	[IdNFT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 14/3/2024 21:36:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[Id] [varchar](3) NOT NULL,
	[Name] [varchar](50) NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvoiceDetail]    Script Date: 14/3/2024 21:36:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceDetail](
	[IdInvoice] [uniqueidentifier] NOT NULL,
	[IdNFT] [uniqueidentifier] NOT NULL,
	[Price] [money] NULL,
	[Tax] [money] NULL,
 CONSTRAINT [PK_InvoiceDetail] PRIMARY KEY CLUSTERED 
(
	[IdInvoice] ASC,
	[IdNFT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvoiceHeader]    Script Date: 14/3/2024 21:36:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceHeader](
	[Id] [uniqueidentifier] NOT NULL DEFAULT NEWID(),
	[IdCard] [uniqueidentifier] NOT NULL,
	[IdClient] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NULL,
	[IdStatus] [uniqueidentifier] NOT NULL,
	[NumCard] [int] NULL,
	[Total] [money] NULL,
 CONSTRAINT [PK_InvoiceHeader] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvoiceStatus]    Script Date: 14/3/2024 21:36:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceStatus](
	[Id] [uniqueidentifier] NOT NULL DEFAULT NEWID(),
	[Description] [varchar](50) NULL,
 CONSTRAINT [PK_InvoiceStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NFT]    Script Date: 14/3/2024 21:36:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NFT](
	[Id] [uniqueidentifier] NOT NULL DEFAULT NEWID(),
	[Description] [varchar](50) NULL,
	[Price] [money] NULL,
	[Image] [varbinary](max) NULL,
	[Date] [datetime] NULL,
	[Quantity] [int] NULL,
	[Author] [varchar](50) NULL,
 CONSTRAINT [PK_NFT] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 14/3/2024 21:36:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [uniqueidentifier] NOT NULL DEFAULT NEWID(),
	[Description] [varchar](50) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 14/3/2024 21:36:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [uniqueidentifier] NOT NULL DEFAULT NEWID(),
	[Email] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[IdRole] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Wallet]    Script Date: 14/3/2024 21:36:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Wallet](
	[Id] [uniqueidentifier] NOT NULL DEFAULT NEWID(),
	[Purse] [money] NULL,
 CONSTRAINT [PK_Wallet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_Client_Country] FOREIGN KEY([IdCountry])
REFERENCES [dbo].[Country] ([Id])
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_Client_Country]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_Client_User] FOREIGN KEY([Id])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_Client_User]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_Client_Wallet] FOREIGN KEY([IdWallet])
REFERENCES [dbo].[Wallet] ([Id])
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_Client_Wallet]
GO
ALTER TABLE [dbo].[ClientNFT]  WITH CHECK ADD  CONSTRAINT [FK_ClientNFT_Client] FOREIGN KEY([IdClient])
REFERENCES [dbo].[Client] ([Id])
GO
ALTER TABLE [dbo].[ClientNFT] CHECK CONSTRAINT [FK_ClientNFT_Client]
GO
ALTER TABLE [dbo].[ClientNFT]  WITH CHECK ADD  CONSTRAINT [FK_ClientNFT_NFT] FOREIGN KEY([IdNFT])
REFERENCES [dbo].[NFT] ([Id])
GO
ALTER TABLE [dbo].[ClientNFT] CHECK CONSTRAINT [FK_ClientNFT_NFT]
GO
ALTER TABLE [dbo].[InvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_InvoiceHeader] FOREIGN KEY([IdInvoice])
REFERENCES [dbo].[InvoiceHeader] ([Id])
GO
ALTER TABLE [dbo].[InvoiceDetail] CHECK CONSTRAINT [FK_InvoiceDetail_InvoiceHeader]
GO
ALTER TABLE [dbo].[InvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_NFT] FOREIGN KEY([IdNFT])
REFERENCES [dbo].[NFT] ([Id])
GO
ALTER TABLE [dbo].[InvoiceDetail] CHECK CONSTRAINT [FK_InvoiceDetail_NFT]
GO
ALTER TABLE [dbo].[InvoiceHeader]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceHeader_Card] FOREIGN KEY([IdCard])
REFERENCES [dbo].[Card] ([Id])
GO
ALTER TABLE [dbo].[InvoiceHeader] CHECK CONSTRAINT [FK_InvoiceHeader_Card]
GO
ALTER TABLE [dbo].[InvoiceHeader]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceHeader_Client] FOREIGN KEY([IdClient])
REFERENCES [dbo].[Client] ([Id])
GO
ALTER TABLE [dbo].[InvoiceHeader] CHECK CONSTRAINT [FK_InvoiceHeader_Client]
GO
ALTER TABLE [dbo].[InvoiceHeader]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceHeader_InvoiceStatus] FOREIGN KEY([IdStatus])
REFERENCES [dbo].[InvoiceStatus] ([Id])
GO
ALTER TABLE [dbo].[InvoiceHeader] CHECK CONSTRAINT [FK_InvoiceHeader_InvoiceStatus]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([IdRole])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
USE [master]
GO
ALTER DATABASE [RareNFTs_db] SET  READ_WRITE 
GO
