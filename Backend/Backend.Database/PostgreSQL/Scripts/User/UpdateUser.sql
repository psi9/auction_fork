UPDATE "Users"
SET "name"     = @name,
    "email"    = @email,
    "password" = @password
WHERE "id" = @id;