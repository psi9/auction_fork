CREATE TABLE "Images"
(
    "id"    integer PRIMARY KEY NOT NULL,
    "lotId" integer             NOT NULL,
    "path"  varchar(300)        NOT NULL,
    CONSTRAINT fk_lot FOREIGN KEY ("lotId") REFERENCES "Lots" ("id")
); 