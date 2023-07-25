UPDATE "Auctions"
SET "name"        = @name,
    "description" = @description
WHERE "id" = @id;