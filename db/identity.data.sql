INSERT INTO "Lookup"."UserStatus"("Id", "Name") VALUES
    (1, 'Pending'),
    (2, 'Active'),
    (3, 'Locked');

INSERT INTO "Identity"."Domain"("Id", "Name") VALUES
    (1, 'Identity');

INSERT INTO "Identity"."Application"("Id", "Name", "DomainId") VALUES
    (1, 'User Account Management', 1);