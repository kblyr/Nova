CREATE DATABASE "Nova_Identity_V1";

CREATE SCHEMA "Identity";

CREATE TABLE "Identity"."UserStatus"
(
    "Id" SMALLINT NOT NULL,
    "Name" TEXT NOT NULL,
    CONSTRAINT "PK_UserStatus" PRIMARY KEY("Id"),
    CONSTRAINT "UQ_UserStatus_Name" UNIQUE("Name")
);

-- DROP TABLE "Identity"."User"
CREATE TABLE "Identity"."User"
(
    "Id" INT NOT NULL GENERATED ALWAYS AS IDENTITY,
    "Username" TEXT NOT NULL,
    "EmailAddress" TEXT NOT NULL,
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
);

CREATE TABLE "Identity"."UserEmailAddress"
(
    "Id" BIGINT NOT NULL GENERATED ALWAYS AS IDENTITY,
    "UserId" INT NOT NULL,
    "EmailAddress" TEXT NOT NULL,
    "IsVerified" BOOLEAN NOT NULL,
    "IsPrimary" BOOLEAN NOT NULL,
    "IsDeleted" BOOLEAN NOT NULL,
    "InsertedById" INT,
    "InsertedOn" TIMESTAMPTZ,
    "UpdatedById" INT,
    "UpdatedOn" TIMESTAMPTZ,
    "DeletedById" INT,
    "DeletedOn" TIMESTAMPTZ,
    CONSTRAINT "PK_UserEmailAddress" PRIMARY KEY("Id"),
    CONSTRAINT "FK_UserEmailAddress_UserId" FOREIGN KEY("UserId") REFERENCES "Identity"."User"("Id")
);

CREATE TABLE "Identity"."UserLoginType"
(
    "Id" SMALLINT NOT NULL,
    "Name" TEXT NOT NULL,
    CONSTRAINT "PK_UserLoginType" PRIMARY KEY("Id"),
    CONSTRAINT "UQ_UserLoginType_Name" UNIQUE("Name")
);

CREATE TABLE "Identity"."UserLogin"
(
    "Id" BIGINT NOT NULL GENERATED ALWAYS AS IDENTITY,
    "UserId" INT NOT NULL,
    "TypeId" SMALLINT NOT NULL,
    "Ordinal" SMALLINT NOT NULL,
    "IsEnabled" BOOLEAN NOT NULL,
    "IsDeleted" BOOLEAN NOT NULL,
    "InsertedById" INT,
    "InsertedOn" TIMESTAMPTZ,
    "UpdatedById" INT,
    "UpdatedOn" TIMESTAMPTZ,
    "DeletedById" INT,
    "DeletedOn" TIMESTAMPTZ,
    CONSTRAINT "PK_UserLogin" PRIMARY KEY("Id"),
    CONSTRAINT "FK_UserLogin_UserId" FOREIGN KEY("UserId") REFERENCES "Identity"."User"("Id"),
    CONSTRAINT "FK_UserLogin_TypeId" FOREIGN KEY("TypeId") REFERENCES "Identity"."UserLoginType"("Id")
);

CREATE TABLE "Identity"."UserPasswordLogin"
(
    "Id" BIGINT NOT NULL,
    "HashedPassword" TEXT NOT NULL,
    CONSTRAINT "PK_UserPasswordLogin" PRIMARY KEY("Id"),
    CONSTRAINT "FK_UserPasswordLogin_Id" FOREIGN KEY("Id") REFERENCES "Identity"."UserLogin"("Id")
);