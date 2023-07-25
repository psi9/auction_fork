UPDATE "Lots"
SET "name"        = @name,
    "description" = @dateTime,
    "betStep"     = @betStep,
    "images"      = @images
WHERE "id" = @id;