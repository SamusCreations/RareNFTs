USE [master]
GO
/****** Object:  Database [RareNFTs_db]    Script Date: 14/3/2024 21:36:34 ******/
CREATE DATABASE [RareNFTs_db]

GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RareNFTs_db].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RareNFTs_db] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RareNFTs_db] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RareNFTs_db] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RareNFTs_db] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RareNFTs_db] SET ARITHABORT OFF 
GO
ALTER DATABASE [RareNFTs_db] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RareNFTs_db] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RareNFTs_db] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RareNFTs_db] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RareNFTs_db] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RareNFTs_db] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RareNFTs_db] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RareNFTs_db] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RareNFTs_db] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RareNFTs_db] SET  DISABLE_BROKER 
GO
ALTER DATABASE [RareNFTs_db] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RareNFTs_db] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RareNFTs_db] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RareNFTs_db] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RareNFTs_db] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RareNFTs_db] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RareNFTs_db] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RareNFTs_db] SET RECOVERY FULL 
GO
ALTER DATABASE [RareNFTs_db] SET  MULTI_USER 
GO
ALTER DATABASE [RareNFTs_db] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RareNFTs_db] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RareNFTs_db] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RareNFTs_db] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [RareNFTs_db] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [RareNFTs_db] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'RareNFTs_db', N'ON'
GO
ALTER DATABASE [RareNFTs_db] SET QUERY_STORE = ON
GO
ALTER DATABASE [RareNFTs_db] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [RareNFTs_db]
GO
/****** Object:  Table [dbo].[Card]    Script Date: 14/3/2024 21:36:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Card](
	[Id] [varchar](50) NOT NULL,
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
	[Id] [varchar](50) NOT NULL,
	[Name] [varchar](50) NULL,
	[Surname] [varchar](50) NULL,
	[Genre] [varchar](50) NULL,
	[IdCountry] [varchar](3) NULL,
	[IdWallet] [varchar](50) NULL,
	[IdUser] [varchar](50) NULL,
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
	[IdClient] [varchar](50) NOT NULL,
	[IdNFT] [varchar](50) NOT NULL,
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
	[IdInvoice] [varchar](50) NOT NULL,
	[IdNFT] [varchar](50) NOT NULL,
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
	[Id] [varchar](50) NOT NULL,
	[IdCard] [varchar](50) NULL,
	[IdClient] [varchar](50) NULL,
	[Date] [datetime] NULL,
	[IdStatus] [varchar](50) NULL,
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
	[Id] [varchar](50) NOT NULL,
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
	[Id] [varchar](50) NOT NULL,
	[Description] [varchar](50) NULL,
	[Price] [money] NULL,
	[Image] [varbinary](max) NULL,
	[Date] [datetime] NULL,
	[Active] [bit] NULL,
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
	[Id] [varchar](50) NOT NULL,
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
	[Id] [varchar](50) NOT NULL,
	[Email] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[IdRole] [varchar](50) NULL,
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
	[Id] [varchar](50) NOT NULL,
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
