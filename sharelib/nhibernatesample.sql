-- MySQL dump 10.13  Distrib 5.1.51, for Win32 (ia32)
--
-- Host: localhost    Database: nhibernatesample
-- ------------------------------------------------------
-- Server version	5.1.51-community

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `t_account`
--

DROP TABLE IF EXISTS `t_account`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `t_account` (
  `t_id` int(10) NOT NULL AUTO_INCREMENT,
  `t_mobilenumber` bigint(11) DEFAULT NULL,
  `t_customerid` int(11) DEFAULT NULL,
  PRIMARY KEY (`t_id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `t_account`
--

LOCK TABLES `t_account` WRITE;
/*!40000 ALTER TABLE `t_account` DISABLE KEYS */;
INSERT INTO `t_account` VALUES (1,13750018815,12),(2,13750018812,13),(3,13750018813,14);
/*!40000 ALTER TABLE `t_account` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `t_callrecord`
--

DROP TABLE IF EXISTS `t_callrecord`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `t_callrecord` (
  `t_id` int(4) NOT NULL AUTO_INCREMENT,
  `f_phonenumber` bigint(11) DEFAULT NULL,
  `t_phonenumber` bigint(11) DEFAULT NULL,
  `t_from` datetime DEFAULT NULL,
  `t_to` datetime DEFAULT NULL,
  PRIMARY KEY (`t_id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `t_callrecord`
--

LOCK TABLES `t_callrecord` WRITE;
/*!40000 ALTER TABLE `t_callrecord` DISABLE KEYS */;
INSERT INTO `t_callrecord` VALUES (1,13750018812,13750018812,'2012-06-11 16:25:17','2012-06-11 16:25:26'),(2,13750018812,13750018812,'2012-06-11 16:52:13','2012-06-11 16:52:18');
/*!40000 ALTER TABLE `t_callrecord` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `t_charge`
--

DROP TABLE IF EXISTS `t_charge`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `t_charge` (
  `t_chargeid` int(11) NOT NULL AUTO_INCREMENT,
  `t_name` varchar(50) DEFAULT NULL,
  `t_chargepermonth` int(10) DEFAULT NULL,
  PRIMARY KEY (`t_chargeid`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `t_charge`
--

LOCK TABLES `t_charge` WRITE;
/*!40000 ALTER TABLE `t_charge` DISABLE KEYS */;
INSERT INTO `t_charge` VALUES (1,'monthlypay',10),(2,'monthlypay',20),(3,'monthlypay',30),(4,'gprs',10),(5,'music',2);
/*!40000 ALTER TABLE `t_charge` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `t_customer`
--

DROP TABLE IF EXISTS `t_customer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `t_customer` (
  `t_customerid` int(11) NOT NULL AUTO_INCREMENT,
  `t_name` varchar(50) DEFAULT NULL,
  `t_address` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`t_customerid`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `t_customer`
--

LOCK TABLES `t_customer` WRITE;
/*!40000 ALTER TABLE `t_customer` DISABLE KEYS */;
INSERT INTO `t_customer` VALUES (12,'kutang','guangdong'),(13,'dai','jiangmen'),(14,'rao','zhuhai');
/*!40000 ALTER TABLE `t_customer` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `t_mobile`
--

DROP TABLE IF EXISTS `t_mobile`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `t_mobile` (
  `t_id` int(10) NOT NULL AUTO_INCREMENT,
  `t_mobilenumber` bigint(11) DEFAULT NULL,
  `t_datetimeofmakecard` datetime DEFAULT NULL,
  `t_balance` float DEFAULT NULL,
  `t_state` varchar(20) DEFAULT NULL,
  `t_mobiletype` varchar(20) DEFAULT NULL,
  `t_password` varchar(20) DEFAULT NULL,
  `t_lasttimepayfor` datetime DEFAULT NULL,
  PRIMARY KEY (`t_id`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `t_mobile`
--

LOCK TABLES `t_mobile` WRITE;
/*!40000 ALTER TABLE `t_mobile` DISABLE KEYS */;
INSERT INTO `t_mobile` VALUES (11,13750018815,'2012-06-11 12:02:16',33,'stop','music','root','2012-06-11 12:02:16'),(12,13750018812,'2012-06-11 14:41:39',4.6,'on','travel','root','2012-06-11 14:41:39'),(13,13750018813,'2012-06-11 14:43:16',-0.2,'on','world','root','2012-06-11 14:43:16');
/*!40000 ALTER TABLE `t_mobile` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `t_rule`
--

DROP TABLE IF EXISTS `t_rule`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `t_rule` (
  `t_ruleid` int(11) NOT NULL AUTO_INCREMENT,
  `t_mobilenumber` bigint(11) DEFAULT NULL,
  `t_chargeid` int(11) DEFAULT NULL,
  PRIMARY KEY (`t_ruleid`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `t_rule`
--

LOCK TABLES `t_rule` WRITE;
/*!40000 ALTER TABLE `t_rule` DISABLE KEYS */;
INSERT INTO `t_rule` VALUES (1,13750018815,2),(3,13750018815,4),(4,13750018815,5),(5,13750018812,1),(6,13750018813,3);
/*!40000 ALTER TABLE `t_rule` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2012-06-11 19:58:42
