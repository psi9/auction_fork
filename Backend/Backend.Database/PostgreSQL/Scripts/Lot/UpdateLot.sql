UPDATE "Lots"
SET "name"        = @name,
    "description" = @description,
    "betStep"     = @betStep
WHERE "id" = @id;