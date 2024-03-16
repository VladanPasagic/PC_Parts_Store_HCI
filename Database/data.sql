CREATE DATABASE  IF NOT EXISTS `pc_store` /*!40100 DEFAULT CHARACTER SET utf8mb3 */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `pc_store`;
-- MySQL dump 10.13  Distrib 8.0.32, for Win64 (x86_64)
--
-- Host: localhost    Database: pc_store
-- ------------------------------------------------------
-- Server version	8.0.32

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `article`
--

DROP TABLE IF EXISTS `article`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `article` (
  `ArticleId` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(32) NOT NULL,
  `ManufacturerId` int NOT NULL,
  `CategoryId` int NOT NULL,
  `Price` decimal(6,2) NOT NULL,
  `ManufacturingYear` year NOT NULL,
  `Quantity` int NOT NULL,
  PRIMARY KEY (`ArticleId`),
  UNIQUE KEY `Name_UNIQUE` (`Name`),
  KEY `fk_article_manufacturer_idx` (`ManufacturerId`),
  KEY `fk_article_category_idx` (`CategoryId`),
  CONSTRAINT `fk_article_category` FOREIGN KEY (`CategoryId`) REFERENCES `category` (`CategoryId`),
  CONSTRAINT `fk_article_manufacturer` FOREIGN KEY (`ManufacturerId`) REFERENCES `manufacturer` (`ManufacturerId`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `article`
--

LOCK TABLES `article` WRITE;
/*!40000 ALTER TABLE `article` DISABLE KEYS */;
INSERT INTO `article` VALUES (11,'RTX 4060',9,5,750.00,2023,3),(12,'RTX 4070 Ti',5,5,2200.00,2023,0);
/*!40000 ALTER TABLE `article` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `article_has_supplier`
--

DROP TABLE IF EXISTS `article_has_supplier`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `article_has_supplier` (
  `ArticleId` int NOT NULL,
  `SupplierId` int NOT NULL,
  `Quantity` int NOT NULL,
  `PurchasePrice` decimal(6,2) NOT NULL,
  PRIMARY KEY (`ArticleId`,`SupplierId`),
  KEY `fk_article_has_supplier_supplier_idx` (`SupplierId`),
  KEY `fk_article_has_supplier_article_idx` (`ArticleId`),
  CONSTRAINT `fk_article_has_supplier_article` FOREIGN KEY (`ArticleId`) REFERENCES `article` (`ArticleId`),
  CONSTRAINT `fk_article_has_supplier_supplier` FOREIGN KEY (`SupplierId`) REFERENCES `supplier` (`SupplierId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `article_has_supplier`
--

LOCK TABLES `article_has_supplier` WRITE;
/*!40000 ALTER TABLE `article_has_supplier` DISABLE KEYS */;
/*!40000 ALTER TABLE `article_has_supplier` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `category`
--

DROP TABLE IF EXISTS `category`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `category` (
  `CategoryId` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(32) NOT NULL,
  PRIMARY KEY (`CategoryId`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `category`
--

LOCK TABLES `category` WRITE;
/*!40000 ALTER TABLE `category` DISABLE KEYS */;
INSERT INTO `category` VALUES (5,'Grafička kartica'),(6,'Procesor'),(7,'Radna memorija');
/*!40000 ALTER TABLE `category` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `item`
--

DROP TABLE IF EXISTS `item`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `item` (
  `Quantity` int NOT NULL,
  `ReceiptId` int NOT NULL,
  `ArticleId` int NOT NULL,
  `Price` decimal(7,2) NOT NULL,
  PRIMARY KEY (`ReceiptId`,`ArticleId`),
  KEY `fk_item_purchase_idx` (`ReceiptId`),
  KEY `fk_item_article_idx` (`ArticleId`),
  CONSTRAINT `fk_item_article` FOREIGN KEY (`ArticleId`) REFERENCES `article` (`ArticleId`),
  CONSTRAINT `fk_item_receipt` FOREIGN KEY (`ReceiptId`) REFERENCES `receipt` (`ReceiptId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `item`
--

LOCK TABLES `item` WRITE;
/*!40000 ALTER TABLE `item` DISABLE KEYS */;
INSERT INTO `item` VALUES (2,5,12,2200.00);
/*!40000 ALTER TABLE `item` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `manufacturer`
--

DROP TABLE IF EXISTS `manufacturer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `manufacturer` (
  `ManufacturerId` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(32) NOT NULL,
  `Country` varchar(64) NOT NULL,
  PRIMARY KEY (`ManufacturerId`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `manufacturer`
--

LOCK TABLES `manufacturer` WRITE;
/*!40000 ALTER TABLE `manufacturer` DISABLE KEYS */;
INSERT INTO `manufacturer` VALUES (5,'Asus','Taiwan'),(6,'Corsair','USA'),(7,'AMD','USA'),(8,'Intel','USA'),(9,'MSI','Taiwan'),(10,'Nvidia','USA'),(11,'Kingston','USA');
/*!40000 ALTER TABLE `manufacturer` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `purchase`
--

DROP TABLE IF EXISTS `purchase`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `purchase` (
  `ArticleId` int NOT NULL,
  `SupplierId` int NOT NULL,
  `Quantity` int NOT NULL,
  `PurchaseId` int NOT NULL AUTO_INCREMENT,
  `Price` decimal(6,2) DEFAULT NULL,
  PRIMARY KEY (`PurchaseId`),
  KEY `fk_article_has_supplier_supplier_idx` (`SupplierId`),
  KEY `fk_article_has_supplier_article_idx` (`ArticleId`),
  CONSTRAINT `fk_article_has_supplier_article1` FOREIGN KEY (`ArticleId`) REFERENCES `article` (`ArticleId`),
  CONSTRAINT `fk_article_has_supplier_supplier1` FOREIGN KEY (`SupplierId`) REFERENCES `supplier` (`SupplierId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `purchase`
--

LOCK TABLES `purchase` WRITE;
/*!40000 ALTER TABLE `purchase` DISABLE KEYS */;
/*!40000 ALTER TABLE `purchase` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `receipt`
--

DROP TABLE IF EXISTS `receipt`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `receipt` (
  `ReceiptId` int NOT NULL AUTO_INCREMENT,
  `TotalPrice` decimal(7,2) NOT NULL,
  `SellerId` int NOT NULL,
  PRIMARY KEY (`ReceiptId`),
  KEY `fk_receipt_seller_idx` (`SellerId`) /*!80000 INVISIBLE */,
  CONSTRAINT `fk_receipt_seller` FOREIGN KEY (`SellerId`) REFERENCES `seller` (`SellerId`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `receipt`
--

LOCK TABLES `receipt` WRITE;
/*!40000 ALTER TABLE `receipt` DISABLE KEYS */;
INSERT INTO `receipt` VALUES (5,4400.00,13);
/*!40000 ALTER TABLE `receipt` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `seller`
--

DROP TABLE IF EXISTS `seller`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `seller` (
  `SellerId` int NOT NULL AUTO_INCREMENT,
  `Username` varchar(32) NOT NULL,
  `PasswordHash` varchar(512) NOT NULL,
  `IsAdmin` tinyint(1) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  PRIMARY KEY (`SellerId`),
  UNIQUE KEY `Username_UNIQUE` (`Username`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `seller`
--

LOCK TABLES `seller` WRITE;
/*!40000 ALTER TABLE `seller` DISABLE KEYS */;
INSERT INTO `seller` VALUES (11,'admin','$2b$10$IOmWYiQcerWZIWPTbI/DHexIRribcJERffMG871yQemfUnUoq4D26',1,1),(12,'Marko','$2b$10$KAQNOz/SUDfavAUfUEXDpeUNu..DOrkQJF.mYA9GHuCIK0MHevMPe',0,1),(13,'Milan','$2b$10$i7wDVE5l20Yij1sWZ0D1ZuJ5wMHvlnb1yvVeChffW1/7rH4ZPNpwq',0,1),(14,'Jovan','$2b$10$v.3bQzcHl9YPldRxKhaMDO6hUbE3GDUfG4W/beaOp/312oLfc/KCe',0,1);
/*!40000 ALTER TABLE `seller` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `settings`
--

DROP TABLE IF EXISTS `settings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `settings` (
  `SellerId` int NOT NULL,
  `Language` int NOT NULL,
  `Theme` int NOT NULL,
  PRIMARY KEY (`SellerId`),
  CONSTRAINT `fk_settings_seller` FOREIGN KEY (`SellerId`) REFERENCES `seller` (`SellerId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `settings`
--

LOCK TABLES `settings` WRITE;
/*!40000 ALTER TABLE `settings` DISABLE KEYS */;
INSERT INTO `settings` VALUES (11,0,2),(12,0,0),(13,0,0),(14,0,0);
/*!40000 ALTER TABLE `settings` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `supplier`
--

DROP TABLE IF EXISTS `supplier`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `supplier` (
  `SupplierId` int NOT NULL,
  `SupplierName` varchar(32) NOT NULL,
  `Location` varchar(64) NOT NULL,
  PRIMARY KEY (`SupplierId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `supplier`
--

LOCK TABLES `supplier` WRITE;
/*!40000 ALTER TABLE `supplier` DISABLE KEYS */;
/*!40000 ALTER TABLE `supplier` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-01-17 14:51:14
