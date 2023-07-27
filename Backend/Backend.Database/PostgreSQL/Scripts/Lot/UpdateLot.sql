UPDATE "Lots"
SET "name"        = @name,
    "description" = @dateTime,
    "betStep"     = @betStep
WHERE "id" = @id;