TRUNCATE TABLE "Books", "Authors" RESTART IDENTITY;

INSERT INTO "Authors" ("Name")
VALUES ('Terry Pratchett');
 
INSERT INTO "Books" ("Title", "AuthorId", "Finished", "Abandoned", "FromLibrary", "Owned", "GivenAway")
VALUES ('Wee Free Men', 1, TRUE, FALSE, FALSE, TRUE, FALSE);

INSERT INTO "Books" ("Title", "AuthorId", "Finished", "Abandoned", "FromLibrary", "Owned", "GivenAway")
VALUES ('Guards!Guards!', 1, TRUE, FALSE, FALSE, TRUE, FALSE);

INSERT INTO "Books" ("Title", "AuthorId", "Finished", "Abandoned", "FromLibrary", "Owned", "GivenAway")
VALUES ('Jingo', 1, TRUE, FALSE, FALSE, TRUE, FALSE);

INSERT INTO "Books" ("Title", "AuthorId", "Finished", "Abandoned", "FromLibrary", "Owned", "GivenAway")
VALUES ('Fifth Elephant', 1, TRUE, FALSE, FALSE, TRUE, FALSE);

INSERT INTO "Books" ("Title", "AuthorId", "Finished", "Abandoned", "FromLibrary", "Owned", "GivenAway")
VALUES ('Men at Arms', 1, TRUE, FALSE, FALSE, TRUE, FALSE);

INSERT INTO "Books" ("Title", "AuthorId", "Finished", "Abandoned", "FromLibrary", "Owned", "GivenAway")
VALUES ('Night Watch', 1, TRUE, FALSE, FALSE, TRUE, FALSE);

INSERT INTO "Books" ("Title", "AuthorId", "Finished", "Abandoned", "FromLibrary", "Owned", "GivenAway")
VALUES ('Snuff', 1, TRUE, FALSE, FALSE, TRUE, FALSE);

INSERT INTO "Books" ("Title", "AuthorId", "Finished", "Abandoned", "FromLibrary", "Owned", "GivenAway")
VALUES ('Thud', 1, TRUE, FALSE, FALSE, TRUE, FALSE);
