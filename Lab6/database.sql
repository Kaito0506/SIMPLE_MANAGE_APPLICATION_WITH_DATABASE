Create Database DVDLibrary;
Use DVDLibrary;

Create Table DVDLibrary(
DVDCodeNo int Primary Key,
DVDTitle nvarchar(100) Not Null,
[Language] nvarchar(20) Not Null,
SubTitle bit Not Null,
Price Money Not Null
)

select max(dvdcodeno) from DVDLibrary;