--Create New Database
Create database Always_Encrypted_DB

USE  Always_Encrypted_DB;
CREATE TABLE Client_Info (
  Id              INT           NOT NULL    IDENTITY    PRIMARY KEY,
  Client_Name      NVARCHAR(50) Not NULL,
  Visa_No           VARCHAR(16)  NOT NULL
)

CREATE COLUMN MASTER KEY [MainMasterKey]
WITH
(
	KEY_STORE_PROVIDER_NAME = N'AZURE_KEY_VAULT',
	KEY_PATH = N'<Azure Key Vault Identfier>'
)
GO

