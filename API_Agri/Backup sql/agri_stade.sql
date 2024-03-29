-- MySQL dump 10.13  Distrib 8.0.34, for Win64 (x86_64)
--
-- Host: localhost    Database: agri
-- ------------------------------------------------------
-- Server version	8.1.0

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
-- Table structure for table `stade`
--

DROP TABLE IF EXISTS `stade`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `stade` (
  `stade_id` int NOT NULL AUTO_INCREMENT,
  `stade_description` varchar(64) DEFAULT NULL,
  `stade_plante_id` int DEFAULT NULL,
  `stade_kc` double DEFAULT NULL,
  PRIMARY KEY (`stade_id`),
  KEY `stade_plante_id_idx` (`stade_plante_id`),
  CONSTRAINT `stade_plante_id` FOREIGN KEY (`stade_plante_id`) REFERENCES `plante` (`Plante_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=53 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `stade`
--

LOCK TABLES `stade` WRITE;
/*!40000 ALTER TABLE `stade` DISABLE KEYS */;
INSERT INTO `stade` VALUES (1,'6 - 8 feuilles',1,0.5),(2,'8 - 10 feuilles',1,0.7),(3,'10 - 12 feuilles',1,0.8),(4,'12 - 14 feuilles',1,1),(5,'Floraison mâle',1,1.1),(6,'Floraison femelle à Soies sèches',1,1.2),(7,'Grain laiteux',1,1),(8,'Grain laiteux pâteux',1,0.8),(9,'Grain pâteux',1,0.5),(10,'Grain vitreux',1,0.3),(42,'Levée (2 cotylédons)',2,0.1),(43,'1 paire de feuilles',2,0.2),(44,'2 paires de feuilles',2,0.3),(45,'3 paires de feuilles',2,0.4),(46,'4 et 5 paires de feuilles',2,0.5),(47,'6 paires de feuilles',2,0.7),(48,'8 à 10 paires de feuilles – bouton à 1 cm',2,0.8),(49,'Bouton florale ( 3 à 4 cm de diamètre)',2,1),(50,'Début floraison',2,1),(51,'Pleine floraison',2,1),(52,'Chute des pétales',2,1);
/*!40000 ALTER TABLE `stade` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-03-29  8:37:22
