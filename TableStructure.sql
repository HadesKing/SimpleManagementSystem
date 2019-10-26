
/*
 * 创建用户信息表，并初始化成员（默认用户admin，密码Dx+123456）
 *
 */
 
CREATE TABLE dbo.UserInfo
(
	ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY
	, Code VARCHAR(64) NULL
	, UserName VARCHAR(64) NULL
	, Password VARCHAR(64) NULL
	, State INT NULL
	, Description VARCHAR(512) NULL
	, Remark VARCHAR(512) NULL
	, CreateTime DATETIME NULL
	, Creator VARCHAR(64)
	, LastUpdateTime DATETIME
	, LastUpdator VARCHAR(64)
)
;

INSERT INTO dbo.UserInfo 
VALUES
(
	''
	, 'administrator'
	, '88a196c7bc65f41d75712f606165f127' /* Dx+123456*/
	, '1'
	, '超级管理员'
	, ''
	, GETUTCDATE()
	, 'System'
	, GETUTCDATE()
	, 'System'
)

