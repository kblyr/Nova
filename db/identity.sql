CREATE DATABASE "Nova_Identity";

CREATE SCHEMA "Identity";

CREATE TABLE "Identity"."UserStatus"
(
    "Id" SMALLINT NOT NULL,
    "Name" TEXT NOT NULL,
    CONSTRAINT "PK_UserStatus" PRIMARY KEY("Id"),
    CONSTRAINT "UQ_UserStatus_Name" UNIQUE("Name")
);

INSERT INTO "Identity"."UserStatus"("Id", "Name") VALUES
    (1, 'Pending'),
    (2, 'Active'),
    (3, 'Deactivated'),
    (4, 'Locked');

CREATE TABLE "Identity"."User"
(
    "Id" INT NOT NULL GENERATED ALWAYS AS IDENTITY,
    "FirstName" TEXT NOT NULL,
    "LastName" TEXT NOT NULL,
    "EmailAddress" TEXT NOT NULL,
    "HashedPassword" TEXT NOT NULL,
    "PasswordSalt" TEXT NOT NULL,
    "StatusId" SMALLINT NOT NULL,
    "IsDeleted" BOOLEAN NOT NULL,
    "InsertedById" INT,
    "InsertedOn" TIMESTAMPTZ,
    "UpdatedById" INT,
    "UpdatedOn" TIMESTAMPTZ,
    "DeletedById" INT,
    "DeletedOn" TIMESTAMPTZ,
    CONSTRAINT "PK_User" PRIMARY KEY("Id"),
    CONSTRAINT "FK_StatusId" FOREIGN KEY("StatusId") REFERENCES "Identity"."UserStatus"("Id")
);