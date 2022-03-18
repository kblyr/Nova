CREATE DATABASE "Nova_Identity_V1";

CREATE SCHEMA "Identity";

CREATE TABLE "Identity"."UserStatus"
(
    "Id" SMALLINT NOT NULL,
    "Name" TEXT NOT NULL,
    CONSTRAINT "PK_UserStatus" PRIMARY KEY("Id"),
    CONSTRAINT "UQ_UserStatus_Name" UNIQUE("Name")
);

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
);

CREATE TABLE "Identity"."Domain"
(
    "Id" SMALLINT NOT NULL,
    "Name" TEXT NOT NULL,
    CONSTRAINT "PK_Domain" PRIMARY KEY("Id"),
    CONSTRAINT "UQ_Domain_Name" UNIQUE("Name")
);

CREATE TABLE "Identity"."Application"
(
    "Id" SMALLINT NOT NULL,
    "Name" TEXT NOT NULL,
    "DomainId" SMALLINT,
    CONSTRAINT "PK_Application" PRIMARY KEY("Id"),
    CONSTRAINT "FK_Application_DomainId" FOREIGN KEY("DomainId") REFERENCES "Identity"."Domain"("Id"),
    CONSTRAINT "UQ_Application_Name_DomainId" UNIQUE("Name", "DomainId") 
);

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
);

CREATE TABLE "Identity"."Role"
(
    "Id" INT NOT NULL GENERATED ALWAYS AS IDENTITY,
    "Name" TEXT NOT NULL,
    "Code" TEXT NOT NULL,
    "DomainId" SMALLINT,
    "ApplicationId" SMALLINT,
    "IsDeleted" BOOLEAN NOT NULL,
    "InsertedById" INT,
    "InsertedOn" TIMESTAMPTZ,
    "UpdatedById" INT,
    "UpdatedOn" TIMESTAMPTZ,
    "DeletedById" INT,
    "DeletedOn" TIMESTAMPTZ,
    CONSTRAINT "PK_Role" PRIMARY KEY("Id"),
    CONSTRAINT "FK_Role_DomainId" FOREIGN KEY("DomainId") REFERENCES "Identity"."Domain"("Id"),
    CONSTRAINT "FK_Role_ApplicationId" FOREIGN KEY("ApplicationId") REFERENCES "Identity"."Application"("Id"),
    CONSTRAINT "UQ_Role_Code" UNIQUE("Code")
);

CREATE TABLE "Identity"."Permission"
(
    "Id" INT NOT NULL GENERATED ALWAYS AS IDENTITY,
    "Name" TEXT NOT NULL,
    "Code" TEXT NOT NULL,
    "DomainId" SMALLINT,
    "ApplicationId" SMALLINT,
    "IsDeleted" BOOLEAN NOT NULL,
    "InsertedById" INT,
    "InsertedOn" TIMESTAMPTZ,
    "UpdatedById" INT,
    "UpdatedOn" TIMESTAMPTZ,
    "DeletedById" INT,
    "DeletedOn" TIMESTAMPTZ,
    CONSTRAINT "PK_Permission" PRIMARY KEY("Id"),
    CONSTRAINT "FK_Permission_DomainId" FOREIGN KEY("DomainId") REFERENCES "Identity"."Domain"("Id"),
    CONSTRAINT "FK_Permission_ApplicationId" FOREIGN KEY("ApplicationId") REFERENCES "Identity"."Application"("Id"),
    CONSTRAINT "UQ_Permission_Code" UNIQUE("Code")
);

CREATE TABLE "Identity"."RolePermission"
(
    "Id" BIGINT NOT NULL GENERATED ALWAYS AS IDENTITY,
    "RoleId" INT NOT NULL,
    "PermissionId" INT NOT NULL,
    "IsDeleted" BOOLEAN NOT NULL,
    "InsertedById" INT,
    "InsertedOn" TIMESTAMPTZ,
    "DeletedById" INT,
    "DeletedOn" TIMESTAMPTZ,
    CONSTRAINT "PK_RolePermission" PRIMARY KEY("Id"),
    CONSTRAINT "FK_RolePermission_UserId" FOREIGN KEY("RoleId") REFERENCES "Identity"."Role"("Id"),
    CONSTRAINT "FK_RolePermission_PermissionId" FOREIGN KEY("PermissionId") REFERENCES "Identity"."Permission"("Id")
);

CREATE TABLE "Identity"."UserRole"
(
    "Id" BIGINT NOT NULL GENERATED ALWAYS AS IDENTITY,
    "UserId" INT NOT NULL,
    "RoleId" INT NOT NULL,
    "IsDeleted" BOOLEAN NOT NULL,
    "InsertedById" INT,
    "InsertedOn" TIMESTAMPTZ,
    "DeletedById" INT,
    "DeletedOn" TIMESTAMPTZ,
    CONSTRAINT "PK_UserRole" PRIMARY KEY("Id"),
    CONSTRAINT "FK_UserRole_UserId" FOREIGN KEY("UserId") REFERENCES "Identity"."User"("Id"),
    CONSTRAINT "FK_UserRole_RoleId" FOREIGN KEY("RoleId") REFERENCES "Identity"."Role"("Id")
);

CREATE TABLE "Identity"."UserPermission"
(
    "Id" BIGINT NOT NULL GENERATED ALWAYS AS IDENTITY,
    "UserId" INT NOT NULL,
    "PermissionId" INT NOT NULL,
    "IsDeleted" BOOLEAN NOT NULL,
    "InsertedById" INT,
    "InsertedOn" TIMESTAMPTZ,
    "DeletedById" INT,
    "DeletedOn" TIMESTAMPTZ,
    CONSTRAINT "PK_UserPermission" PRIMARY KEY("Id"),
    CONSTRAINT "FK_UserPermission_UserId" FOREIGN KEY("UserId") REFERENCES "Identity"."User"("Id"),
    CONSTRAINT "FK_UserPermission_PermissionId" FOREIGN KEY("PermissionId") REFERENCES "Identity"."Permission"("Id")
);