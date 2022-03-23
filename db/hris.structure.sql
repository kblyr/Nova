CREATE DATABASE "Nova_HRIS_V1";

CREATE SCHEMA "Lookup";

CREATE SCHEMA "HRIS";

CREATE TABLE "Lookup"."CivilStatus"
(
    "Id" SMALLINT NOT NULL,
    "Name" TEXT NOT NULL,
    CONSTRAINT "PK_CivilStatus" PRIMARY KEY("Id")
);

CREATE TABLE "Lookup"."Nationality"
(
    "Id" SMALLINT NOT NULL,
    "Name" TEXT NOT NULL,
    CONSTRAINT "PK_Nationality" PRIMARY KEY("Id")
);

CREATE TABLE "Lookup"."EmploymentStatus"
(
    "Id" SMALLINT NOT NULL,
    "Name" TEXT NOT NULL,
    CONSTRAINT "PK_EmploymentStatus" PRIMARY KEY("Id")
);

CREATE TABLE "Lookup"."EmploymentType"
(
    "Id" SMALLINT NOT NULL,
    "Name" TEXT NOT NULL,
    CONSTRAINT "PK_EmploymentType" PRIMARY KEY("Id")
);

CREATE TABLE "Lookup"."Province"
(
    "Id" SMALLINT NOT NULL GENERATED ALWAYS AS IDENTITY,
    "Name" TEXT NOT NULL,
    "IsDeleted" BOOLEAN NOT NULL,
    "InsertedById" INT,
    "InsertedOn" TIMESTAMPTZ,
    "UpdatedById" INT,
    "UpdatedOn" TIMESTAMPTZ,
    "DeletedById" INT,
    "DeletedOn" TIMESTAMPTZ,
    CONSTRAINT "PK_Province" PRIMARY KEY("Id")
);

CREATE TABLE "Lookup"."City"
(
    "Id" SMALLINT NOT NULL GENERATED ALWAYS AS IDENTITY,
    "Name" TEXT NOT NULL,
    "ProvinceId" SMALLINT,
    "IsDeleted" BOOLEAN NOT NULL,
    "InsertedById" INT,
    "InsertedOn" TIMESTAMPTZ,
    "UpdatedById" INT,
    "UpdatedOn" TIMESTAMPTZ,
    "DeletedById" INT,
    "DeletedOn" TIMESTAMPTZ,
    CONSTRAINT "PK_City" PRIMARY KEY("Id"),
    CONSTRAINT "FK_City_ProvinceId" FOREIGN KEY("ProvinceId") REFERENCES "Lookup"."Province"("Id")
);

CREATE TABLE "Lookup"."Barangay"
(
    "Id" SMALLINT NOT NULL GENERATED ALWAYS AS IDENTITY,
    "Name" TEXT NOT NULL,
    "CityId" SMALLINT,
    "IsDeleted" BOOLEAN NOT NULL,
    "InsertedById" INT,
    "InsertedOn" TIMESTAMPTZ,
    "UpdatedById" INT,
    "UpdatedOn" TIMESTAMPTZ,
    "DeletedById" INT,
    "DeletedOn" TIMESTAMPTZ,
    CONSTRAINT "PK_Barangay" PRIMARY KEY("Id"),
    CONSTRAINT "FK_Barangay_CityId" FOREIGN KEY("CityId") REFERENCES "Lookup"."City"("Id")
);

CREATE TABLE "HRIS"."Employee"
(
    "Id" INT NOT NULL GENERATED ALWAYS AS IDENTITY,
    "FirstName" TEXT NOT NULL,
    "MiddleName" TEXT,
    "LastName" TEXT NOT NULL,
    "NameSuffix" TEXT,
    "MaidenMiddleName" TEXT,
    "Sex" BOOLEAN,
    "BirthDate" DATE,
    "BirthPlace" TEXT,
    "ContactNumber" TEXT,
    "CivilStatusId" SMALLINT,
    "NationalityId" SMALLINT,
    "Address" TEXT,
    "BarangayId" SMALLINT,
    "CityId" SMALLINT,
    "ProvinceId" SMALLINT,
    "IsDeleted" BOOLEAN NOT NULL,
    "InsertedById" INT,
    "InsertedOn" TIMESTAMPTZ,
    "UpdatedById" INT,
    "UpdatedOn" TIMESTAMPTZ,
    "DeletedById" INT,
    "DeletedOn" TIMESTAMPTZ,
    "FullName" TEXT NOT NULL,
    "FullAddress" TEXT NOT NULL,
    CONSTRAINT "PK_Employee" PRIMARY KEY("Id"),
    CONSTRAINT "FK_Employee_CivilStatusId" FOREIGN KEY("CivilStatusId") REFERENCES "Lookup"."CivilStatus"("Id"),
    CONSTRAINT "FK_Employee_NationalityId" FOREIGN KEY("NationalityId") REFERENCES "Lookup"."Nationality"("Id"),
    CONSTRAINT "FK_Employee_BarangayId" FOREIGN KEY("BarangayId") REFERENCES "Lookup"."Barangay"("Id"),
    CONSTRAINT "FK_Employee_CityId" FOREIGN KEY("CityId") REFERENCES "Lookup"."City"("Id"),
    CONSTRAINT "FK_Employee_ProvinceId" FOREIGN KEY("ProvinceId") REFERENCES "Lookup"."Province"("Id")
);