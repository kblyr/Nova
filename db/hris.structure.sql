CREATE DATABASE "Nova_HRIS_V1";

CREATE SCHEMA "HRIS";

CREATE TABLE "HRIS"."CivilStatus"
(
    "Id" SMALLINT NOT NULL,
    "Name" TEXT NOT NULL,
    CONSTRAINT "PK_CivilStatus" PRIMARY KEY("Id")
);

CREATE TABLE "HRIS"."Nationality"
(
    "Id" SMALLINT NOT NULL,
    "Name" TEXT NOT NULL,
    CONSTRAINT "PK_Nationality" PRIMARY KEY("Id")
);

CREATE TABLE "HRIS"."EmploymentStatus"
(
    "Id" SMALLINT NOT NULL,
    "Name" TEXT NOT NULL,
    CONSTRAINT "PK_EmploymentStatus" PRIMARY KEY("Id")
);

CREATE TABLE "HRIS"."EmploymentType"
(
    "Id" SMALLINT NOT NULL,
    "Name" TEXT NOT NULL,
    CONSTRAINT "PK_EmploymentType" PRIMARY KEY("Id")
);

CREATE TABLE "HRIS"."Province"
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

CREATE TABLE "HRIS"."City"
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
    CONSTRAINT "FK_City_ProvinceId" FOREIGN KEY("ProvinceId") REFERENCES "HRIS"."Province"("Id")
);

CREATE TABLE "HRIS"."Barangay"
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
    CONSTRAINT "FK_Barangay_CityId" FOREIGN KEY("CityId") REFERENCES "HRIS"."City"("Id")
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
    "EmploymentStatusId" SMALLINT,
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
    CONSTRAINT "FK_Employee_CivilStatusId" FOREIGN KEY("CivilStatusId") REFERENCES "HRIS"."CivilStatus"("Id"),
    CONSTRAINT "FK_Employee_NationalityId" FOREIGN KEY("NationalityId") REFERENCES "HRIS"."Nationality"("Id"),
    CONSTRAINT "FK_Employee_BarangayId" FOREIGN KEY("BarangayId") REFERENCES "HRIS"."Barangay"("Id"),
    CONSTRAINT "FK_Employee_CityId" FOREIGN KEY("CityId") REFERENCES "HRIS"."City"("Id"),
    CONSTRAINT "FK_Employee_ProvinceId" FOREIGN KEY("ProvinceId") REFERENCES "HRIS"."Province"("Id"),
    CONSTRAINT "FK_Employee_EmploymentStatusId" FOREIGN KEY("EmploymentStatusId") REFERENCES "HRIS"."EmploymentStatus"("Id")
);

CREATE TABLE "HRIS"."EmployeeSalaryGradeStep"
(
    "Id" BIGINT NOT NULL GENERATED ALWAYS AS IDENTITY,
    "EmployeeId" INT NOT NULL,
    "Grade" SMALLINT NOT NULL,
    "Step" SMALLINT NOT NULL,
    "EffectivityDateBegin" TIMESTAMPTZ NOT NULL,
    "EffectivityDateEnd" TIMESTAMPTZ,
    "IsDeleted" BOOLEAN NOT NULL,
    "InsertedById" INT,
    "InsertedOn" TIMESTAMPTZ,
    "UpdatedById" INT,
    "UpdatedOn" TIMESTAMPTZ,
    "DeletedById" INT,
    "DeletedOn" TIMESTAMPTZ,
    CONSTRAINT "PK_EmployeeSalaryGradeStep" PRIMARY KEY("Id"),
    CONSTRAINT "FK_EmployeeSalaryGradeStep_EmployeeId" FOREIGN KEY("EmployeeId") REFERENCES "HRIS"."Employee"("Id")
);

CREATE TABLE "HRIS"."Office"
(
    "Id" SMALLINT NOT NULL GENERATED ALWAYS AS IDENTITY,
    "Name" TEXT NOT NULL,
    "Abbreviation" TEXT,
    "IsDeleted" BOOLEAN NOT NULL,
    "InsertedById" INT,
    "InsertedOn" TIMESTAMPTZ,
    "UpdatedById" INT,
    "UpdatedOn" TIMESTAMPTZ,
    "DeletedById" INT,
    "DeletedOn" TIMESTAMPTZ,
    CONSTRAINT "PK_Office" PRIMARY KEY("Id")
);

CREATE TABLE "HRIS"."Position"
(
    "Id" INT NOT NULL GENERATED ALWAYS AS IDENTITY,
    "Name" TEXT NOT NULL,
    "Level" SMALLINT NOT NULL,
    "ParentId" INT,
    "IsDeleted" BOOLEAN NOT NULL,
    "InsertedById" INT,
    "InsertedOn" TIMESTAMPTZ,
    "UpdatedById" INT,
    "UpdatedOn" TIMESTAMPTZ,
    "DeletedById" INT,
    "DeletedOn" TIMESTAMPTZ,
    CONSTRAINT "PK_Position" PRIMARY KEY("Id"),
    CONSTRAINT "FK_Position_ParentId" FOREIGN KEY("ParentId") REFERENCES "HRIS"."Position"("Id")
);

CREATE TABLE "HRIS"."Employment"
(
    "Id" BIGINT NOT NULL GENERATED ALWAYS AS IDENTITY,
    "EmployeeId" INT NOT NULL,
    "TypeId" SMALLINT NOT NULL,
    "OfficeId" SMALLINT NOT NULL,
    "PositionId" INT NOT NULL,
    "EffectivityDateBegin" TIMESTAMPTZ NOT NULL,
    "EffectivityDateEnd" TIMESTAMPTZ,
    "Salary" DECIMAL(10, 2),
    "IsDeleted" BOOLEAN NOT NULL,
    "InsertedById" INT,
    "InsertedOn" TIMESTAMPTZ,
    "UpdatedById" INT,
    "UpdatedOn" TIMESTAMPTZ,
    "DeletedById" INT,
    "DeletedOn" TIMESTAMPTZ,
    CONSTRAINT "PK_Employment" PRIMARY KEY("Id"),
    CONSTRAINT "FK_Employment_EmployeeId" FOREIGN KEY("EmployeeId") REFERENCES "HRIS"."Employee"("Id"),
    CONSTRAINT "FK_Employment_TypeId" FOREIGN KEY("TypeId") REFERENCES "HRIS"."EmploymentType"("Id"),
    CONSTRAINT "FK_Employment_OfficeId" FOREIGN KEY("OfficeId") REFERENCES "HRIS"."Office"("Id"),
    CONSTRAINT "FK_Employment_PositionId" FOREIGN KEY("PositionId") REFERENCES "HRIS"."Position"("Id")
);

CREATE TABLE "HRIS"."SalaryGradeTable"
(
    "Id" INT NOT NULL GENERATED ALWAYS AS IDENTITY,
    "Title" TEXT NOT NULL,
    "EffectivityDateBegin" TIMESTAMPTZ NOT NULL,
    "EffectivityDateEnd" TIMESTAMPTZ,
    "IsDeleted" BOOLEAN NOT NULL,
    "InsertedById" INT,
    "InsertedOn" TIMESTAMPTZ,
    "UpdatedById" INT,
    "UpdatedOn" TIMESTAMPTZ,
    "DeletedById" INT,
    "DeletedOn" TIMESTAMPTZ,
    CONSTRAINT "PK_SalaryGradeTable" PRIMARY KEY("Id")
);

CREATE TABLE "HRIS"."SalaryGradeStep"
(
    "Id" BIGINT NOT NULL GENERATED ALWAYS AS IDENTITY,
    "TableId" INT NOT NULL,
    "Grade" SMALLINT NOT NULL,
    "Step" SMALLINT NOT NULL,
    "Amount" DECIMAL(10, 2) NOT NULL,
    "IsDeleted" BOOLEAN NOT NULL,
    "InsertedById" INT,
    "InsertedOn" TIMESTAMPTZ,
    "UpdatedById" INT,
    "UpdatedOn" TIMESTAMPTZ,
    "DeletedById" INT,
    "DeletedOn" TIMESTAMPTZ,
    CONSTRAINT "PK_SalaryGradeStep" PRIMARY KEY("Id"),
    CONSTRAINT "FK_SalaryGradeStep_TableId" FOREIGN KEY("TableId") REFERENCES "HRIS"."SalaryGradeTable"("Id")
);