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
    "InsertedOn" TIMESTAMP WITH TIME ZONE,
    "UpdatedById" INT,
    "UpdatedOn" TIMESTAMP WITH TIME ZONE,
    "DeletedById" INT,
    "DeletedOn" TIMESTAMP WITH TIME ZONE,
    CONSTRAINT "PK_User" PRIMARY KEY("Id"),
    CONSTRAINT "FK_User_StatusId" FOREIGN KEY("StatusId") REFERENCES "Identity"."UserStatus"("Id")
)