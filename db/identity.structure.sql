CREATE DATABASE "Nova_Identity_V1"

CREATE SCHEMA "Identity"

CREATE TABLE "Identity"."UserStatus"
(
    "Id" SMALLINT NOT NULL,
    "Name" TEXT NOT NULL,
    CONSTRAINT "PK_UserStatus" PRIMARY KEY("Id"),
    CONSTRAINT "UQ_UserStatus_Name" UNIQUE("Name")
)

CREATE TABLE "Identity"."User"
(
    "Id" INT NOT NULL GENERATED ALWAYS AS IDENTITY,
    "Username" TEXT NOT NULL,
    "HashedPassword" TEXT NOT NULL,
    "StatusId" SMALLINT NOT NULL,
    "IsDeleted" BOOLEAN NOT NULL,
    "InsertedById" INT,
    "InsertedOn" TIMESTAMPTZ,
    "UpdatedById" INT,
    "UpdatedOn" TIMESTAMPTZ,
    "DeletedById" INT,
    "DeletedOn" TIMESTAMPTZ,
    CONSTRAINT "PK_User" PRIMARY KEY("Id"),
    CONSTRAINT "FK_User_StatusId" FOREIGN KEY("StatusId") REFERENCES "Identity"."UserStatus"("Id")
)

CREATE TABLE "Identity"."Domain"
(
    "Id" SMALLINT NOT NULL,
    "Name" TEXT NOT NULL,
    CONSTRAINT "PK_Domain" PRIMARY KEY("Id"),
    CONSTRAINT "UQ_Domain_Name" UNIQUE("Name")
)

CREATE TABLE "Identity"."Application"
(
    "Id" SMALLINT NOT NULL,
    "Name" TEXT NOT NULL,
    "DomainId" SMALLINT,
    CONSTRAINT "PK_Application" PRIMARY KEY("Id"),
    CONSTRAINT "FK_Application_DomainId" FOREIGN KEY("DomainId") REFERENCES "Identity"."Domain"("Id"),
    CONSTRAINT "UQ_Application_Name_DomainId" UNIQUE("Name", "DomainId") 
)

CREATE TABLE "Identity"."UserApplication"
(
    "Id" BIGINT NOT NULL GENERATED ALWAYS AS IDENTITY,
    "UserId" INT NOT NULL,
    "ApplicationId" SMALLINT NOT NULL,
    "IsDeleted" BOOLEAN NOT NULL,
    "InsertedById" INT,
    "InsertedOn" TIMESTAMPTZ,
    "UpdatedById" INT,
    "UpdatedOn" TIMESTAMPTZ,
    "DeletedById" INT,
    "DeletedOn" TIMESTAMPTZ,
    CONSTRAINT "PK_UserApplication" PRIMARY KEY("Id"),
    CONSTRAINT "FK_UserApplication_UserId" FOREIGN KEY("UserId") REFERENCES "Identity"."User"("Id"),
    CONSTRAINT "FK_UserApplication_ApplicationId" FOREIGN KEY("ApplicationId") REFERENCES "Identity"."Application"("Id")
)