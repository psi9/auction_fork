UPDATE "Lots"
SET "name"        = @name,
    "description" = @description,
    "betStep"     = @betStep,
    "state"       = @state
WHERE "id" = @id;