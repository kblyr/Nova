-- DELETE FROM "Lookup"."CivilStatus";
INSERT INTO "Lookup"."CivilStatus"("Id", "Name") VALUES
    (1, 'Single'),
    (2, 'Married'),
    (3, 'Separated'),
    (4, 'Widow/Widower'),
    (5, 'Annulled'),
    (6, 'Divorced');

INSERT INTO "Lookup"."Nationality"("Id", "Name") VALUES
    (1, 'Filipino');

INSERT INTO "Lookup"."EmploymentStatus"("Id", "Name") VALUES
    (1, 'Active'),
    (2, 'Resigned'),
    (3, 'Terminated'),
    (4, 'AWOL'),
    (5, 'Retired');

INSERT INTO "Lookup"."EmploymentType"("Id", "Name") VALUES
    (1, 'Regular'),
    (2, 'Contract of Service'),
    (3, 'Job Order'),
    (4, 'Coterminous');