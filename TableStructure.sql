
/*
 * 创建用户信息表，并初始化成员（默认用户admin，密码Dx+123456）
 *
 */
 
USE manager;
CREATE TABLE `userinfo` (
  `ID` int(11) NOT NULL AUTO_INCREMENT, /* sql server 是 identity(1,1)*/
  `Code` varchar(64) DEFAULT NULL,
  `UserName` varchar(64) DEFAULT NULL,
  `Password` varchar(64) DEFAULT NULL,
  `State` int(11) DEFAULT NULL,
  `Description` varchar(512) DEFAULT NULL,
  `Remark` varchar(512) DEFAULT NULL,
  `CreateTime` datetime DEFAULT NULL,
  `Creator` varchar(64) DEFAULT NULL,
  `LastUpdateTime` datetime DEFAULT NULL,
  `LastUpdator` varchar(64) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
;

INSERT INTO UserInfo (
	Code
	, UserName
	, Password
	, State
	, Description
	, Remark
	, CreateTime
	, Creator
	, LastUpdateTime
	, LastUpdator
)
VALUES
(
	''
	, 'admin'
	, 'e10adc3949ba59abbe56e057f20f883e' /* 123456*/
	, 1
	, '超级管理员'
	, ''
	, utc_time()
	, 'System'
	, utc_time()
	, 'System'
)
;
