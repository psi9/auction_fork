CREATE TABLE "Images"
(
    "id"    uuid PRIMARY KEY NOT NULL,
    "lotId" uuid             NOT NULL,
    "path"  varchar(500)     NOT NULL,
    CONSTRAINT fk_lot FOREIGN KEY ("lotId") REFERENCES "Lots" ("id")
); 