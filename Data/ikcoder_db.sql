CREATE DATABASE  IF NOT EXISTS `ikcoder_basic` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */;
USE `ikcoder_basic`;
-- MySQL dump 10.13  Distrib 8.0.11, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: ikcoder_basic
-- ------------------------------------------------------
-- Server version	8.0.11

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
 SET NAMES utf8 ;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `account_students`
--

DROP TABLE IF EXISTS `account_students`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `account_students` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(45) DEFAULT NULL,
  `password` varchar(20) DEFAULT NULL,
  `status` varchar(2) DEFAULT '0',
  `type` varchar(2) DEFAULT '0',
  `realname` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `account_students`
--

LOCK TABLES `account_students` WRITE;
/*!40000 ALTER TABLE `account_students` DISABLE KEYS */;
INSERT INTO `account_students` VALUES (5,'test_1','12345678',NULL,NULL,NULL),(7,'test_2','12345678','0','0',NULL);
/*!40000 ALTER TABLE `account_students` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `groupmessages_students`
--

DROP TABLE IF EXISTS `groupmessages_students`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `groupmessages_students` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `source` varchar(45) DEFAULT NULL,
  `title` varchar(45) DEFAULT NULL,
  `tolist` longblob,
  `isremoved` varchar(1) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `groupmessages_students`
--

LOCK TABLES `groupmessages_students` WRITE;
/*!40000 ALTER TABLE `groupmessages_students` DISABLE KEYS */;
/*!40000 ALTER TABLE `groupmessages_students` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `messages_students`
--

DROP TABLE IF EXISTS `messages_students`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `messages_students` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `content` longblob,
  `symbol` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `messages_students`
--

LOCK TABLES `messages_students` WRITE;
/*!40000 ALTER TABLE `messages_students` DISABLE KEYS */;
/*!40000 ALTER TABLE `messages_students` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `messagesindex_students`
--

DROP TABLE IF EXISTS `messagesindex_students`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `messagesindex_students` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `symbol` varchar(45) DEFAULT NULL,
  `uid` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `messagesindex_students`
--

LOCK TABLES `messagesindex_students` WRITE;
/*!40000 ALTER TABLE `messagesindex_students` DISABLE KEYS */;
/*!40000 ALTER TABLE `messagesindex_students` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `payment_students`
--

DROP TABLE IF EXISTS `payment_students`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `payment_students` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `payment_students`
--

LOCK TABLES `payment_students` WRITE;
/*!40000 ALTER TABLE `payment_students` DISABLE KEYS */;
/*!40000 ALTER TABLE `payment_students` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `profile_students`
--

DROP TABLE IF EXISTS `profile_students`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `profile_students` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uid` varchar(40) DEFAULT NULL,
  `sex` varchar(1) DEFAULT NULL,
  `nickname` varchar(45) DEFAULT NULL,
  `birthday` varchar(20) DEFAULT NULL,
  `header` varchar(50) DEFAULT NULL,
  `country` varchar(20) DEFAULT NULL,
  `state` varchar(40) DEFAULT NULL,
  `city` varchar(40) DEFAULT NULL,
  `schoolmap` longblob,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `profile_students`
--

LOCK TABLES `profile_students` WRITE;
/*!40000 ALTER TABLE `profile_students` DISABLE KEYS */;
INSERT INTO `profile_students` VALUES (1,'test_1','1','tom','1981-01-01',NULL,'china',NULL,NULL,NULL),(2,'test_2','1','test_2','2000-01-01',NULL,'china',NULL,NULL,NULL);
/*!40000 ALTER TABLE `profile_students` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `relations_students`
--

DROP TABLE IF EXISTS `relations_students`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `relations_students` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `puname` varchar(45) DEFAULT NULL,
  `suname` varchar(45) DEFAULT NULL,
  `accectped` varchar(1) DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `relations_students`
--

LOCK TABLES `relations_students` WRITE;
/*!40000 ALTER TABLE `relations_students` DISABLE KEYS */;
/*!40000 ALTER TABLE `relations_students` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `signin_students`
--

DROP TABLE IF EXISTS `signin_students`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `signin_students` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uname` varchar(45) DEFAULT NULL,
  `product` varchar(45) DEFAULT NULL,
  `sdate` varchar(45) DEFAULT NULL,
  `stime` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `signin_students`
--

LOCK TABLES `signin_students` WRITE;
/*!40000 ALTER TABLE `signin_students` DISABLE KEYS */;
/*!40000 ALTER TABLE `signin_students` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'ikcoder_basic'
--
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_account_students` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_account_students`(_operation varchar(40),_id int(11),_name varchar(45),_password varchar(20),_status varchar(2),_type varchar(2))
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from account_students;
elseif _operation='insert' then
insert into ikcoder_basic.account_students(name,password,status,type) values(_name,_password,_status,_type);
elseif _operation='update' and _name IS NOT NULL then
update account_students set name = _name where id = _id;
elseif _operation='update' and _password IS NOT NULL then
update account_students set password = _password where id = _id;
elseif _operation='update' and _status IS NOT NULL then
update account_students set status = _status where id = _id;
elseif _operation='update' and _type IS NOT NULL then
update account_students set type = _type where id = _id;
elseif _operation='selectmixed'then
select * from account_students where id = IFNULL(_id,id) and name = IFNULL(_name,name) and password = IFNULL(_password,password) and status = IFNULL(_status,status) and type = IFNULL(_type,type);
elseif _operation='delete' then
delete from account_students where id = _id;
elseif _operation='deletecondition' then
delete from account_students where id = _id or name = _name or password = _password or status = _status or type = _type;
elseif _operation='deletemixed'then
select * from account_students where id = IFNULL(_id,id) and name = IFNULL(_name,name) and password = IFNULL(_password,password) and status = IFNULL(_status,status) and type = IFNULL(_type,type);
elseif _operation='selectkey' then
select * from account_students where id = _id;
elseif _operation='selectcondition' then
select * from account_students where id = _id or name = _name or password = _password or status = _status or type = _type;
END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_groupmessages_students` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_groupmessages_students`(_operation varchar(40),_id int(11),_isremoved varchar(1),_source varchar(45),_title varchar(45),_tolist longblob)
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from groupmessages_students;
elseif _operation='insert' then
insert into ikcoder_basic.groupmessages_students(isremoved,source,title,tolist) values(_isremoved,_source,_title,_tolist);
elseif _operation='update' and _isremoved IS NOT NULL then
update groupmessages_students set isremoved = _isremoved where id = _id;
elseif _operation='update' and _source IS NOT NULL then
update groupmessages_students set source = _source where id = _id;
elseif _operation='update' and _title IS NOT NULL then
update groupmessages_students set title = _title where id = _id;
elseif _operation='update' and _tolist IS NOT NULL then
update groupmessages_students set tolist = _tolist where id = _id;
elseif _operation='selectmixed'then
select * from groupmessages_students where id = IFNULL(_id,id) and isremoved = IFNULL(_isremoved,isremoved) and source = IFNULL(_source,source) and title = IFNULL(_title,title) and tolist = IFNULL(_tolist,tolist);
elseif _operation='delete' then
delete from groupmessages_students where id = _id;
elseif _operation='deletecondition' then
delete from groupmessages_students where id = _id or isremoved = _isremoved or source = _source or title = _title or tolist = _tolist;
elseif _operation='deletemixed'then
select * from groupmessages_students where id = IFNULL(_id,id) and isremoved = IFNULL(_isremoved,isremoved) and source = IFNULL(_source,source) and title = IFNULL(_title,title) and tolist = IFNULL(_tolist,tolist);
elseif _operation='selectkey' then
select * from groupmessages_students where id = _id;
elseif _operation='selectcondition' then
select * from groupmessages_students where id = _id or isremoved = _isremoved or source = _source or title = _title or tolist = _tolist;
END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_messages_students` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_messages_students`(_operation varchar(40),_content longblob,_id int(11),_lastupdate varchar(45),_source varchar(45),_target varchar(45))
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from messages_students;
elseif _operation='insert' then
insert into ikcoder_basic.messages_students(content,lastupdate,source,target) values(_content,_lastupdate,_source,_target);
elseif _operation='update' and _content IS NOT NULL then
update messages_students set content = _content where id = _id;
elseif _operation='update' and _lastupdate IS NOT NULL then
update messages_students set lastupdate = _lastupdate where id = _id;
elseif _operation='update' and _source IS NOT NULL then
update messages_students set source = _source where id = _id;
elseif _operation='update' and _target IS NOT NULL then
update messages_students set target = _target where id = _id;
elseif _operation='selectmixed'then
select * from messages_students where content = IFNULL(_content,content) and id = IFNULL(_id,id) and lastupdate = IFNULL(_lastupdate,lastupdate) and source = IFNULL(_source,source) and target = IFNULL(_target,target);
elseif _operation='delete' then
delete from messages_students where id = _id;
elseif _operation='deletecondition' then
delete from messages_students where content = _content or id = _id or lastupdate = _lastupdate or source = _source or target = _target;
elseif _operation='deletemixed'then
select * from messages_students where content = IFNULL(_content,content) and id = IFNULL(_id,id) and lastupdate = IFNULL(_lastupdate,lastupdate) and source = IFNULL(_source,source) and target = IFNULL(_target,target);
elseif _operation='selectkey' then
select * from messages_students where id = _id;
elseif _operation='selectcondition' then
select * from messages_students where content = _content or id = _id or lastupdate = _lastupdate or source = _source or target = _target;
END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_profile_students` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_profile_students`(_operation varchar(40),_birthday varchar(20),_city varchar(40),_country varchar(20),_header varchar(50),_id int(11),_nickname varchar(45),_schoolmap longblob,_sex varchar(1),_state varchar(40),_uid varchar(40))
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from profile_students;
elseif _operation='insert' then
insert into ikcoder_basic.profile_students(birthday,city,country,header,nickname,schoolmap,sex,state,uid) values(_birthday,_city,_country,_header,_nickname,_schoolmap,_sex,_state,_uid);
elseif _operation='update' and _birthday IS NOT NULL then
update profile_students set birthday = _birthday where id = _id;
elseif _operation='update' and _city IS NOT NULL then
update profile_students set city = _city where id = _id;
elseif _operation='update' and _country IS NOT NULL then
update profile_students set country = _country where id = _id;
elseif _operation='update' and _header IS NOT NULL then
update profile_students set header = _header where id = _id;
elseif _operation='update' and _nickname IS NOT NULL then
update profile_students set nickname = _nickname where id = _id;
elseif _operation='update' and _schoolmap IS NOT NULL then
update profile_students set schoolmap = _schoolmap where id = _id;
elseif _operation='update' and _sex IS NOT NULL then
update profile_students set sex = _sex where id = _id;
elseif _operation='update' and _state IS NOT NULL then
update profile_students set state = _state where id = _id;
elseif _operation='update' and _uid IS NOT NULL then
update profile_students set uid = _uid where id = _id;
elseif _operation='selectmixed'then
select * from profile_students where birthday = IFNULL(_birthday,birthday) and city = IFNULL(_city,city) and country = IFNULL(_country,country) and header = IFNULL(_header,header) and id = IFNULL(_id,id) and nickname = IFNULL(_nickname,nickname) and schoolmap = IFNULL(_schoolmap,schoolmap) and sex = IFNULL(_sex,sex) and state = IFNULL(_state,state) and uid = IFNULL(_uid,uid);
elseif _operation='delete' then
delete from profile_students where id = _id;
elseif _operation='deletecondition' then
delete from profile_students where birthday = _birthday or city = _city or country = _country or header = _header or id = _id or nickname = _nickname or schoolmap = _schoolmap or sex = _sex or state = _state or uid = _uid;
elseif _operation='deletemixed'then
select * from profile_students where birthday = IFNULL(_birthday,birthday) and city = IFNULL(_city,city) and country = IFNULL(_country,country) and header = IFNULL(_header,header) and id = IFNULL(_id,id) and nickname = IFNULL(_nickname,nickname) and schoolmap = IFNULL(_schoolmap,schoolmap) and sex = IFNULL(_sex,sex) and state = IFNULL(_state,state) and uid = IFNULL(_uid,uid);
elseif _operation='selectkey' then
select * from profile_students where id = _id;
elseif _operation='selectcondition' then
select * from profile_students where birthday = _birthday or city = _city or country = _country or header = _header or id = _id or nickname = _nickname or schoolmap = _schoolmap or sex = _sex or state = _state or uid = _uid;
END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_relations_students` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_relations_students`(_operation varchar(40),_accectped varchar(1),_id int(11),_puname varchar(45),_suname varchar(45))
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from relations_students;
elseif _operation='insert' then
insert into ikcoder_basic.relations_students(accectped,puname,suname) values(_accectped,_puname,_suname);
elseif _operation='update' and _accectped IS NOT NULL then
update relations_students set accectped = _accectped where id = _id;
elseif _operation='update' and _puname IS NOT NULL then
update relations_students set puname = _puname where id = _id;
elseif _operation='update' and _suname IS NOT NULL then
update relations_students set suname = _suname where id = _id;
elseif _operation='selectmixed'then
select * from relations_students where accectped = IFNULL(_accectped,accectped) and id = IFNULL(_id,id) and puname = IFNULL(_puname,puname) and suname = IFNULL(_suname,suname);
elseif _operation='delete' then
delete from relations_students where id = _id;
elseif _operation='deletecondition' then
delete from relations_students where accectped = _accectped or id = _id or puname = _puname or suname = _suname;
elseif _operation='deletemixed'then
select * from relations_students where accectped = IFNULL(_accectped,accectped) and id = IFNULL(_id,id) and puname = IFNULL(_puname,puname) and suname = IFNULL(_suname,suname);
elseif _operation='selectkey' then
select * from relations_students where id = _id;
elseif _operation='selectcondition' then
select * from relations_students where accectped = _accectped or id = _id or puname = _puname or suname = _suname;
END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_signin_students` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_signin_students`(_operation varchar(40),_id int(11),_product varchar(45),_sdate varchar(45),_stime varchar(45),_uname varchar(45))
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from signin_students;
elseif _operation='insert' then
insert into ikcoder_basic.signin_students(product,sdate,stime,uname) values(_product,_sdate,_stime,_uname);
elseif _operation='update' and _product IS NOT NULL then
update signin_students set product = _product where id = _id;
elseif _operation='update' and _sdate IS NOT NULL then
update signin_students set sdate = _sdate where id = _id;
elseif _operation='update' and _stime IS NOT NULL then
update signin_students set stime = _stime where id = _id;
elseif _operation='update' and _uname IS NOT NULL then
update signin_students set uname = _uname where id = _id;
elseif _operation='selectmixed'then
select * from signin_students where id = IFNULL(_id,id) and product = IFNULL(_product,product) and sdate = IFNULL(_sdate,sdate) and stime = IFNULL(_stime,stime) and uname = IFNULL(_uname,uname);
elseif _operation='delete' then
delete from signin_students where id = _id;
elseif _operation='deletecondition' then
delete from signin_students where id = _id or product = _product or sdate = _sdate or stime = _stime or uname = _uname;
elseif _operation='deletemixed'then
select * from signin_students where id = IFNULL(_id,id) and product = IFNULL(_product,product) and sdate = IFNULL(_sdate,sdate) and stime = IFNULL(_stime,stime) and uname = IFNULL(_uname,uname);
elseif _operation='selectkey' then
select * from signin_students where id = _id;
elseif _operation='selectcondition' then
select * from signin_students where id = _id or product = _product or sdate = _sdate or stime = _stime or uname = _uname;
END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-09-24 23:04:25
CREATE DATABASE  IF NOT EXISTS `ikcoder_appmain` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */;
USE `ikcoder_appmain`;
-- MySQL dump 10.13  Distrib 8.0.11, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: ikcoder_appmain
-- ------------------------------------------------------
-- Server version	8.0.11

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
 SET NAMES utf8 ;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `course_basic`
--

DROP TABLE IF EXISTS `course_basic`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `course_basic` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `course_name` varchar(20) DEFAULT NULL,
  `lesson_title` varchar(45) DEFAULT NULL,
  `isfree` varchar(1) DEFAULT NULL,
  `lesson_code` varchar(20) DEFAULT NULL,
  `steam` varchar(20) DEFAULT NULL,
  `udba` varchar(20) DEFAULT NULL,
  `totalsteps` varchar(2) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `course_basic`
--

LOCK TABLES `course_basic` WRITE;
/*!40000 ALTER TABLE `course_basic` DISABLE KEYS */;
INSERT INTO `course_basic` VALUES (1,'A','模式识别','1','A_001','S','UA','4'),(2,'A','路径跟随','1','A_002','S','UDA','4'),(3,'A','顺序','1','A_003','S','UA','4'),(4,'A','条件逻辑','1','A_004','ST','DA','4'),(5,'A','逻辑判断','1','A_005','ST','UA','4'),(6,'A','应用高级逻辑判断','1','A_006','S','UA','4'),(7,'A','应用否定逻辑','1','A_007','S','DA','4'),(8,'A','练习','1','A_008','S','DA','4'),(9,'A','条件循环','1','A_009','T','DA','4'),(10,'A','循环','1','A_010','ST','UA','4');
/*!40000 ALTER TABLE `course_basic` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `course_exp`
--

DROP TABLE IF EXISTS `course_exp`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `course_exp` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(45) DEFAULT NULL,
  `expcenter` varchar(1000) DEFAULT NULL,
  `level` varchar(2) DEFAULT NULL,
  `exp` varchar(45) DEFAULT NULL,
  `doc` varchar(10000) DEFAULT NULL,
  `piclist` varchar(2000) DEFAULT NULL,
  `course_name` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `course_exp`
--

LOCK TABLES `course_exp` WRITE;
/*!40000 ALTER TABLE `course_exp` DISABLE KEYS */;
INSERT INTO `course_exp` VALUES (1,'EXP:路径跟随实验',NULL,'1','100','把吃逗人移动到目的地，使用不同的路径',NULL,'A');
/*!40000 ALTER TABLE `course_exp` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `course_main`
--

DROP TABLE IF EXISTS `course_main`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `course_main` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(20) DEFAULT NULL,
  `title` varchar(45) DEFAULT NULL,
  `isfree` varchar(1) DEFAULT '1',
  `price` int(11) DEFAULT '0',
  `discount` int(11) DEFAULT '0',
  `enabled` varchar(1) DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `course_main`
--

LOCK TABLES `course_main` WRITE;
/*!40000 ALTER TABLE `course_main` DISABLE KEYS */;
INSERT INTO `course_main` VALUES (1,'A','逻辑课程','1',0,0,'1'),(2,'B ','HTML','0',200,0,'0'),(3,'C','JavaScript','0',800,0,'0'),(4,'D','Python','0',1000,0,'0'),(5,'E','C#','0',1000,0,'0');
/*!40000 ALTER TABLE `course_main` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `exp_defined`
--

DROP TABLE IF EXISTS `exp_defined`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `exp_defined` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `symbol` varchar(45) DEFAULT NULL,
  `exp` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `exp_defined`
--

LOCK TABLES `exp_defined` WRITE;
/*!40000 ALTER TABLE `exp_defined` DISABLE KEYS */;
/*!40000 ALTER TABLE `exp_defined` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `students_checkon`
--

DROP TABLE IF EXISTS `students_checkon`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `students_checkon` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uid` varchar(45) DEFAULT NULL,
  `checkdate` varchar(45) DEFAULT NULL,
  `checktime` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `students_checkon`
--

LOCK TABLES `students_checkon` WRITE;
/*!40000 ALTER TABLE `students_checkon` DISABLE KEYS */;
INSERT INTO `students_checkon` VALUES (3,'test_1','2018-09-03',NULL);
/*!40000 ALTER TABLE `students_checkon` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `students_coursepackage`
--

DROP TABLE IF EXISTS `students_coursepackage`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `students_coursepackage` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uid` varchar(45) DEFAULT NULL,
  `courseid` int(11) DEFAULT NULL,
  `overdate` varchar(45) DEFAULT NULL,
  `paymentid` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `students_coursepackage`
--

LOCK TABLES `students_coursepackage` WRITE;
/*!40000 ALTER TABLE `students_coursepackage` DISABLE KEYS */;
/*!40000 ALTER TABLE `students_coursepackage` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `students_exp`
--

DROP TABLE IF EXISTS `students_exp`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `students_exp` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uid` varchar(45) DEFAULT NULL,
  `exp` int(11) DEFAULT NULL,
  `rdate` varchar(45) DEFAULT NULL,
  `symbol` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `students_exp`
--

LOCK TABLES `students_exp` WRITE;
/*!40000 ALTER TABLE `students_exp` DISABLE KEYS */;
/*!40000 ALTER TABLE `students_exp` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `students_learninrecord`
--

DROP TABLE IF EXISTS `students_learninrecord`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `students_learninrecord` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uid` varchar(45) DEFAULT NULL,
  `lesson_id` int(11) DEFAULT NULL,
  `actions` varchar(20) DEFAULT NULL,
  `datetime` varchar(45) DEFAULT NULL,
  `times` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `students_learninrecord`
--

LOCK TABLES `students_learninrecord` WRITE;
/*!40000 ALTER TABLE `students_learninrecord` DISABLE KEYS */;
/*!40000 ALTER TABLE `students_learninrecord` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `students_lessonfinished`
--

DROP TABLE IF EXISTS `students_lessonfinished`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `students_lessonfinished` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uid` varchar(45) DEFAULT NULL,
  `symbol` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `students_lessonfinished`
--

LOCK TABLES `students_lessonfinished` WRITE;
/*!40000 ALTER TABLE `students_lessonfinished` DISABLE KEYS */;
/*!40000 ALTER TABLE `students_lessonfinished` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `students_lessonstatus`
--

DROP TABLE IF EXISTS `students_lessonstatus`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `students_lessonstatus` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uid` varchar(45) DEFAULT NULL,
  `lesson_code` varchar(45) DEFAULT NULL,
  `current_step` varchar(2) DEFAULT NULL,
  `current_statusdoc` longblob,
  `type` varchar(2) DEFAULT '1',
  `recorddt` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `students_lessonstatus`
--

LOCK TABLES `students_lessonstatus` WRITE;
/*!40000 ALTER TABLE `students_lessonstatus` DISABLE KEYS */;
/*!40000 ALTER TABLE `students_lessonstatus` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `students_mood`
--

DROP TABLE IF EXISTS `students_mood`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `students_mood` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uid` varchar(45) DEFAULT NULL,
  `mood` varchar(1) DEFAULT NULL,
  `date` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `students_mood`
--

LOCK TABLES `students_mood` WRITE;
/*!40000 ALTER TABLE `students_mood` DISABLE KEYS */;
INSERT INTO `students_mood` VALUES (1,'test_1','1','2018-8-20'),(2,'','1','2018-09-03');
/*!40000 ALTER TABLE `students_mood` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `titles_defined`
--

DROP TABLE IF EXISTS `titles_defined`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `titles_defined` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(45) DEFAULT NULL,
  `titles` varchar(45) DEFAULT NULL,
  `exp_min` int(11) DEFAULT NULL,
  `exp_max` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `titles_defined`
--

LOCK TABLES `titles_defined` WRITE;
/*!40000 ALTER TABLE `titles_defined` DISABLE KEYS */;
INSERT INTO `titles_defined` VALUES (1,'t_01_001','逻辑战士L1',0,'0'),(2,'t_01_002','逻辑战士L2',100,'300');
/*!40000 ALTER TABLE `titles_defined` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'ikcoder_appmain'
--
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_course_basic` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_course_basic`(_operation varchar(40),_course_name varchar(20),_id int(11),_isfree varchar(1),_lesson_code varchar(20),_lesson_title varchar(45))
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from course_basic;
elseif _operation='insert' then
insert into ikcoder_appmain.course_basic(course_name,isfree,lesson_code,lesson_title) values(_course_name,_isfree,_lesson_code,_lesson_title);
elseif _operation='update' and _course_name IS NOT NULL then
update course_basic set course_name = _course_name where id = _id;
elseif _operation='update' and _isfree IS NOT NULL then
update course_basic set isfree = _isfree where id = _id;
elseif _operation='update' and _lesson_code IS NOT NULL then
update course_basic set lesson_code = _lesson_code where id = _id;
elseif _operation='update' and _lesson_title IS NOT NULL then
update course_basic set lesson_title = _lesson_title where id = _id;
elseif _operation='selectmixed'then
select * from course_basic where course_name = IFNULL(_course_name,course_name) and id = IFNULL(_id,id) and isfree = IFNULL(_isfree,isfree) and lesson_code = IFNULL(_lesson_code,lesson_code) and lesson_title = IFNULL(_lesson_title,lesson_title);
elseif _operation='delete' then
delete from course_basic where id = _id;
elseif _operation='deletecondition' then
delete from course_basic where course_name = _course_name or id = _id or isfree = _isfree or lesson_code = _lesson_code or lesson_title = _lesson_title;
elseif _operation='deletemixed'then
select * from course_basic where course_name = IFNULL(_course_name,course_name) and id = IFNULL(_id,id) and isfree = IFNULL(_isfree,isfree) and lesson_code = IFNULL(_lesson_code,lesson_code) and lesson_title = IFNULL(_lesson_title,lesson_title);
elseif _operation='selectkey' then
select * from course_basic where id = _id;
elseif _operation='selectcondition' then
select * from course_basic where course_name = _course_name or id = _id or isfree = _isfree or lesson_code = _lesson_code or lesson_title = _lesson_title;
END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_course_exp` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_course_exp`(_operation varchar(40),_id int(11),_title varchar(45),_expcenter varchar(1000),_level varchar(2),_exp varchar(45),_doc varchar(10000),_piclist varchar(2000),_course_name varchar(45))
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from course_exp;
elseif _operation='insert' then
insert into ikcoder_appmain.course_exp(title,expcenter,level,exp,doc,piclist,course_name) values(_title,_expcenter,_level,_exp,_doc,_piclist,_course_name);
elseif _operation='update' and _title IS NOT NULL then
update course_exp set title = _title where id = _id;
elseif _operation='update' and _expcenter IS NOT NULL then
update course_exp set expcenter = _expcenter where id = _id;
elseif _operation='update' and _level IS NOT NULL then
update course_exp set level = _level where id = _id;
elseif _operation='update' and _exp IS NOT NULL then
update course_exp set exp = _exp where id = _id;
elseif _operation='update' and _doc IS NOT NULL then
update course_exp set doc = _doc where id = _id;
elseif _operation='update' and _piclist IS NOT NULL then
update course_exp set piclist = _piclist where id = _id;
elseif _operation='update' and _course_name IS NOT NULL then
update course_exp set course_name = _course_name where id = _id;
elseif _operation='selectmixed'then
select * from course_exp where id = IFNULL(_id,id) and title = IFNULL(_title,title) and expcenter = IFNULL(_expcenter,expcenter) and level = IFNULL(_level,level) and exp = IFNULL(_exp,exp) and doc = IFNULL(_doc,doc) and piclist = IFNULL(_piclist,piclist) and course_name = IFNULL(_course_name,course_name);
elseif _operation='delete' then
delete from course_exp where id = _id;
elseif _operation='deletecondition' then
delete from course_exp where id = _id or title = _title or expcenter = _expcenter or level = _level or exp = _exp or doc = _doc or piclist = _piclist or course_name = _course_name;
elseif _operation='deletemixed'then
select * from course_exp where id = IFNULL(_id,id) and title = IFNULL(_title,title) and expcenter = IFNULL(_expcenter,expcenter) and level = IFNULL(_level,level) and exp = IFNULL(_exp,exp) and doc = IFNULL(_doc,doc) and piclist = IFNULL(_piclist,piclist) and course_name = IFNULL(_course_name,course_name);
elseif _operation='selectkey' then
select * from course_exp where id = _id;
elseif _operation='selectcondition' then
select * from course_exp where id = _id or title = _title or expcenter = _expcenter or level = _level or exp = _exp or doc = _doc or piclist = _piclist or course_name = _course_name;
END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_course_main` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_course_main`(_operation varchar(40),_id int(11),_name varchar(20),_title varchar(45))
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from course_main;
elseif _operation='insert' then
insert into ikcoder_appmain.course_main(name,title) values(_name,_title);
elseif _operation='update' and _name IS NOT NULL then
update course_main set name = _name where id = _id;
elseif _operation='update' and _title IS NOT NULL then
update course_main set title = _title where id = _id;
elseif _operation='selectmixed'then
select * from course_main where id = IFNULL(_id,id) and name = IFNULL(_name,name) and title = IFNULL(_title,title);
elseif _operation='delete' then
delete from course_main where id = _id;
elseif _operation='deletecondition' then
delete from course_main where id = _id or name = _name or title = _title;
elseif _operation='deletemixed'then
select * from course_main where id = IFNULL(_id,id) and name = IFNULL(_name,name) and title = IFNULL(_title,title);
elseif _operation='selectkey' then
select * from course_main where id = _id;
elseif _operation='selectcondition' then
select * from course_main where id = _id or name = _name or title = _title;
END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_students_checkon` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_students_checkon`(_operation varchar(40),_checkdate varchar(45),_id int(11),_uid varchar(45))
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from students_checkon;
elseif _operation='insert' then
insert into ikcoder_appmain.students_checkon(checkdate,id,uid) values(_checkdate,_id,_uid);
elseif _operation='update' and _checkdate IS NOT NULL then
update students_checkon set checkdate = _checkdate where id = _id;
elseif _operation='update' and _uid IS NOT NULL then
update students_checkon set uid = _uid where id = _id;
elseif _operation='selectmixed'then
select * from students_checkon where checkdate = IFNULL(_checkdate,checkdate) and id = IFNULL(_id,id) and uid = IFNULL(_uid,uid);
elseif _operation='delete' then
delete from students_checkon where id = _id;
elseif _operation='deletecondition' then
delete from students_checkon where checkdate = _checkdate or id = _id or uid = _uid;
elseif _operation='deletemixed'then
select * from students_checkon where checkdate = IFNULL(_checkdate,checkdate) and id = IFNULL(_id,id) and uid = IFNULL(_uid,uid);
elseif _operation='selectkey' then
select * from students_checkon where id = _id;
elseif _operation='selectcondition' then
select * from students_checkon where checkdate = _checkdate or id = _id or uid = _uid;
END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_students_coursepackage` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_students_coursepackage`(_operation varchar(40),_courseid int(11),_id int(11),_overdate varchar(45),_paymentid int(11),_uid varchar(45))
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from students_coursepackage;
elseif _operation='insert' then
insert into ikcoder_appmain.students_coursepackage(courseid,overdate,paymentid,uid) values(_courseid,_overdate,_paymentid,_uid);
elseif _operation='update' and _courseid IS NOT NULL then
update students_coursepackage set courseid = _courseid where id = _id;
elseif _operation='update' and _overdate IS NOT NULL then
update students_coursepackage set overdate = _overdate where id = _id;
elseif _operation='update' and _paymentid IS NOT NULL then
update students_coursepackage set paymentid = _paymentid where id = _id;
elseif _operation='update' and _uid IS NOT NULL then
update students_coursepackage set uid = _uid where id = _id;
elseif _operation='selectmixed'then
select * from students_coursepackage where courseid = IFNULL(_courseid,courseid) and id = IFNULL(_id,id) and overdate = IFNULL(_overdate,overdate) and paymentid = IFNULL(_paymentid,paymentid) and uid = IFNULL(_uid,uid);
elseif _operation='delete' then
delete from students_coursepackage where id = _id;
elseif _operation='deletecondition' then
delete from students_coursepackage where courseid = _courseid or id = _id or overdate = _overdate or paymentid = _paymentid or uid = _uid;
elseif _operation='deletemixed'then
select * from students_coursepackage where courseid = IFNULL(_courseid,courseid) and id = IFNULL(_id,id) and overdate = IFNULL(_overdate,overdate) and paymentid = IFNULL(_paymentid,paymentid) and uid = IFNULL(_uid,uid);
elseif _operation='selectkey' then
select * from students_coursepackage where id = _id;
elseif _operation='selectcondition' then
select * from students_coursepackage where courseid = _courseid or id = _id or overdate = _overdate or paymentid = _paymentid or uid = _uid;
END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_students_exp` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_students_exp`(_operation varchar(40),_id int(11),_uid int(11),_uname varchar(45),_exp int(11))
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from students_exp;
elseif _operation='insert' then
insert into ikcoder_appmain.students_exp(uid,uname,exp) values(_uid,_uname,_exp);
elseif _operation='update' and _uid IS NOT NULL then
update students_exp set uid = _uid where id = _id;
elseif _operation='update' and _uname IS NOT NULL then
update students_exp set uname = _uname where id = _id;
elseif _operation='update' and _exp IS NOT NULL then
update students_exp set exp = _exp where id = _id;
elseif _operation='selectmixed'then
select * from students_exp where id = IFNULL(_id,id) and uid = IFNULL(_uid,uid) and uname = IFNULL(_uname,uname) and exp = IFNULL(_exp,exp);
elseif _operation='delete' then
delete from students_exp where id = _id;
elseif _operation='deletecondition' then
delete from students_exp where id = _id or uid = _uid or uname = _uname or exp = _exp;
elseif _operation='deletemixed'then
select * from students_exp where id = IFNULL(_id,id) and uid = IFNULL(_uid,uid) and uname = IFNULL(_uname,uname) and exp = IFNULL(_exp,exp);
elseif _operation='selectkey' then
select * from students_exp where id = _id;
elseif _operation='selectcondition' then
select * from students_exp where id = _id or uid = _uid or uname = _uname or exp = _exp;
END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_students_learninrecord` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_students_learninrecord`(_operation varchar(40),_actions varchar(20),_datetime varchar(45),_id int(11),_lesson_id int(11),_times int(11),_uid varchar(45))
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from students_learninrecord;
elseif _operation='insert' then
insert into ikcoder_appmain.students_learninrecord(actions,datetime,lesson_id,times,uid) values(_actions,_datetime,_lesson_id,_times,_uid);
elseif _operation='update' and _actions IS NOT NULL then
update students_learninrecord set actions = _actions where id = _id;
elseif _operation='update' and _datetime IS NOT NULL then
update students_learninrecord set datetime = _datetime where id = _id;
elseif _operation='update' and _lesson_id IS NOT NULL then
update students_learninrecord set lesson_id = _lesson_id where id = _id;
elseif _operation='update' and _times IS NOT NULL then
update students_learninrecord set times = _times where id = _id;
elseif _operation='update' and _uid IS NOT NULL then
update students_learninrecord set uid = _uid where id = _id;
elseif _operation='selectmixed'then
select * from students_learninrecord where actions = IFNULL(_actions,actions) and datetime = IFNULL(_datetime,datetime) and id = IFNULL(_id,id) and lesson_id = IFNULL(_lesson_id,lesson_id) and times = IFNULL(_times,times) and uid = IFNULL(_uid,uid);
elseif _operation='delete' then
delete from students_learninrecord where id = _id;
elseif _operation='deletecondition' then
delete from students_learninrecord where actions = _actions or datetime = _datetime or id = _id or lesson_id = _lesson_id or times = _times or uid = _uid;
elseif _operation='deletemixed'then
select * from students_learninrecord where actions = IFNULL(_actions,actions) and datetime = IFNULL(_datetime,datetime) and id = IFNULL(_id,id) and lesson_id = IFNULL(_lesson_id,lesson_id) and times = IFNULL(_times,times) and uid = IFNULL(_uid,uid);
elseif _operation='selectkey' then
select * from students_learninrecord where id = _id;
elseif _operation='selectcondition' then
select * from students_learninrecord where actions = _actions or datetime = _datetime or id = _id or lesson_id = _lesson_id or times = _times or uid = _uid;
END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_students_lessonstatus` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_students_lessonstatus`(_operation varchar(40),_id int(11),_uid varchar(45),_lesson_code varchar(45),_current_step varchar(2),_current_statusdoc longblob)
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from students_lessonstatus;
elseif _operation='insert' then
insert into ikcoder_appmain.students_lessonstatus(uid,lesson_code,current_step,current_statusdoc) values(_uid,_lesson_code,_current_step,_current_statusdoc);
elseif _operation='update' and _uid IS NOT NULL then
update students_lessonstatus set uid = _uid where id = _id;
elseif _operation='update' and _lesson_code IS NOT NULL then
update students_lessonstatus set lesson_code = _lesson_code where id = _id;
elseif _operation='update' and _current_step IS NOT NULL then
update students_lessonstatus set current_step = _current_step where id = _id;
elseif _operation='update' and _current_statusdoc IS NOT NULL then
update students_lessonstatus set current_statusdoc = _current_statusdoc where id = _id;
elseif _operation='selectmixed'then
select * from students_lessonstatus where id = IFNULL(_id,id) and uid = IFNULL(_uid,uid) and lesson_code = IFNULL(_lesson_code,lesson_code) and current_step = IFNULL(_current_step,current_step) and current_statusdoc = IFNULL(_current_statusdoc,current_statusdoc);
elseif _operation='delete' then
delete from students_lessonstatus where id = _id;
elseif _operation='deletecondition' then
delete from students_lessonstatus where id = _id or uid = _uid or lesson_code = _lesson_code or current_step = _current_step or current_statusdoc = _current_statusdoc;
elseif _operation='deletemixed'then
select * from students_lessonstatus where id = IFNULL(_id,id) and uid = IFNULL(_uid,uid) and lesson_code = IFNULL(_lesson_code,lesson_code) and current_step = IFNULL(_current_step,current_step) and current_statusdoc = IFNULL(_current_statusdoc,current_statusdoc);
elseif _operation='selectkey' then
select * from students_lessonstatus where id = _id;
elseif _operation='selectcondition' then
select * from students_lessonstatus where id = _id or uid = _uid or lesson_code = _lesson_code or current_step = _current_step or current_statusdoc = _current_statusdoc;
END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_students_mood` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_students_mood`(_operation varchar(40),_id int(11),_uid varchar(45),_mood varchar(1),_date varchar(45))
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from students_mood;
elseif _operation='insert' then
insert into ikcoder_appmain.students_mood(uid,mood,date) values(_uid,_mood,_date);
elseif _operation='update' and _uid IS NOT NULL then
update students_mood set uid = _uid where id = _id;
elseif _operation='update' and _mood IS NOT NULL then
update students_mood set mood = _mood where id = _id;
elseif _operation='update' and _date IS NOT NULL then
update students_mood set date = _date where id = _id;
elseif _operation='selectmixed'then
select * from students_mood where id = IFNULL(_id,id) and uid = IFNULL(_uid,uid) and mood = IFNULL(_mood,mood) and date = IFNULL(_date,date);
elseif _operation='delete' then
delete from students_mood where id = _id;
elseif _operation='deletecondition' then
delete from students_mood where id = _id or uid = _uid or mood = _mood or date = _date;
elseif _operation='deletemixed'then
select * from students_mood where id = IFNULL(_id,id) and uid = IFNULL(_uid,uid) and mood = IFNULL(_mood,mood) and date = IFNULL(_date,date);
elseif _operation='selectkey' then
select * from students_mood where id = _id;
elseif _operation='selectcondition' then
select * from students_mood where id = _id or uid = _uid or mood = _mood or date = _date;
END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `spa_operation_titles_defined` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `spa_operation_titles_defined`(_operation varchar(40),_id int(11),_name varchar(45),_titles varchar(45),_exp_min int(11),_exp_max varchar(45))
BEGIN
DECLARE tmpsql VARCHAR(800);
if _operation='select' then
select * from titles_defined;
elseif _operation='insert' then
insert into ikcoder_appmain.titles_defined(name,titles,exp_min,exp_max) values(_name,_titles,_exp_min,_exp_max);
elseif _operation='update' and _name IS NOT NULL then
update titles_defined set name = _name where id = _id;
elseif _operation='update' and _titles IS NOT NULL then
update titles_defined set titles = _titles where id = _id;
elseif _operation='update' and _exp_min IS NOT NULL then
update titles_defined set exp_min = _exp_min where id = _id;
elseif _operation='update' and _exp_max IS NOT NULL then
update titles_defined set exp_max = _exp_max where id = _id;
elseif _operation='selectmixed'then
select * from titles_defined where id = IFNULL(_id,id) and name = IFNULL(_name,name) and titles = IFNULL(_titles,titles) and exp_min = IFNULL(_exp_min,exp_min) and exp_max = IFNULL(_exp_max,exp_max);
elseif _operation='delete' then
delete from titles_defined where id = _id;
elseif _operation='deletecondition' then
delete from titles_defined where id = _id or name = _name or titles = _titles or exp_min = _exp_min or exp_max = _exp_max;
elseif _operation='deletemixed'then
select * from titles_defined where id = IFNULL(_id,id) and name = IFNULL(_name,name) and titles = IFNULL(_titles,titles) and exp_min = IFNULL(_exp_min,exp_min) and exp_max = IFNULL(_exp_max,exp_max);
elseif _operation='selectkey' then
select * from titles_defined where id = _id;
elseif _operation='selectcondition' then
select * from titles_defined where id = _id or name = _name or titles = _titles or exp_min = _exp_min or exp_max = _exp_max;
END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-09-24 23:04:26
