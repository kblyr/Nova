INSERT INTO "Identity"."UserStatus"("Id", "Name") VALUES
    (1, 'Pending'),
    (2, 'Active'),
    (3, 'Deactivated'),
    (4, 'Locked');

INSERT INTO "Identity"."UserLoginType"("Id", "Name") VALUES
    (1, 'Password'),
    (2, 'EmailAuth');